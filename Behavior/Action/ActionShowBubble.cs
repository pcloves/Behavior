using Godot;
using Godot.Collections;

namespace Game.Behavior.Action;

/// <summary>
/// 显示气泡
/// </summary>
public partial class ActionShowBubble : AbstractAction
{
    [Export] public string _content { get; set; }

    [Export] public Control _bubbleUI { get; set; }

    public override void Execute(params Variant[] signalParam)
    {
        GD.Print("content:", _content);
    }
}