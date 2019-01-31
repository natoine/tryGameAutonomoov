using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonGoTo : MonoBehaviour
{ 
    public string scene;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GoTo);
    }
    
    void GoTo()
    {
        if(scene == "Quit")
        {
            Application.Quit();
        }
        SceneManager.LoadScene(scene);
    }
}
