using System;
using Godot;

namespace BehaviorAI;

[GlobalClass]
[Tool]
public partial class ActionCachePosition : BehaviorAction
{
    [Export] public string Key;
    [Export] public Type PositionType;
    [Export] public NodePath PositionNodePath;

    public enum Type
    {
        Global,
        Local,
    }

    public override void Execute(Behavior behavior, StringName signal, params Variant[] signalArgs)
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
