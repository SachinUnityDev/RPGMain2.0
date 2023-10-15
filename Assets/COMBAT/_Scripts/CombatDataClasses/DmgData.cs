using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class DmgData  // Dmg Delivered data
    {

        public CharController targetController; 
        public CharController striker; 
        public DamageType dmgRecievedType;
        public AttackType attackType; 
        public StrikeType strikeType;
        public float dmgDelivered; 
        public bool minArmorTriggered;
        public bool maxArmorTriggered;
        public bool minDmgRecieved;
        public bool maxDmgRecieved;

        public DmgData(CharController _targetController, CharController _striker, float _dmgDelivered,  DamageType _dmgRecievedType,
            AttackType _attackType, StrikeType _strikeType,bool _minArmorTriggered, bool _maxArmorTriggered
           ,bool _dodgedTheAttack, bool _minDmg, bool maxDmg)
        {

              striker = _striker;   
              targetController = _targetController;
              dmgRecievedType = _dmgRecievedType;
              strikeType = _strikeType;                      
              minArmorTriggered = _minArmorTriggered;
              maxArmorTriggered = _maxArmorTriggered;
              //dodgedTheAttack = _dodgedTheAttack;
              minDmgRecieved = _minArmorTriggered ;
              dmgDelivered = _dmgDelivered; 

        }

    }

}

