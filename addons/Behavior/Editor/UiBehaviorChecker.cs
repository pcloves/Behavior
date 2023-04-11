using Behavior.addons.Behavior.Check;
using Godot;
using System;
using System.Linq;

namespace Behavior.addons.Behavior.Editor;

[Tool]
public partial class UiBehaviorChecker : HBoxContainer
{
    private OptionButton _optionButton;
    private Button _remove;

    public CheckAndOr CheckerBelong { get; set; }
    public BehaviorChecker Checker { get; set; }

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
        if (Checker != null)
        {
            BehaviorPlugin.Plugin.GetEditorInterface().EditResource(Checker);
        }
    }

    private void OnRemovePressed()
    {
        if (Checker != null)
        {
            CheckerBelong.Checkers.Remove(Checker);
        }

        QueueFree();
    }

    private void OnItemSelected(long index)
    {
        GD.Print($"{nameof(OnItemSelected)}");
        var typeName = _optionButton.GetItemMetadata((int)index).AsString();
        var type = Type.GetType(typeName);

        var behaviorChecker = (BehaviorChecker)Activator.CreateInstance(type);

        var indexOld = CheckerBelong.Checkers.IndexOf(Checker);
        if (indexOld != -1)
        {
            CheckerBelong.Checkers[indexOld] = behaviorChecker;
        }
        else
        {
            CheckerBelong.Checkers.Add(behaviorChecker);
        }

        Checker = behaviorChecker;
        BehaviorPlugin.Plugin.GetEditorInterface().EditResource(Checker);
    }

    private void InitOptionButton()
    {
        _optionButton.Clear();

        var behaviorTypes = BehaviorPlugin.GetBehaviorTypes(typeof(BehaviorChecker))
            .Where(type => type != typeof(CheckAndOr))
            .ToList();

        for (var index = 0; index < behaviorTypes.Count; index++)
        {
            var checkerType = behaviorTypes[index];
            _optionButton.AddItem(checkerType.Name);
            _optionButton.SetItemMetadata(index, checkerType.FullName);
        }

        if (Checker == null)
        {
            _optionButton.Selected = -1;
        }
        else
        {
            _optionButton.Selected = behaviorTypes.IndexOf(Checker.GetType());
        }
    }
}