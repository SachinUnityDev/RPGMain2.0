using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{


    public class PermaTraitBase : MonoBehaviour
    {

        public virtual PermaTraitName permTraitName { get; set; }
        public PermaTraitSO permaTraitSO { get; set; }

        public virtual int charID { get; set; }

        // protected CharModData charModData; 
        public virtual void ApplyTrait(CharController charController)
        {
            permaTraitSO =
            PermanentTraitsService.Instance.allPermaTraitSO.GetPermaTraitSO(permTraitName);


        }
    }
}