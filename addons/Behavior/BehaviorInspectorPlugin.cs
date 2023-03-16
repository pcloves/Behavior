﻿using Game.addons.Behavior.Action;
using Godot;

namespace Game.addons.Behavior;

public partial class BehaviorInspectorPlugin : EditorInspectorPlugin
{
    public override bool _CanHandle(GodotObject @object)
    {
        var canHandle = @object is ActionCreateTimer;
        
        GD.Print("Type:", @object.GetType(), ", canHandle:", canHandle);
        
        return canHandle;
    }

    public override bool _ParseProperty(GodotObject @object, Variant.Type type, string name, PropertyHint hintType, string hintString,
        PropertyUsageFlags usageFlags, bool wide)
    {
        
        // PrintArgs(@object, type, name, hintType, hintString, usageFlags, wide);

        return base._ParseProperty(@object, type, name, hintType, hintString, usageFlags, wide);
    }


    public static void PrintArgs(params object[] args)
    {
        GD.PrintS(args);
    }
    
}