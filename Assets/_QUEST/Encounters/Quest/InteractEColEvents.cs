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
            StartInteraction();
        }

        void SetCollider()
        {
            if (interactNo == 1)
            {
                if (qRoomModel.interact1Chked)
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                else
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
            if (interactNo == 2)
            {
                if (qRoomModel.interact2Chked)
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                else
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
            if (interactNo == 3)
            {
                if (qRoomModel.interact3Chked)
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                else
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }

        void StartInteraction()
        {
            if (interactNo == 1)
            {                
                qRoomModel.interact1Chked = true;
            }
            else if (interactNo == 2)
            {
                qRoomModel.interact2Chked = true;
            }else if(interactNo == 3)
            {
                qRoomModel.interact3Chked = true;
            }


            
            QRoomService.Instance.canAbbasMove = false;
            //if (!GetChanceVal().GetChance())
            //{
            //    OnContinue();
              
            //}else
            if (interactData.questEName != QuestENames.None)
            {
                Debug.Log("Detected: Q E ");
                OnContinue();
            }
            else if(interactData.allBarks.Count != 0)
            {
                Debug.Log("Detected: BARKS");
                //List<QBarkNames> list = new List<QBarkNames>() { QBarkNames.Qbark_001, QBarkNames.Qbark_002
                //, QBarkNames.Qbark_003, QBarkNames.Qbark_006, QBarkNames.Qbark_007};
              bool result =   BarkService.Instance.qbarkController.ShowBark(interactData.allBarks, this);
                if(!result)
                    OnContinue();
            }
            else if(interactData.enemyPack != EnemyPack.None)
            {
                Debug.Log("Detected: Enemy Pack ");
                OnContinue();
            }
            else
            {
                // init trap game
                Debug.Log("Detected: Traps ");
                OnContinue();
            }
        }



        float GetChanceVal()
        {
            QuestMode questMode = QuestMissionService.Instance.currQuestMode;
            switch (questMode)
            {
                case QuestMode.None:
                    break;
                case QuestMode.Stealth:
                    return interactData.stealthChance; 
                case QuestMode.Exploration:
                    return interactData.explorationChance; 
                case QuestMode.Taunt:
                    return interactData.tauntChance;
                default:
                    return 0f;                     
            }
            return 0f;
        }

        public void OnContinue()
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            QRoomService.Instance.canAbbasMove = true;
            
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                OnContinue();   
            }
        }




    }
}