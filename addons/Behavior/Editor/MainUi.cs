using System.IO;
using System.Linq;
using Game.addons.Behavior.Action;
using Godot;

namespace Game.addons.Behavior.Editor;

[Tool]
public partial class MainUi : Control
{
    public BehaviorPlugin Plugin { get; set; }

    private Tree _tree;
    private string _path = "res://";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _tree = GetNodeOrNull<Tree>("%Tree");

        var treeItem = _tree.CreateItem();
        treeItem.SetText(0, "Test111");
        treeItem.SetTooltipText(0, "hello!!!!!!!!!!!!!!!");

        _tree.ItemSelected += OnItemSelected;
        _tree.ButtonClicked += (_, _, _, _) => GD.Print(nameof(_tree.ButtonClicked));
        _tree.ItemEdited += () => GD.Print(nameof(_tree.ItemEdited));

        GD.Print("_tree:", _tree?.Name ?? "null");

        LoadResource();
    }

    private void OnItemSelected()
    {
        var text = _tree.GetSelected().GetText(0);
        GD.Print("treeItem click, name:", text);

        var actionCreateTimer = new ActionCreateTimer();

        Plugin.GetEditorInterface().EditResource(actionCreateTimer);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    private void LoadResource()
    {
        var globalizePath = ProjectSettings.GlobalizePath(_path);
        var paths = Directory.GetFiles(globalizePath).Where(path => path.EndsWith("tres"));
        foreach (var path in paths)
        {
            var resource = ResourceLoader.Load(path);
            if (resource == null) continue;

            if (resource.GetType() == typeof(BehaviorDefine))
            {
                GD.Print($"find {nameof(BehaviorDefine)}, path:{ProjectSettings.LocalizePath(path)}");
            }
            else
            {
                GD.Print($"type error, path:{ProjectSettings.LocalizePath(path)}, type:{resource.GetType()}");
            }
        }
    }
}