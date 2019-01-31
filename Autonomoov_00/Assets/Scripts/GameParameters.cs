using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum MOVEMENT
{
    R_HAND,
    L_HAND
}

public class GameParameters : MonoBehaviour
{
    public static GameParameters instance;
    public string tempDataPath;
    private int _playerCount = 0;
    private int _timerInSeconds = 0;
    private MOVEMENT _requiredMovement;
    private System.DateTime dateTime = System.DateTime.UtcNow;
    void Awake()
    {
        if (!instance)
        {
            instance = this;
            tempDataPath = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Temp_Data");
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public int GetPlayerCount()
    {
        return _playerCount;
    }
    public void SetPlayerCount(int count)
    {
        _playerCount = count;
    }

    public int GetTimer()
    {
        return _timerInSeconds;
    }
    public void SetTimer(int timer)
    {
        _timerInSeconds = timer;
    }

    public string GetMovementString()
    {
        switch(_requiredMovement)
        {
            case MOVEMENT.L_HAND: { return "left hand"; }
            case MOVEMENT.R_HAND: { return "right hand"; }
            default: return "left hand";
        }
    }
    public void SetMovement(MOVEMENT movement)
    {
        _requiredMovement = movement;
    }

    public string GetDateTime()
    {
        return dateTime.ToString();
    }

    public string GetTempDataPath()
    {
        return tempDataPath;
    }
}