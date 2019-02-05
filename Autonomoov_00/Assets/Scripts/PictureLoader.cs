using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PictureLoader : MonoBehaviour
{
    public static PictureLoader instance = null;
    public string _tempDirectory;
    public Sprite [] photo;
    public Texture2D [] avatars;
    private void Awake()
    {
        if (instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    void Start()
    {
        if (_tempDirectory == "") _tempDirectory = GameParameters.instance.GetTempDataPath();
        if (!Directory.Exists(_tempDirectory))
        {
            Directory.CreateDirectory(_tempDirectory);
        }
    }

    void LoadPicture(Texture2D texture, int player)
    {
        /*Texture2D tex = new Texture2D(texture.width, texture.height, TextureFormat.RGB24, false);
        tex.SetPixels(avatars[i].GetPixels(0, 0, avatars[i].width, avatars[i].height));
        tex.Apply();*/
        byte[] png = texture.EncodeToPNG();
        File.WriteAllBytes(_tempDirectory + "/Player" + (player + 1) + ".png", png);

    }
}