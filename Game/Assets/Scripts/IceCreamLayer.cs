using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamLayer : MonoBehaviour
{

    public int collisionNum;
    // Start is called before the first frame update
    void Start()
    {
        collisionNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        gameObject.layer = 0;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "penguin")
        {
            collisionNum++;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "penguin")
        {
            collisionNum--;
        }
    }

}
