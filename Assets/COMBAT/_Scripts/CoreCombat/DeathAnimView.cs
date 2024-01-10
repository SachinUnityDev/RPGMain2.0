using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Common;
using Spine.Unity;
using UnityEngine.UI;
using System;

namespace Combat
{
    public class DeathAnimView : MonoBehaviour
    {
        [Header(" Death Goddess prefab TBR")]
        [SerializeField] GameObject deathGoddess;

        [Header(" global var")]
        [SerializeField] float intermediateSize = 0.85f;
        [SerializeField] float finalSize = 1.2f;
        GameObject deathGoddessGO;
        CharController prevCharController; 
        
        void Start()
        {
            DOTween.defaultRecyclable = true;
            CharService.Instance.OnCharDeath -= PlayDeathAnim;
            CharService.Instance.OnCharDeath += PlayDeathAnim;
        }
        private void OnDisable()
        {
            CharService.Instance.OnCharDeath -= PlayDeathAnim;
        }

        public void Add2DeathAnimLs(CharController charController)
        {

        }

        public void PlayDeathAnim(CharController charController)
        {
                SkeletonAnimation skeletonAnim = charController.gameObject.GetComponentInChildren<SkeletonAnimation>();
                if (charController == prevCharController)
                {
                    Debug.Log("DOUBLE PLAY ATTEMPT" + charController.charModel.charName);
                     return; 
                }
                Sequence deathSeq = DOTween.Sequence();
                deathSeq
                    //.AppendCallback(() => skeletonAnim.Skeleton.SetColor(Color.black))
                    .AppendCallback(() => StartCoroutine(BlackOutSpine(skeletonAnim, 0.01f)))
                    .AppendInterval(0.1f)
                    .AppendCallback(() => SpawnDeathGodess(charController))
                    .AppendInterval(0.2f)
                   .AppendCallback(() => StartCoroutine(FadeOutSpine(skeletonAnim, 0.005f)))
                    // .Append(deathGoddessGO.transform.GetChild(0).DOScale(finalSize, 0.15f))
                    .AppendInterval(0.2f)
                    .AppendCallback(() => fadeOut(deathGoddessGO, 0.4f))
                    //.AppendInterval(3f)
                    .AppendCallback(() => GOAnimCompletion(charController.gameObject))
                    ;
                    deathSeq.Play().OnComplete(() => deathSeq = null)
                    ;
                prevCharController = charController;
            //}
           
        }

        void GOAnimCompletion(GameObject charGO)
        {
            foreach (Transform child in charGO.transform)
            {
                child.gameObject.SetActive(false);
            }

            Collider[] colliders = charGO.GetComponents<Collider>();
            foreach (var c in colliders)
            {
                c.enabled = false; 
            }
        }

        void fadeOut(GameObject toFade, float animSpeed)
        {
            toFade.transform.GetComponentInChildren<SpriteRenderer>().DOFade(0, animSpeed);       
        }
        IEnumerator FadeOutSpine(SkeletonAnimation skeleton, float animSpeed)
        {
            Color color = skeleton.Skeleton.GetColor();
            for (float alpha = 1f; alpha >= 0; alpha -= animSpeed)
            {
                color.a = alpha;
                skeleton.Skeleton.SetColor(color);
                yield return null;
            }
        }
        IEnumerator BlackOutSpine(SkeletonAnimation skeleton, float animSpeed)
        {
            Color color = skeleton.skeleton.GetColor();
            
            for (float i = 1f; i >= 0; i -= animSpeed)
            {
                color.r = i;
                color.g = i;
                color.b = i; 
                skeleton.Skeleton.SetColor(color);
                yield return null;
            }
        }


        void SpawnDeathGodess(CharController charController)
        {
            GameObject charGO = charController.gameObject;
            if (deathGoddessGO == null)
            {   
                deathGoddessGO = Instantiate(deathGoddess, charController.gameObject.transform.position, Quaternion.identity);           
            }
            else
            {
                deathGoddess.transform.DOMove(charController.transform.position, 0.1f);
            }
            deathGoddessGO.transform.GetChild(0).DOScale(intermediateSize, 0.15f).OnComplete(() =>
             deathGoddessGO.transform.GetChild(0).DOPunchScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f, 1, 0.5f));
            deathGoddessGO.transform.SetParent(charGO.transform);
        }


     

        //private void Update()
        //{
        //    //if (Input.GetKeyDown(KeyCode.B))
        //    //{
        //    //    CharController charController = CharacterService.Instance.CharsInPlayControllers[1];
        //    //    CombatService.Instance.roundController.ReorderAfterCharDeath(charController); 

        //    //    PlayDeathAnim(charController);
        //    //}
        //}



    }
}





