using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviour
{

    public GameObject collider1;
    public GameObject collider2;
    public GameObject collider3;
    public GameObject spawner;
    public GameObject prefab1;
    public GameObject prefab2;
    private Animator bAnimator;
    bool waiting = false;
    // Start is called before the first frame update
    void Start()
    {
        bAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
            if (hit2D.collider!=null)
            {
                if (hit2D.transform.gameObject == collider1) {
                    bAnimator.SetBool("coneClicked", true);
                    bAnimator.SetBool("snowClicked", false);
                    bAnimator.SetBool("syrupClicked", false);
                }
                else if (!waiting && hit2D.transform.gameObject == collider2) {
                    bAnimator.SetBool("coneClicked", false);
                    bAnimator.SetBool("snowClicked", true);
                    bAnimator.SetBool("syrupClicked", false);
                    waiting = true;

                }
                else if (waiting && hit2D.transform.gameObject == collider2)
                {
                    bAnimator.SetBool("coneClicked", false);
                    bAnimator.SetBool("snowClicked", false);
                    bAnimator.SetBool("syrupClicked", false);
                    waiting = false;
                    StartCoroutine(MakeIceCream1());

                }
                else if (hit2D.transform.gameObject == collider3) {
                    bAnimator.SetBool("coneClicked", false);
                    bAnimator.SetBool("snowClicked", false);
                    bAnimator.SetBool("syrupClicked", true);
                    waiting = false;
                    StartCoroutine(MakeIceCream2());
                    
                }
            }
        }
    }

    IEnumerator MakeIceCream1()
    {
        yield return new WaitForSeconds(1.5f);
        Instantiate(prefab1, spawner.transform.position, Quaternion.identity);
    }

    IEnumerator MakeIceCream2()
    {
        yield return new WaitForSeconds(1.8f);
        Instantiate(prefab2, spawner.transform.position, Quaternion.identity);
    }
}
