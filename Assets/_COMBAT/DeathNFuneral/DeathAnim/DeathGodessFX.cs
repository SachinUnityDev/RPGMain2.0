using Common;
using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{


    public class DeathGodessFX : MonoBehaviour
    {

        Sequence deathSeq;
        public void PlayDeathAnim(CharController charController)
        {
            SkeletonAnimation skeletonAnim = charController.gameObject.GetComponentInChildren<SkeletonAnimation>(); 
            deathSeq = DOTween.Sequence();
            deathSeq

               //.AppendCallback(() => StartCoroutine(BlackOutSpine(skeletonAnim, 0.5f)))
               .AppendCallback(() => StartCoroutine(FadeOutSpine(skeletonAnim, 0.5f)))               
               .Append(transform.DOPunchScale(new Vector3(2f, 2f, 2f), 0.2f, 1, 0.1f))
                .AppendCallback(() => GOAnimCompletion(charController.gameObject))
                .AppendInterval(0.25f)
                .AppendCallback(() => fadeOut(gameObject, 0.05f))                
                ;
            deathSeq.Play()
                .OnComplete(() => OnAnimComplete())
                ;
        }

        void OnAnimComplete()
        {
            deathSeq.Kill();
            Destroy(gameObject, 0.5f);
            
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
        void fadeOut(GameObject toFade, float duration)
        {
            toFade.transform.GetComponentInChildren<SpriteRenderer>().DOFade(0, duration);
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
            skeleton.gameObject.SetActive(false);
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
                skeleton.Skeleton.SetColor(new Color(0,0,0));
            }
        }
    }
}