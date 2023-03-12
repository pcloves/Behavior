using Godot;

namespace Game.addons.Behavior;

public partial class MainUi : Control
{
    public BehaviorPlugin BehaviorPlugin { get; set; }

    private Tree _tree;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _tree = GetNodeOrNull<Tree>("%Tree");

        var treeItem = _tree.CreateItem();
        treeItem.SetText(0, "Test");

        _tree.ItemSelected += TreeOnItemSelected;
    }

    private void TreeOnItemSelected()
    {
        var text = _tree.GetSelected().GetText(0);
        GD.Print("treeItem click, name:", text);
        
        var behaviorDefine = ResourceLoader.Load<BehaviorDefine>("res://My.tres");

        BehaviorPlugin._Handles(behaviorDefine);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}