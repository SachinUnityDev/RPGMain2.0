//using Combat;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//namespace Common
//{

//    //public class StatBuffData
//    //{
//    //    public int dmgBuffID;
//    //    public bool isBuff;   // true if BUFF and false if DEBUFF
//    //    public int startRoundNo;
//    //    public TimeFrame timeFrame;
//    //    public int buffedNetTime;
//    //    public int buffCurrentTime;
//    //    public DmgAltData altData;  // contains value for the buff        

//    //    public StatBuffData(int dmgBuffID, bool isBuff, int startRoundNo, TimeFrame timeFrame
//    //                        , int buffedNetTime, DmgAltData altData)
//    //    {
//    //        this.dmgBuffID = dmgBuffID;
//    //        this.isBuff = isBuff;
//    //        this.startRoundNo = startRoundNo;
//    //        this.timeFrame = timeFrame;
//    //        this.buffedNetTime = buffedNetTime;
//    //        this.buffCurrentTime = 0;// time counter for the dmgBuff
//    //        this.altData = altData;
//    //    }
//    //}


//    public class BuffOnStatValController : MonoBehaviour
//    {
//        #region DAMAGE RECEIVE BUFF ALTERER

//        public List<DmgBuffData> allDmgReceivedBuffData = new List<DmgBuffData>();

//        int dmgBuffID = 0;
//        public int ApplyDmgReceivedAltBuff(float valPercent, CauseType causeType, int causeName, int causeByCharID,
//             TimeFrame timeFrame, int netTime, bool isBuff,
//            AttackType attackType = AttackType.None, DamageType dmgType = DamageType.None,
//            CultureType cultType = CultureType.None, RaceType raceType = RaceType.None)
//        {
//            dmgBuffID = allDmgReceivedBuffData.Count + 1;

//            DmgAltData dmgAltData = new DmgAltData(valPercent, attackType, dmgType, cultType, raceType);
//            int startRoundNo = CombatEventService.Instance.currentRound;

//            DmgBuffData dmgBuffData = new DmgBuffData(dmgBuffID, isBuff, startRoundNo, timeFrame
//                            , netTime, dmgAltData);

//            allDmgReceivedBuffData.Add(dmgBuffData);
//            return dmgBuffID;
//        }
//        public void EOCTickDmgBuff()
//        {
//            foreach (DmgBuffData dmgBuffData in allDmgReceivedBuffData.ToList())
//            {
//                if (dmgBuffData.timeFrame == TimeFrame.EndOfCombat)
//                {
//                    RemoveDmgBuffData(dmgBuffData);
//                }
//            }
//        }

//        public void EORTick(int roundNo)  // to be completed
//        {
//            foreach (DmgBuffData dmgBuffData in allDmgReceivedBuffData.ToList())
//            {
//                if (dmgBuffData.timeFrame == TimeFrame.EndOfRound)
//                {
//                    if (dmgBuffData.buffCurrentTime >= dmgBuffData.buffedNetTime)
//                    {
//                        RemoveDmgBuffData(dmgBuffData);
//                    }
//                    dmgBuffData.buffCurrentTime++;
//                }
//            }
//        }

//        void RemoveDmgBuffData(DmgBuffData dmgBuffData)
//        {
//            allDmgReceivedBuffData.Remove(dmgBuffData);
//        }
//        public bool RemoveDmgReceivedAltBuff(int dmgBuffID)
//        {
//            int index = allDmgReceivedBuffData.FindIndex(t => t.dmgBuffID == dmgBuffID);
//            if (index == -1) return false;
//            DmgBuffData dmgBuffData = allDmgReceivedBuffData[index];
//            RemoveDmgBuffData(dmgBuffData);
//            return true;
//        }

//        public float GetDmgReceivedAlt(CharModel strikerModel, AttackType attackType = AttackType.None
//                                         , DamageType damageType = DamageType.None)
//        {
//            // 20% physical attack against beastmen            
//            foreach (DmgBuffData dmgBuffData in allDmgReceivedBuffData.ToList())
//            {
//                DmgAltData dmgAltData = dmgBuffData.altData;
//                if (dmgAltData.damageType != DamageType.None && dmgAltData.damageType == damageType)// Damage Type Block 
//                {
//                    float val = 0;
//                    if (dmgAltData.raceType != RaceType.None
//                        && dmgAltData.raceType == strikerModel.raceType
//                                && dmgAltData.cultType != CultureType.None
//                                && dmgAltData.cultType == strikerModel.cultType)
//                    {

//                        val = dmgAltData.valPercent; // COMBO RACE AND CULT

//                    }
//                    else   // NOT A COMBO OF RACE AND CULT
//                    {
//                        if (dmgAltData.raceType != RaceType.None
//                                  && dmgAltData.raceType == strikerModel.raceType)
//                        {
//                            val = dmgAltData.valPercent;
//                        }
//                        if (dmgAltData.cultType != CultureType.None
//                                        && dmgAltData.cultType == strikerModel.cultType)
//                        {
//                            val = dmgAltData.valPercent;
//                        }
//                    }
//                    return val;
//                }
//                else if (dmgAltData.attackType != AttackType.None && dmgAltData.attackType == attackType)// Attack type block
//                {
//                    float val = 0;
//                    if (dmgAltData.raceType != RaceType.None
//                        && dmgAltData.raceType == strikerModel.raceType
//                                && dmgAltData.cultType != CultureType.None
//                                && dmgAltData.cultType == strikerModel.cultType)
//                    {

//                        val = dmgAltData.valPercent; // COMBO RACE AND CULT

//                    }
//                    else   // NOT A COMBO OF RACE AND CULT
//                    {
//                        if (dmgAltData.raceType != RaceType.None
//                                  && dmgAltData.raceType == strikerModel.raceType)
//                        {
//                            val = dmgAltData.valPercent;
//                        }
//                        if (dmgAltData.cultType != CultureType.None
//                                        && dmgAltData.cultType == strikerModel.cultType)
//                        {
//                            val = dmgAltData.valPercent;
//                        }
//                    }
//                    return val;
//                }
//                else if (dmgAltData.attackType == AttackType.None && dmgAltData.damageType == DamageType.None)
//                {
//                    return dmgAltData.valPercent;
//                }
//            }
//            return 0f;
//        }
//        #endregion




//    }
//}