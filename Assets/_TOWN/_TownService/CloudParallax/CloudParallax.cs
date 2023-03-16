using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{


    public class CloudParallax : MonoBehaviour
    {

        // Speed of the cloud movement
        public float cloudSpeed = 0.5f;

        // Screen boundaries
        [SerializeField] float leftBoundary;
        [SerializeField] float rightBoundary;
        public float imgWidth; 


        void Start()
        {
        
            leftBoundary = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
            rightBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
            imgWidth = GetComponent<RectTransform>().rect.width;
        }

        public void WrapAround()
        {
            transform.DOLocalMoveX(-imgWidth * 1.5f, 0.1f);
            transform.SetAsFirstSibling();
        }
        void Update()
        {
            transform.position += Vector3.right * cloudSpeed * Time.deltaTime;


            if(transform.GetSiblingIndex() == 1)
            {
                if (transform.localPosition.x > rightBoundary)
                {
                    Transform lasttrans = transform.parent.GetChild(2);                    
                    lasttrans.GetComponent<CloudParallax>().WrapAround();
                }
            }   
        }

    }
}