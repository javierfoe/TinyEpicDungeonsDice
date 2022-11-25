
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class DiceAmount
{
    public readonly UnityEvent<int> OnDiceAmountChange = new();

    private readonly DiceButton[] _buttons;
    private int _currentDice;

    public int CurrentDice => _currentDice;
    
    public DiceAmount(VisualElement root)
    {
        var one = root.Q<TemplateContainer>("Dice1");
        var two = root.Q<TemplateContainer>("Dice2");
        var three = root.Q<TemplateContainer>("Dice3");
        var diceButton1 = new DiceButton(one, Click, 1);
        var diceButton2 = new DiceButton(two, Click, 2);
        var diceButton3 = new DiceButton(three, Click, 3);
        _buttons = new[] { diceButton1, diceButton2, diceButton3 };
        Click(1);
    }

    private void Click(int value)
    {
        var length = _buttons.Length;
        for (var i = 0; i < length; i++)
        {
            _buttons[i].IsOn = i < value;
        }

        if (_currentDice == value) return;
        
        OnDiceAmountChange.Invoke(value);
        _currentDice = value;
    }
}
