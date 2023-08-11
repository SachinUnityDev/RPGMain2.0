using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;


namespace Town
{

    public class WoodAnimController : MonoBehaviour
    {
        [SerializeField] GameObject woodAnim;

        [SerializeField] List<Sprite> WoodSplitSprites = new List<Sprite>();
        [SerializeField] GameObject axeSpriteGO;
        [SerializeField] Sprite woodLog;

        [SerializeField] Vector3 finalRot;
        [SerializeField] Vector3 initRot;

        WoodGameView1 woodGameView;
        Sequence axeSeq; 
        void Start()
        {
            initRot = axeSpriteGO.GetComponent<RectTransform>().rotation.eulerAngles;
        }

        public void InitAnim(WoodGameView1 woodGameView)
        {
            this.woodGameView = woodGameView;
        }
        public void StartAnim()
        {
            if (woodGameView.gameState == WoodGameState.Running)
            {
               axeSeq = DOTween.Sequence();
                axeSeq
                    .Append(axeSpriteGO.transform.DOLocalRotate(finalRot, 0.4f))
                    .Append(axeSpriteGO.transform.DOLocalRotate(initRot, 0.4f))
                    ;
                axeSeq.Play().SetLoops(100);
            }
        }
        public void StopAnim()
        {
            axeSeq.Pause();
        }

    }
}
