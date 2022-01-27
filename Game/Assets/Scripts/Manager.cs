using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public static int score;
    private float timeLeft=6000.0f;
    private float timeLeftSimple;
    private string uiText;
    [SerializeField] GameObject uiTextUI;

    //list of penguins to spawn during the game 
    public List<GameObject> penguinList;
    [SerializeField] GameObject penguinPrefab;
    public List<Vector3> penguinLoc;
    private int penguinNum = 50;

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
        timeLeftSimple = timeLeft / 100.0f;
        uiText = "Cones Sold: "+score + " | Time left: " + timeLeftSimple.ToString("0");
       
        //generate a list of penguins 
        for (int i = 0; i < penguinNum; i++) {
            int newOrder = Random.Range(1, 3);
            GameObject obj = Instantiate(penguinPrefab) as GameObject; 
            PenguinController penguinScript = obj.GetComponent<PenguinController>();

            penguinScript.destination = penguinLoc[i%4];  
            //Set layer order depending on y location
            if (penguinScript.destination.y > 0) { penguinScript.layer = 1; }
            else if (penguinScript.destination.y < 0) { penguinScript.layer = 0; }
            //set start position based on destination
            penguinScript.start = new Vector3(10, penguinScript.destination.y, 0);
            penguinScript.order = newOrder;
            penguinScript.bubbleImage= obj.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            if (newOrder == 1) {penguinScript.bubbleImage.GetComponent<Image>().sprite = icecream1; }
            else {penguinScript.bubbleImage.GetComponent<Image>().sprite = icecream2; }
            obj.name = "penguin" + i;
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
        timeLeftSimple = timeLeft / 100.0f;
        uiText = "Cones Sold: " + score + " | Time left: " + timeLeftSimple.ToString("0") +" seconds";
        uiTextUI.GetComponent<Text>().text=uiText;
        if (timeLeft <= 0) {
            if (score >= 20)
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
        for (int i = 0; i < penguinNum; i++)
        {   
            yield return new WaitForSeconds(2.0f);
            GameObject[] activePenguins = GameObject.FindGameObjectsWithTag("penguin");
            if (activePenguins.Length <4 ) { penguinList[i].SetActive(true); }
            else { i = i - 1; }
            
        }
    }
}


