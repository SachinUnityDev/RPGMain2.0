using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public abstract class PoeticSetBase 
    {
        public abstract PoeticSetName poeticSetName { get; }
        public CharController charController { get; set; }
        public List<int> allBuffIds { get; set; } = new List<int>();
        public List<int> dmgAltBuffIndex { get; set; }= new List<int>();

        public List<int> allSkillDmgModBuffIndex { get; set; } = new List<int>();
        public abstract void BonusFx();
        public virtual void RemoveBonusFX()
        {
            allBuffIds.ForEach(t=>charController.buffController.RemoveBuff(t));
            dmgAltBuffIndex.ForEach(t => charController.strikeController.RemoveDmgAltBuff(t));
            allSkillDmgModBuffIndex.ForEach(t => charController.skillController.RemoveSkillDmgModBuff(t)); 
            allBuffIds.Clear();
            dmgAltBuffIndex.Clear();
            allSkillDmgModBuffIndex.Clear();
        } 
    }
}