using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class QuestEController : MonoBehaviour
    {
        GameObject canvas;
        [Header("Quest E view TBR")]
        [SerializeField] QuestEView questEViewPrefab; 
        [SerializeField]QuestEView questEViewGO;

        InteractEColEvents questENodeColEvents; // ref to node on map that triggered the MapE 

        public List<QuestEModel> allQuestEModels = new List<QuestEModel>();
        public List<QuestEbase> allQuestEBases = new List<QuestEbase>();
        [SerializeField] int QuestBaseCount = 0;

        public QuestEFactory questEFactory;

      

        void OnEnable()
        {
            questEFactory = GetComponent<QuestEFactory>();
            
            

        }
        
        
        public void ShowQuestE(InteractEColEvents questENodeCol, QuestENames questEName)
        {
            canvas = FindObjectOfType<Canvas>().gameObject;
            questEViewGO = FindObjectOfType<QuestEView>();
            if (questEViewGO == null)
                questEViewGO =  
                    Instantiate(questEViewPrefab, canvas.transform); 
            questEViewGO.gameObject.transform.SetParent(canvas.transform);
            questEViewGO.gameObject.SetActive(true);
            this.questENodeColEvents = questENodeCol;
            QuestEModel questEModel = GetQuestEModel(questEName);
            questEViewGO.InitEncounter(questEModel, questENodeCol);
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
        public void InitQuestE(List<QuestEModel> allQuestEModels)
        {
           this.allQuestEModels = allQuestEModels.DeepClone();
            InitAllQuestEBase();
        }
        void InitAllQuestEBase()
        {
            foreach (QuestEModel questEModel in allQuestEModels)
            {
                QuestEbase questEBase = questEFactory.GetQuestEBase(questEModel.questEName);
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