#if TOOLS
using Game.addons.Behavior.Action;
using Game.addons.Behavior.Editor;
using Godot;

namespace Game.addons.Behavior;

[Tool]
public partial class BehaviorPlugin : EditorPlugin
{
    private MainUi _mainUi = ResourceLoader.Load<PackedScene>("addons/Behavior/Editor/MainUi.tscn").Instantiate<MainUi>();
    private BehaviorInspectorPlugin _inspectorPlugin = new();

    public override void _EnterTree()
    {
        GD.Print(nameof(BehaviorPlugin), ":", nameof(_EnterTree));
        _mainUi.Plugin = this;
        _mainUi.Visible = false;

        GetEditorInterface().GetEditorMainScreen().AddChild(_mainUi);
        // AddInspectorPlugin(_inspectorPlugin);
    }

    public override void _ExitTree()
    {
        GD.Print(nameof(BehaviorPlugin), ":", nameof(_ExitTree));
        GetEditorInterface().GetEditorMainScreen().RemoveChild(_mainUi);
        // RemoveInspectorPlugin(_inspectorPlugin);

        _mainUi.Plugin = null;
        _mainUi.QueueFree();
    }

    public override void _Edit(GodotObject @object)
    {
        _MakeVisible(true);
        GD.Print(nameof(BehaviorPlugin), ":", nameof(_Edit));
    }

    public override string _GetPluginName()
    {
        GD.Print(nameof(BehaviorPlugin), ":", nameof(_GetPluginName));
        return "Behavior";
    }

    public override bool _Handles(GodotObject @object)
    {
        GD.Print(nameof(BehaviorPlugin), ":", nameof(_Handles), ":", @object?.GetType().Name ?? "null");

        if (@object is ActionCreateTimer)
        {
            GD.Print("_Handles, type:", @object.GetType(), ", class:", @object.GetClass());

            return true;
        }

        return false;
    }

    public override bool _HasMainScreen()
    {
        GD.Print(nameof(BehaviorPlugin), ":", nameof(_HasMainScreen));
        return true;
    }

    public override void _MakeVisible(bool visible)
    {
        GD.Print(nameof(BehaviorPlugin), ":", nameof(_MakeVisible), ":", visible.ToString());
        _mainUi.Visible = visible;
    }
}
#endif