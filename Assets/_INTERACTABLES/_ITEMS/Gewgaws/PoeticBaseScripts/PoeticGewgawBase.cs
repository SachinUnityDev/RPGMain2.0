using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace Interactables
{
    public abstract class PoeticGewgawBase 
    {
        public abstract PoeticGewgawNames poeticGewgawName { get; }
        public CharController charController { get; set; }
        public List<int> allBuffIds { get; set; } = new List<int>();
        public List<int> dmgAltBuffIndex { get; set; } = new List<int>();
        public List<int> allSkillDmgModBuffIndex { get; set; } = new List<int>();
        public List<int> expIndex { get; set; } = new List<int>();  
        public List<string> displayStrs { get; set; } = new List<string>(); 
        public abstract void PoeticInit();  // connect the charController and other things
        public abstract void EquipGewgawPoetic();
        public virtual void UnEquipPoetic()
        {
            allBuffIds.ForEach(t => charController.buffController.RemoveBuff(t));
            dmgAltBuffIndex.ForEach(t => charController.strikeController.RemoveDmgAltBuff(t));
            allSkillDmgModBuffIndex.ForEach(t => charController.skillController.RemoveSkillDmgModBuff(t));
            allBuffIds.Clear();
            dmgAltBuffIndex.Clear();
            allSkillDmgModBuffIndex.Clear();
        }

    }
}

