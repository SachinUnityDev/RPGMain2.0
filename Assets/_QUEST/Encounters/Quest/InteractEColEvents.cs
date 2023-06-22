using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

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
        }
        public void OnTriggerEnter2D(Collider2D collision)
        {
            StartInteraction();

        }        
        
        void StartInteraction()
        {
            if (!GetChanceVal().GetChance()) return;
            QRoomService.Instance.canAbbasMove = false;
            if(interactData.questEName != QuestENames.None)
            {
                Debug.Log("Detected: Q E "); 
            }
            else if(interactData.allBarks.Count != 0)
            {
                Debug.Log("Detected: BARKS");
            }
            else if(interactData.enemyPack != EnemyPack.None)
            {
                Debug.Log("Detected: Enemy Pack ");
            }
            else
            {
                // init trap game
                Debug.Log("Detected: Traps ");
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
            QRoomService.Instance.canAbbasMove = true;

        }

    
        

    }
}