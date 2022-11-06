using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common

{
    // 120 and -120 the values will stagnate but chg will only take place in reverse dir 
    // fame is player fame ..not connected 
    public class FameService : MonoSingletonGeneric<FameService>, ISaveableService
    {

        public float fameModifier=1 ;

        public FameData currentFame;
        public int fameMultiplier = 1;

        public FameModel fameModel;
        public FameSO fameSO;
        public 
        
      
        void Start()
        {
            fameModel = new FameModel();
        }

        public void Init()
        {
            // save service integration here pending
        }



        public int GetFameValue(int currPage)
        {
            switch (currPage)
            {
                case 0:
                    return (int)fameModel.currGlobalFame; 
                    
                case 1:
                    return (int)fameModel.currNekkisariFame;
                    
                default:
                    return 0; 
            }

        }


        public int GetFameModValue(int currPage)
        {
            switch (currPage)
            {
                case 0:
                    return (int)fameModel.globalFameMod;

                case 1:
                    return (int)fameModel.nekkisariFameMod;

                default:
                    return 0;
            }
        }

        // used to update score from outside Services 
        public void FameScoreUpdate(FameChgData _fameChgData)
        {
            List<FameChgData> currList = GetFameChgList(_fameChgData.fameLoc);
            currList.Add(_fameChgData);
            switch (_fameChgData.fameLoc)
            {
                case FameLoc.FameGlobal:
                    fameModel.currGlobalFame += _fameChgData.scoreAdded * fameMultiplier;
                    break;
                case FameLoc.FameNekkisari:
                    fameModel.currNekkisariFame += _fameChgData.scoreAdded * fameMultiplier;
                    break;
                case FameLoc.FameBluetown:
                    break;
                case FameLoc.FameSmaeru:
                    break;
                default:
                    break;
            }
        }

        // fetch fame chg list for local use and view Controller
        public List<FameChgData> GetFameChgList(FameLoc fameLoc)
        {
            switch (fameLoc)
            {
                case FameLoc.FameGlobal:
                    return fameModel.globalfameDataAll;
                    
                case FameLoc.FameNekkisari:
                    return fameModel.nekkisarifameDataAll;
                case FameLoc.FameBluetown:
                    return null;                     
                case FameLoc.FameSmaeru:
                    return null; 
                default:
                    return null;
            }
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
