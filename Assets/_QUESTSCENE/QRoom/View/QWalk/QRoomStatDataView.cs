using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Quest
{


    public class QRoomStatDataView : MonoBehaviour
    {
        [SerializeField] AttribName attribName;
        [SerializeField] StatName statName;
        void Start()
        {

        }

        public void InitStat(CharModel charModel)
        {
            if (statName != StatName.None)
            {
                foreach (StatData stat in charModel.statList)
                {
                    if (stat.statName == statName)
                        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text
                            = stat.currValue + "/" + stat.maxLimit;
                }
            }
            else if (attribName != AttribName.None)
            {
                foreach (AttribData attrib in charModel.attribList)
                {
                    if (attrib.AttribName == attribName)
                        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text
                            = attrib.currValue.ToString();
                }
            }
            else // Char main Exp 
            {
                int totalExp = CharService.Instance.lvlNExpSO.GetTotalExpPts4Lvl(charModel.charLvl);
                 transform.GetChild(1).GetComponent<TextMeshProUGUI>().text
                         = charModel.mainExp.ToString() + "/" + totalExp;
            }
        }
    }
}