using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat; 

namespace Common
{
    public class PostStatChgApplied : MonoBehaviour  // ONLY APPLIES ADDITIONAL STATES OR MODIFY OTHER PARAMS , 
    {       
        //// define a delegate here subscribe and unsubscribe in bulk => DONE

        //// instantiate n add to given controller => DONE 
        //// subscribe to changeStat event 
        //// get hold of controller and modify the stat as per change in parameters ;     

        //CharController charController;
        //float prevVal = 0f;

        //void Start()
        //{
        //    charController = gameObject.GetComponent<CharController>();
        //    charController.OnStatSet += ModifyHealth;
        //    charController.OnStatSet += ModifyFocus;
        //    charController.OnStatSet += ModifyHaste;
        //    charController.OnStatSet += ModifyLuck;
        //    charController.OnStatSet += ModifyMorale;
        //    charController.OnStatSet += ModifyLightRes;
        //    charController.OnStatSet += ModifyDarkRes;
        //    charController.OnStatSet += ModifyAirRes;
        //    charController.OnStatSet += ModifyWaterRes;
        //    charController.OnStatSet += ModifyEarthRes;
        //    charController.OnStatSet += ModifyFireRes;
        //    CombatEventService.Instance.OnCharClicked += Hello; 

        //    // accuracy to be included 
        //}
        //void Hello()
        //{
        //    Debug.Log("CHAR CLICKED " + charController.charModel.charName); 
        //}
        //void ModifyHealth(StatsName _statName, float _valueChange)
        //{
        //    if (_statName != StatsName.health)
        //        return;
        //    Debug.Log("HEALTH MODIFIED " + charController.charModel.charName);

        //    if (charController.GetStat(_statName).currValue <=0 ) 
        //    {
        //        // player dies :(... Enemies
        //        // heroes last drop of blood 
            
            
        //    }
        //}
        //private void ModifyFocus(StatsName _statName, float _valueChange)
        //{
        //    // if focus is zero(min limit )=> confused,
        //    // if focus is 12 (max limit) => concentrated 

        //    if (_statName != StatsName.focus) return;

        //    float newVal = charController.GetStat(_statName).currValue;
        //    float minL = charController.GetStatChanceData(_statName).minLimit;
        //    float maxL = charController.GetStatChanceData(_statName).maxLimit;

        //    if ( newVal<= minL)   // confused char state 
        //    {
        //        CharStatesService.Instance.SetCharState(charController.gameObject, CharStateName.Confused);                 
        //    }
        //    if(newVal >= maxL)
        //    {
        //        CharStatesService.Instance.SetCharState(charController.gameObject, CharStateName.Concentrated);
        //    }           
        //}

        //private void ModifyLuck(StatsName _statName, float _valueChange)
        //{

        //    // if focus is zero(min limit )=> feeble minded,
        //    // if focus is 12 (max limit) =>  luckyduck  
        //    if (_statName != StatsName.luck) return;

        //    float newVal = charController.GetStat(_statName).currValue;
        //    float minL = charController.GetStatChanceData(_statName).minLimit;
        //    float maxL = charController.GetStatChanceData(_statName).maxLimit;

        //    if (newVal <= minL)   // confused char state 
        //    {
        //        CharStatesService.Instance.SetCharState(charController.gameObject, CharStateName.Feebleminded);
        //    }
        //    if (newVal >= maxL)
        //    {
        //        CharStatesService.Instance.SetCharState(charController.gameObject, CharStateName.LuckyDuck);
        //    }          
        //}
        //private void ModifyHaste(StatsName _statName, float _valueChange)
        //{

        //    // if focus is zero(min limit )=> rooted, 
        //    // if focus is 12 (max limit) =>  pioneer,   

        //    if (_statName != StatsName.haste) return;

        //    float newVal = charController.GetStat(_statName).currValue;
        //    float minL = charController.GetStatChanceData(_statName).minLimit;
        //    float maxL = charController.GetStatChanceData(_statName).maxLimit;

        //    if (newVal == minL)   // confused char state 
        //    {
        //        CharStatesService.Instance.SetCharState(charController.gameObject, CharStateName.Rooted);
        //    }
        //    if (newVal >= maxL)
        //    {
        //        CharStatesService.Instance.SetCharState(charController.gameObject, CharStateName.Pioneer);
        //    }           
        //}


        //private void ModifyMorale(StatsName _statName, float _valueChange)
        //{
        //    // if focus is zero(min limit )=> despair, 
        //    // if focus is 12 (max limit) =>  spirited,   

        //    if (_statName != StatsName.morale) return;

        //    float newVal = charController.GetStat(_statName).currValue;
        //    float minL = charController.GetStatChanceData(_statName).minLimit;
        //    float maxL = charController.GetStatChanceData(_statName).maxLimit;

        //    if (newVal == minL)   // confused char state 
        //    {
        //        CharStatesService.Instance.SetCharState(charController.gameObject, CharStateName.Despaired);                
        //    }
        //    if (newVal >= maxL)
        //    {
        //        CharStatesService.Instance.SetCharState(charController.gameObject, CharStateName.Inspired);
        //    }          
         
        //}
        //private void ModifyLightRes(StatsName _statName, float _valueChange)
        //{
        //    //When Light resistance is Max(60), lose 20 Dark res

        //    if (_statName != StatsName.lightRes) return;
        //    float newVal = charController.GetStat(_statName).currValue;
        //    float minL = charController.GetStatChanceData(_statName).minLimit;
        //    float maxL = charController.GetStatChanceData(_statName).maxLimit;
        //    if (newVal >= maxL)
        //    {
        //        CharStatesService.Instance.SetCharState(charController.gameObject, CharStateName.Radiant);
        //    }
        //    //    charController.ChangeStat(StatsName.DarkRes, -20.0f, 0, 0);
        //    //    prevVal = newVal; 
        //    //}else if(prevVal >= newVal && prevVal >= maxL)
        //    //{
        //    //    charController.ChangeStat(StatsName.DarkRes, 20.0f, 0, 0);
        //    //}
        //}

        //private void ModifyDarkRes(StatsName _statName, float _valueChange)
        //{
        //    // When Dark resistance is Max(60), lose 20 Light res
        //    if (_statName == StatsName.darkRes) return;

        //    float newVal = charController.GetStat(_statName).currValue;
        //    float minL = charController.GetStatChanceData(_statName).minLimit;
        //    float maxL = charController.GetStatChanceData(_statName).maxLimit;
        //    if (newVal >= maxL)
        //    {
        //        Debug.Log("char" + charController.charModel.charName + "lunatic" + newVal + "max L" + maxL); 
        //        CharStatesService.Instance.SetCharState(charController.gameObject, CharStateName.Lunatic);
        //        charController.ChangeStat(StatsName.lightRes, -20.0f, 0, 0);            
        //    }          

        //}

        //private void ModifyAirRes(StatsName _statName, float _valueChange)
        //{  //When Air resistance is Max(90), lose 30 Earth res

        //    if (_statName != StatsName.airRes) return;

        //    float newVal = charController.GetStat(_statName).currValue;
        //    float minL = charController.GetStatChanceData(_statName).minLimit;
        //    float maxL = charController.GetStatChanceData(_statName).maxLimit;
        //    if (newVal >= maxL)
        //    {
        //        CharStatesService.Instance.SetCharState(charController.gameObject, CharStateName.Charged);

        //        charController.ChangeStat(StatsName.earthRes, -30.0f, 0, 0);               
        //    }
           
        //}

        //private void ModifyWaterRes(StatsName _statName, float _valueChange)
        //{  //When Water resistance is Max(90), lose 30 Fire res
        //    if (_statName != StatsName.waterRes) return;

        //    float newVal = charController.GetStat(_statName).currValue;
        //    float minL = charController.GetStatChanceData(_statName).minLimit;
        //    float maxL = charController.GetStatChanceData(_statName).maxLimit;
        //    if (newVal >= maxL)
        //    {
        //        CharStatesService.Instance.SetCharState(charController.gameObject, CharStateName.Frigid);

        //        charController.ChangeStat(StatsName.fireRes, -30.0f, 0, 0);              
        //    }         
            
        //}

        //private void ModifyEarthRes(StatsName _statName, float _valueChange)
        //{ //When Earth resistance is Max(90), lose 30 Air res

        //    if (_statName != StatsName.earthRes) return;

        //    float newVal = charController.GetStat(_statName).currValue;
        //    float minL = charController.GetStatChanceData(_statName).minLimit;
        //    float maxL = charController.GetStatChanceData(_statName).maxLimit;
        //    if (newVal >= maxL)
        //    {
        //        CharStatesService.Instance.SetCharState(charController.gameObject, CharStateName.Calloused);

        //        charController.ChangeStat(StatsName.airRes, -30.0f, 0, 0);              

        //    }           
        //}
        //private void ModifyFireRes(StatsName _statName, float _valueChange)
        //{
        //    //When Fire resistance is Max(90), lose 30 Water res

        //    if (_statName != StatsName.fireRes) return;

        //    float newVal = charController.GetStat(_statName).currValue;
        //    float minL = charController.GetStatChanceData(_statName).minLimit;
        //    float maxL = charController.GetStatChanceData(_statName).maxLimit;

        //    if (newVal >= maxL)
        //    {
        //        CharStatesService.Instance.SetCharState(charController.gameObject, CharStateName.Enraged);

        //        charController.ChangeStat(StatsName.waterRes, -30.0f, 0, 0);
 
        //    }           
        //}

    }
}



