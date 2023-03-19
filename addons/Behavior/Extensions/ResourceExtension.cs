using Godot;

namespace Game.addons.Behavior.Extensions;

public static class ResourceExtension
{
    public static void Save(this Resource resource, ResourceSaver.SaverFlags flags = ResourceSaver.SaverFlags.None)
    {
        ResourceSaver.Save(resource, resource.ResourcePath, flags);
    }
}