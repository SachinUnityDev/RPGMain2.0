using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using Common; 

namespace Interactables
{
    

    public class PotionService : MonoSingletonGeneric<PotionService>
    {
        [Header("SOs")]
        public List<PotionSO> allPotionSOs = new List<PotionSO>();

        [SerializeField] PotionFactory potionFactory;
        public PotionController allPotionController; 

        public List<PotionsBase> allPotionBase = new List<PotionsBase>();
        public List<PotionModel> allPotionModel = new List<PotionModel>();
        
        public CharController selectCharCtrl; 


        void Start()
        {
          
        }


        #region CHECKS 
       
        #endregion

        #region Getters

        public PotionSO GetPotionModelSO(PotionName potionName)
        {
            foreach (PotionSO potion in allPotionSOs)
            {
                if (potion.potionName == potionName)
                {
                    return potion;
                }
            }
            Debug.Log("Char State Model Not found");
            return null;
        }
        public PotionsBase GetPotionBase(PotionName _potionName)
        {
            PotionsBase potionBase = allPotionBase.Find(t => t.potionName == _potionName);
            if (potionBase != null)
                            return potionBase;            
            else            
                Debug.Log("POTION BASE NOT FOUND");
                return null;
            
        }
        public PotionModel GetPotionModel(PotionName _potionName)
        {
            PotionModel potionModel = allPotionModel.Find(t => t.potionName == _potionName);
            if (potionModel != null)
                return potionModel;
            else
                Debug.Log("POTION MODEL NOT FOUND");
            return null;
        }
        #endregion


        #region Setters

        #endregion


        public void InitPotion2CommonInv(CharNames charName, PotionName _potionName, CharController causedby
          , CauseType causeType, int causeID)
        {

            // Init a potion.. 
            // Add to common Inv.. 

            PotionsBase potionBase = potionFactory.GetPotionBase(_potionName);            
            
            PotionSO potionSO = GetPotionModelSO(_potionName);
            PotionModel potionModel = potionBase.PotionInit(potionSO);
            allPotionModel.Add(potionModel);

           Iitems item = potionBase as Iitems;
           item.invType = SlotType.CommonInv;
            CharModel charModel = CharService.Instance.GetAllyCharModel(charName);
            if(charModel != null)
            {
                InvData invData = new InvData(charModel.charName, item);
                InvService.Instance.invMainModel.AddItem2CommInv(invData);
            }
            else
            {
                Debug.Log("CharModel is null" + charName);
            }                      
        }

        public void InitPotion2ActiveInv(CharNames charName,PotionName _potionName)
        {
            //init potion 
            // Equip to char Name

            //PotionsBase potionBase = potionFactory.GetPotionBase(_potionName);
            //AddPotion2Char(potionBase);  // potion init here too and then added.

            //Iitems item = potionBase as Iitems;
            //item.invType = SlotType.CommonInv;
            //CharModel charModel = CharService.Instance.GetAllyCharModel(charName);
            //if (charModel != null)
            //{
            //    InvData invData = new InvData(charModel.charName, item);
            //    InvService.Instance.invMainModel.AddItem2CommInv(invData);
            //}
            //else
            //{
            //    Debug.Log("CharModel is null" + charName);
            //}
        }




        //void LocChange(PotionName potionName, InvType invType)
        //{
        //    if (GetPotionModel(potionName) != null)
        //        GetPotionModel(potionName).potionLoc = invType;

        //    PotionsBase potionbase = GetPotionBase(potionName);
        //    Iitems item = potionbase as Iitems;
        //    item.invType = invType; 

        //}
    

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                CharController charController = CharService.Instance.charsInPlayControllers[0];

                InitPotion2CommonInv(CharNames.Abbas_Skirmisher, PotionName.HealthPotion, charController
                                        , CauseType.CharSkill, 2); 
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                //CharController charController = CharService.Instance.CharsInPlayControllers[0];
                //InitPotion2CommonInv(charController, PotionName.StaminaPotion, charController
                //    , CauseType.CharSkill, 2);

            }

        }


    }




}
//public bool AddPotion2ActiveInv(CharNames CharName, PotionName potionName, int slotNo)
//{
//    CharModel charModel = CharService.Instance.GetAllyCharModel(CharName);
//    switch (slotNo)
//    {
//        case 1:
//            charModel.beltSlot1 = potionName; return true;                 
//        case 2:
//            charModel.beltSlot2 = potionName; return true;                    
//        default:
//            Debug.Log("slot num incorrect"); 
//            break;
//    }
//    return false; 
//}