using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public static int score;
    private string scoreText;
    [SerializeField] GameObject scoreTextUI;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null) {
            Debug.LogWarning("More than one manager in scene");
        }
        instance = this;
        score = 0;
        scoreText = "Shells: "+score;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText = "Shells: " + score;
        scoreTextUI.GetComponent<Text>().text=scoreText;
        
    }
}
