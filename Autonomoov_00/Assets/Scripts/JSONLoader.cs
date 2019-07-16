using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONLoader : MonoBehaviour
{
    public bool Awake()
    {
        string jsonFileName = "launchparams.json";

        try
        {
            string jsonFileContent = (System.IO.File.ReadAllText(Path.Combine(Directory.GetParent(Application.dataPath).FullName, jsonFileName)));
            JsonUtility.FromJsonOverwrite(jsonFileContent, GameObject.Find("GameParameters").GetComponent<GameParameters>());
            Debug.Log(jsonFileContent);
            new GameObject("UI Debug").AddComponent<UIDebug>();
            return true;
        }
        catch (Exception)
        {
            Debug.LogError("Oh oh, pas de json trouvé sur ce chemin");

        }
        return false;
    }

    public GameParameters UnserializeParameters(string jsonFileContent)
    {
        return JsonUtility.FromJson<GameParameters>(jsonFileContent);
    }
}