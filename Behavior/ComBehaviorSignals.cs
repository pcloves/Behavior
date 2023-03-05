using Godot;

namespace Game.Behavior;

public partial class ComBehavior
{
    [Signal]
    public delegate void TimeoutEventHandler(string timerId);
}