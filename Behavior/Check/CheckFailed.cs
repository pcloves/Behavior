using Godot.Collections;

namespace Game;

public class CheckFailed : ICheck
{
    public bool Check(Array param = null)
    {
        return false;
    }
}