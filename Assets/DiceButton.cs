using System;
using UnityEngine.UIElements;

public class DiceButton
{
    private readonly VisualElement _isOnImage;
    private bool _isOn;

    public bool IsOn
    {
        get => _isOn;
        set
        {
            _isOn = value;
            _isOnImage.visible = value;
        }
    }
    
    public DiceButton(TemplateContainer template, Action<int> action = null, int value = 0)
    {
        var button = template.Q<Button>("Dice");
        _isOnImage = button.Q<VisualElement>("IsOnImage");
        if(action != null)
            button.clickable.clicked += () => action.Invoke(value);
    }
}
