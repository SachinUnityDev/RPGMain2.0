using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat; 
namespace Common
{

    public class TestService : MonoBehaviour
    {
        [Header ("TEST OBJECT")]
        [SerializeField] GameObject charGOTEST; 
        void Start()
        {

        }

       
        void Update()
        {
            //KeyCode.R
      
            if (Input.GetKeyDown(KeyCode.P))
            {
                string str = "HelloWorld";
                str.CreateSpace(); 
                //CharStatesService.Instance.RemoveCharState(charGOTEST, CharStateName.BurnHighDOT);
                //charGOTEST = CharacterService.Instance.allyInPlay[0];
                //CharStatesService.Instance.SetCharState(charGOTEST, CharStateName.PoisonedHighDOT);
            }

        }
    }
}