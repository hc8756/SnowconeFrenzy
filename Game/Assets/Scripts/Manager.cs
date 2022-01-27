using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public static int score;
    private float timeLeft=5000.0f;
    private string scoreText;
    [SerializeField] GameObject scoreTextUI;

    //list of penguins to spawn during the game 
    public List<GameObject> penguinList;
    [SerializeField] GameObject penguinPrefab;
    private int penguinNum = 10;

    //UI images
    public Sprite icecream1;
    public Sprite icecream2;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null) {
            Debug.LogWarning("More than one manager in scene");
        }
        instance = this;
        score = 0;
        scoreText = "Shells: "+score + " | Time left: " + timeLeft.ToString("0.0");

        //generate a list of penguins 
        for (int i = 0; i < penguinNum; i++) {
            int newX = Random.Range(0,8);
            int newY = Random.Range(-3,1);
            int newOrder = Random.Range(1, 3);
            GameObject obj = Instantiate(penguinPrefab) as GameObject; 
            PenguinController penguinScript = obj.GetComponent<PenguinController>();
            penguinScript.start = new Vector3(12, newY, 0);
            penguinScript.destination = new Vector3(newX, newY, 0);
            penguinScript.order = newOrder;
            penguinScript.bubbleImage= obj.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            if (newOrder == 1) {penguinScript.bubbleImage.GetComponent<Image>().sprite = icecream1; }
            else {penguinScript.bubbleImage.GetComponent<Image>().sprite = icecream2; }
            obj.SetActive(false);
            penguinList.Add(obj);
        }

        //spawn them
        StartCoroutine("SpawnPenguins");
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft = timeLeft - 0.1f;
        scoreText = "Shells: " + score + " | Time left: " + timeLeft.ToString("0.0");
        scoreTextUI.GetComponent<Text>().text=scoreText;
        if (timeLeft <= 0) {
            if (score >= 100)
            {
                SceneManager.LoadScene("WinScene");
            }
            else {
                SceneManager.LoadScene("LoseScene");
            }
        }
    }
    IEnumerator SpawnPenguins()
    {
        for (int i=0;i<penguinNum;i++)
        {
            yield return new WaitForSeconds(5.0f);
            penguinList[i].SetActive(true);
        }
    }
}

