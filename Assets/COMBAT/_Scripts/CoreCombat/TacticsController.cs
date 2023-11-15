using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common; 
namespace Combat
{
    public class TacticsController : MonoBehaviour
    {
       // [SerializeField] Button StartCombatBtn;
        [SerializeField]
        DynamicPosData targetDyna = null;
        [SerializeField]
        DynamicPosData selectDyna = null;


        void Start()
        {
           // StartCombatBtn.onClick.AddListener(CombatEventService.Instance.On_SOC);
            CombatEventService.Instance.OnCharRightClicked += OnCharRightClicked;
            GridService.Instance.OnCellPosClicked += OnTileClicked;
           // StartCombatBtn.onClick.AddListener(StartCombat);
            CombatEventService.Instance.OnSOTactics += StartTactics;
            CombatEventService.Instance.OnCharClicked += OnCharClickedIN_TACTICS;
            CombatEventService.Instance.OnSOC += OnSOC; 
        }
        private void OnDisable()
        {
            CombatEventService.Instance.OnSOC -= OnSOC;

        }
        void OnSOC()
        {
            CombatEventService.Instance.OnCharRightClicked -= OnCharRightClicked;
            GridService.Instance.OnCellPosClicked -= OnTileClicked;
            // StartCombatBtn.onClick.AddListener(StartCombat);
            CombatEventService.Instance.OnSOTactics -= StartTactics;
            CombatEventService.Instance.OnCharClicked -= OnCharClickedIN_TACTICS;

        }
        public void StartTactics()
        {
            CombatService.Instance.combatState = CombatState.INTactics;
            CombatEventService.Instance.On_CharClicked(CharService.Instance.charsInPlay[0]); 
            // show animations
        }

        public void StartCombat()
        {




        }
        void OnCharClickedIN_TACTICS(CharController charController)
        {
            //CharController charController = CombatService.Instance.currCharClicked;
            // get game object  and Dyna
            DynamicPosData dyna = GridService.Instance.GetDyna4GO(charController.gameObject);
            GridService.Instance.gridView.CharOnTurnHL(dyna);
        }

        public void OnCharRightClicked(GameObject _charGO)
        {
            if (CombatService.Instance.combatState == CombatState.INTactics)
            {
                targetDyna = GridService.Instance.GetDyna4GO(_charGO);
                selectDyna = GridService.Instance.GetDyna4GO(CombatService.Instance.currCharClicked.gameObject);
                if (targetDyna.charMode == selectDyna.charMode)
                    GridService.Instance.gridController.SwapPos(selectDyna, targetDyna);

            }
            
            // use from current system 
            // target tile clicked.. get the tile from gridService
            // checked if its vacant otherwise swap.....pos
            // set all skill icons state to unclickable_inTactics 

        }

        void CharColToggle(bool toEnable)
        {
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {
                charCtrl.GetComponent<BoxCollider2D>().enabled = toEnable;
            }
        }

        public void OnTileClicked(CellPosData currCellPos)
        {
            
            if (CombatService.Instance.combatState == CombatState.INTactics)
            {
                
                 targetDyna = GridService.Instance.gridView.GetDynaFromPos(currCellPos.pos, currCellPos.charMode);
                 selectDyna = GridService.Instance.GetDyna4GO(CombatService.Instance.currCharClicked.gameObject);
               // Debug.Log("cell pos clicked" + currCellPos.pos + targetDyna.charGO.name );
                if (targetDyna == null)
                {
                    if(selectDyna.charMode == currCellPos.charMode)
                        GridService.Instance.gridController.Move2Pos(selectDyna, currCellPos.pos); 

                }else
                {
                    if (targetDyna.charMode == selectDyna.charMode)
                        GridService.Instance.gridController.SwapPos(selectDyna, targetDyna); 
                }
                // if target pos empty
               // move to the target pos
               // else if occupied swipe char... 
            }


        }

    }


}

