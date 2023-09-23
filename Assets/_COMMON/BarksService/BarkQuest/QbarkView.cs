using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Audio;
using DG.Tweening;

namespace Quest
{
    public class QbarkView : MonoBehaviour
    {
        [Header(" Interact bark Data")]
        [SerializeField] List<BarkCharData> allBarkData = new List<BarkCharData>();
        [SerializeField] AudioSource audioSourceVO;
        [SerializeField] AudioSource audioSourceUI;
        InteractEColEvents interactEColEvents;

        [Header("Curio bark Data")]
        BarkCharData barkCharData;
        AudioClip UICurioAudio;
        AudioClip BarkCurioAudio; 
        CurioColEvents curioColEvents;

        void Start()
        {
          
        }

        public void InitBark(List<BarkCharData> allBarkCharData, InteractEColEvents interactEColEvents)
        {
            this.allBarkData = allBarkCharData;
            this.interactEColEvents= interactEColEvents;
            //this is sorted for quest mode and char in Quest 
            StartCoroutine(StartBarkLine(false));
        }
        public void InitCurioBark(BarkCharData barkCharData, CurioColEvents curioColEvents
                                                                        , AudioClip AudioUI)
        {
            this.curioColEvents= curioColEvents;
            allBarkData.Clear();
            allBarkData.Add(barkCharData);
            float time = AudioUI.length;
            Sequence seqUI = DOTween.Sequence();
            seqUI
                .AppendCallback(() => PlayCurioUI(AudioUI))
                .AppendInterval(time)
                .AppendCallback(() => StartCoroutine(StartBarkLine(true)))
                ;

            seqUI.Play();
        }
        void PlayCurioUI(AudioClip audioClip)
        {
            audioSourceUI.clip= audioClip;
            audioSourceUI.Play();
        }
        IEnumerator StartBarkLine(bool isCurio)
        {
            int i = 0;
            while (i < allBarkData.Count)
            {
                // start seq 
                // get portrait charName
                float time = 0; 
                List<QRoomPortPtrEvents> portPtrEvents = transform
                                            .GetComponentsInChildren<QRoomPortPtrEvents>().ToList();
                int index =
                    portPtrEvents.FindIndex(t => t.charName == allBarkData[i].charName);
               
                    
                if (allBarkData[i].audioClip != null)
                {
                    time = allBarkData[i].audioClip.length;
                    audioSourceVO.clip = allBarkData[i].audioClip;
                    audioSourceVO.Play();
                }
                if (index != -1)
                {
                    ShowBark(index, allBarkData[i]);
                }
                else
                {
                    CloseAllBarks();
                    Debug.Log("its a squeek");
                }
                yield return new WaitForSeconds(time);               
               
                i++;
            }
            if (!isCurio)                           
               interactEColEvents.OnContinue();
            //else
                // curioColEvents.OnContinue();

                CloseAllBarks();
        }
    
        void ShowBark(int index, BarkCharData barkCharData)
        {
            CloseAllBarks();
            transform.GetChild(index).GetComponent<QBarkViewEvents>().ShowBark(barkCharData); 

        }
        void CloseAllBarks()
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<QBarkViewEvents>().CloseBark();
            }
        }
    }
}