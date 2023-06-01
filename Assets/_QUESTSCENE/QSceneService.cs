using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;


namespace Quest
{
    public class QSceneService : MonoSingletonGeneric<QSceneService>
    {

        public event Action<QRoomState> OnQRoomStateChg;
        public event Action<QuestNames> OnStartOfQScene; 

        [Header("Quest Room SO")]
        public AllQNodeSO allQNodeSO;
        public QRoomView qRoomView;

        public QRoomState qRoomState;
        public QRoomController qRoomController;

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
        void InitQRooms(QuestNames questName)
        {
            QNodeAllRoomSO qNodeAllRoomSO = 
            allQNodeSO.GetQuestSceneSO(questName);
            ChangeRoomSprites(questName, 1);
            qRoomController.InitQRoomController(qNodeAllRoomSO);
            On_QuestStateChg(QRoomState.Prep);
        }

        public void ChangeRoomSprites(QuestNames questName, int roomNo)
        {
            QNodeAllRoomSO allRoomSO = 
                    allQNodeSO.GetQuestSceneSO(questName);

            bgSprite.sprite = allRoomSO.bGSprite;
            fgSprite.sprite = allRoomSO.fGSprite;

            bgSprite.sortingOrder = 0;
            fgSprite.sortingOrder = 4;
            QRoomSO qRoomSO = 
                        allRoomSO.GetQRoomSO(roomNo);
            prop.sprite = qRoomSO.prop;
            prop.sortingOrder = 3;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                On_QuestSceneStart(QuestNames.RatInfestation);
            }
        }

    }
}