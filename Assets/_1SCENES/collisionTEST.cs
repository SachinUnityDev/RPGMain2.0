using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionTEST : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(" hello I found the cube");
    }
}
