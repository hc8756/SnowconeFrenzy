using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    private GameObject penguin;
    private PenguinController penguinScript;
    // Start is called before the first frame update
    void Start()
    {
        penguin = transform.parent.parent.parent.gameObject;
        penguinScript = penguin.GetComponent<PenguinController>();
    }

    // Update is called once per frame
    void Update()
    {
        //update this to be relative to parent penguin location 
        transform.localScale= new Vector3(penguinScript.timeLeft*0.001f,1,1);
        transform.localPosition= new Vector3(penguinScript.timeLeft * 0.0011f-1.15f, -0.55f, 0);

    }
}
