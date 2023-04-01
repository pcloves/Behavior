using System;
using System.Linq;
using Behavior.addons.Behavior.Action;
using Behavior.addons.Behavior.Define;
using Godot;

namespace Behavior.addons.Behavior.Editor;

[Tool]
public partial class UiBehaviorAction : HBoxContainer
{
    private OptionButton _optionButton;
    private Button _remove;

    public BehaviorUnit BehaviorUnitBelong { get; set; }
    public BehaviorAction Action { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _optionButton = GetNodeOrNull<OptionButton>("%OptionButton");
        _optionButton.ItemSelected += OnItemSelected;
        _optionButton.FocusEntered += OnFocusEntered;

        _remove = GetNodeOrNull<Button>("%Remove");
        _remove.Pressed += OnRemovePressed;

        InitOptionButton();
    }
    
    private void OnFocusEntered()
    {
        if (Action != null)
        {
            BehaviorPlugin.Plugin.GetEditorInterface().EditResource(Action);
        }
    }


    private void OnRemovePressed()
    {
        if (Action != null)
        {
            BehaviorUnitBelong.Actions.Remove(Action);
        }
        
        QueueFree();
    }

    private void OnItemSelected(long index)
    {
        GD.Print($"{nameof(OnItemSelected)}");
        
        var typeName = _optionButton.GetItemMetadata((int)index).AsString();
        var type = Type.GetType(typeName);

        var behaviorAction = (BehaviorAction)Activator.CreateInstance(type);

        var indexOld = BehaviorUnitBelong.Actions.IndexOf(Action);
        if (indexOld != -1)
        {
            BehaviorUnitBelong.Actions[indexOld] = behaviorAction;
        }
        else
        {
            BehaviorUnitBelong.Actions.Add(behaviorAction);
        }

        Action = behaviorAction;
        BehaviorPlugin.Plugin.GetEditorInterface().EditResource(behaviorAction);
    }

    private void InitOptionButton()
    {
        _optionButton.Clear();
        
        var behaviorTypes = BehaviorPlugin.GetBehaviorTypes(typeof(BehaviorAction))
            .ToList();

        for (var index = 0; index < behaviorTypes.Count; index++)
        {
            var actionType = behaviorTypes[index];
            _optionButton.AddItem(actionType.Name);
            _optionButton.SetItemMetadata(index, actionType.FullName);
        }

        if (Action == null)
        {
            _optionButton.Selected = -1;
        }
        else
        {
            _optionButton.Selected = behaviorTypes.IndexOf(Action.GetType());
        }
    }
}