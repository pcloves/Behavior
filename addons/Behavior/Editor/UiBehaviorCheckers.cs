using Godot;

namespace Game.addons.Behavior.Editor;

[Tool]
public partial class UiBehaviorCheckers : PanelContainer
{
    private const string UiBehaviorCheckersScenePath = "res://addons/Behavior/Editor/UiBehaviorCheckers.tscn";
    private const string UiBehaviorCheckerScenePath = "res://addons/Behavior/Editor/UiBehaviorChecker.tscn";

    private static readonly PackedScene UiBehaviorCheckersPackedScene =
        ResourceLoader.Load<PackedScene>(UiBehaviorCheckersScenePath);

    private static readonly PackedScene UiBehaviorCheckerPackedScene =
        ResourceLoader.Load<PackedScene>(UiBehaviorCheckerScenePath);
    
    private Button _and;
    private Button _or;
    private VBoxContainer _childContainer;

    private Button _addRule;
    private Button _addGroup;
    private Button _delete;

    public override void _Ready()
    {
        _and = GetNodeOrNull<Button>("%And");
        _and.Pressed += () => _or.ButtonPressed = false;
        _or = GetNodeOrNull<Button>("%Or");
        _or.Pressed += () => _and.ButtonPressed = false;

        _childContainer = GetNodeOrNull<VBoxContainer>("%ChildContainer");

        _addRule = GetNodeOrNull<Button>("%AddRule");
        _addRule.Pressed += OnAddRulePressed;
        _addGroup = GetNodeOrNull<Button>("%AddGroup");
        _delete = GetNodeOrNull<Button>("%Delete");
        _delete.Pressed += OnDeletePressed;
        _delete.Visible = HasMeta("deleteVisible") && GetMeta("deleteVisible").AsBool();

        _addGroup.Pressed += OnAddGroupPressed;
    }

    private void OnDeletePressed()
    {
        GetParent().RemoveChild(this);
        QueueFree();
    }

    private void OnAddRulePressed()
    {
        var uiBehaviorChecker = UiBehaviorCheckerPackedScene.Instantiate<UiBehaviorChecker>();
        _childContainer.AddChild(uiBehaviorChecker);
    }

    private void OnAddGroupPressed()
    {
        var parent = UiBehaviorCheckersPackedScene.Instantiate<UiBehaviorCheckers>();
        parent.SetMeta("deleteVisible", true);

        _childContainer.AddChild(parent);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public override void _Draw()
    {
        if (_and == null) return;

        var childCount = _childContainer.GetChildCount();
        if (childCount == 0) return;

        //起点位于AND按钮的底部中间位置
        var lastFrom = (_and.GlobalPosition - GlobalPosition /*_andButton相对于本控件的位置*/) +
                       new Vector2(_and.Size.X / 4, _and.Size.Y);

        for (var i = 0; i < childCount; i++)
        {
            var child = _childContainer.GetChild<Control>(i);

            if (!child.Visible) continue;

            var to1 = new Vector2(lastFrom.X, (child.GlobalPosition - GlobalPosition).Y + child.Size.Y / 2);
            var to2 = new Vector2((child.GlobalPosition - GlobalPosition).X + 1, to1.Y);

            //画出一个：|_
            //竖线
            DrawLine(lastFrom, to1, Colors.Gray, 1.0f);
            //横线
            DrawLine(to1, to2, Colors.Gray, 1.0f);

            lastFrom = to1;
        }
    }
}