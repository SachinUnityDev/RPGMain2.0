using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class QuestEController : MonoBehaviour
    {
        GameObject questCanvas;
        [Header("Quest E view TBR")]
        [SerializeField]QuestEView questEView;

        InteractEColEvents questENodeColEvents; // ref to node on map that triggered the MapE 

        public List<QuestEModel> allQuestEModels = new List<QuestEModel>();
        public List<QuestEbase> allQuestEBases = new List<QuestEbase>();
        [SerializeField] int QuestBaseCount = 0;

        public QuestEFactory questEFactory;

      

        void Start()
        {
            questCanvas = GameObject.FindWithTag("QuestCanvas");          
        }


        public void ShowQuestE(InteractEColEvents questENodeCol, QuestENames questEName)
        {
         questEView =  
            Instantiate(questEView, questCanvas.transform); 
            questEView.gameObject.transform.SetParent(questCanvas.transform);
            questEView.gameObject.SetActive(true);
            this.questENodeColEvents = questENodeCol;
            QuestEModel questEModel = GetQuestEModel(questEName);
            questEView.InitEncounter(questEModel, questENodeCol);
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