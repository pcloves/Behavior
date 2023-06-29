using System.Threading.Tasks;
using Godot;

namespace Behavior.addons.Behavior.Action;

/// <summary>
/// 显示气泡
/// </summary>
[Tool]
[GlobalClass]
public partial class ActionShowBubble : BehaviorAction
{
    [Export] public string Content { get; set; }

    [Export] public Control BubbleUi { get; set; }

    public override async Task Execute(Node parent, params Variant[] signalArgs)
    {
        GD.Print("content:", Content);
    }
}