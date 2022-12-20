using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity; 
public enum AnimState
{
    None, 
    Idle,
    Walking,
}


public class DemoQuestMovementController : MonoBehaviour
{

    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, walking;
    public AnimState currentState; 
    public float speed;
    public float movement;
    public Rigidbody2D rb;
    [SerializeField]string currentAnim; 

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = AnimState.Idle; 


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
            Debug.Log("IDLE Set");
            skeletonAnimation.AnimationName = "idle_standart"; 
           // SetAnimation(idle, true, 1f); 
        } 
        if (animState == AnimState.Walking)
        {
            Debug.Log("Walk Set");
            skeletonAnimation.AnimationName = "walk_standart";


          //  SetAnimation(walking, true, 1f); 
        }
    }


    public void Move()
    {
        movement = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movement * speed, rb.velocity.y); 
        if (movement != 0)
        {
            Debug.Log("heelo ");
            SetCharacterState(AnimState.Walking);
        }
        else
        {
            SetCharacterState(AnimState.Idle);

        }


    }
    // Update is called once per frame
    void Update()
    {
        Move(); 
    }
}
