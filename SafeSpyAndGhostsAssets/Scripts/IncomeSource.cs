using System;

[Serializable]
public class IncomeSource
{
    public IncomeSourceData Data { get; }

    public int Level { get; private set; }

    public bool IsUnlocked { get; private set; }

    public double NextUpgradeCost => Data.BaseCost * Math.Pow(Data.CostMultiplier, Level);

    public double IncomePerSecond => Level * Data.BaseIncome * Math.Pow(Data.IncomeMultiplier, Level - 1);

    public IncomeSource(IncomeSourceData data)
    {
        Data = data;
        Level = 0;
        IsUnlocked = data.UnlockedByDefault;
    }

    public void SetLevel(int level)
    {
        Level = Math.Max(0, level);
    }

    public void Unlock()
    {
        IsUnlocked = true;
    }

    public void Lock()
    {
        IsUnlocked = false;
    }

    public void RestoreUnlockedState(bool unlocked)
    {
        IsUnlocked = unlocked;
    }

    public void Upgrade()
    {
        Level++;
    }
}
