using System.Threading;
using System.Threading.Tasks;
using Godot;
using Timer = Godot.Timer;

namespace Game;

public partial class MapCreator : Node2D
{
    [Export(PropertyHint.Range)] private int _width = 800;
    [Export(PropertyHint.Range)] private int _height = 600;

    [Export] private FastNoiseLite _fastNoiseLite = new();
    private Timer _timer = new Timer();

    private Vector2 currentDirection = Vector2.Up;
    private Vector2 desireDirection = Vector2.Right;

    public override async void _Ready()
    {
        GD.Print("await start, thread:", Thread.CurrentThread.ManagedThreadId.ToString());
        await ToSignal(GetTree().CreateTimer(1.0f), Timer.SignalName.Timeout);
        GD.Print("await finish, thread:", Thread.CurrentThread.ManagedThreadId.ToString());
        Generate();

        _timer.Autostart = true;
        _timer.OneShot = true;
        _timer.WaitTime = 5;
        _timer.Timeout += TimerOnTimeout;
		
        AddChild(_timer);
		
        GameEvents.Instance.ItemDropped += OnInstanceOnItemDropped;
        GameEvents.EmitEventOnItemDropped("hello", 5);
    }

    private void OnInstanceOnItemDropped(string name, int amount)
    {
        GD.Print($"name:{name}, amount:{amount}");
    }

    private void TimerOnTimeout()
    {
        GD.Print("timeout");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override async void _Process(double delta)
    {
        await ToSignal(GetTree().CreateTimer(1.0f), Timer.SignalName.Timeout);
        
        currentDirection = currentDirection.Slerp(desireDirection, (float)(1f - Mathf.Exp(0.1 * GetProcessDeltaTime())));
		
        // GD.Print(currentDirection);
        
        GD.Print(GetProcessDeltaTime());
    }
    

    private async void Generate()
    {
        await Task.Run(() =>
        {
            GD.Print("generate run, thread:", Thread.CurrentThread.ManagedThreadId.ToString());
        });
        var ticks = Time.GetTicksMsec();
        Parallel.For(0, _width, i =>
        {
            Parallel.For(0, _height, j =>
            {
                var unused = Mathf.Abs(_fastNoiseLite.GetNoise2D(i, j));
                // GD.Print($"i:{i}, j:{j}, noise:{unused}");
            });
        });

        GD.Print("time:", (Time.GetTicksMsec() - ticks).ToString(), ", thread:", Thread.CurrentThread.ManagedThreadId.ToString());

        var ticks1 = Time.GetTicksMsec();
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                var unused = Mathf.Abs(_fastNoiseLite.GetNoise2D(i, j));
            }
        }
        GD.Print("time:", (Time.GetTicksMsec() - ticks1).ToString(), ", thread:", Thread.CurrentThread.ManagedThreadId.ToString());
		
        // _timer.Start();
    }
}