using Combat;
using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class TrapMGController : MonoBehaviour
    {
        public AllTrapMGSO allTrapSO;
        public TrapMGSO trapMGSO;

        public TrapMGModel trapMGModel;

        [SerializeField] TrapView trapViewPrefab; 
        public TrapView trapViewGO; 

        public MGGameState trapGameState;

        

        [Header(" global Var")]
        InteractEColEvents interactEColEvents; 
    
        public void InitGame(InteractEColEvents interactEColEvents)
        {
            GameDifficulty gameDiff = GameDifficulty.Easy;
                  // GameService.Instance.gameModel.gameDifficulty;

            trapMGSO = allTrapSO.GetTrapSO(gameDiff); 
            trapMGModel = new TrapMGModel(trapMGSO);
            
            trapGameState = MGGameState.Start;
            this.interactEColEvents= interactEColEvents;    
            // show trap game here dia service copy 
            if(trapViewGO== null)
            {
                GameObject parent = FindObjectOfType<Canvas>().gameObject;
                trapViewGO = Instantiate(trapViewPrefab);

                
                trapViewGO.transform.SetParent(parent.transform);

                //UIControlServiceGeneral.Instance.SetMaxSiblingIndex(diaGO);
                int index = trapViewGO.transform.parent.childCount - 2;
                trapViewGO.transform.SetSiblingIndex(index);
                RectTransform mgRect = trapViewGO.GetComponent<RectTransform>();

                mgRect.anchorMin = new Vector2(0.5f, 0.5f);
                mgRect.anchorMax = new Vector2(0.5f, 0.5f);
                mgRect.pivot = new Vector2(0.5f, 0.5f);
                mgRect.localScale = Vector3.one;
                mgRect.offsetMin = new Vector2(0, 0); // new Vector2(left, bottom);
                mgRect.offsetMax = new Vector2(0, 0); // new Vector2(-right, -top);
                //isDiaViewInitDone = true;
            }
            trapViewGO.gameObject.SetActive(true);
            QRoomService.Instance.canAbbasMove = false;
            trapViewGO.StartSeq(trapMGModel, allTrapSO, this);

        }
        public void OnEndGame()
        {
            interactEColEvents.OnContinue(); 
        }

    }
}