using System;
using System.Linq;
using Godot;
using ActionResource = Behavior.Define.ActionResource;
using BehaviorUnit = Behavior.Define.BehaviorUnit;

namespace Behavior.UI;

[Tool]
public partial class UiBehaviorAction : HBoxContainer
{
    private OptionButton _optionButton;
    private Button _remove;

    public BehaviorUnit BehaviorUnitBelong { get; set; }
    public ActionResource ActionResource { get; set; }

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
        if (ActionResource != null)
        {
            BehaviorPlugin.Plugin.GetEditorInterface().EditResource(ActionResource);
        }
    }


    private void OnRemovePressed()
    {
        if (ActionResource != null)
        {
            BehaviorUnitBelong.Actions.Remove(ActionResource);
        }

        QueueFree();
    }

    private void OnItemSelected(long index)
    {
        GD.Print($"{nameof(OnItemSelected)}");

        var typeName = _optionButton.GetItemMetadata((int)index).AsString();
        var type = Type.GetType(typeName);

        var behaviorAction = (ActionResource)Activator.CreateInstance(type);

        var indexOld = BehaviorUnitBelong.Actions.IndexOf(ActionResource);
        if (indexOld != -1)
        {
            BehaviorUnitBelong.Actions[indexOld] = behaviorAction;
        }
        else
        {
            BehaviorUnitBelong.Actions.Add(behaviorAction);
        }

        ActionResource = behaviorAction;
        BehaviorPlugin.Plugin.GetEditorInterface().EditResource(behaviorAction);
    }

    private void InitOptionButton()
    {
        _optionButton.Clear();

        var behaviorTypes = BehaviorPlugin.GetBehaviorTypes(typeof(ActionResource))
            .ToList();

        for (var index = 0; index < behaviorTypes.Count; index++)
        {
            var actionType = behaviorTypes[index];
            _optionButton.AddItem(actionType.Name);
            _optionButton.SetItemMetadata(index, actionType.FullName);
        }

        if (ActionResource == null)
        {
            _optionButton.Selected = -1;
        }
        else
        {
            _optionButton.Selected = behaviorTypes.IndexOf(ActionResource.GetType());
        }
    }
}