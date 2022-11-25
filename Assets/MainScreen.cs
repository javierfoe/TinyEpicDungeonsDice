using UnityEngine;
using UnityEngine.UIElements;

public class MainScreen : MonoBehaviour
{
    private readonly Length _thirty = Length.Percent(30), _twenty = Length.Percent(20);
    
    private VisualElement _modifier, _attack, _skillCheck;
    private Label _result;
    private NumberModifier _numberModifier, _skillCheckModifier, _defenseModifier, _hitModifier;
    private DiceAmount _diceAmount;
    private bool _isSkillCheck = true;
    
    void OnEnable()
    {
        var rootElement = GetComponent<UIDocument>().rootVisualElement;
        var diceAmount = rootElement.Q<VisualElement>("DiceAmount");
        _diceAmount = new DiceAmount(diceAmount);
        _modifier = rootElement.Q<TemplateContainer>("Modifier");
        _numberModifier = new NumberModifier(_modifier);
        _skillCheck = rootElement.Q<TemplateContainer>("SkillCheck");
        _skillCheckModifier = new SkillCheckModifier(_skillCheck);
        _attack = rootElement.Q<VisualElement>("Attack");
        var defense = rootElement.Q<TemplateContainer>("Defense");
        _defenseModifier = new DefenseModifier(defense);
        var hits = rootElement.Q<TemplateContainer>("Hits");
        _hitModifier = new HitModifier(hits);
        var skillCheckRadio = rootElement.Q<RadioButton>("SkillCheck");
        var attackRadio = rootElement.Q<RadioButton>("Attack");
        skillCheckRadio.RegisterValueChangedCallback(EnableSkillCheck);
        attackRadio.RegisterValueChangedCallback(EnableAttack);
        var reset = rootElement.Q<Button>("Reset");
        reset.clickable.clicked += ResetModifiers;
        _result = rootElement.Q<Label>("Result");
    }

    private void ResetModifiers()
    {
        _numberModifier.Reset();
        _skillCheckModifier.Reset();
        _defenseModifier.Reset();
        _hitModifier.Reset();
    }

    private void EnableSkillCheck(ChangeEvent<bool> evt)
    {
        if (!evt.newValue) return;
        _modifier.style.height = _thirty;
        _attack.style.display = DisplayStyle.None;
        _skillCheck.style.display = DisplayStyle.Flex;
        _isSkillCheck = true;
    }

    private void EnableAttack(ChangeEvent<bool> evt)
    {
        if (!evt.newValue) return;
        _modifier.style.height = _twenty;
        _attack.style.display = DisplayStyle.Flex;
        _skillCheck.style.display = DisplayStyle.None;
        _isSkillCheck = false;
    }

    private void Update()
    {
        var dice = _diceAmount.CurrentDice;
        var modifier = _numberModifier.CurrentValue;
        _result.text = _isSkillCheck ? 
            DicePercentage.SkillCheck(dice, modifier,_skillCheckModifier.CurrentValue) : 
            DicePercentage.Attack(dice, modifier, _defenseModifier.CurrentValue, _hitModifier.CurrentValue);
    }
}
