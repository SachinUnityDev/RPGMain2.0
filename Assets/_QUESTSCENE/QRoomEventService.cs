using Combat;
using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Town;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Quest
{
    public class QRoomEventService :  MonoSingletonGeneric<QRoomEventService>
    {
        
        public Action OnStartOFQRoom;
        public Action OnEndOFQRoom;

        [Header("Quest and I result Ref")]
        public QuestNames questName;
        public iResult iResult;

        [Header("Current Result")]
        public Result currQRoomResult;

        void OnEnable()
        {
            SceneManager.activeSceneChanged -= OnSceneChange;
            SceneManager.activeSceneChanged += OnSceneChange;
        }

        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnSceneChange;
            SceneManager.activeSceneChanged -= SceneChg;
        }

        void OnSceneChange(Scene current, Scene next)
        {
            if (next.name == GameScene.QUEST.ToString())
            {
                QRoomEventStart();
            }
        }   

        public void OnQRoomStart(QuestNames questName, iResult iResult)
        {
            this.questName = questName;
            this.iResult = iResult;
            SceneMgmtService.Instance.LoadGameScene(GameScene.QUEST);

        }

        public void QRoomEventStart()
        {
            QRoomService.Instance.On_QRoomSceneStart(questName); 
        }

        public void On_EndOfQRoom(Result combatResult)
        {               
            //foreach (CharController c in CharService.Instance.charsInPlayControllers.ToList())
            //{
            //    if (c == null)
            //    {
            //        CharService.Instance.charsInPlayControllers.Remove(c); continue;
            //    }
            //    if (c.charModel.orgCharMode == CharMode.Enemy)
            //    {
            //        CharService.Instance.charsInPlayControllers.Remove(c);
            //    }
            //}
           
            try { OnEndOFQRoom?.Invoke(); }
            catch (Exception e)
            {
                Debug.Log("EXCEPTION OCCURED111   QUEST ENDS!!!!" + e.Message);
                Debug.Log(e.StackTrace); // Log the stack trace of the exception
            }
        }
        public void OnQRoomEndClicked()
        {
            SceneMgmtService.Instance.LoadGameScene(iResult.gameScene);
            iResult.OnResultClicked(currQRoomResult);

            SceneManager.activeSceneChanged += SceneChg;
        }
        void SceneChg(Scene oldScene, Scene newScene)
        {
            if (newScene.name == iResult.gameScene.GetMainGameScene().ToString())
            {
                iResult.OnResult_AfterSceneLoad(currQRoomResult);
                SceneManager.activeSceneChanged -= SceneChg;
            }
        }
    }
}

