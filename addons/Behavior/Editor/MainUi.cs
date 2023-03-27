using System.Collections.Generic;
using System.IO;
using Game.addons.Behavior.Define;
using Game.addons.Behavior.Extensions;
using Godot;

namespace Game.addons.Behavior.Editor;

[Tool]
public partial class MainUi : Control
{
    private const string UiBehaviorDefineScenePath = "res://addons/Behavior/Editor/UiBehaviorDefine.tscn";

    private static readonly PackedScene UiBehaviorDefinePackedScene =
        ResourceLoader.Load<PackedScene>(UiBehaviorDefineScenePath);

    public BehaviorPlugin Plugin { get; set; }

    private HSplitContainer _splitContainer;
    private Tree _tree;
    private Label _label;
    private string _path = "res://";
    private BehaviorDefine _currentBehaviorDefine;
    private readonly Dictionary<string, BehaviorDefine> _behaviorDefines = new();
    private readonly Dictionary<string, TreeItem> _behaviorDefinePath2TreeItems = new();

    public override void _Ready()
    {
        _splitContainer = GetNodeOrNull<HSplitContainer>("%HSplitContainer");

        _tree = GetNodeOrNull<Tree>("%Tree");
        _tree.ItemSelected += OnItemSelected;

        _label = GetNodeOrNull<Label>("%Label");
        _label.Visible = true;

        var behaviorDefinePath = HasMeta("behaviorDefinePath") ? GetMeta("behaviorDefinePath").AsString() : null;
        LoadBehaviorDefine(behaviorDefinePath);
    }

    public void SetSelected(string path)
    {
        var treeItem = _behaviorDefinePath2TreeItems[path];
        if (treeItem != null && _tree.GetSelected() != treeItem)
        {
            _tree.SetSelected(treeItem, 0);
        }
    }

    private void OnItemSelected()
    {
        var treeItem = _tree.GetSelected();
        var path = treeItem.GetMeta("path").AsString();

        Plugin.GetEditorInterface().EditResource(_behaviorDefines[path]);

        //先把之前的删掉
        var uiBehaviorDefine = _splitContainer.RemoveFirstChild<UiBehaviorDefine>();
        var behaviorDefine = uiBehaviorDefine?.BehaviorDefine;
        behaviorDefine?.Save();

        uiBehaviorDefine = UiBehaviorDefinePackedScene.Instantiate<UiBehaviorDefine>();
        uiBehaviorDefine.BehaviorDefine = _behaviorDefines[path];

        _splitContainer.AddChild(uiBehaviorDefine);
        _label.Visible = false;
    }

    private void LoadBehaviorDefine(string selectedBehaviorDefine = null)
    {
        var globalizePath = ProjectSettings.GlobalizePath(_path);
        var paths = Directory.GetFiles(globalizePath, "*.tres", SearchOption.AllDirectories);

        _tree.Clear();
        _tree.CreateItem().SetText(0, "Root");

        foreach (var path in paths)
        {
            if (_behaviorDefines.ContainsKey(path)) continue;

            var resource = ResourceLoader.Load(path);
            if (resource is not BehaviorDefine behaviorDefine) continue;

            var treeItem = _tree.CreateItem();

            treeItem.SetText(0, behaviorDefine.Name);
            treeItem.SetTooltipText(0, behaviorDefine.ResourcePath);
            treeItem.SetMeta("path", behaviorDefine.ResourcePath);

            if (behaviorDefine.ResourcePath.Equals(selectedBehaviorDefine))
            {
                _tree.SetSelected(treeItem, 0);
            }

            _behaviorDefinePath2TreeItems[behaviorDefine.ResourcePath] = treeItem;
            _behaviorDefines[behaviorDefine.ResourcePath] = behaviorDefine;
        }
    }
}