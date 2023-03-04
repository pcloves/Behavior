using Godot;

namespace Game;

public partial class GameEvents : Node
{
    [Signal]
    public delegate void ItemDroppedEventHandler(string itemName, int amount);

    public static GameEvents Instance { get; private set; }

    public override void _EnterTree()
    {
        base._EnterTree();

        Instance = this;
    }

    public static void EmitEventOnItemDropped(string itemName, int amount)
    {
        Instance.EmitSignal(SignalName.ItemDropped, itemName, amount);
    }
}