using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

namespace Quest
{
    public class QWalkBtmView : MonoBehaviour
    {
        [SerializeField] QRoomPortView qRoomPortView; 
        QRoomView qRoomView;

        [SerializeField] QRoomStatsView qRoomStatsView;
        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if(GameService.Instance.gameModel.gameState == GameState.InQuestRoom)
            {
                qRoomView = FindObjectOfType<QRoomView>();
                qRoomPortView = transform.GetComponentInChildren<QRoomPortView>(true);
                qRoomStatsView = transform.GetComponentInChildren<QRoomStatsView>(true);
            }
        }
        public void QWalkInit(QRoomView qRoomView)
        {
            this.qRoomView = qRoomView;
            
            qRoomPortView.gameObject.SetActive(true);
            qRoomPortView.QPortViewInit(qRoomView);
        }
        public void OnCharHovered(CharController charController)
        {
            // show stat panel
            qRoomStatsView.gameObject.SetActive(true);  
            qRoomStatsView.StatViewInit(charController);

        }
        public void OnCharHoverExit(CharController charController)
        {
            // hide stat Panel
            qRoomStatsView.gameObject.SetActive(false);
        }

    }
}