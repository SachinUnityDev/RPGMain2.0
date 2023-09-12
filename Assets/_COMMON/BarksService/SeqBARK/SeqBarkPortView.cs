using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Common;
using UnityEngine.UI;
using TMPro;

namespace Town
{
    public class SeqBarkPortView : MonoBehaviour
    {

        [Header(" Audio Source")]
        [SerializeField] AudioSource audioSourceVO;

        [SerializeField] SeqBarkData seqBarkData;
        float time = 0f;

        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteInactive;
        [Header("Bark box")]
        [SerializeField] Transform barkBox;

        private void Awake()
        {
            barkBox = transform.GetChild(2);
        }
        public void ShowBark(SeqBarkData seqBarkData)
        {
            this.seqBarkData = seqBarkData;
            FillSprites();
            FillBarktxt(); 
            barkBox.gameObject.SetActive(true);
            time = seqBarkData.audioClip.length;
            Sequence barkSeq = DOTween.Sequence();
            barkSeq
                .Append(barkBox.DOScale(1.0f, 0.2f))
                .AppendCallback(()=>PlayAudio())
                .AppendInterval(time)
                .Append(barkBox.DOScale(0f, 0.1f))
                .AppendCallback(()=>ToggleSprites(false))
                ;
            barkSeq.Play(); 
        }

        void FillBarktxt()
        {  
            barkBox.GetComponentInChildren<TextMeshProUGUI>().text = seqBarkData.str; 
        }
        void FillSprites()
        {
            Sprite[] sprites = DialogueService.Instance.
                GetDialogueSprites(seqBarkData.charName, seqBarkData.npcName); 

            spriteN = sprites[0];
            spriteInactive = sprites[1];
            transform.GetChild(0).GetComponent<Image>().sprite = spriteN;
            transform.GetChild(1).GetComponent<Image>().sprite = spriteInactive;
            ToggleSprites(true);
        }

        void ToggleSprites(bool isActive)
        {
            transform.GetChild(0).gameObject.SetActive(isActive);
            transform.GetChild(1).gameObject.SetActive(!isActive);
            
        }
        void PlayAudio()
        {
            if (seqBarkData.audioClip != null)
            {
                time = seqBarkData.audioClip.length;
                audioSourceVO.clip = seqBarkData.audioClip;
                audioSourceVO.Play();
            }
        }
      
    }
}