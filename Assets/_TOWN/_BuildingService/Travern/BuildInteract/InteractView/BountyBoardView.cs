using Common;
using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{
    public class BountyBoardView : MonoBehaviour, IPanel
    {
        [SerializeField] Button exitBtn;
        [SerializeField] Transform bountyContainer;      

        private void Awake()
        {
            exitBtn.onClick.AddListener(OnExitBtnPressed); 
        }
        void OnExitBtnPressed()
        {
            UnLoad();
        }

        public void InitBountyBoardLs()
        {
            int i = 0; 
            foreach (QuestModel model in QuestMissionService.Instance
                                            .GetQModelsOfType(QuestType.Bounty))
            {   
                if (model.isUnBoxed && model.questState == QuestState.Locked)
                {
                    bountyContainer.GetChild(i).gameObject.SetActive(true);
                    bountyContainer.GetChild(i).GetComponent<BountyQPtrEvents>()
                        .InitBountyQ(model, this);
                    bountyContainer.GetChild(i).gameObject.SetActive(true);
                    i++; 
                }
            }
            for (int j = i; j < bountyContainer.childCount; j++)
            {
                bountyContainer.GetChild(j).gameObject.SetActive(false);
            }
        }



        public void Init()
        {
            InitBountyBoardLs();
        }

        public void Load()
        {
            InitBountyBoardLs();
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }
    }
}