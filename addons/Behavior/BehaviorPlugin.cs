#if TOOLS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Godot;
using BehaviorDefine = Behavior.Define.BehaviorDefine;
using MainUi = Behavior.UI.MainUi;

namespace Behavior;

[Tool]
public partial class BehaviorPlugin : EditorPlugin
{
    private const string MainUiScene = "addons/Behavior/UI/MainUi.tscn";
    public static BehaviorPlugin Plugin { get; private set; }

    private MainUi _mainUi;

    public override void _EnterTree()
    {
        Plugin = this;
        
        _mainUi = ResourceLoader.Load<PackedScene>(MainUiScene).Instantiate<MainUi>();
        _mainUi.Plugin = this;
        _mainUi.Visible = false;


        GetEditorInterface().GetEditorMainScreen().AddChild(_mainUi);
    }

    public override void _ExitTree()
    {
        GetEditorInterface().GetEditorMainScreen().RemoveChild(_mainUi);

        _mainUi.Plugin = null;
        _mainUi.QueueFree();
    }

    public override void _Edit(GodotObject @object)
    {
        _MakeVisible(true);
    }

    public override string _GetPluginName()
    {
        return "Behavior";
    }

    public override bool _Handles(GodotObject godotObject)
    {
        if (godotObject is not BehaviorDefine define) return false;

        _mainUi.SetSelected(define.ResourcePath);

        _MakeVisible(true);
        return true;
    }

    public override bool _HasMainScreen()
    {
        return true;
    }

    public override void _MakeVisible(bool visible)
    {
        _mainUi.Visible = visible;
    }

    public static IEnumerable<Type> GetBehaviorTypes(Type type)
    {
        var assembly = Assembly.GetAssembly(typeof(BehaviorPlugin));

        return assembly?.GetTypes().Where(t => t.IsSubclassOf(type) && !t.IsAbstract).ToList();
    }
}
#endif