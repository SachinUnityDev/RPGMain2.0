using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro; 
public class WoodAnimController : MonoBehaviour
{
    [SerializeField] GameObject woodAnim; 

    [SerializeField] List<Sprite> WoodSplitSprites = new List<Sprite>();
    [SerializeField] GameObject axeSpriteGO;
    [SerializeField] Sprite woodLog;

    [SerializeField] Vector3 finalRot;
    [SerializeField] Vector3 initRot; 
    void Start()
    {
        initRot = axeSpriteGO.GetComponent<RectTransform>().rotation.eulerAngles; 
    }

    public void StartAnim()
    {
        if (WoodGameService.Instance.gameState == WoodGameState.Running)
        {
            Debug.Log("TRAP " + initRot);
            Sequence axeSeq = DOTween.Sequence();
            axeSeq
                .Append(axeSpriteGO.transform.DOLocalRotate(finalRot, 0.6f))
                .Append(axeSpriteGO.transform.DOLocalRotate(initRot, 0.6f))                
                ;
            axeSeq.Play().SetLoops(100); 
        }
    }

    private void Update()
    {
       
    }


   
    }
