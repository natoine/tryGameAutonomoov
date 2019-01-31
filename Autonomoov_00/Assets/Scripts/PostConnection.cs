using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PostConnection : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            ScoreManager.instance._endGameTime = Time.time;
            Time.timeScale = 0;
            Upload();
        });
        
    }

    void Upload()
    {
        string JsonArraystring = "{\"Game\": [{\"name\":\""+Application.productName+ "\",\"params_playercount\":\""+ GameParameters.instance.GetPlayerCount().ToString()+
            "\",\"params_timer\":\"" + string.Format("{0:00}:{1:00}:{2:00}", GameParameters.instance.GetTimer() / 3600, GameParameters.instance.GetTimer() / 60,  GameParameters.instance.GetTimer() % 60) + 
            "\",\"params_movement\":\"" + GameParameters.instance.GetMovementString()+
            "\",\"launchingdate\":\"" + GameParameters.instance.GetDateTime() +
            "\",\"gametimer\":\"" + string.Format("{0:00}:{1:00}:{2:00}", ScoreManager.instance.GetEndGameTime() / 3600, ScoreManager.instance.GetEndGameTime() / 60, ScoreManager.instance.GetEndGameTime() % 60) +
            "\",\"playersPicturePath\":\"" + "/Temp_Data" +
            "\",\"playersScores\":[";

        for (int i = 0; i < GameParameters.instance.GetPlayerCount(); i++)
        {
            JsonArraystring += "\"" + ScoreManager.instance.GetPlayerScore(i).GetPoints().ToString() + "\"";
            if (i != GameParameters.instance.GetPlayerCount() - 1)
            {
                JsonArraystring += ",";
            }
        }
        JsonArraystring += "]}]}";
        Debug.Log(JsonArraystring);
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");
        byte[] body = Encoding.UTF8.GetBytes(JsonArraystring);

        WWW www = new WWW("localhost:8080/gameresult/" + Application.productName, body, headers);
        
        Debug.Log("localhost:8080/gameresult/" + Application.productName);

        StartCoroutine(PostdataEnumerator(www));
    }

    IEnumerator PostdataEnumerator(WWW www)
    {
        yield return www;
        if (www.error != null)
        {
            Debug.Log("Data Submitted");
        }
        else
        {
            Debug.Log(www.error);
        }
    }
}