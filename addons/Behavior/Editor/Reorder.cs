using Godot;

namespace Behavior.addons.Behavior.Editor;

[Tool]
public partial class Reorder : Button
{
    public override Variant _GetDragData(Vector2 atPosition)
    {
        var owner = GetOwnerOrNull<UiBehaviorUnit>();
        owner.Modulate = Colors.LightGreen;

        return owner;
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        var owner = GetOwnerOrNull<UiBehaviorUnit>();
        var unit = data.As<UiBehaviorUnit>();

        if (owner.UiBehaviorStateBelong != unit.UiBehaviorStateBelong)
        {
            unit.UiBehaviorStateBelong.RemoveUiBehaviorUnit(unit, false);
            owner.UiBehaviorStateBelong.AddUiBehaviorUnit(unit);
        }
        else
        {
            owner.UiBehaviorStateBelong.MoveUiBehaviorUnit(unit, owner.GetIndex());
        }

        return true;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        var owner = GetOwnerOrNull<UiBehaviorUnit>();
        owner.Modulate = Colors.White;
    }
}