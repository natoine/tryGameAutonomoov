using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour
{
    float _RemainingTime = 0;
    void Start()
    {
        _RemainingTime = GameParameters.instance.GetTimer();
    }

    void Update()
    {
        _RemainingTime -= Time.deltaTime;
        GetComponent<Text>().text = string.Format("{0:00}:{1:00}:{2:00}", (int)_RemainingTime / 3600, (int)_RemainingTime / 60, (int)_RemainingTime % 60);
    }

    public float RemainingTime()
    {
        return _RemainingTime;
    }
}
