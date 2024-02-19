using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class Scary : PassiveSkillBase
    {
        public override PassiveSkillName passiveSkillName => PassiveSkillName.Scary;

        private CharNames _charName; 
        public override CharNames charName { get => _charName; set => _charName = value;  }

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override string desc => "";

     
        public override void ApplyFX()
        {
           
        }

        protected override void DisplayFX1(PassiveSkillName passiveSkillName)
        {
            if (this.passiveSkillName != passiveSkillName) return;
            str0 = "+50%<style=Bleed>Physical Dmg</style> vs <style=States>Fearful, Despaired, Horrified</style>";
            PassiveSkillService.Instance.descLines.Add(str0);

        }
    }


}
