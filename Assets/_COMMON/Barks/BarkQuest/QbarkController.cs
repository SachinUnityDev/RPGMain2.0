using Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Quest
{


    public class QbarkController : MonoBehaviour
    {
        public QBarkSO qBarkSO;
        [SerializeField]List<BarkData> allBarkDataSelect = new List<BarkData>();
        void Start()
        {

        }

        
        public List<BarkData> GetBarkData(List<QBarkNames> qBarkNames)
        {
            allBarkDataSelect = qBarkSO.GetBarkData(qBarkNames);            
            RemoveOnQModeBasis();
            RemoveOnCharNameBasis();


            return allBarkDataSelect;
        }

        void RemoveOnCharNameBasis()
        {
            foreach (BarkData barkData in allBarkDataSelect.ToList())
            {
                foreach (BarkCharData lineData in barkData.charData)
                {
                    if (lineData.charName == CharNames.None)
                        continue;
                    if(!CharService.Instance.allCharsInPartyLocked
                        .Any(t=>t.charModel.charName == lineData.charName))
                    {
                        allBarkDataSelect.Remove(barkData);    break;                    
                    }
                }                
            }
        }

        void RemoveOnQModeBasis()
        {
            QuestMode questMode = QuestMissionService.Instance.currQuestMode;
            foreach (BarkData barkData in allBarkDataSelect.ToList())
            {
                if (barkData.InValidQMode[0] == QuestMode.None)
                    continue; 
                if(barkData.InValidQMode.Any(t=>t == questMode))
                    allBarkDataSelect.Remove(barkData);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                GetBarkData(new List<QBarkNames>() { QBarkNames.Qbark_001
                    , QBarkNames.Qbark_003, QBarkNames.Qbark_006, QBarkNames.Qbark_007,
                      QBarkNames.Qbark_012  });
                

            }
        }

    }
}