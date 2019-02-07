using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance = null;

    private int playersCount = 0;

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
        UpdateUI();
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
            scoreTexts[i].text = scores[i].GetPoints().ToString();
        }
    }

    public PlayerScore GetPlayerScore(int player)
    {
        Debug.Log("Get Player " + player);
        return scores[player];
    }

    public float GetEndGameTime()
    {
        return GameParameters.instance.GetTimer() - FindObjectOfType<Timer>().RemainingTime();
    }
}
