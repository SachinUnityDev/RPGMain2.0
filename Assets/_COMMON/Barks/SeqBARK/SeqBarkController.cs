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
            seqBarkView.InitBark(seqbarkSO.allBarkData); 
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
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                ShowSeqbark(SeqBarkNames.KhalidHouse);
            }
        }
    }
}