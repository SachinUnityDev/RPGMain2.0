using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurioColEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ENtered collider"); 
    }

}
