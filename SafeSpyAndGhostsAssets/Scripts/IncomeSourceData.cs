using UnityEngine;

[CreateAssetMenu(
    fileName = "IncomeSource",
    menuName = "Idle Game/Income Source"
)]

public class IncomeSourceData : ScriptableObject
{
    [Min(0)]
    public double BaseCost = 10;

    [Min(0)]
    public double BaseIncome = 1;

    [Min(1)]
    public float CostMultiplier = 1.5f;

    [Min(1)]
    public float IncomeMultiplier = 1.2f;

    public bool UnlockedByDefault;

    [Min(1)]
    public int RequiredFirstSourceLevel = 5;
}
