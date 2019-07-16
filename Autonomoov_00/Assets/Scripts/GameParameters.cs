using System;
using UnityEngine;

public enum MOVEMENT
{
    R_HAND,
    L_HAND
}

[Serializable]
public class GameParameters : MonoBehaviour
{
    public static GameParameters instance;
    public string tempDataPath;
    public int playersNb = 0;
    public int timer = 0;
    public string movement;
    private MOVEMENT requiredMovement;
    private DateTime dateTime = DateTime.UtcNow;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        requiredMovement = (MOVEMENT)Enum.Parse(typeof(MOVEMENT), movement);
    }

    public int GetPlayerCount()
    {
        return playersNb;
    }
    public void SetPlayerCount(int count)
    {
        playersNb = count;
    }

    public int GetTimer()
    {
        return timer;
    }
    public void SetTimer(int timer)
    {
        this.timer = timer;
    }

    public string GetMovementString()
    {
        return requiredMovement.ToString();
        /*switch (requiredMovement)
        {
            case MOVEMENT.L_HAND: { return "L_HAND"; }
            case MOVEMENT.R_HAND: { return "R_HAND"; }
            default: return "L_HAND";
        }*/
    }
    public void SetMovement(MOVEMENT movement)
    {
        requiredMovement = movement;
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