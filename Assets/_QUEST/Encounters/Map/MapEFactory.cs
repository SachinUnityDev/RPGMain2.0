using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq; 

namespace Quest
{
    public class MapEFactory : MonoBehaviour
    {

        public Dictionary<MapENames, Type> allMapETypes = new Dictionary<MapENames, Type>();
        [SerializeField] int mapECount = 0;

        void Awake()
        {
            InitMapE(); 
        }
        public void InitMapE()
        {
            if (allMapETypes.Count > 0) return;

            var getAllMapE = Assembly.GetAssembly(typeof(MapEbase)).GetTypes()
                            .Where(myType => myType.IsClass
                            && !myType.IsAbstract && myType.IsSubclassOf(typeof(MapEbase)));

            foreach (var mapE in getAllMapE)
            {
                var t = Activator.CreateInstance(mapE) as MapEbase;
                allMapETypes.Add(t.mapEName, mapE);
            }
            mapECount = allMapETypes.Count;
        }

        public MapEbase GetMapEBase(MapENames mapEName)
        {
            foreach (var mapEtype in allMapETypes)
            {
                if (mapEtype.Key == mapEName)
                {
                    var t = Activator.CreateInstance(mapEtype.Value) as MapEbase;
                    return t;
                }
            }
            return null;
        }

    }
}