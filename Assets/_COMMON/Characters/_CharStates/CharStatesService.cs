using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Combat;




namespace Common
{

    public class CharStateData  // braodcast data 
    {
       public CharNames causeByCharName;
       public int causeCharID; 
       public CauseType causeType; 
       public int causeID;
       public CharStateModel charStateModel; // effected char Inside this model

        public CharStateData(CharNames causeByCharName, int causeCharID, CauseType causeType, int causeID
           , CharStateModel charStateModel)
        {
            this.causeByCharName = causeByCharName;
            this.causeCharID = causeCharID;
            this.causeType = causeType;
            this.causeID = causeID;
            this.charStateModel = charStateModel; 


        }

        public CharStateData()
        {

        }
    }

    //public class CharStateInstance
    //{
    //    public int seq;
    //    public CharStatesBase charStateBase;
    //    public CharStateInstance(int seq, CharStatesBase charStateBase)
    //    {
    //        this.seq = seq;
    //        this.charStateBase = charStateBase;
    //    }
    //}

    public class CharStatesService : MonoSingletonGeneric<CharStatesService>
    {
        // apply n remove char States....Start Here.. 
        // event and view control 
        // view control .. hover upon a Icon the scripts directs here with controller name and stateName
        // view controll logic easy access point 

        public event Action<CharStateData> OnCharStateStart;  // events will drive the animations
        public event Action<CharStateData> OnCharStateEnd;   
        public event Action<CharStateName, CharController> OnStateHovered; 


        public List<CharStateSO1> allCharStateSOs = new List<CharStateSO1>();


        public CharStatesFactory charStateFactory;
        public CharStateModelSO charStateModelSO; 
       // public List<CharStateInstance> allCharStates = new List<CharStateInstance>();// not used 
       // public List<CharStateData> allCharStatesData = new List<CharStateData>();
        public List<CharStateModel> allCharStateModel = new List<CharStateModel>(); // All char State In Use

        void Start()
        {
            charStateFactory = GetComponent<CharStatesFactory>();         
        }

        public CharStateModel GetCharStateModelSO(CharStateName _charStateName)
        {
            foreach (CharStateModel charStateModel in charStateModelSO.allCharStatesModels)
            {
                if(charStateModel.charStateName == _charStateName)
                {
                    return charStateModel; 
                }
            }
            Debug.Log("Char State Model Not found");
            return null; 
        }

        public void ApplyCharState(GameObject _charGOEffected, CharStateName _charStateName, CharController causedby
                  , CauseType causeType, int causeID, TimeFrame timeFrame = TimeFrame.None, int castTime = -5)
        {
            CharController charController = _charGOEffected.GetComponent<CharController>();
            CharNames charEffected = charController.charModel.charName;
            CharNames causeCharName = causedby.charModel.charName; 

            int effectedCharID = charController.charModel.charID;
            int causeByCharID = causedby.charModel.charID;
            int startRound = CombatService.Instance.currentRound;

            if (HasCharState(_charGOEffected, _charStateName))  // increase casttime
            {
                CharStateModel charStateModel = GetCharStateModel(_charStateName);  // from all charStatemodel list
                CharStatesBase charStateBase = charController.charStateController.GetCurrCharStateBase(_charStateName);
                if(charStateModel.timeFrame == TimeFrame.EndOfRound)
                {                    
                    charStateBase.ResetState();
                }
            }
            else
            {                
                CharStatesBase charBase = charStateFactory.GetCharState(_charStateName);  // charBase 
                CharStateModel charStateModelSO = GetCharStateModelSO(_charStateName);  // charStateModel from SO 
                charBase.StateInit(charStateModelSO, charController, timeFrame, castTime);
                charBase.StateBaseApply();
                charBase.StateApplyFX();
                charBase.StateApplyVFX();
                charController.charStateController.allCharBases.Add(charBase);
                charController.charModel.InCharStatesList.Add(_charStateName);  // this is to shifted locally 

            }
            CharStateModel charStateModelReload = GetCharStateModel(_charStateName);

            // broadcasting
            CharStateData charStateData = new CharStateData(causeCharName, causeByCharID, causeType, causeID
                                                       ,  charStateModelReload); 
            OnCharStateStart?.Invoke(charStateData);
        }


        CharStateModel GetCharStateModel(CharStateName _charStateName)
        {

           return allCharStateModel.Find(t => t.charStateName == _charStateName); 

           // return null; 
        }


        //public void SetCharState(GameObject _charGOEffected, CharController Causedby, CharStateName _charStateName)
        //{


        //}

        public bool HasCharState(GameObject _charGO, CharStateName _charStateName)
        {
            return _charGO.GetComponent<CharController>()
                        .charModel.InCharStatesList.Any(t => t == _charStateName);
        }
        public void RemoveCharState(GameObject _charGO, CharStateName _charStateName)
        {
            CharController charController = _charGO.GetComponent<CharController>();
            CharStatesBase charStateBase = charController.charStateController.GetCurrCharStateBase(_charStateName);
            if (HasCharState(_charGO, _charStateName))
            {
                charStateBase.EndState();                 
            }          
        }

        public bool IsImmune(GameObject _charGO, CharStateName _charStateName)
        {
            return _charGO.GetComponent<CharController>().charModel.Immune2CharStateList.Any(t => t == _charStateName); 
        }

 

        //public void RemoveImmunity(GameObject _charGO, CharStateName _charStateName)
        //{
        //    _charGO.GetComponent<CharController>().charModel.Immune2CharStateList.Remove(_charStateName);
        //}
        //public void RemoveImmunityDOT(GameObject _charGO, CharStateName _charStateName)
        //{
        //    if (_charStateName == CharStateName.BurnHighDOT
        //       || _charStateName == CharStateName.BurnMedDOT
        //       || _charStateName == CharStateName.BurnLowDOT)
        //    {
        //        RemoveImmunity(_charGO, CharStateName.BurnHighDOT);
        //        RemoveImmunity(_charGO, CharStateName.BurnLowDOT);
        //    }
        //    if (_charStateName == CharStateName.BleedHighDOT
        //      || _charStateName == CharStateName.BleedMedDOT
        //      || _charStateName == CharStateName.BleedLowDOT)
        //    {
        //        RemoveImmunity(_charGO, CharStateName.BleedHighDOT);
        //        RemoveImmunity(_charGO, CharStateName.BleedLowDOT);
        //    }
        //    if (_charStateName == CharStateName.PoisonedHighDOT
        //      || _charStateName == CharStateName.PoisonedMedDOT
        //      || _charStateName == CharStateName.PoisonedLowDOT)
        //    {

        //        RemoveImmunity(_charGO, CharStateName.PoisonedHighDOT);
        //        RemoveImmunity(_charGO, CharStateName.PoisonedLowDOT);
        //    }
        //}
     
        public void ClearDOT(GameObject _charGO, CharStateName _charStateName)
        {
            if (_charStateName == CharStateName.BurnHighDOT
                || _charStateName == CharStateName.BurnMedDOT
                || _charStateName == CharStateName.BurnLowDOT)
            {
                RemoveCharState(_charGO, CharStateName.BurnHighDOT);
                RemoveCharState(_charGO, CharStateName.BurnMedDOT);
                RemoveCharState(_charGO, CharStateName.BurnLowDOT);
            }

            if (_charStateName == CharStateName.BleedHighDOT
              || _charStateName == CharStateName.BleedMedDOT
              || _charStateName == CharStateName.BleedLowDOT)
            {
                RemoveCharState(_charGO, CharStateName.BleedHighDOT);
                RemoveCharState(_charGO, CharStateName.BleedMedDOT);
                RemoveCharState(_charGO, CharStateName.BleedLowDOT);
            }

            if (_charStateName == CharStateName.PoisonedHighDOT
              || _charStateName == CharStateName.PoisonedMedDOT
              || _charStateName == CharStateName.PoisonedLowDOT)
            {
                RemoveCharState(_charGO, CharStateName.PoisonedHighDOT);
                RemoveCharState(_charGO, CharStateName.PoisonedMedDOT);
                RemoveCharState(_charGO, CharStateName.PoisonedLowDOT);
            }
        }

        public bool HasCharDOTState(GameObject _charGO, CharStateName _charStateName)
        {
            if (_charStateName == CharStateName.BurnHighDOT
               || _charStateName == CharStateName.BurnMedDOT
               || _charStateName == CharStateName.BurnLowDOT)
            {
              return (HasCharState(_charGO, CharStateName.BurnHighDOT)
                || HasCharState(_charGO, CharStateName.BurnMedDOT)
                || HasCharState(_charGO, CharStateName.BurnLowDOT));
            }

            if (_charStateName == CharStateName.BleedHighDOT
              || _charStateName == CharStateName.BleedMedDOT
              || _charStateName == CharStateName.BleedLowDOT)
            {
             return (HasCharState(_charGO, CharStateName.BleedHighDOT)
                || HasCharState(_charGO, CharStateName.BleedMedDOT)
                || HasCharState(_charGO, CharStateName.BleedLowDOT));
            }

            if (_charStateName == CharStateName.PoisonedHighDOT
              || _charStateName == CharStateName.PoisonedMedDOT
              || _charStateName == CharStateName.PoisonedLowDOT)
            {
               return( HasCharState(_charGO, CharStateName.PoisonedHighDOT)
                || HasCharState(_charGO, CharStateName.PoisonedMedDOT)
                || HasCharState(_charGO, CharStateName.PoisonedLowDOT));
            }


            return false; 
        }

        #region GETTERS
        public CharStateBehavior GetCharStateType(CharStateName _charStateName)  // Positive // negative
        {
            return charStateModelSO.allCharStatesModels.Find(t => t.charStateName == _charStateName).charStatebehavior;
        }

        #endregion


        #region SETTERS


        #endregion

        


    }


}
