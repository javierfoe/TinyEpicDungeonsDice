public static class DicePercentage
{
    private static readonly float[][] Stats = {
        new []{ 83.33f, 66.67f, 50, 33.33f, 16.67f }, 
        new []{ 97.22f, 88.89f, 81.25f, 47.22f, 16.67f, 5.56f}, 
        new []{ 99.07f, 93.05f, 75.93f, 43.06f, 20.83f, 5.56f, 1.39f}};

    public static string SkillCheck(int dice, int modifier, int minimum)
    {
        var starting = dice + modifier;
        var array = Stats[dice-1];
        var index = minimum - starting-1;
        float result;
        if (index < 0) result = 100;
        else if (index >= array.Length) result = 0;
        else result = array[index];
        return result + "%";
    }

    public static string Attack(int dice, int modifier, int defense, int hits)
    {
        return SkillCheck(dice, modifier, defense + hits);
    }
}
