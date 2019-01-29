using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDebug : MonoBehaviour
{
    [SerializeField]
    Button[] players;

    void Start()
    {
        for(int i = 0; i < GameParameters.instance.GetPlayerCount(); i++)
        {
            Debug.Log("Player " + i);
            int count = i;
               players[i].onClick.AddListener(delegate
            {
                Debug.Log(" Player score " + count);
                ScoreManager.instance.GetPlayerScore(count).EarnPoints(10);
            });
        }
    }

    void Update()
    {
        
    }
}
