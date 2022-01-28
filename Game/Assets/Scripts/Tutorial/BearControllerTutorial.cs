using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearControllerTutorial : MonoBehaviour
{

    public GameObject collider1;
    public GameObject collider2;
    public GameObject collider3;
    public GameObject spawner;
    public GameObject prefab1;
    public GameObject prefab2;
    private Animator bAnimator;
    private bool waiting = false;
    public static bool canPressAgain = false;
    public GameObject[] tutorialUI;

    bool firstIceCream = true;
    public static bool tutorialFinished = false;

    
    // Start is called before the first frame update
    void Awake()
    {
        bAnimator = GetComponent<Animator>();
        
        //initially collider2 and 3 are disabled
        collider1.SetActive(true);
        collider2.SetActive(false);
        collider3.SetActive(false);

        foreach (GameObject ui in tutorialUI)
        {

            ui.SetActive(false);
        }
        tutorialUI[0].SetActive(true);
        canPressAgain = false;
        firstIceCream = true;
        waiting = false;
        tutorialFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPressAgain && firstIceCream)
            {
                tutorialUI[1].SetActive(false);
                tutorialUI[2].SetActive(true);
            }
        if (tutorialFinished) {
            tutorialUI[9].SetActive(true);
        }
        if (Input.GetMouseButtonDown(0)) {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
            if (hit2D.collider!=null)
            {
                if (hit2D.transform.gameObject == collider1) {
                    bAnimator.SetBool("coneClicked", true);
                    bAnimator.SetBool("snowClicked", false);
                    bAnimator.SetBool("syrupClicked", false);

                    collider1.SetActive(false);
                    collider2.SetActive(true);
                    collider3.SetActive(false);

                    tutorialUI[0].SetActive(false);
                    tutorialUI[1].SetActive(true);
                    tutorialUI[5].SetActive(false);

                }
                else if (!waiting && hit2D.transform.gameObject == collider2) {
                    bAnimator.SetBool("coneClicked", false);
                    bAnimator.SetBool("snowClicked", true);
                    bAnimator.SetBool("syrupClicked", false);
                    waiting = true;
                    canPressAgain = false;
                    if (firstIceCream)
                    {
                        collider1.SetActive(false);
                        collider2.SetActive(true);
                        collider3.SetActive(false);
                    }
                    else {
                        collider1.SetActive(false);
                        collider2.SetActive(false);
                        collider3.SetActive(true);
                    }
                    tutorialUI[1].SetActive(false);
                    if (!firstIceCream) { StartCoroutine(ActivateLater(tutorialUI[3], 1.0f)); }
                    
                }
                //You have to wait for snow animation to finish before you can click again
                else if (waiting && canPressAgain && hit2D.transform.gameObject == collider2)
                {
                    bAnimator.SetBool("coneClicked", false);
                    bAnimator.SetBool("snowClicked", false);
                    bAnimator.SetBool("syrupClicked", false);
                    waiting = false;
                    canPressAgain = false;
                    
                    StartCoroutine(MakeIceCream1());

                    collider1.SetActive(false);
                    collider2.SetActive(false);
                    collider3.SetActive(false);

                    
                    tutorialUI[2].SetActive(false);
                    if (firstIceCream) { 
                        StartCoroutine(ActivateLater(tutorialUI[5],1.0f)); 
                        StartCoroutine(DestroyLater(tutorialUI[5],3.0f)); }
                    firstIceCream = false;
                    StartCoroutine(ActivateLater(collider1, 3.0f));
                    StartCoroutine(ActivateLater(tutorialUI[0],3.0f));

                }
                else if (hit2D.transform.gameObject == collider3) {
                    bAnimator.SetBool("coneClicked", false);
                    bAnimator.SetBool("snowClicked", false);
                    bAnimator.SetBool("syrupClicked", true);
                    waiting = false;
                    StartCoroutine(MakeIceCream2());

                    collider1.SetActive(false);
                    collider2.SetActive(false);
                    collider3.SetActive(false);
                    tutorialUI[3].SetActive(false);
                    StartCoroutine(ActivateLater(tutorialUI[6],2.0f));
                    StartCoroutine(DestroyLater(tutorialUI[6], 4.0f));
                    StartCoroutine(ActivateLater(tutorialUI[7], 5.0f));
                    StartCoroutine(DestroyLater(tutorialUI[7], 7.0f));
                    StartCoroutine(ActivateLater(tutorialUI[8], 8.0f));
                    StartCoroutine(DestroyLater(tutorialUI[8], 12.0f));
                    StartCoroutine(ActivateLater(tutorialUI[4], 14.0f));
                    StartCoroutine(DestroyLater(tutorialUI[4], 17.0f));
                    StartCoroutine(PenguinSpawn(12.0f));
                }
            }
        }
    }

    IEnumerator MakeIceCream1()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(prefab1, spawner.transform.position, Quaternion.identity);
    }
    IEnumerator MakeIceCream2()
    {
        yield return new WaitForSeconds(1.2f);
        Instantiate(prefab2, spawner.transform.position, Quaternion.identity);
    }

    IEnumerator ActivateLater(GameObject ui, float time)
    {
        yield return new WaitForSeconds(time);
        ui.SetActive(true);
    }

    IEnumerator DestroyLater(GameObject ui,float time)
    {
        yield return new WaitForSeconds(time);
        ui.SetActive(false);
    }
    IEnumerator PenguinSpawn(float time)
    {
        yield return new WaitForSeconds(time);
        ManagerTutorial.beginSpawn = true;
    }
}
