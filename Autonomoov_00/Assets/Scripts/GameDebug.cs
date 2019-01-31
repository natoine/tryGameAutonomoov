using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDebug : MonoBehaviour
{
    [SerializeField]
    Image[] avatars;
    [SerializeField]
    Button[] players;

    void Start()
    {
        for(int i = 0; i < GameParameters.instance.GetPlayerCount(); i++)
        {
            int count = i;
               players[i].onClick.AddListener(delegate
            {
                ScoreManager.instance.GetPlayerScore(count).EarnPoints(10);
            });
        }

        for (int j = 0; j < Mathf.Min(GameParameters.instance.GetPlayerCount(), PictureLoader.instance.photo.Length); j++)
        {
            avatars[j].overrideSprite = PictureLoader.instance.photo[j];
        }
    }

    void Update()
    {
        
    }
}
