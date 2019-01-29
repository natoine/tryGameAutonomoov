using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance = null;

    private int playersCount = 0;
    private float _gameStartTime = 0;

    [SerializeField]
    private PlayerScore[] scores;
    [SerializeField]
    private Text[] scoreTexts;

    void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            Initialize();
        }
    }

    void Start()
    {
        GameObject.Find("Timer").transform.GetChild(1).GetComponent<Text>().text = string.Format("{0:00}:{1:00}", GameParameters.instance.GetTimer()/60, GameParameters.instance.GetTimer()%60); ;
        UpdateUI();
    }

    void Update()
    {
        GameObject.Find("Timer").transform.GetChild(0).GetComponent<Text>().text = string.Format("{0:00}:{1:00}", Time.timeSinceLevelLoad / 60, Time.timeSinceLevelLoad % 60);
    }

    private void Initialize()
    {
        playersCount = GameParameters.instance.GetPlayerCount();
        scores = new PlayerScore[playersCount];
        for(int i = 0; i < playersCount; i++)
        {
            scores[i] = new PlayerScore();
        }
    }

    public void UpdateUI()
    {
        for(int i = 0; i < scores.Length; i++)
        {
            Debug.Log("Update !");
            scoreTexts[i].text = scores[i].GetPoints().ToString();
        }
    }

    public PlayerScore GetPlayerScore(int player)
    {
        Debug.Log("Get Player " + player);
        return scores[player];
    }
}
