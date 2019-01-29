using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int _points = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void EarnPoints(int points)
    {
        _points += points;
        ScoreManager.instance.UpdateUI();
    }

    public int GetPoints()
    {
        return _points;
    }
}
