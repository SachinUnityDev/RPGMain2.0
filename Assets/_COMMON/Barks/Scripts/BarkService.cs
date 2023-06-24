using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class BarkService : MonoSingletonGeneric<BarkService>
    {
        // Start is called before the first frame update
        public AllBarkSO allBarkSO;
        public QbarkController qbarkController; 
        

        void Start()
        {
            qbarkController = gameObject.GetComponent<QbarkController>();
            // no controllers needed 
            // one line call to manage UI 
            // barkAudio controller to manager sound files 
        }

        public void PlayBark( int BarkID, BarkType barkTrigger, 
            CharNames charName)
        {
            // Bark trigger .. who will carry the bark 
            // get bark SO play here 
        }

        public void PlayBark(int barkID)
        {



        }
        // method overloading... 
        BarkSO GetbarkSO(BarkType barkTrigger)
        {
            switch (barkTrigger)
            {
                case BarkType.None:
                    // generic barks to be owned by BarkModel  here 
                    break;
                case BarkType.Quest_Barks:
                    // get from the quest model 
                    break;
                case BarkType.Prep_Quest_Barks:
                    // get fromthe quest model 
                    break;
                case BarkType.dead_Barks:
                    // get from the charModels 
                    break;
                case BarkType.Curio_Barks:
                    // get from the CurioModel
                    break;
                case BarkType.Town_Barks:
                    // get from the townModels
                    break;
                case BarkType.NPC_Barks:
                    // get from the NPC models
                    break;
                case BarkType.Combat_Barks:
                    // get from the CharCombatModels
         
                default:
                    break;
            }
            return null; 
        }

      

    }



}
