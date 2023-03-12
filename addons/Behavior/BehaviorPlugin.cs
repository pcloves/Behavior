#if TOOLS
using Game.addons.Behavior.Action;
using Godot;

namespace Game.addons.Behavior;

[Tool]
public partial class BehaviorPlugin : EditorPlugin
{
    private MainUi _mainUi = ResourceLoader.Load<PackedScene>("addons/Behavior/MainUi.tscn").Instantiate<MainUi>();
    private BehaviorInspectorPlugin _inspectorPlugin = new();

    public override void _EnterTree()
    {
        _mainUi.Plugin = this;

        AddControlToBottomPanel(_mainUi, "Behavior");
        AddInspectorPlugin(_inspectorPlugin);
    }

    public override void _ExitTree()
    {
        // Clean-up of the plugin goes here.
        RemoveControlFromBottomPanel(_mainUi);
        RemoveInspectorPlugin(_inspectorPlugin);

        _mainUi.Plugin = null;
        _mainUi.QueueFree();
    }

    public override void _Edit(GodotObject @object)
    {
        base._Edit(@object);
        GD.Print(nameof(_Edit), "########################");
    }

    public override bool _Handles(GodotObject @object)
    {
        GD.Print(nameof(_Handles), "---------------------------------");

        if (@object is ActionCreateTimer)
        {
            GD.Print("_Handles, type:", @object.GetType(), ", class:", @object.GetClass());

            return true;
        }

        return base._Handles(@object);
    }
}
#endif