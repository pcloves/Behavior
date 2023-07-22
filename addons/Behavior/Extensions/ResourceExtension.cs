using Behavior.Resources.Define;
using Godot;

namespace Behavior.Extensions;

public static class ResourceExtension
{
    public static void Save(this Resource resource, ResourceSaver.SaverFlags flags = ResourceSaver.SaverFlags.None)
    {
        GD.Print($"resource save, path:{resource.ResourcePath}");
        ResourceSaver.Save(resource, resource.ResourcePath, flags);
    }


    public static Node ToNode(this State behaviorState)
    {
        return null;
    }
}