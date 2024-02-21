using Interactables;
using Spine;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;



namespace Common
{
    public class WeekOfScholars : WeekEventBase
    {
        public override WeekEventsName weekName => WeekEventsName.WeekOfScholars;

        public override void OnWeekApply()
        {
            
        }
        public override void OnWeekBonusClicked()
        {
            //  "%20 Lore Book ..% 30 Abbas Gain EXP 30...% 20 Abbas Gain EXP 50...% 30 nothing happens"
            List<float> chances = new List<float>() { 20f, 30f, 20f, 30f }; 
            int res = chances.GetChanceFrmList();
            switch (res)
            {
                case 1:                    
                    ItemData itemData = new ItemData(ItemType.LoreBooks, (int)LoreBookNames.LandsOfShargad);
                    Iitems item = ItemService.Instance.GetNewItem(itemData);
                    InvService.Instance.invMainModel.AddItem2CommORStash(item);
                    resultStr = $"{LoreBookNames.LandsOfShargad}"; 
                break;
                case 2:
                    CharController charAbbas = CharService.Instance.GetAbbasController(CharNames.Abbas);
                    int exp = (int)UnityEngine.Random.Range(30f, 50f);
                    charAbbas.ExpPtsGain(exp);
                    resultStr = $"{exp} Exp";
                break;
                case 3:
                    int ran = ItemService.Instance.allItemSO.GetRandomItem(ItemType.Scrolls);
                    ScrollSO scrollSO = ItemService.Instance.allItemSO.allScrollSO[ran];
                    ItemData itemData1 = new ItemData(ItemType.Scrolls, (int)scrollSO.scrollName);
                    Iitems item1 = ItemService.Instance.GetNewItem(itemData1);
                    InvService.Instance.invMainModel.AddItem2CommORStash(item1);
                    resultStr = $"{scrollSO.scrollName}"; 
                break;
                case 4:
                    resultStr = $"Nothing!";
                    // nothing happens
                    break;
            }
        }
    }
}