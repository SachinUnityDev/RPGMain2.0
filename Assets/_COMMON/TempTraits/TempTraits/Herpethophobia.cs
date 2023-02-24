﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Herpethophobia : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Herpethophobia;

        public override void OnApply(CharController charController)
        {
            this.charController = charController;
        }

        public override void OnEnd()
        {
            
        }
    }
}