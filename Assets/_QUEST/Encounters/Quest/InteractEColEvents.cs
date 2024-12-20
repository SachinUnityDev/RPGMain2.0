using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using Common;

namespace Quest
{

    public class InteractEColEvents : MonoBehaviour
    {
        /// <summary>
        /// Barks
        /// Combat 
        /// QuestECounter
        /// Trap 
        /// </summary>
        [SerializeField] QRoomModel qRoomModel;
        [SerializeField] QRoomInteractData interactData;
        [SerializeField] int interactNo;
        // Start is called before the first frame update
        [SerializeField] bool hasInteraction = false; 

        void Start()
        {

        }
        public void InitInteract(QRoomModel qRoomModel)
        {
            this.qRoomModel= qRoomModel;
            interactData = qRoomModel.GetInteractData(interactNo);            
            SetCollider();
        }        
        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                StartInteraction();
            }
        }

        void SetCollider()
        {
            if (interactNo == 1)
            {
                if (qRoomModel.interact1Chked || !qRoomModel.interact1Data.HasInteraction())
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                else
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
            if (interactNo == 2)
            {
                if (qRoomModel.interact2Chked || !qRoomModel.interact2Data.HasInteraction())
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                else
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
            if (interactNo == 3)
            {
                if (qRoomModel.interact3Chked || !qRoomModel.interact3Data.HasInteraction())
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                else
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }

        void StartInteraction()
        {
            QRoomBase qRoomBase = QRoomService.Instance.qRoomController.GetQRoomBase(qRoomModel); 
            if(qRoomBase != null)
            {
                if (interactNo == 1)
                {
                    if (qRoomModel.interact1Data.HasCombat())
                    {
                        qRoomBase.Trigger1();
                    }                        
                    else
                    {
                        qRoomModel.interact1Chked = true;
                        OnContinue();
                    }
                }                    
                if (interactNo == 2)
                {
                    if (qRoomModel.interact2Data.HasCombat())
                    {
                        qRoomBase.Trigger2();
                    }
                    else
                    {
                        qRoomModel.interact2Chked = true;
                        OnContinue();
                    }                    
                }                    
                if (interactNo == 3)
                {
                    if (qRoomModel.interact3Data.HasCombat())
                    {
                        qRoomBase.Trigger3();
                    }
                    else
                    {
                        qRoomModel.interact3Chked = true;
                        OnContinue();
                    }
                }                    
            }
            else
            {
                Debug.LogError("QBase is null" +qRoomModel.questName);
            }
            //if (interactData.questEName != QuestENames.None)
            //{
            //    Debug.Log("Detected: Q E ");
            //    EncounterService.Instance.questEController.ShowQuestE(this, interactData.questEName);
            //    OnPosChked();
            //}
            //else if(interactData.allBarks.Count != 0)
            //{
            //    Debug.Log("Detected: BARKS" + interactData.allBarks[0]);

            //   BarkService.Instance.qbarkController.ShowBark(interactData.allBarks, this);
            //    OnPosChked();               
            //    OnContinue();
            //}
            //else if(interactData.enemyPack != EnemyPackName.None)
            //{
            //    Debug.Log("Detected: Enemy Pack ");


            //    OnPosChked();
            //    OnContinue();
            //}
            //else if(interactData.trapNames != Traps.None)
            //{
            //    // init trap game
            //    Debug.Log("Detected: Traps ");
            //    MGService.Instance.trapMGController.InitGame(this); 
            //    OnPosChked();
            //}
            //else
            //{

            //}
        }

        public void OnContinue()
        {   
           StartCoroutine(WaitForSec());
            

        }
        IEnumerator WaitForSec()
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(1);          
            QRoomService.Instance.canAbbasMove = true;
        }

        //void OnPosChked()
        //{
        //    QRoomService.Instance.canAbbasMove = false;
        //    if (interactNo == 1)
        //    {
        //        qRoomModel.interact1Chked = true;
        //    }
        //    else if (interactNo == 2)
        //    {
        //        qRoomModel.interact2Chked = true;
        //    }
        //    else if (interactNo == 3)
        //    {
        //        qRoomModel.interact3Chked = true;
        //    }
        //}
   
    }
}

//float GetChanceVal()
//{
//    QuestMode questMode = QuestMissionService.Instance.currQuestMode;
//    switch (questMode)
//    {
//        case QuestMode.None:
//            break;
//        case QuestMode.Stealth:
//            return interactData.stealthChance; 
//        case QuestMode.Exploration:
//            return interactData.explorationChance; 
//        case QuestMode.Taunt:
//            return interactData.tauntChance;
//        default:
//            return 0f;                     
//    }
//    return 0f;
//}