using Godot.Collections;

namespace Game;

public interface IAction
{
    void Execute(Array param = null);
}
