using Godot;

namespace Game.addons.Behavior.Action;

/// <summary>
/// 显示气泡
/// </summary>
[Tool]
public partial class ActionShowBubble : BehaviorAction
{
    [Export] public string _content { get; set; }

    [Export] public Control _bubbleUI { get; set; }

    public override void Execute(Node entity, params Variant[] signalArgs)
    {
        GD.Print("content:", _content);
    }
}