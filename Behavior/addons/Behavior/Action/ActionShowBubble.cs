using Godot;

namespace Behavior.addons.Behavior.Action;

/// <summary>
/// 显示气泡
/// </summary>
[Tool]
public partial class ActionShowBubble : BehaviorAction
{
    [Export] public string Content { get; set; }

    [Export] public Control BubbleUi { get; set; }

    public override void Execute(Node entity, params Variant[] signalArgs)
    {
        GD.Print("content:", Content);
    }
}