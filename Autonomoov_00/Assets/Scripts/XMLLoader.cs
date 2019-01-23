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
    private TextAsset xmlFile;
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
        xmlFile = Resources.Load<TextAsset>("Parameters");
        if (xmlFile)
        {
            Debug.Log(xmlFile.text);
            xmlDocument.LoadXml((xmlFile.text));
            foreach (XmlNode node in xmlDocument.ChildNodes[1])
            {
                switch (node.Name) {
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
                            if(node.InnerText == "L_HAND")
                            {
                                GameParameters.instance.SetMovement(MOVEMENT.L_HAND);
                            }
                            if (node.InnerText == "R_HAND")
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
        return false;
    }

    public XmlDocument GetXmlDocument()
    {
        return xmlDocument;
    }
}