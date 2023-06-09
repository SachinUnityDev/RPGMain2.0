using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class QRoomService : MonoSingletonGeneric<QRoomService>
    {
        public event Action<QRoomState> OnQRoomStateChg;
        public event Action<QuestNames> OnStartOfQScene;
        public event Action<QuestNames, int> OnRoomChg;


        [Header("Quest Room SO")]
        public QNodeAllRoomSO qNodeAllRoomSO; 
        public AllQNodeSO allQNodeSO;
        public QRoomView qRoomView;

        public QRoomState qRoomState;
        public QRoomController qRoomController;

        // SPRITES HERE NOT IN VIEW AS Its sprite renderer not canvas
        [Header("Sprites")]   
        public SpriteRenderer bgSprite;
        public SpriteRenderer fgSprite;
        public SpriteRenderer curio1; 
        public SpriteRenderer curio2;
        public SpriteRenderer prop; 

        private void Start()
        {
            qRoomController = GetComponent<QRoomController>();          
        }
        public void On_QuestStateChg(QRoomState qRoomState)
        {
            this.qRoomState = qRoomState;
            OnQRoomStateChg?.Invoke(qRoomState);
        }
        public void On_QuestSceneStart(QuestNames questName)
        {
            InitQRooms(questName);
            OnStartOfQScene?.Invoke(questName);
        }
        void InitQRooms(QuestNames questName)   // On 1st room Enter 
        {
            qNodeAllRoomSO = 
                      allQNodeSO.GetQuestSceneSO(questName);
            ChangeRoomSprites(questName, 1);
            qRoomController.InitQRoomController(qNodeAllRoomSO);
            On_QuestStateChg(QRoomState.Prep);
        }

        public void On_RoomChg(QuestNames questName, int roomNo)
        {
            ChangeRoomSprites(questName, roomNo);
            OnRoomChg?.Invoke(questName, roomNo); 
        }

        public void ChangeRoomSprites(QuestNames questName, int roomNo)
        {
            QNodeAllRoomSO allRoomSO = 
                    allQNodeSO.GetQuestSceneSO(questName);

            bgSprite.sprite = allRoomSO.bGSprite;
            

            bgSprite.sortingOrder = 0;
            fgSprite.sortingOrder = 5;
            QRoomSO qRoomSO = 
                        allRoomSO.GetQRoomSO(roomNo);
            prop.sprite = qRoomSO.prop;
            prop.sortingOrder = 3;
            fgSprite.sprite = qRoomSO.fgSprite;

        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                On_QuestSceneStart(QuestNames.RatInfestation);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                foreach (CharController charCtrl in CharService.Instance.charsInPlayControllers)
                {
                    if(charCtrl.charModel.charName != CharNames.Abbas_Skirmisher)
                    {
                        CharService.Instance.allCharsInPartyLocked.Add(charCtrl);
                    }
                }
            }            
        }

    }
}