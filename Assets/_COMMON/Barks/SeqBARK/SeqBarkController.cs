using Common;
using Quest;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;


namespace Town
{


    public class SeqBarkController : MonoBehaviour
    {       
        public List<SeqbarkModel> allSeqBarkModel = new List<SeqbarkModel>();

        public SeqBarkView seqBarkView;
        SeqBarkNames seqBarkName; 
        void Start()
        {
            seqBarkView.gameObject.SetActive(false);    
        }

        public void InitBarkController()
        {
            foreach (SeqBarkSO seq in BarkService.Instance.allBarkSO.allSeqbarkSO.allSeqBark)
            {
                SeqbarkModel seqBarkModel = new SeqbarkModel(seq); 
                allSeqBarkModel.Add(seqBarkModel);
            }
        }

        public void ShowSeqbark(SeqBarkNames barkNames)
        {
            // get bark SO and 
            //get bark Model
            seqBarkView.gameObject.SetActive(true); 
            SeqBarkSO seqbarkSO  = 
                        BarkService.Instance.allBarkSO.allSeqbarkSO.GetBarkSeqSO(barkNames);
            SeqbarkModel seqbarkModel = GetSeqBarkModel(barkNames);
            seqBarkName = barkNames;
            seqBarkView.InitBark(seqbarkSO.allBarkData, this); 
            seqbarkModel.isLocked = true;

        }
        public SeqbarkModel GetSeqBarkModel(SeqBarkNames seqbarkNames)
        {
            int index = allSeqBarkModel.FindIndex(t=>t.seqbarkName== seqbarkNames);    
            if(index  != -1)
            {
                return allSeqBarkModel[index];
            }
            else
            {
                Debug.Log("Seq Bark Model not found" + seqbarkNames);
            }
            return null; 
        }

        public void OnComplete()
        {
            switch (seqBarkName)
            {
                case SeqBarkNames.None:
                    break;
                case SeqBarkNames.KhalidHouse:
                    BuildingIntService.Instance.houseController.houseView
                                    .GetComponent<IPanel>().Init();
                    WelcomeService.Instance.welcomeView.RevealWelcomeTxt("Click the portrait to talk with Khalid");
                    QuestMissionService.Instance.On_QuestStart(QuestNames.LostMemory);                    
                    break;
                case SeqBarkNames.TavernIntro:
                    break;
                default:
                    break;
            }
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                ShowSeqbark(SeqBarkNames.KhalidHouse);
            }
        }
    }
}