using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    [SerializeField] private IncomeSourceData[] _sourceData;

    [SerializeField] private double _startingCurrency = 0;
    [SerializeField] private double _manualClickReward = 1;

    private IncomeSource[] _sources;

    public double Currency { get; private set; }

    public double TotalIncomePerSecond
    {
        get
        {
            double total = 0;

            if (_sources == null)
                return total;

            foreach (IncomeSource source in _sources)
            {
                if (source.IsUnlocked)
                    total += source.IncomePerSecond;
            }

            return total;
        }
    }

    public int SourceCount => _sources?.Length ?? 0;

    public event Action OnStateChanged;

    private void Awake()
    {
        if (_sourceData == null || _sourceData.Length != 3)
        {
            enabled = false;
            return;
        }

        _sources = new IncomeSource[_sourceData.Length];

        for (int i = 0; i < _sourceData.Length; i++)
        {
            _sources[i] = new IncomeSource(_sourceData[i]);
        }

        LoadProgress();
        UpdateUnlocks();
        NotifyChanged();
    }

    private void Update()
    {
        double income = TotalIncomePerSecond;

        if (income > 0)
        {
            Currency += income * Time.deltaTime;
            NotifyChanged();
        }
    }

    public IncomeSource GetSource(int index)
    {
        if (index < 0 || index >= SourceCount)
            return null;

        return _sources[index];
    }

    public void ManualClick()
    {
        AddCurrency(_manualClickReward);
    }

    public void AddCurrency(double amount)
    {
        if (amount <= 0)
            return;

        Currency += amount;
        NotifyChanged();
    }

    public bool TryUpgrade(int index)
    {
        IncomeSource source = GetSource(index);

        if (source == null)
            return false;

        if (!source.IsUnlocked)
        {
            NotifyChanged();
            return false;
        }

        double cost = source.NextUpgradeCost;

        if (Currency < cost)
        {
            NotifyChanged();
            return false;
        }

        Currency -= cost;
        source.Upgrade();

        UpdateUnlocks();
        NotifyChanged();

        return true;
    }

    private void UpdateUnlocks()
    {
        if (_sources == null || _sources.Length == 0)
            return;

        int firstSourceLevel = _sources[0].Level;

        for (int i = 1; i < _sources.Length; i++)
        {
            if (firstSourceLevel >= _sources[i].Data.RequiredFirstSourceLevel)
            {
                _sources[i].Unlock();
            }
        }
    }

    private void LoadProgress()
    {
        GameSaveData save = SaveSystem.Load();

        if (save == null)
        {
            Currency = _startingCurrency;
            return;
        }

        Currency = Math.Max(0, save.Currency);

        for (int i = 0; i < _sources.Length; i++)
        {
            if (save.Levels != null && i < save.Levels.Length)
                _sources[i].SetLevel(save.Levels[i]);

            if (save.Unlocked != null && i < save.Unlocked.Length)
                _sources[i].RestoreUnlockedState(save.Unlocked[i]);
        }

        UpdateUnlocks();

        long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        long elapsed = Math.Max(
            0, now - save.LastExitTime
        );

        elapsed = Math.Min(elapsed, 8 * 60 * 60);

        double offlineIncome = TotalIncomePerSecond * elapsed;

        Currency += offlineIncome;
    }

    public void SaveProgress()
    {
        if (_sources == null)
            return;

        GameSaveData save = new GameSaveData
        {
            Currency = Currency,
            Levels = new int[_sources.Length],
            Unlocked = new bool[_sources.Length],
        };

        for (int i = 0; i < _sources.Length; i++)
        {
            save.Levels[i] = _sources[i].Level;
            save.Unlocked[i] = _sources[i].IsUnlocked;
        }

        SaveSystem.Save(save);
    }

    public void ResetProgress()
    {
        SaveSystem.Delete();

        Currency = _startingCurrency;

        for (int i = 0; i < _sources.Length; i++)
        {
            _sources[i].SetLevel(0);
            _sources[i].RestoreUnlockedState(_sources[i].Data.UnlockedByDefault);
        }

        UpdateUnlocks();
        SaveProgress();
        NotifyChanged();
    }

    private void OnApplicationPause(bool paused)
    {
        if (paused)
            SaveProgress();
    }

    private void OnApplicationQuit()
    {
        SaveProgress();
    }

    private void NotifyChanged()
    {
        OnStateChanged?.Invoke();
    }
}
