using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Quest
{
    public class QRoomPortView : MonoBehaviour
    {
        QRoomView qRoomView;
        void OnEnable()
        {
            CharService.Instance.OnCharFleeCombat +=(CharController charController)=> FillPort(); 
        }
     
        public void QPortViewInit(QRoomView qRoomView)
        {
            this.qRoomView= qRoomView;
            // get the char in party from charService
            FillPort();     

        }
        void FillPort()
        {
           
            for (int i = 0; i < transform.childCount; i++)
            {
                 if(i < CharService.Instance.allCharsInPartyLocked.Count)
                 {  
                    transform.GetChild(i).gameObject.SetActive(true);
                    transform.GetChild(i).GetComponent<QRoomPortPtrEvents>()
                                 .InitPort(qRoomView, CharService.Instance.allCharsInPartyLocked[i]);                       
                 }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }   
            }
        }
    }
}