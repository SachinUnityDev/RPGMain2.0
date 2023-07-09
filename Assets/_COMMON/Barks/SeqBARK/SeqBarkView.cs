using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Town;

namespace Common
{
    public class SeqBarkView : MonoBehaviour
    {
        //tags
        [Header(" Interact bark Data")]
        [SerializeField] List<SeqBarkData> allSeqBarkData = new List<SeqBarkData>();
        [SerializeField] AudioSource audioSourceVO;
        [SerializeField] AudioSource audioSourceUI;

        [Header("Port container")]
        Transform portContainer;

        SeqBarkController seqBarkController; 
        void Awake()
        {
            portContainer = transform.GetChild(0);
        }

        public void InitBark(List<SeqBarkData> allSeqBarkData, SeqBarkController seqBarkController)
        {
            this.allSeqBarkData = allSeqBarkData;
           this.seqBarkController= seqBarkController;
            //this is sorted for quest mode and char in Quest 
            StartCoroutine(StartBarkLine());
        }
   
        IEnumerator StartBarkLine()
        {
            int i = 0;
            while (i < allSeqBarkData.Count)
            {
                
              
                if (i % 2 == 0)
                    portContainer.GetChild(0).GetComponent<SeqBarkPortView>()
                                                    .ShowBark(allSeqBarkData[i]);

                if (i % 2 == 1)
                    portContainer.GetChild(1).GetComponent<SeqBarkPortView>()
                                                    .ShowBark(allSeqBarkData[i]);
                
                float time = allSeqBarkData[i].audioClip.length + 0.3f;// 0.3f correction for port amins

                yield return new WaitForSeconds(time);

                i++;
            }
          
            if(i == allSeqBarkData.Count)
            {
                gameObject.SetActive(false);
                OnComplete();
            }
        }

        void OnComplete()
        {
            seqBarkController.OnComplete(); 
        }

    }
}