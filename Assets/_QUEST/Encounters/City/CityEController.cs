using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Quest
{


    public class CityEController : MonoBehaviour
    {

        public List<CityEModel> allCityEModels = new List<CityEModel>();    

        void Start()
        {

        }

        public CityEModel GetCityEModel(CityEncounterNames encounterName)
        {
            CityEModel cityEModel = allCityEModels.Find(t=>t.encounterName== encounterName);
            return cityEModel;

        }

        public void CloseCityETree(CityEncounterNames encounterName, int startSeq)
        {
            List<CityEModel> cityEModelOfTree = 
                allCityEModels.Where(t => t.encounterName == encounterName &&
                                            t.encounterSeq > startSeq).ToList();

            cityEModelOfTree.ForEach(t => t.state = CityEState.Locked); 
        }

        public void UnLockNext(CityEncounterNames encounterName, int currSeq)
        {
            if(HasNextLvl(encounterName, currSeq))
            {
                CityEModel cityM = 
                allCityEModels.Find(t => t.encounterName == encounterName && t.encounterSeq == (currSeq + 1));
                cityM.state = CityEState.UnLockedNAvail;                     
            }
        }

        public CityEModel GetPreModel(CityEncounterNames encounterName, int seq)
        {
            if (seq == 0) return null;
            CityEModel cityEModel = 
            allCityEModels.Find(t => t.encounterName== encounterName && t.encounterSeq == (seq - 1)); 
            return cityEModel;
        }

        public bool HasNextLvl(CityEncounterNames encounterName, int currSeq)
        {
            return
            allCityEModels.Any(t => t.encounterName == encounterName && t.encounterSeq == (currSeq + 1)); 
        }


    }
}