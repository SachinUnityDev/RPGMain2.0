using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using DG.Tweening;
using Cinemachine;
using static UnityEngine.EventSystems.EventTrigger;
using System.Web.Hosting;

public enum AnimState
{
    None, 
    Idle,
    Walking,
}

namespace Quest
{



    public class QAbbasMovementController : MonoBehaviour
    {
        [Header("Virtual Cam TBR")]
        [SerializeField] CinemachineVirtualCamera virtualCam;


        [Header(" Skeleton Animation ref")]
        public SkeletonAnimation skeletonAnimation;
        public AnimationReferenceAsset idle, walking;
        public AnimState currentState;
        public float speed;
        public float movement;
        public Rigidbody2D rb;
        [SerializeField] string currentAnim;


        [Header("Entry collider")]
        [SerializeField] GameObject entryCollider;
        

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            currentState = AnimState.Idle;

            virtualCam.enabled = false;
            QSceneService.Instance.OnQRoomStateChg += OnQRoomStateChg;
        }
        public void SetAnimation(AnimationReferenceAsset animRef, bool loop, float timeScale)
        {
            if (animRef.name.Equals(currentAnim))
                return;

            skeletonAnimation.state.SetAnimation(0, animRef, loop).TimeScale = timeScale;
            currentAnim = animRef.name;
        }

        public void SetCharacterState(AnimState animState)
        {
            if (animState == AnimState.Idle)
            {
                skeletonAnimation.AnimationName = "idle_standart";
                // SetAnimation(idle, true, 1f); 
            }
            if (animState == AnimState.Walking)
            {
                skeletonAnimation.AnimationName = "walk_standart";
                //  SetAnimation(walking, true, 1f); 
            }
        }


        public void Move()
        {
            if(QSceneService.Instance.qRoomState == QRoomState.Walk)
                movement = Input.GetAxis("Horizontal");            
            rb.velocity = new Vector2(movement * speed, rb.velocity.y);
            if (movement != 0)
            {
                SetCharacterState(AnimState.Walking);
            }
            else
            {
                SetCharacterState(AnimState.Idle);
            }


        }
        public void CreateAbbasEntry()
        {
            virtualCam.enabled = false;
            movement = 1;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            string name = collision.gameObject.name;
            if (name == "EntryCollider")
            {
                movement = 0;
                SetCharacterState(AnimState.Idle);           
            }
            if (name == "ArrowTrigger")
            {

            }
        }
        public void OnQRoomStateChg(QRoomState qRoomState)
        {
            if(qRoomState== QRoomState.Walk)
            {
                virtualCam.enabled = true;
                entryCollider?.gameObject.SetActive(false);
            }

            if (qRoomState == QRoomState.Prep)
            {
                virtualCam.enabled = false;
                entryCollider?.gameObject.SetActive(true);
                CreateAbbasEntry();

            }



        }


        void Update()
        {
            Move();      
        }
    }
}