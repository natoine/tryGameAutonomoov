using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOVEMENT
{
    R_HAND,
    L_HAND
}

public class GameParameters : MonoBehaviour
{
    public static GameParameters instance;

    private int _playerCount = 0;
    private int _timerInSeconds = 0;
    private MOVEMENT _requiredMovement = MOVEMENT.L_HAND;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
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

    public MOVEMENT GetMovement()
    {
        return _requiredMovement;
    }
    public void SetMovement(MOVEMENT movement)
    {
        _requiredMovement = movement;
    }
}