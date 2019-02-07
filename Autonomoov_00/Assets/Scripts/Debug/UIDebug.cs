using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UIDebug : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("PlayersCount").GetComponent<Text>().text += GameParameters.instance.GetPlayerCount();
        GameObject.Find("Timer").GetComponent<Text>().text += GameParameters.instance.GetTimer();
        GameObject.Find("Movement").GetComponent<Text>().text += GameParameters.instance.GetMovementString().ToString();
        GameObject.Find("XmlFile").GetComponent<Text>().text += (System.IO.File.ReadAllText(Path.Combine(Directory.GetParent(Application.dataPath).FullName, XMLLoader.instance.GetXmlFileName())));
        GameObject.Find("XmlFile").GetComponent<Text>().text += XMLLoader.instance.GetXmlDocument().InnerXml;
    }

    void Update()
    {
        
    }
}
