using System.Linq;
using Godot;

namespace Game.Extensions;

public static class NodeExtension
{
    public static T GetFirstChild<T>(this Node node)
    {
        var first = node.GetChildren().OfType<T>().First();

        return first;
    }
}