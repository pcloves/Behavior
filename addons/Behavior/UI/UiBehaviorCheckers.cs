using Behavior.Resources.Define;
using Godot;
using Godot.Collections;
using CheckAndOr = Behavior.Resources.Check.CheckAndOr;

namespace Behavior.UI;

[Tool]
public partial class UiBehaviorCheckers : PanelContainer
{
    private const string UiBehaviorCheckersScenePath = "res://addons/Behavior/UI/UiBehaviorCheckers.tscn";
    private const string UiBehaviorCheckerScenePath = "res://addons/Behavior/UI/UiBehaviorChecker.tscn";

    private static readonly PackedScene UiBehaviorCheckersPackedScene =
        ResourceLoader.Load<PackedScene>(UiBehaviorCheckersScenePath);

    private static readonly PackedScene UiBehaviorCheckerPackedScene =
        ResourceLoader.Load<PackedScene>(UiBehaviorCheckerScenePath);

    public CheckAndOr CheckerAndOrBelong { get; set; }
    public CheckAndOr CheckerAndOr { get; set; }

    private Button _and;
    private Button _or;
    private VBoxContainer _childContainer;

    private Button _addRule;
    private Button _addGroup;
    private Button _remove;

    public override async void _Ready()
    {
        _or = GetNodeOrNull<Button>("%Or");
        _or.Pressed += () => OnAndOrButtonPressed(true);
        _or.ButtonPressed = CheckerAndOr is { Or: true };

        _and = GetNodeOrNull<Button>("%And");
        _and.Pressed += () => OnAndOrButtonPressed(false);
        _and.ButtonPressed = CheckerAndOr is { Or: false };

        _childContainer = GetNodeOrNull<VBoxContainer>("%ChildContainer");

        _addRule = GetNodeOrNull<Button>("%AddRule");
        _addRule.Pressed += () => OnAddRulePressed();

        _addGroup = GetNodeOrNull<Button>("%AddGroup");
        _addGroup.Pressed += () => OnAddGroupPressed();

        _remove = GetNodeOrNull<Button>("%Remove");
        _remove.Pressed += OnRemovePressed;
        _remove.Visible = HasMeta("deleteVisible") && GetMeta("deleteVisible").AsBool();

        foreach (var checker in CheckerAndOr?.Checkers ?? new Array<Checker>())
        {
            if (checker is CheckAndOr checkAndOr)
            {
                OnAddGroupPressed(checkAndOr);
            }
            else
            {
                OnAddRulePressed(checker);
            }
        }

        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

        QueueRedraw();
    }

    private void OnAndOrButtonPressed(bool or)
    {
        _or.ButtonPressed = or;
        _and.ButtonPressed = !or;

        CheckerAndOr.Or = or;
    }

    private void OnRemovePressed()
    {
        //从父Checker中删除
        CheckerAndOrBelong?.Checkers?.Remove(CheckerAndOr);

        QueueFree();
    }

    private void OnAddRulePressed(Checker checker = null)
    {
        var uiBehaviorChecker = UiBehaviorCheckerPackedScene.Instantiate<UiBehaviorChecker>();

        uiBehaviorChecker.Checker = checker;
        uiBehaviorChecker.CheckerBelong = CheckerAndOr;

        _childContainer.AddChild(uiBehaviorChecker);
    }

    private void OnAddGroupPressed(CheckAndOr checkAndOr = null)
    {
        var uiBehaviorCheckers = UiBehaviorCheckersPackedScene.Instantiate<UiBehaviorCheckers>();

        checkAndOr ??= new CheckAndOr();

        uiBehaviorCheckers.CheckerAndOr = checkAndOr;
        uiBehaviorCheckers.CheckerAndOrBelong = CheckerAndOr;
        uiBehaviorCheckers.SetMeta("deleteVisible", true);

        if (!CheckerAndOr.Checkers.Contains(checkAndOr))
        {
            CheckerAndOr.Checkers.Add(checkAndOr);
        }

        _childContainer.AddChild(uiBehaviorCheckers);
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