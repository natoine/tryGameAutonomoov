using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent (typeof(Button))]
public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject PlayersSelection;
    [SerializeField] GameObject GameScreen;
    void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(Pause);
    }
   

    public void Pause()
    {
        PlayersSelection.SetActive(true);
        GameScreen.SetActive(false);
        Time.timeScale = 0;
    }
}
