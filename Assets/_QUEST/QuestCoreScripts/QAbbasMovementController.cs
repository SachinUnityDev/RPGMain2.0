﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using DG.Tweening;
using Cinemachine;

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
        [SerializeField] CinemachineBrain brain;

        [Header(" Skeleton Animation ref")]
        public SkeletonAnimation skeletonAnimation;
        public AnimationReferenceAsset idle, walking;    
        public float speed;
        public float movement;
        public Rigidbody2D rb;
        [SerializeField] string currentAnim;

        [Header("Entry collider")]
        [SerializeField] GameObject entryCollider;
        [SerializeField] bool canMove; 

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();           
         
            QRoomService.Instance.OnQRoomStateChg += OnQRoomStateChg;
            
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
            if (QRoomService.Instance.qRoomState == QRoomState.Prep) return;
            canMove = QRoomService.Instance.canAbbasMove;
           
            if (QRoomService.Instance.qRoomState == QRoomState.AutoWalk)
            {
                // Auto walk is prep to Walk State entry 
                rb.velocity = new Vector2(movement * speed, rb.velocity.y);
            }
            if(QRoomService.Instance.qRoomState == QRoomState.Walk && canMove)
            {
                movement = Input.GetAxis("Horizontal");
                rb.velocity = new Vector2(movement * speed, rb.velocity.y);
              
            }
          
            if(movement != 0 && canMove)
            {
                SetCharacterState(AnimState.Walking);
            }
            else
            {
                SetCharacterState(AnimState.Idle);
            }
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            QRoomService.Instance.canAbbasMove = false;
            string name = collision.gameObject.name;
            Debug.Log(" Name " + name); 
            if (name == "EntryCollider")
            {
                movement = 0;
                SetCharacterState(AnimState.Idle);
                virtualCam.enabled = true;
                entryCollider?.gameObject.SetActive(false);
                QRoomService.Instance.On_QRoomStateChg(QRoomState.Walk);
                QRoomService.Instance.canAbbasMove = true;
            }
            if (name == "ArrowTrigger")
            {
                QRoomService.Instance.qRoomView.ShowEndArrow();              
            }
            if(name == "Floor")
            {
                QRoomService.Instance.canAbbasMove = true;
            }
        }
        public void OnQRoomStateChg(QRoomState qRoomState)
        {
            if(qRoomState== QRoomState.AutoWalk)
            {
                entryCollider?.gameObject.SetActive(true);
                CreateAbbasEntry();
            }
        }
        public void CreateAbbasEntry()
        {
            QRoomService.Instance.canAbbasMove = true;
            movement = 1;            
        }

        public void ResetAbbas()
        {



        }

        void Update()
        {
            Move();      
        }
    }
}