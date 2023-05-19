using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndColliderEvents : MonoBehaviour
{
        
    void Start()
    {
        
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collider met");
        QSceneService.Instance.qRoomView.OnEndColliderMet(); 


    }
}

