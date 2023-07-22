using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{
    public class FameController : MonoBehaviour
    {
        public int index;
        public FameModel fameModel;
        private void Start()
        {
            CalendarService.Instance.OnStartOfCalDay += (int day) => FameValueChgOnEndDay(); 
        }
        public void InitFameController(FameSO fameSO)
        {
            fameModel = new FameModel(fameSO); 
        }

        public void ApplyFameChg(CauseType causeType, int causeName, int val) 
        {
            FameChgData fameChgData = new FameChgData(causeType, causeName, val);
            fameModel.fameVal += val;
            fameModel.fameType = FameService.Instance.GetFameType(); 
            fameModel.allFameData.Add(fameChgData);
        }
        public void ApplyFameYieldChg(CauseType causeType, int causeName, int val)
        {
            FameChgData fameChgData = new FameChgData(causeType, causeName, val);
            fameModel.fameYield += fameChgData.fameAdded;          
        }
        void FameValueChgOnEndDay()
        {
            fameModel.fameVal += fameModel.fameYield; 
        }
        public bool IsFameBehaviorMatching(CharController charController)
        {
            FameBehavior fameBehavior = charController.charModel.fameBehavior;

            foreach (FameBehaviorMap map in FameService.Instance.fameSO.allFameBehaviorMaps)
            {
                if(fameBehavior == map.fameBehavior) 
                {
                    foreach(FameType fameType in map.allAntiFameTypes)
                    {
                        if (fameModel.fameType == fameType)
                            return false; 
                    }
                    return true; 
                }
            }
            return true;
        }
    }


}

