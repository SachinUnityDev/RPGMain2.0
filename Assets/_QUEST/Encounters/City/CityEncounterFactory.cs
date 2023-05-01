using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq; 
namespace Quest
{
    [Serializable]
    public class CityEnCounterData
    {
        public CityENames encounterName;
        public int seq;
        public Type type;

        public CityEnCounterData(CityENames encounterName, int seq, Type type)
        {
            this.encounterName = encounterName;
            this.seq = seq;
            this.type = type;
        }
    }

    public class CityEncounterFactory : MonoBehaviour
    {
        public List<CityEnCounterData> allCityEncounterBases;
        [SerializeField] int CityEncounterCount = 0;
        void Start()
        {
            allCityEncounterBases = new List<CityEnCounterData>();
            InitCityEncounter();  // start of the game
        }

        public void InitCityEncounter()
        {

            if (allCityEncounterBases.Count > 0) return;

            var getAllCityEncounters = Assembly.GetAssembly(typeof(CityEncounterBase)).GetTypes()
                            .Where(myType => myType.IsClass
                            && !myType.IsAbstract && myType.IsSubclassOf(typeof(CityEncounterBase)));

            foreach (var cityEncounter in getAllCityEncounters)
            {
                var t = Activator.CreateInstance(cityEncounter) as CityEncounterBase;
                allCityEncounterBases.Add(new CityEnCounterData(t.encounterName,t.seq, cityEncounter));
            }
            CityEncounterCount = allCityEncounterBases.Count;
        }

        public CityEncounterBase GetCityEncounterBase(CityENames encounterName, int seq)
        {
            foreach (var encounterBase in allCityEncounterBases)
            {
                if (encounterBase.encounterName == encounterName && encounterBase.seq == seq)
                {
                    var t = Activator.CreateInstance(encounterBase.type) as CityEncounterBase;
                    return t;
                }
            }
            Debug.Log("Encounter class Not found" + encounterName + "Seq" + seq);
            return null;
        }
    }
}