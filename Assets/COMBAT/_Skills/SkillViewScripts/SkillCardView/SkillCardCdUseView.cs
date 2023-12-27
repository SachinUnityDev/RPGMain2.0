using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Common
{
    public class SkillCardCdUseView : MonoBehaviour
    {
        [Header(" Sprite TBR")]
        [SerializeField] Sprite useN;
        [SerializeField] Sprite useNA;

        [SerializeField] Sprite cdN;
        [SerializeField] Sprite cdNA;

        [SerializeField] SkillModel skillModel;
        [SerializeField] bool isCdBased = false;
        

        private void Start()
        {
            
        }
        private void OnDisable()
        {
            
        }
        public void InitSkillCdNUse(SkillModel skillModel)
        {
            this.skillModel= skillModel;
      
            if(skillModel.maxUsagePerCombat> 0)
            {
                isCdBased= false;
                FillUseImg();
            }
            else
            {
                isCdBased = true;
                FillCdImg();
            }    
        }

        void FillUseImg()
        {   
                int j = skillModel.maxUsagePerCombat; 
                for (j = 0; j < skillModel.maxUsagePerCombat; j++)
                {
                    transform.GetChild(j).gameObject.SetActive(true);
                    if (j < skillModel.maxUsagePerCombat - skillModel.noOfTimesUsed)
                        transform.GetChild(j).GetComponent<Image>().sprite = useN;
                    else
                        transform.GetChild(j).GetComponent<Image>().sprite = useNA;                    
                }
                for (int k = skillModel.maxUsagePerCombat; k < transform.childCount; k++)
                {
                    transform.GetChild(k).gameObject.SetActive(false);  
                }
            
        }
        void FillCdImg()
        {

            int j = 0; int currRd = 0; 
            if(GameService.Instance.gameModel.gameState == GameState.InTown ||
                GameService.Instance.gameModel.gameState == GameState.InQuestRoom)            
                        currRd = skillModel.cd;                
            else if (GameService.Instance.gameModel.gameState == GameState.InTown)
                        currRd = CombatService.Instance.currentRound;

            for (j = 0; j < skillModel.cd; j++)
            {
                transform.GetChild(j).gameObject.SetActive(true);
                if (j >= skillModel.cdRemaining)
                    transform.GetChild(j).GetComponent<Image>().sprite = cdN;
                else
                    transform.GetChild(j).GetComponent<Image>().sprite = cdNA;
            }
            for (int k = j; k < transform.childCount; k++)
            {
                transform.GetChild(k).gameObject.SetActive(false);
            }
        }
    }
}