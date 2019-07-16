using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class EndSignal : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            Time.timeScale = 0;
            Upload();
        });

    }

    void Upload()
    {
        string JsonArraystring = "{\"name\":\"" + Application.productName + "\"," +
            "\"params\":{\"playersNb\":\"" + GameParameters.instance.GetPlayerCount().ToString() +
            "\",\"timer\":\"" + string.Format("{0:00}:{1:00}:{2:00}", GameParameters.instance.GetTimer() / 3600, GameParameters.instance.GetTimer() / 60, GameParameters.instance.GetTimer() % 60) +
            "\",\"movement\":\"" + GameParameters.instance.GetMovementString()+
            "\"},\"results\":[";

        for (int i = 0; i < GameParameters.instance.GetPlayerCount(); i++)
        {
            JsonArraystring += "{\"score\":\"" + ScoreManager.instance.GetPlayerScore(i).GetPoints().ToString() + "\"}";
            if (i != GameParameters.instance.GetPlayerCount() - 1)
            {
                JsonArraystring += ",";
            }
        }
        JsonArraystring += "]}";
        Debug.Log(JsonArraystring);
        Console.WriteLine("results: " + JsonArraystring);
        StartCoroutine(waitToQuit(1f));
    }

    IEnumerator waitToQuit(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Application.Quit();
    }
}