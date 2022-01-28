using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one manager in scene");
        }
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToMenu() {
        SceneManager.LoadScene("Menu");
    }
    public void ToGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void ToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
