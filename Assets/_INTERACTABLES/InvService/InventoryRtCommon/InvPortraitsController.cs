//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using DG.Tweening;
//using UnityEngine.EventSystems;
//using Common;
//using UnityEngine.UI;
//using System.Linq;
//using Combat;
//namespace Interactables
//{
//    public class InvPortraitsController : MonoBehaviour, IPointerClickHandler
//    {
//        [SerializeField] GameObject portraitGO;
//        [SerializeField] CharController currCharOnClicked; 
//        [SerializeField] GameObject StackGO;
//        [SerializeField]  List<CharController> allExceptCurr = new List<CharController>(); 
//        float widthPerPortrait = 0f;
//        private void Start()
//        {
//            widthPerPortrait = portraitGO.GetComponent<RectTransform>().sizeDelta.x;
//            CombatEventService.Instance.OnSOC += SetCurrCharClicked;
//        }

//        void SetCurrCharClicked()
//        {
//            currCharOnClicked = CharService.Instance.GetCharCtrlWithCharID(3);
//        }
//        public void OnPointerClick(PointerEventData eventData)
//        {
//            Sequence portraitSeq = DOTween.Sequence();
//            StackGO.transform.DOScaleX(0f, 0.2f);
//            portraitSeq.Append(portraitGO.transform.DOLocalMoveY(140, 0.2f))
//                       .AppendInterval(0.1f)
//                       .Append(portraitGO.transform.DOScaleX(0, 0.1f))
//                       .AppendCallback(() => BuildCharPortraits())
//                       .AppendInterval(0.2f)
//                       .Append(StackGO.transform.DOScaleX(1f, 0.2f))                      
//                        ;

//            // get all the char In stack
//            // increase size with code or content size fitter
//            // if list is odd make it even and setactive false the last portrait 
//            // insert the current charPortrait in middle
//            // animate and increase in X 

//        }

//        void BuildCharPortraits()
//        {
//            RectTransform stackrect = StackGO.GetComponent<RectTransform>();

//            allExceptCurr = CharService.Instance.allyInPlayControllers
//                                                    .Where(t => t != currCharOnClicked).ToList(); 

//            if(allExceptCurr.Count%2 == 0)
//            {
//                if (allExceptCurr.Count == 2)
//                {
//                    allExceptCurr.Insert(1,currCharOnClicked);
//                }
//                else
//                {
//                    int index = (allExceptCurr.Count / 2) + 1;  // check this out  for round off value 
//                    allExceptCurr.Insert(index, currCharOnClicked);
//                }
//            }
//            else
//            {
//                if (allExceptCurr.Count == 1)
//                {
//                    allExceptCurr.Insert(1,currCharOnClicked);
//                }
//                else
//                {
//                    int index = (allExceptCurr.Count / 2) + 1;
//                    allExceptCurr.Insert(index, currCharOnClicked);
//                }
//            }


//            foreach (CharController ally in allExceptCurr)
//            {
//                CharacterSO charSO = CharService.Instance.GetCharSO(ally);
//                Sprite portSprite = charSO.bpPortraitUnLocked;
//                ClassType classType = charSO.classType;
//                float expPoints = ally.charModel.expPoints;

//                // instantiate GO... make parent , change values 
//                GameObject CharGO = Instantiate(portraitGO, Vector3.zero, Quaternion.identity);
//                CharGO.transform.SetParent(StackGO.transform);
//                //increase the size
//                float x = stackrect.sizeDelta.x + widthPerPortrait;
//                float y = stackrect.sizeDelta.y;
//                stackrect.sizeDelta = new Vector2(x, y);
//                //Set VALUES
//                CharGO.transform.GetChild(0).GetComponent<Image>().sprite = portSprite;


//                CharGO.transform.DOScaleX(1, 0.4f);
//            }
//            StackGO.transform.DOLocalMoveY(140, 0.4f);
//        }


//    }

//}

