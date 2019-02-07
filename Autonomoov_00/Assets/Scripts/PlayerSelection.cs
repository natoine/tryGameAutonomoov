using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    public BodySourceView _BodyView;

    private void OnEnable()
    {
        Time.timeScale = 0.0f;
        _BodyView.EnableSkeletonView();
    }

    private void OnDisable()
    {
        _BodyView.DisableSkeletonView();
    }
}
