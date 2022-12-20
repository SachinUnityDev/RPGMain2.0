using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;

namespace Quest
{

    public class LandscapeService : MonoSingletonGeneric<LandscapeService>  
    {
        public event Action<LandscapeNames> OnLandScapeChg; 

        public LandscapeModel landscapeModel; 
        public List<LandscapeController> landscapeControllers; // distributed controller
        public LandScapeViewController LandScapeViewController; 

    }
}