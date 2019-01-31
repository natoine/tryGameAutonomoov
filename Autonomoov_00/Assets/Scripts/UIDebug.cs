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
        GameObject.Find("Movement").GetComponent<Text>().text += GameParameters.instance.GetMovementString().ToString();
        GameObject.Find("XmlFile").GetComponent<Text>().text += (System.IO.File.ReadAllText(Path.Combine(Directory.GetParent(Application.dataPath).FullName, XMLLoader.instance.GetXmlFileName())));
        GameObject.Find("XmlFile").GetComponent<Text>().text += XMLLoader.instance.GetXmlDocument().InnerXml;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
