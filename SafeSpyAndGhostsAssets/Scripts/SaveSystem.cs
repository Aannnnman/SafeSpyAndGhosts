using System;
using UnityEngine;

[Serializable]
public class GameSaveData
{
    public double Currency;
    public int[] Levels;
    public bool[] Unlocked;
    public bool[] PurchasedMemories;
    public long LastExitTime;
}

public static class SaveSystem
{
    private const string SaveKey = "IdleGame_Save";

    public static void Save(GameSaveData data)
    {
        data.LastExitTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    public static GameSaveData Load()
    {
        if (!PlayerPrefs.HasKey(SaveKey))
            return null;

        string json = PlayerPrefs.GetString(SaveKey);

        try
        {
            return JsonUtility.FromJson<GameSaveData>(json);
        }
        catch (Exception exception)
        {
            Debug.LogError($"Error loading save: {exception.Message}");

            return null;
        }
    }

    public static void Delete()
    {
        PlayerPrefs.DeleteKey(SaveKey);
        PlayerPrefs.Save();
    }
}
