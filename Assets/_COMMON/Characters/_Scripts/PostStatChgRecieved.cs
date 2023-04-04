using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat; 
namespace Common
{
    public class PostStatChgRecieved : MonoBehaviour
    {
        CharController charController;
        void Start()
        {
            charController = gameObject.GetComponent<CharController>();
           // CombatEventService.Instance.OnStrikeFired += ApplyPhysicalDmg;
           
            //    charController.OnStatChgApplied += ModifyInit;
            //    charController.OnStatChgApplied += ModifyLuck;
            //    charController.OnStatChgApplied += ModifyMorale;
            //    charController.OnStatChgApplied += ModifyLightRes;
            //    charController.OnStatChgApplied += ModifyDarkRes;
            //    charController.OnStatChgApplied += ModifyAirRes;
            //    charController.OnStatChgApplied += ModifyWaterRes;
            //    charController.OnStatChgApplied += ModifyEarthRes;
            //    charController.OnStatChgAppied += ModifyFireRes; // subscribe .. unsubscribe here 
            //}
            // armour and resistance to be substracted.l. 
        }
        void ApplyPhysicalDmg(SkillNames combatSkillName, CharController target, float dmgValue)
        {
          
                //float armorMinVal = charController.GetStat(StatsName.armor).minRange;
                //float armorMaxVal = charController.GetStat(StatsName.armor).maxRange;
                //float armorSelected = UnityEngine.Random.Range(armorMinVal, armorMaxVal);
                //float currVal = charController.GetStat(StatsName.health).currValue;
                //float newVal = armorSelected + currVal;
                //charController.SetCurrStat()
                //charController.SetCurrStat(StatsName.health, newVal,false, true); 
                //// get combat skill data // assign skillID to all skills 
                //// from skillService ....

          

        }

        void ApplyMagicalDmg(AttribName _statsName, float value)
        {

                switch (_statsName)
                {
                    
                    case AttribName.fireRes:

                        break;
                    case AttribName.earthRes:
                        break;
                    case AttribName.waterRes:
                        break;
                    case AttribName.airRes:
                        break;
                    case AttribName.lightRes:
                        break;
                    case AttribName.darkRes:
                        break;
                    default:
                        break;
                }

            }


    }


}

