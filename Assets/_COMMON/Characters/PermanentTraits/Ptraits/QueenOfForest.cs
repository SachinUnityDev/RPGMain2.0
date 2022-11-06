﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class QueenOfForest : PermTraitBase
    {
        //Can't get ambushed in rainforest, jungle and swamps	
        //Doesn't suffer rainforest, jungle and swamp handicaps 
        //Finds her way in rainforest and jungle
        public override TraitBehaviour traitBehaviour => TraitBehaviour.Positive;

        public override PermanentTraitName permTraitName => PermanentTraitName.QueenOfTheForest;
        public override void ApplyTrait(CharController charController )
        {

        }   


    }



}

