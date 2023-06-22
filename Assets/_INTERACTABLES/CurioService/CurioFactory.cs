using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq; 

namespace Quest
{
    public class CurioFactory : MonoBehaviour
    {
        Dictionary<CurioNames, Type> allCurios;
        [SerializeField] int curioCount = 0;
        void Awake()
        {
            allCurios = new Dictionary<CurioNames, Type>();
            InitCurioBase();//  to test all temp traits in use
        }

        public void InitCurioBase()
        {

            if (allCurios.Count > 0) return;
            var getCurios = Assembly.GetAssembly(typeof(CurioBase)).GetTypes()
                                .Where(myType => myType.IsClass && 
                                !myType.IsAbstract && 
                                myType.IsSubclassOf(typeof(CurioBase)));

            foreach (var curio in getCurios)
            {
                var p = Activator.CreateInstance(curio) as CurioBase;

                allCurios.Add(p.curioName, curio);
                curioCount++;
            }

        }



        public CurioBase GetNewCurio(CurioNames curioName)
        {
            foreach (var curio in allCurios)
            {
                if (curio.Key == curioName)
                {
                    var t = Activator.CreateInstance(curio.Value) as CurioBase;                    
                    return t;
                }
            }
            Debug.Log("Curio base not found" + curioName);
            return null;
        }



    }
}