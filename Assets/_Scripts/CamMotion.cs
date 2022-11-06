using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMotion : MonoBehaviour
{    
        public GameObject camTarget;
        Vector3 newDirection;
        void LateUpdate()
        {
            newDirection = new Vector3(camTarget.transform.position.x, transform.position.y, transform.position.z);
            if (newDirection != null)
                transform.position = newDirection;

            transform.LookAt(camTarget.transform);
        }
}