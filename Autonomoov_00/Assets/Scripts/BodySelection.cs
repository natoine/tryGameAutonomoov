using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BodySelection : MonoBehaviour
{
    [SerializeField]
    Text ToSelectUI;
    [SerializeField]
    int photoWidth;
    [SerializeField]
    int photoHeight;
    [SerializeField]
    GameObject[] PlayersAvatar;

    BodySourceView _bodyView;
    int totalPlayers = 0;
    

    int count = 0;
    bool isSelecting = false;

    void Start()
    {
        totalPlayers = GameParameters.instance.GetPlayerCount();
        _bodyView = GameObject.Find("BodyView").GetComponent<BodySourceView>();
        GetComponent<Button>().onClick.AddListener(delegate
        {
            count = 0;
            isSelecting = true;
            _bodyView.ResetMarkers();
            PlayersManager.instance.InitPlayers();
            ToSelectUI.text = "Selectionner joueur " + (count + 1) + " / " + _bodyView.GetTrackedBodiesCount();
            GetComponent<Button>().interactable = false;
        });
    }

    void Update()
    {
        if (!GetComponent<Button>().interactable && _bodyView.GetTrackedBodiesCount() > 0)
        {
            GetComponent<Button>().interactable = true;

        }
        else if (GetComponent<Button>().interactable && _bodyView.GetTrackedBodiesCount() == 0)
        {
            GetComponent<Button>().interactable = false;
            ToSelectUI.text = "Pas de selection possible";

        }

        if (isSelecting && count < _bodyView.GetTrackedBodiesCount())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Clic !");
                RaycastHit hitInfo;
                Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 500f, Color.red);
                if (Physics.Raycast(new Ray(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 500f), out hitInfo) && hitInfo.transform.name != "ColorView")
                {
                    Debug.Log("Mr " + hitInfo.transform.gameObject.name + " est enregistré comme Player " + (count + 1));
                    TakePhoto(hitInfo.transform.gameObject);
                    _bodyView.AssignBody(count, ulong.Parse(hitInfo.transform.gameObject.name));
                    PlayersManager.instance.AddPlayer(ulong.Parse(hitInfo.transform.gameObject.name));
                    count++;
                    if (count < _bodyView.GetTrackedBodiesCount())
                    {
                        ToSelectUI.text = "Selectionner joueur " + (count) + " / " + _bodyView.GetTrackedBodiesCount();
                    }
                    else
                    {
                        ToSelectUI.text = count + " joueur(s) selectionné(s), cliquez pour jouer pour commencer !";
                        GetComponent<Button>().interactable = true;
                        GameObject.Find("PlayButton").GetComponent<Button>().interactable = true;
                    }
                }
                else
                {
                    Debug.Log("Aucun joueur n'a été touché");
                }
            }
        }
        if (count == totalPlayers)
        {
            ToSelectUI.text = count + " joueur(s) selectionné(s), cliquez pour jouer pour commencer !";
        }
    }

    void TakePhoto(GameObject body)
    {
        RaycastHit hitInfo;
        Vector3 centerPhoto;
        if (Physics.Raycast(new Ray(Camera.main.transform.position, body.transform.position * 500f), out hitInfo))
        {
            //Get the coordinates of the hit in percentage
            centerPhoto = hitInfo.textureCoord;
            //Get the Camera render mesh texture
            Texture2D tex = (Texture2D)hitInfo.collider.gameObject.GetComponent<Renderer>().material.mainTexture;
            Texture2D photo = new Texture2D(photoWidth, photoHeight, TextureFormat.RGB24, false);
            //Get the actual texture coordinates
            Vector2 coord = new Vector2(((1 - centerPhoto.x) * tex.width), (centerPhoto.y * tex.height));
            //Print pixels in new texture
            photo.SetPixels(tex.GetPixels((int)(coord.x - (photoWidth / 2)), (int)(coord.y), photoWidth, photoHeight));
            photo.Apply();
            //Invert pixel order so it's not vertically flipped, convert it in png format
            Texture2D result = InvertPixels(photo);
            byte[] png = result.EncodeToPNG();
            //Saves the png file in Temp_Data directory
            File.WriteAllBytes(Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Temp_Data") + "/Player" + (count + 1) + ".png", png);
            //Displays photo new to correct player number
            PlayersAvatar[count].SetActive(true);
            PlayersAvatar[count].transform.GetChild(0).GetComponent<Image>().sprite = Sprite.Create(result, new Rect(0, 0, result.width, result.height), new Vector2(0.5f, 0.5f));
        }
    }

    Texture2D InvertPixels(Texture2D photo)
    {
        Texture2D result = new Texture2D(photo.width, photo.height);
        for (int i = 0; i < result.width; i++)
        {
            for (int j = 0; j < result.height; j++)
            {
                result.SetPixel(i, result.height - j - 1, photo.GetPixel(i, j));
            }
        }
        result.Apply();
        return result;
    }
}