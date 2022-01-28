using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerTutorial : MonoBehaviour
{
    public static ManagerTutorial instance;

    //list of penguins to spawn during the game 
    public List<GameObject> penguinList;
    [SerializeField] GameObject penguinPrefab;
    public List<Vector3> penguinLoc;
    private int penguinNum = 2;

    //UI images
    public Sprite icecream1;
    public Sprite icecream2;

    public AudioSource audioSourceG;
    public AudioSource audioSourceB;

    public static bool beginSpawn;

    // Start is called before the first frame update
    void Start()
    {
        beginSpawn = false;
        if (instance != null) {
            Debug.LogWarning("More than one tutorial manager in scene");
        }
        instance = this;

        //generate a list of penguins 
        for (int i = 0; i < penguinNum; i++) {
            int newOrder = 2-i;
            GameObject obj = Instantiate(penguinPrefab) as GameObject;
            PenguinController penguinScript = obj.GetComponent<PenguinController>();

            penguinScript.destination = penguinLoc[i];
            //Set layer order depending on y location
            if (penguinScript.destination.y > 0) { penguinScript.layer = 1; }
            else if (penguinScript.destination.y < 0) { penguinScript.layer = 0; }
            //set start position based on destination
            penguinScript.start = new Vector3(10, penguinScript.destination.y, 0);
            penguinScript.order = newOrder;
            penguinScript.bubbleImage = obj.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            if (newOrder == 1) { penguinScript.bubbleImage.GetComponent<Image>().sprite = icecream1; }
            else { penguinScript.bubbleImage.GetComponent<Image>().sprite = icecream2; }
            obj.name = "penguin" + i;
            obj.SetActive(false);
            penguinList.Add(obj);
        }

        audioSourceG.enabled = true;
        audioSourceB.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (beginSpawn) {
            if (penguinList[0] != null) { penguinList[0].SetActive(true); }
            else if (penguinList[1] != null) { penguinList[1].SetActive(true); }
            else { BearControllerTutorial.tutorialFinished = true; }
            
        }
    }

    public void PlaySound(int index) {
        if (index == 0) 
        { audioSourceG.Play(); }
        if ( index == 1)
        { audioSourceB.Play(); }
    }

}


