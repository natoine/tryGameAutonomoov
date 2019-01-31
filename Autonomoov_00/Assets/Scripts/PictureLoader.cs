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
        for (int i = 0; i < photo.Length; i++)
        {
            Texture2D tex = new Texture2D(avatars[i].width, avatars[i].height, TextureFormat.RGB24, false);
            tex.SetPixels(avatars[i].GetPixels(0, 0, avatars[i].width, avatars[i].height));
            tex.Apply();
            byte[] png = tex.EncodeToPNG();
            File.WriteAllBytes(_tempDirectory + "/Player" + (i + 1) + ".png", png);
        }

    }
}