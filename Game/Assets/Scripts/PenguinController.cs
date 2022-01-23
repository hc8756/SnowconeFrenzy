using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinController : MonoBehaviour
{
    private Animator pAnimator;
    private Vector3 destination;
    private Vector3 start;
    public float timeLeft;
    private bool leaving;
    private GameObject bubble;

    // Start is called before the first frame update
    void Start()
    {
        //set amount of time penguin will wait
        timeLeft = 1000.0f;
        leaving = false;

        //vector that determins destination of penguin
        start = new Vector3(12, 0, 0);
        destination = new Vector3(0,0,0);
        //set starting position 
        transform.position = new Vector3(12, 0, 0);

        //set animator parameter
        pAnimator = GetComponent<Animator>();
        //bool that gets set to true when destination is reached
        pAnimator.SetBool("locReached", false);
        //bool that gets set to true when penguin leaves the screen
        pAnimator.SetBool("leftScreen", false);
        //bool that gets set to true when the penguin timer runs out
        pAnimator.SetBool("timeOut", false);
        //bool that gets set to true when the penguin gets correct ice cream
        pAnimator.SetBool("gotOrder", false);
        //int that determines what type of order the penguin wants
        pAnimator.SetInteger("orderType", 1);

        //get child bubble
        bubble = transform.GetChild(0).gameObject;

        
    }

    // Update is called once per frame
    void Update()
    {
        //if penguin is not at destination yet, move penguin to destination
        if (transform.position.x > destination.x &&!leaving)
        {
            bubble.SetActive(false);
            transform.position -= new Vector3(2 * Time.deltaTime, 0, 0);
        }
        else if (transform.position.x <= destination.x && !leaving)
        {
            pAnimator.SetBool("locReached", true);
            bubble.SetActive(true);
            //start timer
            timeLeft -= 0.1f;
            //if timer runs out
            if (timeLeft <= 0.0f)
            {
                pAnimator.SetBool("timeOut", true);
                leaving = true;
            }

            //if I press f key
            if (Input.GetKey(KeyCode.F))
            {
                pAnimator.SetBool("gotOrder", true);
                leaving = true;
                Manager.score += 1;
            }
        }

        //penguin leaves upset
        if (leaving && transform.position.x < start.x) {
            Destroy(bubble);
            transform.localScale = new Vector3(-1, 1, 1);
            transform.position += new Vector3(2 * Time.deltaTime, 0, 0);
        }
        else if (leaving && transform.position.x >= start.x) {
            pAnimator.SetBool("leftScreen", true);
        }

        
    }
}