using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UIDebug : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("playersCount").GetComponent<Text>().text += GameParameters.instance.GetPlayerCount();
        GameObject.Find("timer").GetComponent<Text>().text += GameParameters.instance.GetTimer();
        GameObject.Find("Movement").GetComponent<Text>().text += GameParameters.instance.GetMovement().ToString();
        GameObject.Find("XmlFile").GetComponent<Text>().text += Directory.GetParent(Application.dataPath).Parent.FullName+ XMLLoader.instance.GetXmlFileName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
