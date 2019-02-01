using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodySelection : MonoBehaviour
{
    [SerializeField]
    Text ToSelectUI;
    BodySourceView _bodyView;
    int count = 0;
    bool isSelecting = false;

    void Start()
    {
        _bodyView = GameObject.Find("BodyView").GetComponent<BodySourceView>();
        GetComponent<Button>().onClick.AddListener(delegate
        {
            count = 0;
            isSelecting = true;
            _bodyView.HideUnusedImage();
            ToSelectUI.text = "Selectionner joueur " + (count+1) + " / " + _bodyView.GetTrackedBodiesCount();
            GetComponent<Button>().interactable = false;
        });
    }

    void Update()
    {
        if(!GetComponent<Button>().interactable && _bodyView.GetTrackedBodiesCount() > 0)
        {
            GetComponent<Button>().interactable = true;

        }
        else if(GetComponent<Button>().interactable && _bodyView.GetTrackedBodiesCount() == 0)
        {
            GetComponent<Button>().interactable = false;
            ToSelectUI.text = "Pas de selection";

        }

        if (isSelecting && count < _bodyView.GetTrackedBodiesCount())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Clic !");
                RaycastHit hitInfo;
                Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 500f, Color.red);
                if(Physics.Raycast(new Ray(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 500f), out hitInfo))
                {
                    Debug.Log("Mr " + hitInfo.transform.gameObject.name + " est enregistré comme Player "+(count+1));
                    _bodyView.AssignBody(count, ulong.Parse(hitInfo.transform.gameObject.name));
                    count++;
                    if(count < _bodyView.GetTrackedBodiesCount())
                    {
                        ToSelectUI.text = "Selectionner joueur " + (count) + " / " + _bodyView.GetTrackedBodiesCount();
                    }
                    else
                    {
                        ToSelectUI.text = count + " joueur(s) selectionné(s)";
                        GetComponent<Button>().interactable = true;
                    }
                }
                else
                {
                    Debug.Log("Plouf");
                }
            }
        }
    }


}
