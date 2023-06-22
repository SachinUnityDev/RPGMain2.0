using DG.Tweening;
using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{


    public class QuestEController : MonoBehaviour
    {
        GameObject canvas; 
        [Header("Quest E view TBR")]
        [SerializeField] GameObject questEViewObj;
         public InteractEColEvents questENodeColEvents; // ref to node on map that triggered the MapE 

        public List<QuestEModel> allQuestEModels = new List<QuestEModel>();
        public List<QuestEbase> allQuestEBases = new List<QuestEbase>();
        [SerializeField] int QuestBaseCount = 0;

        public QuestEFactory questEFactory;

      

        void Start()
        {
            canvas = GameObject.FindWithTag("QuestCanvas"); 
        }


        public void ShowQuestE(InteractEColEvents questENodeCol, QuestENames questEName)
        {
            // get your prefab spawn and make child in the current canvas
            GameObject questEObj = 
                        Instantiate(questEViewObj, new Vector3(0, 0, 0), Quaternion.identity);
            questEObj.transform.SetParent(canvas.transform, true); 
            
            RectTransform rectTransform = questEObj.GetComponent<RectTransform>();  

            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.DOScale(1, 0); 
            // Set the position and size of the object within the canvas
            rectTransform.anchoredPosition = new Vector2(0, 0);
            rectTransform.sizeDelta = new Vector2(960, 540);

            this.questENodeColEvents = questENodeCol;            
            QuestEModel questEModel = GetQuestEModel(questEName);
            questEViewObj.GetComponent<QuestEView>().InitEncounter(questEModel);
        }

        public void InitQuestE(AllQuestESO allQuestESO)
        {
            foreach (QuestESO questESO in allQuestESO.allQuestESO)
            {
                QuestEModel questEModel = new QuestEModel(questESO);
                allQuestEModels.Add(questEModel);
            }
            InitAllQuestEBase();
        }

        void InitAllQuestEBase()
        {
            foreach (QuestEModel questEModel in allQuestEModels)
            {
                QuestEbase questEBase = EncounterService.Instance.questEFactory.GetQuestEBase(questEModel.questEName);
                questEBase.QuestEInit(questEModel);
                allQuestEBases.Add(questEBase);
            }
            QuestBaseCount = allQuestEBases.Count;
        }

        public QuestEModel GetQuestEModel(QuestENames questENames)
        {
            int index = allQuestEModels.FindIndex(t => t.questEName == questENames);
            if (index != -1)
                return allQuestEModels[index];
            else
                Debug.Log("Quest E not found" + questENames);
            return null;
        }

        public QuestEbase GetQuestEBase(QuestENames questEName)
        {
            int index = allQuestEBases.FindIndex(t => t.questEName == questEName);
            if (index != -1)
                return allQuestEBases[index];
            else
                Debug.Log("Quest EBase not found" + questEName);
            return null;
        }



    }
}