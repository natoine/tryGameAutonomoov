using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PostConnection : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            StartCoroutine(Upload());
        });
        
    }

    IEnumerator Upload()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        for(int i = 0; i < GameParameters.instance.GetPlayerCount(); i++)
        {
            formData.Add(new MultipartFormDataSection(string.Concat("player",(i+1)), ScoreManager.instance.GetPlayerScore(i).GetPoints().ToString()));
        }
        Debug.Log(formData[0].ToString());
        UnityWebRequest www = new UnityWebRequest();
#if UNITY_WINDOWS || UNITY_EDITOR
        www = UnityWebRequest.Post("localhost:8080/startgame/agameforwindows", formData);
#elif UNITY_LINUX
        www = UnityWebRequest.Post("localhost:8080/startgame/agameforlinux", formData);
#endif
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
}
