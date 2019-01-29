using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

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
        try
        {
#if UNITY_EDITOR
            xmlFile = (System.IO.File.ReadAllText(Path.Combine(Directory.GetParent(Application.dataPath).Parent.FullName, "Parameters.xml")));
#elif UNITY_WINDOWS
            xmlFile = (System.IO.File.ReadAllText(Path.Combine(Directory.GetParent(Application.dataPath).FullName, "launchparams.xml")));
#endif
            xmlFileName = "launchparams.xml";
        }
        catch (Exception)
        {
            Debug.LogError("Oh oh, pas de xml trouvé sur ce chemin");

        }
        new GameObject("UI Debug").AddComponent<UIDebug>();


        //xmlFile = Resources.Load<TextAsset>("Parameters");
        if (xmlFile != null && xmlFile != "")
        {
            Debug.Log(xmlFile);
            xmlDocument.LoadXml((xmlFile));
            foreach (XmlNode node in xmlDocument["root"])
            {
                switch (node.Name)
                {
                    case "playersCount":
                        {
                            GameParameters.instance.SetPlayerCount(int.Parse(node.InnerText));
                            break;
                        }
                    case "timer":
                        {
                            GameParameters.instance.SetTimer(int.Parse(node.InnerText));
                            break;
                        }
                    case "movement":
                        {
                            if (node.InnerText == "")
                            {
                                GameParameters.instance.SetMovement(MOVEMENT.L_HAND);
                            }
                            if (node.InnerText == MOVEMENT.R_HAND.ToString())
                            {
                                GameParameters.instance.SetMovement(MOVEMENT.R_HAND);
                            }
                            break;
                        }
                }
            }
            Debug.Log("Registred player count = " + GameParameters.instance.GetPlayerCount());
            Debug.Log("Registred timer in seconds = " + GameParameters.instance.GetTimer());
            Debug.Log("Registred movement = " + GameParameters.instance.GetMovement().ToString());

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