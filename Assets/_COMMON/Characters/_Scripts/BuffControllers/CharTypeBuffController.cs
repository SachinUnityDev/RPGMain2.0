using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

    public class RaceCultClassBuffData
    {
        public CauseData causeData; 
        public int buffID;
        public AttribName attribName;
        public int valChg; 
        public RaceType raceType;
        public ClassType classType;
        public CultureType cultureType;

        public RaceCultClassBuffData(CauseData causeData, int buffID, RaceType raceType, AttribName attribName,
         int valChg)
        {
            this.attribName= attribName;
            this.valChg= valChg;
            this.causeData = causeData;
            this.buffID = buffID;
            this.raceType = raceType;
        }
        public RaceCultClassBuffData(CauseData causeData, int buffID, CultureType cultureType, AttribName attribName,
         int valChg)
        {
            this.attribName = attribName;
            this.valChg = valChg;
            this.causeData = causeData;
            this.buffID = buffID;
            this.cultureType = cultureType;
        }
        public RaceCultClassBuffData(CauseData causeData, int buffID, ClassType classType, AttribName attribName,
         int valChg)
        {
            this.attribName = attribName;
            this.valChg = valChg;
            this.causeData = causeData;
            this.buffID = buffID;
            this.classType = classType;
        }
    }


    public class CauseData
    {
        public CauseType causeType;  // add cause name here 
        public int causeName;
        public int causeByCharID;
        public int effectedCharNameID;

        public CauseData(CauseType causeType, int causeName, int causeByCharID, int effectedCharNameID)
        {
            this.causeType = causeType;
            this.causeName = causeName;
            this.causeByCharID = causeByCharID;
            this.effectedCharNameID = effectedCharNameID;
        }
    }
    public class CharTypeBuffController : MonoBehaviour
    {
        public List<RaceCultClassBuffData> allBuffData = new List<RaceCultClassBuffData>();
        CharController charController;

        private void Start()
        {
            charController = GetComponent<CharController>();    
        }

        public int ApplyRaceBuff(CauseType causeType, int causeName, CharController causeByCtrl, RaceType raceType, AttribName attribName, int valChg, bool isBuff)
        {
            if (charController.charModel.raceType != raceType)
                return -1;

            int causeByCharID = causeByCtrl.charModel.charID;
            CauseData causeData = new CauseData(causeType, causeName, causeByCharID, charController.charModel.charID);
           
            int buffID = 
                charController.buffController.ApplyBuff(causeType, (int)causeName, causeByCharID,
                                                    attribName, valChg, TimeFrame.Infinity, -1, isBuff); 
           

            RaceCultClassBuffData charTypeBuffData = new RaceCultClassBuffData(causeData,buffID , raceType, attribName, valChg);

            allBuffData.Add(charTypeBuffData);
            return buffID;
        }

        public int ApplyClassBuff(CauseType causeType, int causeName, CharController causeByCtrl,  ClassType classType, AttribName attribName, int valChg, bool isBuff)
        {
            if (charController.charModel.classType != classType)
                return -1;

            int causeByCharID = causeByCtrl.charModel.charID;
            CauseData causeData = new CauseData(causeType, causeName, causeByCharID, charController.charModel.charID);


            int buffID =
                charController.buffController.ApplyBuff(causeData.causeType, causeData.causeName, causeData.causeByCharID,
                attribName, valChg, TimeFrame.Infinity, -1, isBuff);


            RaceCultClassBuffData charTypeBuffData = new RaceCultClassBuffData(causeData, buffID, classType, attribName, valChg);

            allBuffData.Add(charTypeBuffData);
            return buffID;
        }
        public int ApplyCultBuff(CauseType causeType, int causeName, CharController causeByCtrl, CultureType cultType, AttribName attribName, int valChg, bool isBuff)
        {
            if (charController.charModel.cultType != cultType)
                return -1;

            int causeByCharID = causeByCtrl.charModel.charID;
            CauseData causeData = new CauseData(causeType, causeName, causeByCharID, charController.charModel.charID);

            int buffID =
            charController.buffController.ApplyBuff(causeData.causeType, causeData.causeName, causeData.causeByCharID,
                attribName, valChg, TimeFrame.Infinity, -1, isBuff);


            RaceCultClassBuffData charTypeBuffData = new RaceCultClassBuffData(causeData, buffID, cultType, attribName, valChg);

            allBuffData.Add(charTypeBuffData);
            return buffID;
        }
        public void RemoveRaceBuff(RaceType raceType) 
        {
            foreach (RaceCultClassBuffData buff in allBuffData)
            {
                if(buff.raceType == raceType)
                {
                    charController.buffController.RemoveBuff(buff.buffID); 
                }
            }        
        }
        public void RemoveClassBuff(ClassType classType)
        {
            foreach (RaceCultClassBuffData buff in allBuffData)
            {
                if (buff.classType == classType)
                {
                    charController.buffController.RemoveBuff(buff.buffID);
                }
            }
        }
        public void RemoveCultBuff(CultureType cultType)
        {
            foreach (RaceCultClassBuffData buff in allBuffData)
            {
                if (buff.cultureType == cultType)
                {
                    charController.buffController.RemoveBuff(buff.buffID);
                }
            }
        }
    }
}