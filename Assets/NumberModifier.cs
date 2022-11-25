using UnityEngine.UIElements;

public class NumberModifier
{
    private const int MaxModifier = 9;
    
    private readonly Label _label;
    private readonly int _min, _max, _default;

    private int _currentValue;

    public int CurrentValue
    {
        get => _currentValue;
        private set
        {
            _currentValue = value;
            _label.text = _currentValue.ToString();
        }
    }

    public NumberModifier(VisualElement template) : this(template, 0, -MaxModifier, MaxModifier)
    {
        
    }
    
    protected NumberModifier(VisualElement template, int defaultNumber = 0, int min = int.MinValue, int max = int.MaxValue)
    {
        _min = min;
        _max = max;
        _default = defaultNumber;
        _label = template.Q<Label>("Label"); 
        var decrease = template.Q<Button>("Decrease");
        decrease.clickable.clicked += DecreaseValue;
        var increase = template.Q<Button>("Increase");
        increase.clickable.clicked += IncreaseValue;
        CurrentValue = defaultNumber;
    }

    public void Reset()
    {
        CurrentValue = _default;
    }

    private void DecreaseValue()
    {
        if (CurrentValue == _min) return;
        CurrentValue--;
    }

    private void IncreaseValue()
    {
        if (CurrentValue == _max) return;
        CurrentValue++;
    }
}

public class SkillCheckModifier : NumberModifier
{
    private const int MinSkillCheck = 3, MaxSkillCheck = 7, DefaultSkillCheck = 5;
    public SkillCheckModifier(VisualElement template) : base(template, DefaultSkillCheck, MinSkillCheck,MaxSkillCheck)
    {
    }
}

public class DefenseModifier : NumberModifier
{
    private const int MinDefense = 4, MaxDefense = 8, DefaultDefense = 4;
    public DefenseModifier(VisualElement template) : base(template, DefaultDefense, MinDefense, MaxDefense)
    {
    }
}

public class HitModifier : NumberModifier
{
    private const int MinHit = 1, DefaultHit = 1;

    public HitModifier(VisualElement template) : base(template, DefaultHit, MinHit)
    {
    }
}
