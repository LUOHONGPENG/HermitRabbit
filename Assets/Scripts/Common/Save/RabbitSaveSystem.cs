using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RabbitSaveSystem
{
    public static void SaveByPlayerPrefs(string key, object data)
    {
        Debug.Log("SaveFromPlayerPrefs " + key);

        var json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    public static string LoadFromPlayerPrefs(string key)
    {
        Debug.Log("LoadFromPlayerPrefs " + key);

        return PlayerPrefs.GetString(key, null);
    }
}
