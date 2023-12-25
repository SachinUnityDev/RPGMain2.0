using Combat;
using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


namespace Combat
{
    public class CombatEndView : MonoBehaviour
    {
        [Header("TBR")]
        [SerializeField] Transform charPortContainer;
        [SerializeField] CombatEndContinueBtn combatEndContinueBtn; 

        [SerializeField] List<CharController> allAllyInclDeadNFled = new List<CharController>();
        EnemyPacksSO enemyPacksSO; 

      
        public void ShowCombatEndView()
        {
            InitCombatEndView();

            transform.GetChild(0).gameObject.SetActive(true); 
        }

        public void InitCombatEndView()
        {
            allAllyInclDeadNFled = CharService.Instance
                                 .allCharsInPartyLocked.Where(t => t.charModel.orgCharMode == CharMode.Ally).ToList();
            combatEndContinueBtn.InitContinueBtn(this); 

            FillCharPort();
            
        }

        public void CloseCombatView()
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        int GetSharedExp()
        {
            enemyPacksSO = CombatService.Instance.currEnemyPackSO;
            int sharedExp = enemyPacksSO.sharedExp;
            int allyExceptDeadNFledCount = 0;
            foreach (CharController charCtrl in allAllyInclDeadNFled)
            {
                if(charCtrl.charModel.stateOfChar == StateOfChar.UnLocked)
                {
                    allyExceptDeadNFledCount++; 
                }
            }
            if (allyExceptDeadNFledCount > 0)
                return sharedExp / allyExceptDeadNFledCount;
            else return 0; 
        }

        void FillCharPort()
        {
            int charSharedExp = GetSharedExp();
            for (int i = 0; i < charPortContainer.childCount; i++)
            {
                if (i < allAllyInclDeadNFled.Count)
                {
                    charPortContainer.GetChild(i).gameObject.SetActive(true);
                    charPortContainer.GetChild(i).GetComponent<PortView>()
                                    .InitPortView(allAllyInclDeadNFled[i].charModel, charSharedExp); 
                }
                else
                {
                    charPortContainer.GetChild(i).gameObject.SetActive(false);
                }
            }
        }



    }
}