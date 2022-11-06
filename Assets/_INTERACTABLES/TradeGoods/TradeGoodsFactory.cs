using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.Linq;

namespace Interactables
{
    public class TradeGoodsFactory : MonoBehaviour
    {
        public Dictionary<TgNames, Type> allTGs;
        void Start()
        {
            allTGs = new Dictionary<TgNames, Type>();
            InitTradeGoods();  // ON GAME START 
        }

        public void InitTradeGoods()
        {

            if (allTGs.Count > 0) return;

            var getAllTGs = Assembly.GetAssembly(typeof(TradeGoodBase)).GetTypes()
                                 .Where(myType => myType.IsClass
                                 && !myType.IsAbstract && myType.IsSubclassOf(typeof(TradeGoodBase)));

            foreach (var tg in getAllTGs)
            {
                var t = Activator.CreateInstance(tg) as TradeGoodBase;
                allTGs.Add(t.tgName, tg);
            }
        }

        public TradeGoodBase GetTGBase(TgNames _tgName)
        {
            foreach (var tg in allTGs)
            {
                if (tg.Key == _tgName)
                {
                    var t = Activator.CreateInstance(tg.Value) as TradeGoodBase;
                    return t;
                }
            }
            Debug.Log("trade goods base Not found" + _tgName);
            return null;
        }


    }


}


