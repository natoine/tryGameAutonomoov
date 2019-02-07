using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LauchGame : MonoBehaviour
{
    public PlayerSelection _SelectionScreen;
    public GameObject _GameScene;
    void Start()
    {
        GetComponent<Button>().interactable = false;
        GetComponent<Button>().onClick.AddListener(delegate
        {
            _SelectionScreen.gameObject.SetActive(false);
            _GameScene.SetActive(true);
            Time.timeScale = 1.0f;
        });
    }
}
