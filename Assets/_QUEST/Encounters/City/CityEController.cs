using Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Quest
{


    public class CityEController : MonoBehaviour
    {


        [Header("City Encounter Panel and View/ TBR")]
        public Transform cityEPanel;
        public Transform cityEView;

        CityEncounterFactory cityEFactory; 
        public List<CityEModel> allCityEModels = new List<CityEModel>();    

        public List<CityEncounterBase> allCityEBase = new List<CityEncounterBase>();
        [SerializeField] int allCityE =0; 
        void Awake()
        {
            cityEFactory = GetComponent<CityEncounterFactory>();
        }


        public void ShowCityE(CityEModel cityEModel)
        {
            cityEView.GetComponent<CityEncounterView>().InitEncounter(cityEModel); 

        }

        public void InitCityE(AllCityESO allCityESO)
        {
            foreach (CityEncounterSO cityESO in allCityESO.allCityESO)
            {
                CityEModel cityEModel = new CityEModel(cityESO); 
                allCityEModels.Add(cityEModel); 
            }
           
            InitAllCityEBase();
        }
        public void InitCityE(List<CityEModel> allCityEModels)
        {
            this.allCityEModels = allCityEModels.DeepClone();           
            InitAllCityEBase();
        }
        void InitAllCityEBase()
        {
            foreach (CityEModel cityEModel in allCityEModels)
            {

                CityEncounterBase cityEBase = cityEFactory
                                .GetCityEncounterBase(cityEModel.cityEName, cityEModel.encounterSeq); 
                cityEBase.CityEInit(cityEModel);
                CalendarService.Instance.OnStartOfCalDay += (int day) => cityEBase.UnLockCondChk();
                allCityEBase.Add(cityEBase);    
            }
            allCityE = allCityEBase.Count;
        }

        public CityEModel GetCityEModel(CityENames encounterName, int seq)
        {
            CityEModel cityEModel = allCityEModels.Find(t=>t.cityEName== encounterName && t.encounterSeq == seq);
            return cityEModel;

        }

        public CityEncounterBase GetCityEBase(CityENames encounterName, int seq) 
        {
             int index = allCityEBase.FindIndex(t=>t.encounterName== encounterName && t.seq == seq);
            if (index != -1)
                return allCityEBase[index];
            else
                Debug.Log("city encounterBase not found" + encounterName); 
            return null;        
        }


        public void CloseCityETree(CityENames encounterName, int startSeq)
        {
            List<CityEModel> cityEModelOfTree = 
                allCityEModels.Where(t => t.cityEName == encounterName &&
                                            t.encounterSeq > startSeq).ToList();

            cityEModelOfTree.ForEach(t => t.state = CityEState.Completed); 
        }

        public void UnLockNext(CityENames encounterName, int currSeq)
        {
            if(HasNextLvl(encounterName, currSeq))
            {
                CityEModel cityM = 
                allCityEModels.Find(t => t.cityEName == encounterName && t.encounterSeq == (currSeq + 1));
                cityM.state = CityEState.UnLockedNAvail;                     
            }
        }

        public CityEModel GetPreModel(CityENames encounterName, int seq)
        {
            if (seq == 0) return null;
            //seq--; // correction for the 0 based index
            int index =
            allCityEModels.FindIndex(t => t.cityEName == encounterName && t.encounterSeq == (seq));
           if(index != -1)
            {
                return allCityEModels[index];
            }
            Debug.Log(" PreModel not found"+ encounterName + seq);
            return null;             
        }
        public bool HasNextLvl(CityENames encounterName, int currSeq)
        {
            return
            allCityEModels.Any(t => t.cityEName == encounterName && t.encounterSeq == (currSeq + 1)); 
        }
    }
}