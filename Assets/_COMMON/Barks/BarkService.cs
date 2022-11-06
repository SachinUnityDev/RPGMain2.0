using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class BarkService : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            // no controllers needed 
            // one line call to manage UI 
            // barkAudio controller to manager sound files 
        }

        public void PlayBark( int BarkID, BarkTrigger barkTrigger, 
            CharNames charName)
        {
            // Bark trigger .. who will carry the bark 
            // get bark SO play here 
        }

        public void PlayBark(int barkID)
        {



        }
        // method overloading... 
        BarkSO GetbarkSO(BarkTrigger barkTrigger)
        {
            switch (barkTrigger)
            {
                case BarkTrigger.None:
                    // generic barks to be owned by BarkModel  here 
                    break;
                case BarkTrigger.Quest_Barks:
                    // get from the quest model 
                    break;
                case BarkTrigger.Prep_Quest_Barks:
                    // get fromthe quest model 
                    break;
                case BarkTrigger.dead_Barks:
                    // get from the charModels 
                    break;
                case BarkTrigger.Curio_Barks:
                    // get from the CurioModel
                    break;
                case BarkTrigger.Town_Barks:
                    // get from the townModels
                    break;
                case BarkTrigger.NPC_Barks:
                    // get from the NPC models
                    break;
                case BarkTrigger.Combat_Barks:
                    // get from the CharCombatModels
         
                default:
                    break;
            }
            return null; 
        }

      

    }



}
