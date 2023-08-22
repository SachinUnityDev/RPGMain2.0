using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

namespace Quest
{
    public class QAnimView : MonoBehaviour
    {
        [SerializeField] Sprite qStart;       
        [SerializeField] Sprite qCompleted;

        [SerializeField] Image centerImg;
        [SerializeField] TextMeshProUGUI centerText;
        [SerializeField] TextMeshProUGUI nameTxt; 
       

        public void ShowQStartAnim(QuestNames questName)
        {
            centerImg.sprite = qStart;
            centerText.text = "Quest Taken";
            nameTxt.text = questName.ToString().CreateSpace();
            FadeInOut(); 
        }
    
        public void ShowObjStart(QuestNames questName, ObjNames objName)
        {
            centerImg.sprite = qStart;
            centerText.text = "Quest Updated";
            nameTxt.text = objName.ToString().CreateSpace();
            FadeInOut();
        }

        public void ShowQCompleted(QuestNames questName)
        {
            centerImg.sprite = qCompleted;
            centerText.text = "Quest Completed";
            nameTxt.text = questName.ToString().CreateSpace();
            FadeInOut();
        }
        void FadeInOut()
        {
            Sequence QSeq = DOTween.Sequence();
            QSeq
                .AppendInterval(0.25f)
                .Append(centerImg.DOFade(1.0f, 0.2f))
                .Append(centerText.DOFade(1.0f, 0.2f))
                .AppendInterval(1.6f)
                .Append(centerImg.DOFade(0.0f, 0.4f))
                .Append(centerText.DOFade(0.0f, 0.4f))
                ;

            QSeq.Insert(1f, nameTxt.DOFade(1.0f, 0.2f));
            QSeq.Insert(3f, nameTxt.DOFade(0.0f, 0.25f));
            QSeq.Play().OnComplete(()=>Destroy(gameObject, 0.1f));    


        }


    }
}
