using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonGoTo : MonoBehaviour
{ 
    public Object scene;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GoTo);
    }
    
    void GoTo()
    {
        if(name == "Quit")
        {
            Application.Quit();
        }
        SceneManager.LoadScene(scene.name);
    }
}
