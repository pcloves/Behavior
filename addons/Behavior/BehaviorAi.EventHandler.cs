﻿using Godot;

namespace Behavior;

public partial class BehaviorAi
{
    [Signal]
    public delegate void TimeoutEventHandler(string timerName);

    [Signal]
    public delegate void StateEnterEventHandler(string stateId);

    [Signal]
    public delegate void StateExitEventHandler(string stateId);
}