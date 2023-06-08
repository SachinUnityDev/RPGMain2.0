using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq; 

namespace Quest
{

    public class LandscapeFactory : MonoBehaviour
    {
        [Header("Landscapes")]
        Dictionary<LandscapeNames, Type> allLandscapes = new Dictionary<LandscapeNames, Type>();

        [SerializeField] int landscapeCount = 0;

        private void Start()
        {
            LandscapesInit();      
        }
        #region LANDSCAPE 
        void LandscapesInit()
        {
            if (allLandscapes.Count > 0) return;

            var getAllLandscapes = Assembly.GetAssembly(typeof(LandscapeBase)).GetTypes()
                                 .Where(myType => myType.IsClass
                                 && !myType.IsAbstract && myType.IsSubclassOf(typeof(LandscapeBase)));

            foreach (var landscape in getAllLandscapes)
            {
                var t = Activator.CreateInstance(landscape) as LandscapeBase;
                allLandscapes.Add(t.landscapeName, landscape);
            }
            landscapeCount = allLandscapes.Count;
        }

        public LandscapeBase GetNewLandscape(LandscapeNames _landscapeName)
        {
            foreach (var landscape in allLandscapes)
            {
                if (landscape.Key == _landscapeName)
                {
                    var t = Activator.CreateInstance(landscape.Value) as LandscapeBase;
                    return t;
                }
            }
            Debug.Log("landscape class Not found" + _landscapeName);
            return null;
        }
        #endregion


    }
}