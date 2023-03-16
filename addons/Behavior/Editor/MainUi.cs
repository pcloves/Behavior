using System.Collections.Generic;
using System.IO;
using System.Linq;
using Godot;

namespace Game.addons.Behavior.Editor;

[Tool]
public partial class MainUi : Control
{
    public BehaviorPlugin Plugin { get; set; }

    private Tree _tree;
    private string _path = "res://";
    private readonly Dictionary<string, BehaviorDefine> _behaviorDefines = new();

    public override void _Ready()
    {
        _tree = GetNodeOrNull<Tree>("%Tree");
        _tree.ItemSelected += OnItemSelected;

        LoadBehaviorDefine();
    }

    private void OnItemSelected()
    {
        var treeItem = _tree.GetSelected();
        var path = treeItem.GetMeta("path").AsString();

        Plugin.GetEditorInterface().EditResource(_behaviorDefines[path]);
    }

    public override void _Process(double delta)
    {
    }

    private void LoadBehaviorDefine()
    {
        var globalizePath = ProjectSettings.GlobalizePath(_path);
        var paths = Directory.GetFiles(globalizePath).Where(path => path.EndsWith("tres"));

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

            _behaviorDefines[behaviorDefine.ResourcePath] = behaviorDefine;
        }
    }
}