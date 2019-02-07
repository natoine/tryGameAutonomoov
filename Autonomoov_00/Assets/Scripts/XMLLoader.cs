using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class XMLLoader : MonoBehaviour
{
    /* <summary>
     * Ce singleton charge le fichier XML des paramètres. 
     * </summary> */
    public static XMLLoader instance = null;
    private string xmlFile = "";
    private string xmlFileName = "";
    private XmlDocument xmlDocument = new XmlDocument();

    void Start()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            Initiate(Application.systemLanguage);
        }
    }

    public bool Initiate(SystemLanguage language)
    {
        xmlFileName = "launchparams.xml";

        try
        {
              xmlFile = (System.IO.File.ReadAllText(Path.Combine(Directory.GetParent(Application.dataPath).FullName, xmlFileName)));
        }
        catch (Exception)
        {
            Debug.LogError("Oh oh, pas de xml trouvé sur ce chemin");

        }

        if (xmlFile != null && xmlFile != "")
        {
            Debug.Log(xmlFile);
            xmlDocument.LoadXml((xmlFile));
            foreach (XmlNode node in xmlDocument["root"])
            {
                Debug.Log(node.Name);
                switch (node.Name)
                {
                    case "playersCount":
                        {
                            GameParameters.instance.SetPlayerCount(int.Parse(node.InnerText));
                            break;
                        }
                    case "timer":
                        {
                            GameParameters.instance.SetTimer(DateTime.Parse(node.InnerText).Second + DateTime.Parse(node.InnerText).Minute*60 + DateTime.Parse(node.InnerText).Hour * 3600);
                            break;
                        }
                    case "movement":
                        {
                            if (node.InnerText == "left hand")
                            {
                                GameParameters.instance.SetMovement(MOVEMENT.L_HAND);
                            }
                            if (node.InnerText == "right hand")
                            {
                                GameParameters.instance.SetMovement(MOVEMENT.R_HAND);
                            }
                            break;
                        }
                }
            }
            Debug.Log("Registred player count = " + GameParameters.instance.GetPlayerCount());
            Debug.Log("Registred timer in seconds = " + GameParameters.instance.GetTimer());
            Debug.Log("Registred movement = " + GameParameters.instance.GetMovementString().ToString());
            new GameObject("UI Debug").AddComponent<UIDebug>();
            return true;
        }
        else Debug.LogError("Oh oh, pas de xml trouvé sur ce chemin");
        return false;
    }

    public XmlDocument GetXmlDocument()
    {
        return xmlDocument;
    }

    public string GetXmlFileName()
    {
        return xmlFileName;
    }
}