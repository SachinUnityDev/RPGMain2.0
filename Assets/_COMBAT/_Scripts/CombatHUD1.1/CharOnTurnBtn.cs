using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Common;
using UnityEngine.SceneManagement;

namespace Combat
{
    public class CharOnTurnBtn : MonoBehaviour
    {
   
        private void OnEnable()
        {
            CombatEventService.Instance.OnSOC += DisableImg;
            CombatEventService.Instance.OnSOTactics += DisableImg;
            SceneManager.activeSceneChanged -= OnActiveSceneChg;
            SceneManager.activeSceneChanged += OnActiveSceneChg;
            gameObject.GetComponent<Button>().onClick
                               .AddListener(OnClickedONCharInNormalCombatState);
            CombatEventService.Instance.OnCharClicked += Check4DiffChar;
        }
        private void OnDisable()
        {
            CombatEventService.Instance.OnSOC -= DisableImg; 
            CombatEventService.Instance.OnSOTactics -= DisableImg;
            SceneManager.activeSceneChanged -= OnActiveSceneChg;
            CombatEventService.Instance.OnCharClicked -= Check4DiffChar;
        }
        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnActiveSceneChg;
        }


        void OnClickedONCharInNormalCombatState()
        {
            if (CombatService.Instance.combatState == CombatState.INCombat_normal)
             CombatEventService.Instance.On_CharClicked(CombatService.Instance.currCharOnTurn.gameObject);
        }
        void OnActiveSceneChg(Scene curr, Scene next)
        {
            if (next.name == "COMBAT")
            {
         
               
               
            }
        }
        public void Check4DiffChar(CharController charController)
        {
            if (CombatService.Instance.combatState == CombatState.INTactics)
                return;
      

            if (CombatService.Instance.combatState == CombatState.INCombat_normal)
            {
                if (charController.gameObject == null)
                    return;
                if (charController.gameObject != CombatService.Instance.currCharOnTurn.gameObject)
                {
                    UIControlServiceCombat.Instance.TurnOnOff(gameObject, true);
                }
                else
                    UIControlServiceCombat.Instance.TurnOnOff(gameObject, false);
            }
        }

        void DisableImg()
        {
            Image Img = transform.GetComponentInChildren<Image>();
            Img.enabled = false;
        }
    }

}
