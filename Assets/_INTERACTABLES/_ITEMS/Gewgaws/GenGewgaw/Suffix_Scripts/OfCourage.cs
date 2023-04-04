using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class OfCourage : SuffixBase, IFolkloric, IEpic
    {       
        public override SuffixNames suffixName => SuffixNames.OfCourage;

        //of Courage    NA	3-5 fortitude org	6-9 fortitude origin
        int valFolk, valEpic;

        public void FolkloricInit()
        {
            valFolk = Random.Range(3, 6);
            string str = $"+{valFolk} Fortitude Origin";
            displayStrs.Add(str);
        }

        public void ApplyFXFolkloric()
        {
            charController = InvService.Instance.charSelectController;     

            int index = 
            charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                , charController.charModel.charID, AttribName.fortOrg, valFolk, TimeFrame.Infinity, -1, true);

            buffIndex.Add(index);
        }
        public void EpicInit()
        {
            valEpic = Random.Range(6, 10);
            string str = $"+{valEpic} Fortitude Origin";
            displayStrs.Add(str);
        }

        public void  ApplyFXEpic()
        {
            charController = InvService.Instance.charSelectController;
            int index = 
            charController.buffController.ApplyBuff(CauseType.SuffixGenGewgaw, (int)suffixName
                , charController.charModel.charID, AttribName.fortOrg, valEpic, TimeFrame.Infinity, -1, true);

            buffIndex.Add(index);
        }

        public void RemoveFXFolkloric()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            buffIndex.Clear();
        }

    
        public void RemoveFXEpic()
        {
            buffIndex.ForEach(t => charController.buffController.RemoveBuff(t));
            buffIndex.Clear();
        }

    }


}

