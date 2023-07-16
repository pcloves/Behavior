using Godot;
using ActionResource = Behavior.Resources.Define.ActionResource;

namespace Behavior.Resources.Action;

/// <summary>
/// 显示气泡
/// </summary>
[Tool]
[GlobalClass]
public partial class ActionShowBubble : ActionResource
{
    [Export] public string Content { get; set; }

    [Export] public Control BubbleUi { get; set; }

    public override void Execute(Core.Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        GD.Print("content:", Content);
    }
}