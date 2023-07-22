using Behavior.Core;
using Behavior.Extensions;
using Godot;

namespace Behavior.Resources.Action;

[GlobalClass]
[Tool]
public partial class ActionBlackboardRemove : Define.BehaviorAction
{
    [Export] public string Key;

    public override void Execute(Core.Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        behavior.GetFirstChild<Blackboard>().RemoveData(Key);
    }
}