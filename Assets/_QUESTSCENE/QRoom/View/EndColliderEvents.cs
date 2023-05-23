using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class EndColliderEvents : MonoBehaviour
    {
        public void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Collider met");
            QSceneService.Instance.qRoomView.EndArrowShow();
        }
    }
}