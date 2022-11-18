using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class DmgModel
    {
        public float dmgAltCombo; // percent only
        public float dmgAltRaceType; // percent only
        public float dmgAltCultType; // percent only 
        public float dmgAltAttackType; // percent only 
        public float dmgAltDmgType; // percent only 
    }

    public class DmgAltData
    {
        public AttackType attackType = AttackType.None;
        public DamageType damageType = DamageType.None;
        public CultureType cultType = CultureType.None;
        public RaceType raceType = RaceType.None;
        public float valPercent = 0f;

        public DmgAltData(float valPercent, AttackType attackType, DamageType damageType, CultureType cultType, RaceType raceType)
        {
            this.attackType = attackType;
            this.damageType = damageType;
            this.cultType = cultType;
            this.raceType = raceType;
            this.valPercent = valPercent;
        }

    }


