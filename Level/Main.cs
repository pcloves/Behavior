using System.Linq;
using System.Reflection;
using Game.addons.Behavior;
using Game.addons.Behavior.Extensions;
using Godot;

namespace Game.Level;

public partial class Main : Node2D
{
    [Signal]
    public delegate void MySignal1EventHandler(int m);
    [Signal]
    public delegate void MySignal2EventHandler(int m, Node2D node2D);
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var characterBody2D = this.GetFirstChild<CharacterBody2D>();

        // GD.Print("CharacterBody2D:", characterBody2D?.Name);

        // var define = new BehaviorDefine();
        //
        // var behaviorState = new BehaviorState();
        //
        // behaviorState.Id = "Init";
        // var behaviorUnit = new BehaviorUnit();
        // behaviorUnit.Signal = "StateEnter";
        // behaviorUnit.Checkers.Add(new CheckSuccess());
        // var actionCreateTimer = new ActionCreateTimer();
        // actionCreateTimer.TimeSecond = 10;
        // actionCreateTimer.TimerName = "myTimer";
        //
        // behaviorUnit.Actions.Add(actionCreateTimer);
        //
        //
        // behaviorState.Units.Add(behaviorUnit);
        //
        // define.BehaviorStates.Add(behaviorState);
        //
        // ResourceSaver.Save(define, "res://My.tres");


        var callingAssembly = Assembly.GetCallingAssembly();
        GD.Print("calling");
        DumpAssembly(callingAssembly);
        GD.Print("entry");
        DumpAssembly(Assembly.GetEntryAssembly());
        GD.Print("executing");
        DumpAssembly(Assembly.GetExecutingAssembly());
        
        GD.Print("godotObject");
        DumpAssembly(Assembly.GetAssembly(typeof(GodotObject)));


        GetTree().CreateTimer(2).Connect(Timer.SignalName.Timeout, new Callable(this, nameof(Test)));

        Connect(SignalName.MySignal1, new Callable(this, nameof(Test)));
        Connect(SignalName.MySignal2, new Callable(this, nameof(Test)));


        EmitSignal(SignalName.MySignal1, 5);
        EmitSignal(SignalName.MySignal2, 5, new Node2D());
    }

    void Test(Variant arg1)
    {
        GD.Print("timeout arg1:", arg1);
    }
    
    void Test(Variant arg1, Variant arg2)
    {
        GD.Print("timeout arg1:", arg1, ", arg2:", arg2);
    }

    
    void Test()
    {
        GD.Print("timeout");
    }

    void DumpAssembly(Assembly assembly)
    {
        if (assembly == null)
        {
            return;
        }
        GD.Print("assembly:", assembly.FullName);
        var types = assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(GodotObject)));

        GD.Print("GodotObject count:", types.Count());
        foreach (var type in types)
        {
            // GD.Print("  type:", type.Name);
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}