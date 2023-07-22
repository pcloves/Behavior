using System;
using Behavior.Core;
using Behavior.Extensions;
using Godot;

namespace Behavior.Resources.Action;

[GlobalClass]
[Tool]
public partial class ActionCachePosition : Define.BehaviorAction
{
    [Export] public string Key;
    [Export] public Type PositionType;
    [Export] public NodePath PositionNodePath;

    public enum Type
    {
        Global,
        Local,
    }

    public override void Execute(Core.Behavior behavior, StringName signal, params Variant[] signalArgs)
    {
        var position = PositionType switch
        {
            Type.Global => behavior.Owner.GetNode<Node2D>(PositionNodePath).GlobalPosition,
            Type.Local => behavior.Owner.GetNode<Node2D>(PositionNodePath).Position,
            _ => throw new ArgumentOutOfRangeException()
        };

        behavior.GetFirstChild<Blackboard>().SetData(Key, position);
    }
}
