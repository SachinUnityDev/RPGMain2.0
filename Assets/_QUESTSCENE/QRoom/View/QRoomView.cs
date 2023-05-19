using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Quest
{
    public class QRoomView : MonoBehaviour
    {

        [Header("ROOM END REF TBR")]
        [SerializeField] QRoomEndArrowW arrowW;
        [SerializeField] QRoomEndArrowS arrowS; 
        [SerializeField] Transform endCollider;

        

        void Start()
        {           
            arrowS.GetComponent<Image>().DOFade(0, 0.05f);
            arrowW.GetComponent<Image>().DOFade(0, 0.05f);
        }

        public void OnEndColliderMet()
        {
            arrowS.GetComponent<Image>().DOFade(1, 0.1f);
            arrowW.GetComponent<Image>().DOFade(1, 0.1f);
        }


    }
}