using Common;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;



namespace Interactables
{
    public abstract class ArmorBase
    {
        public abstract ArmorType armorType { get; }
        
        public CharController charController;
        public ArmorSO armorSO;
        public List<string> allLines = new List<string>();

        int dayCount = 0;
        public ArmorModel armorModel;
        public virtual void InitArmor(CharController charController, ArmorModel armorModel)
        {
            this.charController = charController;
            armorSO = ArmorService.Instance.allArmorSO.GetArmorSOWithType(armorType);
            CalendarService.Instance.OnStartOfCalDay += (int gameDay)=>DayTick();
          
            this.armorModel = armorModel;

        }
        public virtual void OnArmorFortify()
        {
           // if(armorState == Armor)
            allLines.Clear();

            charController.buffController.ApplyBuff(CauseType.ArmorType, (int)armorType
            , charController.charModel.charID, AttribName.armorMin, armorSO.minArmor,
             TimeFrame.EndOfDay, 3, true);

            charController.buffController.ApplyBuff(CauseType.ArmorType, (int)armorType
          , charController.charModel.charID, AttribName.armorMax, armorSO.maxArmor,
           TimeFrame.EndOfDay, 3, true);

            string str = $"{armorSO.minArmor}-{armorSO.maxArmor} Armor for 3 days";
            allLines.Add(str);

            armorModel.armorState = ArmorState.Fortified; 
           
        }         
        public abstract void OnArmorFortifyUpgraded();

        public virtual void DayTick()
        {
            dayCount++;
            if(dayCount >= 3)
                OnArmorFortifyEnd();    
        } 
        public virtual void OnArmorFortifyEnd()
        {
            armorModel.armorState = ArmorState.Fortifiable; 
        } 

    

    }
}