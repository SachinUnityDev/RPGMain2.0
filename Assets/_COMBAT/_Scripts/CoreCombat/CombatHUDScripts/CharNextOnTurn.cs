using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Combat
{
    public class CharNextOnTurn : MonoBehaviour
    {
        [Header("Rd dsply")]
        [SerializeField] Transform rdDsply;
        [SerializeField] TextMeshProUGUI rdDsplyTxt; 


        [SerializeField] Transform allyPort;
        [SerializeField] Transform enemyPort;
        [SerializeField] Transform ptrTrans;

        [Header(" Round controller")]
        RoundController roundController;

        [SerializeField] int allyTurn;
        [SerializeField] int enemyTurn;
        [SerializeField] int charTurn;

        [SerializeField] bool isEnemyTurn;
        [SerializeField] bool isAllyTurn;

        private void OnEnable()
        {  
            CombatEventService.Instance.OnSOR1 += RoundDsply;
            CombatEventService.Instance.OnCharClicked += (CharController c)=>ToggleRdDsply();
             
            Reset(0);
        }
        private void OnDisable()
        {
            //CombatEventService.Instance.OnSOT -= ChgPort;
            CombatEventService.Instance.OnCharClicked += (CharController c) => ToggleRdDsply();

        }


        #region Char Portrait 
        private void Reset(int roundNo)
        {
            allyTurn = 0;
            enemyTurn = 0;
            charTurn = 0;
            isEnemyTurn = false;
            isAllyTurn = false;
            allyPort.gameObject.SetActive(false);
            enemyPort.gameObject.SetActive(false);

        }
        void ChgPort()
        {
            CharController charController = CombatService.Instance.currCharOnTurn; 
           roundController = CombatService.Instance.roundController;        
            charTurn = CharService.Instance.allCharInCombat.FindIndex(t=>t.charModel.charID== charController.charModel.charID);
            CharMode charMode = charController.charModel.charMode; 
            if(charTurn== 0)
            {
                allyTurn = 0;
                enemyTurn = 0;
            }
            if(charTurn < CharService.Instance.allCharInCombat.Count)
            {
                if(charMode== CharMode.Ally)
                {
                    isEnemyTurn = false; isAllyTurn= true;
                }
                if(charMode== CharMode.Enemy) 
                {
                    isEnemyTurn = true; isAllyTurn = false;
                }
            }
            else
            {
               // charTurn = 0; 
               allyTurn= 0;
               enemyTurn= 0;
            }
            AllyPortUpdate();
            EnemyPortUpdate();
            // charTurn++;
        }
        void AllyPortUpdate()
        {           
            if (allyTurn < roundController.allyTurnOrder.Count)
            {
                // show port
                allyPort.gameObject.SetActive(true);
                CharacterSO charSO = CharService.Instance
                    .GetCharSO(roundController.allyTurnOrder[allyTurn].charModel.charName);
                allyPort.GetChild(0).GetChild(0).GetComponent<Image>().sprite = charSO.charHexPortrait;
                if (isAllyTurn)
                    allyTurn++;
            }
            else
            {
                // disable port
                allyTurn = 0;
                allyPort.gameObject.SetActive(false);
            }
         
        }
        void EnemyPortUpdate()
        {
          
            if (enemyTurn < roundController.enemyTurnOrder.Count)
            {
                // show port
                enemyPort.gameObject.SetActive(true);
                CharacterSO charSO = CharService.Instance
                    .GetCharSO(roundController.enemyTurnOrder[enemyTurn].charModel.charName);
                enemyPort.GetChild(0).GetChild(0).GetComponent<Image>().sprite = charSO.charHexPortrait;
                if (isEnemyTurn)
                    enemyTurn++;
            }
            else
            {
                // disable port
                enemyTurn = 0;
                enemyPort.gameObject.SetActive(false);
            }
        }

        #endregion

#region round DSLY

        void RoundDsply(int rd)
        {
            rdDsplyTxt.text = rd.ToString();
        }
        void ToggleRdDsply()
        {
            if(CombatService.Instance.combatState == CombatState.INCombat_InSkillSelected ||
                CombatService.Instance.combatState == CombatState.INCombat_normal)
            {
                rdDsply.gameObject.SetActive(true);
            }
            else
            {
                rdDsply.gameObject.SetActive(false);
            }
            
        }


#endregion

    }
}