﻿using Godot;
using MonoCustomResourceRegistry;

namespace Game.addons.Behavior.Action;

/// <summary>
/// 行为执行器
/// </summary>
[RegisteredType(nameof(BehaviorAction))]
[Tool]
public partial class BehaviorAction : Resource
{
    /// <summary>
    /// 行为执行操作
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="signalArgs">Signal附带的参数</param>
    public virtual void Execute(Node entity, params Variant[] signalArgs)
    {
    }
}