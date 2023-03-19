using System.Linq;
using Godot;

namespace Game.addons.Behavior.Extensions;

public static class NodeExtension
{
    public static T GetFirstChild<T>(this Node parent) where T: Node
    {
        var first = parent.GetChildren().OfType<T>().First();

        return first;
    }

    public static T RemoveFirstChild<T>(this Node parent) where T: Node
    {
        var child = parent.GetFirstChild<T>();
        if (child == null) return null;
        
        parent.RemoveChild(child);
        return child;
    }

    public static void AddChildBefore<T>(this Node parent, T child, Node brother) where T : Node
    {

        var brotherIndex = brother.GetIndex();
        var node = parent.GetChild(brotherIndex);
        if (node != brother) return;

        parent.AddChild(child);
        parent.MoveChild(child, brotherIndex);
    }
}