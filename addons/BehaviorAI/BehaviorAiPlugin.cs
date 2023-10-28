#if TOOLS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Godot;

namespace BehaviorAI;

[Tool]
public partial class BehaviorAiPlugin : EditorPlugin
{
    private const string MainUiScene = "res://addons/BehaviorAI/UI/MainUi.tscn";

    private MainUi _mainUi;
    
    public override void _EnterTree()
    {
        _mainUi = ResourceLoader.Load<PackedScene>(MainUiScene).Instantiate<MainUi>();
        _mainUi.Plugin = this;
        _mainUi.Visible = false;
        
        EditorInterface.Singleton.GetEditorMainScreen().AddChild(_mainUi);
    }
    
    public override void _ExitTree()
    {
        EditorInterface.Singleton.GetEditorMainScreen().RemoveChild(_mainUi);
        
        _mainUi.Plugin = null;
        _mainUi.QueueFree();
    }
    
    public override void _Edit(GodotObject @object)
    {
        _MakeVisible(true);
    }
    
    public override string _GetPluginName()
    {
        return "BehaviorAI";
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
        var assembly = Assembly.GetAssembly(typeof(BehaviorAiPlugin));
        
        return assembly?.GetTypes().Where(t => t.IsSubclassOf(type) && !t.IsAbstract).ToList();
    }
}
#endif