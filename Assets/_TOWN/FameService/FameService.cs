using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common

{
    // 120 and -120 the values will stagnate but chg will only take place in reverse dir 
    // fame is player fame ..not connected 
    public class FameService : MonoSingletonGeneric<FameService>, ISaveableService
    {

        public int fameMultiplier = 1;

        public FameModel fameModel;
        public FameSO fameSO;
        public FameController fameController;
        public FameViewController fameViewController;
        
      
        void Start()
        {
            fameModel = new FameModel();
        }

        public void Init()
        {
            // save service integration here pending
        }
        public FameType GetFameType()
        {
            float currentFame = FameService.Instance.GetFameValue();
            //Debug.Log("fame" + currFameData.fameVal); 
            if (currentFame >= 30 && currentFame < 60) return FameType.Respectable;
            else if (currentFame >= 60 && currentFame < 120) return FameType.Honorable;
            else if (currentFame >= 120) return FameType.Hero;
            else if (currentFame > -60 && currentFame <= -30) return FameType.Despicable;
            else if (currentFame > -120 && currentFame <= -60) return FameType.Notorious;
            else if (currentFame <= -120) return FameType.Villain;
            else if (currentFame > -30 && currentFame < 30) return FameType.Unknown;
            else return FameType.None;
        }
        public int GetFameValue()
        {
            return (int)fameModel.fameVal;
        }
        public int GetFameYieldValue()
        {
            return (int)fameModel.fameYield;          
        }

        // used to update score from outside Services 
        public void FameScoreUpdate(FameChgData _fameChgData)
        {
           
            fameModel.fameVal += _fameChgData.scoreAdded * fameMultiplier;
            fameModel.allFameData.Add(_fameChgData);        
        }

        // fetch fame chg list for local use and view Controller
        public List<FameChgData> GetFameChgList()
        {
            return fameModel.allFameData;        
        }
        public void RestoreState()
        {

        }

        public void ClearState()
        {

        }

        public void SaveState()
        {

        }

    }




}
