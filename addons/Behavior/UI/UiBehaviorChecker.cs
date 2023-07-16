using System;
using System.Linq;
using Godot;
using CheckAndOr = Behavior.Check.CheckAndOr;
using CheckerResource = Behavior.Define.CheckerResource;

namespace Behavior.UI;

[Tool]
public partial class UiBehaviorChecker : HBoxContainer
{
    private OptionButton _optionButton;
    private Button _remove;

    public CheckAndOr CheckerBelong { get; set; }
    public CheckerResource CheckerResource { get; set; }

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
        if (CheckerResource != null)
        {
            BehaviorPlugin.Plugin.GetEditorInterface().EditResource(CheckerResource);
        }
    }

    private void OnRemovePressed()
    {
        if (CheckerResource != null)
        {
            CheckerBelong.Checkers.Remove(CheckerResource);
        }

        QueueFree();
    }

    private void OnItemSelected(long index)
    {
        GD.Print($"{nameof(OnItemSelected)}");
        var typeName = _optionButton.GetItemMetadata((int)index).AsString();
        var type = Type.GetType(typeName);

        var behaviorChecker = (CheckerResource)Activator.CreateInstance(type);

        var indexOld = CheckerBelong.Checkers.IndexOf(CheckerResource);
        if (indexOld != -1)
        {
            CheckerBelong.Checkers[indexOld] = behaviorChecker;
        }
        else
        {
            CheckerBelong.Checkers.Add(behaviorChecker);
        }

        CheckerResource = behaviorChecker;
        BehaviorPlugin.Plugin.GetEditorInterface().EditResource(CheckerResource);
    }

    private void InitOptionButton()
    {
        _optionButton.Clear();

        var behaviorTypes = BehaviorPlugin.GetBehaviorTypes(typeof(CheckerResource))
            .Where(type => type != typeof(CheckAndOr))
            .ToList();

        for (var index = 0; index < behaviorTypes.Count; index++)
        {
            var checkerType = behaviorTypes[index];
            _optionButton.AddItem(checkerType.Name);
            _optionButton.SetItemMetadata(index, checkerType.FullName);
        }

        if (CheckerResource == null)
        {
            _optionButton.Selected = -1;
        }
        else
        {
            _optionButton.Selected = behaviorTypes.IndexOf(CheckerResource.GetType());
        }
    }
}