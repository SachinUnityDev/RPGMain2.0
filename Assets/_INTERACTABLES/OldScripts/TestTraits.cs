using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class TestTraits : MonoBehaviour
{
        [SerializeField] TempTraitName traitName ;  
        
        
        // Start is called before the first frame update
    void Start()
    {




    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        //if (collision.gameObject.GetComponent<CharacterController>() != null)
        //{
        //        GameObject go = collision.gameObject;
        //        TraitsFactory.Instance.TempTraitsFactory(go, traitName);     
           
        //    Debug.Log("collided with character");

        //}
    }


    // Update is called once per frame
    void Update()
    {

    }
}






}
