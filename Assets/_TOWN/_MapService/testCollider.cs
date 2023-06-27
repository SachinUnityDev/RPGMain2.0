using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{


    public class testCollider : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("COLLISION2D 2");
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("TRIGGER 2");
        }
        void Start()
        {

        }

    }
}