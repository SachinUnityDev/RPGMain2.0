using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using System; 

namespace Common
{

    public class PermaTraitData
    {
        public PermaTraitName permaName;
        public CharNames effectedCharName;
        public Vector3 effectCharPos;

        public PermaTraitData(PermaTraitName permaName, CharNames effectedCharName
                                , Vector3 effectCharPos)
        {
            this.permaName = permaName;
            this.effectedCharName = effectedCharName;
            this.effectCharPos = effectCharPos;
        }
    }

    public class PermanentTraitsService : MonoSingletonGeneric<PermanentTraitsService>
    {

        // get all permanet traits from the SOs 
        // loop thru them add perma permane traits 
        // ensure no traits are added twice 
        // apply trait to be controlled from here 
        // event raised on addition of change stat from done from here
        public List<PermaTraitData> allPermaTraitData = new List<PermaTraitData>(); 
        PermaTraitsFactory permaTraitsFactory;
        public event Action<PermaTraitData> OnPermaTraitAdded;

        public AllPermaTraitSO allPermaTraitSO; 


        void Start()
        {
            permaTraitsFactory = GetComponent<PermaTraitsFactory>();
            CombatEventService.Instance.OnSOC += AddPermaTrait2AllChar; 
        }

        public void AddPermaTrait2AllChar()
        {
            Debug.Log("all char perma"); 
            CharService.Instance.allyInPlayControllers.ForEach(t => AddPermaTraits2Char(t.gameObject)); 
            
        }

        public void AddPermaTraits2Char(GameObject go)
        {
            permaTraitsFactory.InitPermTraits();  // timing thing 

            CharController charctrl = go.GetComponent<CharController>();
            //foreach (TraitsDataSO charptraits in allPermTraitsSO)   // list of sos
            //{

            //    if (charctrl.charModel.cultType == charptraits.cultType)
            //    {
            //        foreach (PermTraitsINChar traitvalue in charptraits.PermTraitsINCharList)
            //        {
            //            permaTraitsFactory.AddPermTrait(traitvalue.permanentTraitName, go);

            //            PermaTraitData permaTrait = new PermaTraitData(traitvalue.permanentTraitName
            //                , charctrl.charModel.charName, go.transform.position);

            //            allPermaTraitData.Add(permaTrait); 

            //            OnPermaTraitAdded?.Invoke(permaTrait); 

            //        }
            //    }
            //}
          //  permaTraitsFactory.ApplyPermTraits(go);

        }




    }

}
