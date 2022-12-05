using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.Linq;


namespace Interactables
{
    public class GenGewgawFactory : MonoBehaviour
    {
        public Dictionary<GenGewgawNames, Type> allGenGewgaws; 

        public Dictionary<PrefixNames, Type> allPrefixes;
        public Dictionary<SuffixNames, Type> allSuffixes;


        
        void Start()
        {
            allPrefixes = new Dictionary<PrefixNames, Type>();
            InitPrefixes();  // ON GAME START 

            allSuffixes = new Dictionary<SuffixNames, Type>();
            InitSuffixes();

            allGenGewgaws = new Dictionary<GenGewgawNames, Type>();



        }

        public void InitGenGewagaws()
        {
            if (allGenGewgaws.Count > 0) return;

            var getallGenGewgaws = Assembly.GetAssembly(typeof(GenGewgawBase)).GetTypes()
                                 .Where(myType => myType.IsClass
                                 && !myType.IsAbstract && myType.IsSubclassOf(typeof(GenGewgawBase)));

            foreach (var genGewgaws in getallGenGewgaws)
            {
                var t = Activator.CreateInstance(genGewgaws) as GenGewgawBase;
                allGenGewgaws.Add(t.genGewgawNames, genGewgaws);
            }
        }
        public GenGewgawBase GetGenGewgaws(GenGewgawNames genGewgawName, PrefixNames prefixName
            , SuffixNames suffixName)
        {
            foreach (var gewgaw in allGenGewgaws)
            {
                if (gewgaw.Key == genGewgawName)
                {
                    var t = Activator.CreateInstance(gewgaw.Value) as GenGewgawBase;

                    t.suffixBase = GetSuffixBase(suffixName);
                    t.prefixBase = GetPrefixBase(prefixName);

                    return t;
                }
            }
            Debug.Log("gewgawbase class Not found" + genGewgawName);
            return null;

        }

        public void InitSuffixes()
        {

            if (allSuffixes.Count > 0) return;

            var getallsuffixes = Assembly.GetAssembly(typeof(SuffixBase)).GetTypes()
                                 .Where(myType => myType.IsClass
                                 && !myType.IsAbstract && myType.IsSubclassOf(typeof(SuffixBase)));

            foreach (var suffix in getallsuffixes)
            {
                var t = Activator.CreateInstance(suffix) as SuffixBase;
                allSuffixes.Add(t.suffixName, suffix);
            }
        }

        public SuffixBase GetSuffixBase(SuffixNames _suffixName)
        {
            foreach (var suffix in allSuffixes)
            {
                if (suffix.Key == _suffixName)
                {
                    var t = Activator.CreateInstance(suffix.Value) as SuffixBase;
                    return t;
                }
            }
            Debug.Log("suffix class Not found" + _suffixName);
            return null;
        }
        public void InitPrefixes()
        {

            if (allPrefixes.Count > 0) return;

            var getallPrefixes = Assembly.GetAssembly(typeof(PrefixBase)).GetTypes()
                                 .Where(myType => myType.IsClass
                                 && !myType.IsAbstract && myType.IsSubclassOf(typeof(PrefixBase)));

            foreach (var prefix in getallPrefixes)
            {
                var t = Activator.CreateInstance(prefix) as PrefixBase;
                allPrefixes.Add(t.prefixName, prefix);
            }
        }

        public PrefixBase GetPrefixBase(PrefixNames _prefixName)
        {
            foreach (var prefix in allPrefixes)
            {
                if (prefix.Key == _prefixName)
                {
                    var t = Activator.CreateInstance(prefix.Value) as PrefixBase;
                    return t;
                }
            }
            Debug.Log("Prefix class Not found" + _prefixName);
            return null;
        }

    }


}

