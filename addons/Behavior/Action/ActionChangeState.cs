using System.Threading.Tasks;
using Behavior.addons.Behavior.Extensions;
using Godot;

namespace Behavior.addons.Behavior.Action;

[Tool]
[GlobalClass]
public partial class ActionChangeState : BehaviorAction
{
    [Export] public string NewStateId { get; set; }

    public override async Task Execute(Node parent, params Variant[] signalArgs)
    {
        var behaviorAi = parent.GetFirstChild<BehaviorAi>();
        behaviorAi.ChangeState(NewStateId);
    }
}