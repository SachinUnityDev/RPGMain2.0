using Common;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Quest
{
    public class QAnimController : MonoBehaviour
    {
        [Header("TBR")]
        [SerializeField] GameObject qAnimPrefab;


        [Header(" Global var")]
        [SerializeField] GameObject qAnimViewGo;
        [SerializeField] QAnimView qAnimView;

        private void Start()
        {
            QuestMissionService.Instance.OnQuestStart -= ShowQStartAnim;
            QuestMissionService.Instance.OnQuestStart += ShowQStartAnim;

            QuestMissionService.Instance.OnQuestEnd -= ShowQCompleted;
            QuestMissionService.Instance.OnQuestEnd += ShowQCompleted;

            QuestMissionService.Instance.OnObjStart -= ShowObjStart;
            QuestMissionService.Instance.OnObjStart += ShowObjStart;
        }


        public void StartAnim()
        {
            Transform parent = GameObject.FindGameObjectWithTag("Canvas").transform;
            // in 

            qAnimViewGo = Instantiate(qAnimPrefab);

            qAnimView = qAnimViewGo.GetComponent<QAnimView>();
            qAnimViewGo.transform.SetParent(parent);

            //UIControlServiceGeneral.Instance.SetMaxSiblingIndex(diaGO);
            int index = qAnimViewGo.transform.parent.childCount-2 ;
            qAnimViewGo.transform.SetSiblingIndex(index);
            RectTransform animRect = qAnimViewGo.GetComponent<RectTransform>();

            animRect.anchorMin = new Vector2(0, 0);
            animRect.anchorMax = new Vector2(1, 1);
            animRect.pivot = new Vector2(0.5f, 0.5f);
            animRect.localScale = Vector3.one;
            animRect.offsetMin = new Vector2(0, 0); // new Vector2(left, bottom);
            animRect.offsetMax = new Vector2(0, 0); // new Vector2(-right, -top);
         
        }

        void ShowQStartAnim(QuestNames questName)
        {
            StartAnim();
            qAnimView.ShowQStartAnim(questName);
        }

        void ShowQCompleted(QuestNames questName)
        {
            StartAnim();
            qAnimView.ShowQCompleted(questName);
        }

        void ShowObjStart(QuestNames questName, ObjNames objName) 
        {  
            QuestModel questModel = 
                QuestMissionService.Instance.GetQuestModel(questName);
            int netObj = questModel.allObjModel.Count; 
            for (int i = 0; i < netObj; i++)
            {
                if (questModel.allObjModel[i].objName == objName)
                {
                    if((i != 0)) 
                    {
                        StartAnim();
                        qAnimView.ShowObjStart(questName, objName);
                    }
                }
            }             
        }
    }
}