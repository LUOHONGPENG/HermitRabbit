using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeletePlayerPref : MonoBehaviour
{
    [UnityEditor.MenuItem("RabbitTools/Delete All Player Pref")]
    public static void DeleteAllPref()
    {
        PlayerPrefs.DeleteAll();
    }
}
