using Common;
using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class CurioColEvents : MonoBehaviour
    {
        [SerializeField] QRoomModel qRoomModel; 

        [SerializeField] CurioModel curioModel;
        [SerializeField] CurioSO curioSO;

        [SerializeField] int curioNo; // fill in inspector
        [SerializeField] CurioNames curioName; 

        public void InitCurio(QRoomModel qRoomModel)
        {           
            this.qRoomModel = qRoomModel;
            if(curioNo == 1)            
                curioName = qRoomModel.GetCurio1Name();            
            else if(curioNo == 2)
                curioName = qRoomModel.GetCurio2Name();

            SetCollider();
            if (curioName == CurioNames.None)
            {
                gameObject.SetActive(false);
                return;
            }                
            else
                gameObject.SetActive(true);

            curioSO = CurioService.Instance.allCurioSO.GetCurioSO(curioName);
            curioModel = CurioService.Instance.curioController.GetCurioModel(curioName);
         
            SetSprite();
        }
        public void OnInteractBegin()
        {
            if(curioNo == 1)
            {
                qRoomModel.curio1Chked= true;
            }else if(curioNo == 2)
            {
                qRoomModel.curio2Chked = true;
            }
            SetSprite();
        }
        public void OnContinue()
        {         
            SetSprite();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            QRoomService.Instance.canAbbasMove= true;  
        }
        void SetCollider()
        {
            if (curioNo == 1)
            {
                if (qRoomModel.curio1Chked)
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                else
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
            if (curioNo == 2)
            {
                if (qRoomModel.curio2Chked)
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                else
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }

        void SetSprite()
        {
            if(curioNo == 1) 
            {
                if(qRoomModel.curio1Chked)
                    gameObject.GetComponent<SpriteRenderer>().sprite = curioSO.curioOpn;
                else
                    gameObject.GetComponent<SpriteRenderer>().sprite = curioSO.curioN;
            }
            if (curioNo == 2)
            {
                if (qRoomModel.curio2Chked)
                    gameObject.GetComponent<SpriteRenderer>().sprite = curioSO.curioOpn;
                else
                    gameObject.GetComponent<SpriteRenderer>().sprite = curioSO.curioN;
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = curioSO.curioHL;
                CurioService.Instance.curioView.InitCurioView(this, curioModel, curioNo);
            }            
        }
    }
}