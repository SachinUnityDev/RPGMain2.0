using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;

namespace Quest
{

    public class LandscapeService : MonoSingletonGeneric<LandscapeService>  
    {
        public event Action<LandscapeNames> OnLandscapeEnter;
        public event Action<LandscapeNames> OnLandscapeExit;

        public LandscapeNames currLandscape; 

        public List<LandscapeModel> allLandScapeModels = new List<LandscapeModel>();         
        
        public List<LandscapeController> landscapeControllers; // distributed controller
        public LandScapeViewController LandScapeViewController; 
    }
}