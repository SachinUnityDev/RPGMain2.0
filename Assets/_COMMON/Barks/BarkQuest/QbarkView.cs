using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Audio;
namespace Quest
{
    public class QbarkView : MonoBehaviour
    {
        [SerializeField] List<BarkCharData> allBarkData = new List<BarkCharData>();
        AudioSource audioSource;
        InteractEColEvents interactEColEvents; 
        void Start()
        {
            audioSource= GetComponent<AudioSource>();
        }

        public void InitBark(List<BarkCharData> allBarkCharData, InteractEColEvents interactEColEvents)
        {
            this.allBarkData = allBarkCharData;
            this.interactEColEvents= interactEColEvents;
            //this is sorted for quest mode and char in Quest 
            StartCoroutine(StartBarkLine());
        }


        IEnumerator StartBarkLine()
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
                    audioSource.clip = allBarkData[i].audioClip;
                    audioSource.Play();
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
            interactEColEvents.OnContinue();
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