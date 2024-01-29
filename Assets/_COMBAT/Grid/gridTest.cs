using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class gridTest : MonoBehaviour
{
    void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Movement detected ");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Movement detected ");
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Movement detected ");
    }
}
