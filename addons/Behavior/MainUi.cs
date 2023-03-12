using Game.addons.Behavior.Action;
using Godot;

namespace Game.addons.Behavior;

[Tool]
public partial class MainUi : Control
{
    public BehaviorPlugin Plugin { get; set; }

    private Tree _tree;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _tree = GetNodeOrNull<Tree>("%Tree");

        var treeItem = _tree.CreateItem();
        treeItem.SetText(0, "Test");

        _tree.ItemSelected += OnItemSelected;
    }

    private void OnItemSelected()
    {
        var text = _tree.GetSelected().GetText(0);
        GD.Print("treeItem click, name:", text);
        
        var actionCreateTimer = new ActionCreateTimer();

        Plugin._Handles(actionCreateTimer);
        // EditorPlugin._Edit(actionCreateTimer);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}