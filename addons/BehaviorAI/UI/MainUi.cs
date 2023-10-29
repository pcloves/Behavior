using System.Collections.Generic;
using System.IO;
using System.Linq;
using Godot;

namespace BehaviorAI;

[Tool]
public partial class MainUi : Control
{
    private const string UiBehaviorConfigScenePath = "res://addons/BehaviorAI/UI/UiBehaviorConfig.tscn";

    private static readonly PackedScene UiBehaviorConfigPackedScene =
        ResourceLoader.Load<PackedScene>(UiBehaviorConfigScenePath);

    public BehaviorAiPlugin Plugin { get; set; }

    private HSplitContainer _splitContainer;
    private Tree _tree;
    private Label _label;
    private string _path = "res://";
    private BehaviorConfig _currentBehavior;
    private readonly Dictionary<string, BehaviorConfig> _behaviorConfigs = new();
    private readonly Dictionary<string, TreeItem> _behaviorConfigPath2TreeItems = new();

    public override void _Ready()
    {
        _splitContainer = GetNodeOrNull<HSplitContainer>("%HSplitContainer");

        _tree = GetNodeOrNull<Tree>("%Tree");
        _tree.ItemSelected += OnItemSelected;

        _label = GetNodeOrNull<Label>("%Label");
        _label.Visible = true;

        var behaviorConfigPath = HasMeta("behaviorConfigPath") ? GetMeta("behaviorConfigPath").AsString() : null;
        LoadBehaviorConfig(behaviorConfigPath);
    }

    public void SetSelected(string path)
    {
        var treeItem = _behaviorConfigPath2TreeItems[path];
        if (treeItem != null && _tree.GetSelected() != treeItem)
        {
            _tree.SetSelected(treeItem, 0);
        }
    }

    private void OnItemSelected()
    {
        var treeItem = _tree.GetSelected();
        var path = treeItem.GetMeta("path").AsString();

        var newBehaviorConfig = _behaviorConfigs[path];
        EditorInterface.Singleton.EditResource(newBehaviorConfig);

        //先把之前的删掉
        _splitContainer.RemoveFirstChild<UiBehaviorConfig>();
        _currentBehavior?.Save();
        _currentBehavior = newBehaviorConfig;

        var uiBehaviorConfig = UiBehaviorConfigPackedScene.Instantiate<UiBehaviorConfig>();
        uiBehaviorConfig.BehaviorConfig = newBehaviorConfig;

        _splitContainer.AddChild(uiBehaviorConfig);
        _label.Visible = false;
    }

    private void LoadBehaviorConfig(string selectedBehaviorConfig = null)
    {
        var globalizePath = ProjectSettings.GlobalizePath(_path);
        var paths = Directory.GetFiles(globalizePath, "*.tres", SearchOption.AllDirectories);

        _tree.Clear();
        _tree.CreateItem().SetText(0, "Root");
        _behaviorConfigs.Clear();
        _behaviorConfigPath2TreeItems.Clear();

        foreach (var path in paths)
        {
            if (_behaviorConfigs.ContainsKey(path)) continue;

            var resource = ResourceLoader.Load(path);
            if (resource is not BehaviorConfig behaviorConfig) continue;

            var treeItem = _tree.CreateItem();

            treeItem.SetText(0, behaviorConfig.Name);
            treeItem.SetTooltipText(0, behaviorConfig.ResourcePath);
            treeItem.SetMeta("path", behaviorConfig.ResourcePath);

            if (behaviorConfig.ResourcePath.Equals(selectedBehaviorConfig))
            {
                _tree.SetSelected(treeItem, 0);
            }

            _behaviorConfigPath2TreeItems[behaviorConfig.ResourcePath] = treeItem;
            _behaviorConfigs[behaviorConfig.ResourcePath] = behaviorConfig;
        }
        
#if DEBUG
        GD.Print($"{nameof(LoadBehaviorConfig)}:{string.Join(",", _behaviorConfigs.Keys.ToArray())}");
#endif
    }
}