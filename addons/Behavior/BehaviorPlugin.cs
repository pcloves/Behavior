#if TOOLS
using Godot;

namespace Game.addons.Behavior;

[Tool]
public partial class BehaviorPlugin : EditorPlugin
{
	private MainUi _mainUi;

	public override void _EnterTree()
	{
		_mainUi = ResourceLoader.Load<PackedScene>("addons/Behavior/MainUi.tscn").Instantiate<MainUi>();
		_mainUi.BehaviorPlugin = this;
		
		AddControlToBottomPanel(_mainUi, "Behavior");
	}

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
		RemoveControlFromBottomPanel(_mainUi);
		
		_mainUi.BehaviorPlugin = null;
		_mainUi.QueueFree();
	}

	public override void _Edit(GodotObject @object)
	{
		base._Edit(@object);
		GD.Print(nameof(_Edit));
	}

	public override bool _Handles(GodotObject @object)
	{
		GD.Print(nameof(_Handles));

		var @class = @object.GetClass();
		
		GD.Print("GetClass:", @class);
		
		return true;
	}
}
#endif
