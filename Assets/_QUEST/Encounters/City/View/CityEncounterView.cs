using Common;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using TMPro;
using UnityEngine;

namespace Quest
{
    public class CityEncounterView : MonoBehaviour, IPanel
    {

        public Transform mainPage;
        public Transform resultPage;

        CityEncounterBase cityBase;

        public void Init()
        {

        }

        public void Load()
        {

        }

        public void ShowEncounter(CityEncounterNames encounterName, int seq)
        {
            cityBase = EncounterService.Instance.cityEFactory.GetCityEncounterBase(encounterName, seq);
        }

        public void UnLoad()
        {

        }
    }
}