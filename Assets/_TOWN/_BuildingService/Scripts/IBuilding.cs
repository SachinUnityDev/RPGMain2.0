using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Town
{
    public interface IBuilding
    {
        BuildingNames buildingName { get;  }
        void Init(); 
        void Load();

        void UnLoad(); 


    }



}

