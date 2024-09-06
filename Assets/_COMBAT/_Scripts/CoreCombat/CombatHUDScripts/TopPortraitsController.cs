using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using DG.Tweening;
using System.Linq;
using UnityEngine.UI; 
namespace Combat
{
    [System.Serializable]
    public class RectPos
    {
        public RectTransform rectTrans;
        public int pos;

        public RectPos(RectTransform rectTrans, int pos)
        {
            this.rectTrans = rectTrans;
            this.pos = pos;
        }
    }

    [System.Serializable]
    public class CharPosData
    {
        public CharController charController;
        public int pos;
        public CharPosData(CharController charController, int pos)
        {
            this.charController = charController;
            this.pos = pos;
        }
    }



    public class TopPortraitsController : MonoBehaviour
    {     

        [Header("ref Ally And Enemy Portraits")]

        public List<GameObject> allyInCombat = new List<GameObject>();
        public List<GameObject> enemyInCombat = new List<GameObject>();


        [Header("PORTRAITS PARENT")]
        public GameObject allyPortraits;
        public GameObject enemyPortraits;

        [Header("WAY POINTS")]
        public List<Transform> allyWayPoints = new List<Transform>();
        public List<Transform> enemyWayPoints = new List<Transform>();

        [Header("POS DATA")]
        public List<CharPosData> allyPosData = new List<CharPosData>();
        public List<CharPosData> enemyPosData = new List<CharPosData>();
        public List<CharPosData> turnCompleteData = new List<CharPosData>();

        [Header("List from the round Controller")]
        [SerializeField]
        List<CharController> allyTurnOrder = new List<CharController>();
        [SerializeField]
        List<CharController> enemyTurnOrder = new List<CharController>();

        [Header("TOP PANEL EVENTS COMPONENTS")]
        List<TopPanelEvents> allAllyPortraitMovement = new List<TopPanelEvents>();
        List<TopPanelEvents> allEnemyPortraitMovement = new List<TopPanelEvents>();
        [SerializeField] GameObject portraitObj;


        RoundController roundController;

        void Start()
        {
            roundController = GetComponent<RoundController>(); 
            CombatEventService.Instance.OnCharOnTurnSet += OnCharSetPortrait;
            CombatEventService.Instance.OnEOT += EOTRemovePortrait;
            CharService.Instance.OnCharDeath += (CharController c) => SetDefaultTurnOrder();

            foreach (Transform child in allyPortraits.transform)
            {
                allyInCombat.Add(child.gameObject); 
            }
            foreach (Transform child in enemyPortraits.transform)
            {
                enemyInCombat.Add(child.gameObject);
            }
        }

        public void SetDefaultTurnOrder()
        {

            CleanPanelGO(allyInCombat);
            CleanPanelGO(enemyInCombat);
            for (int i = 0; i < roundController.allyTurnOrder.Count; i++)
            {

                CharacterSO charSO = CharService.Instance.GetCharSO(roundController.allyTurnOrder[i].charModel.charName); 

                allyInCombat[i].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite =
                    charSO.charHexPortrait;
                //     roundController.allyTurnOrder[i].charModel.charHexSprite;
                allyInCombat[i].transform.GetComponent<TopPanelEvents>().charController
                                     = roundController.allyTurnOrder[i];
                allyInCombat[i].SetActive(true);
            }
            for (int i = 0; i < roundController.enemyTurnOrder.Count; i++)
            {
                CharacterSO charSO = CharService.Instance.GetCharSO(roundController.enemyTurnOrder[i].charModel.charName);

                enemyInCombat[i].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite =
                    charSO.charHexPortrait;
                //enemyInCombat[i].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite =
                //     roundController.enemyTurnOrder[i].charModel.charHexSprite;
                enemyInCombat[i].transform.GetComponent<TopPanelEvents>().charController
                        = roundController.enemyTurnOrder[i];
                enemyInCombat[i].SetActive(true);
            }
        }
        public void CleanPanelGO(List<GameObject> toBeCleanList)
        {
            for (int i = 0; i < toBeCleanList.Count; i++)
            {
                toBeCleanList[i].SetActive(false);
            }
        }
        void ClearTurnCompleteData()
        {
            turnCompleteData.Clear();
          
        }
        public void BuildCharPosData()
        {
            Debug.Log("new chaR POS DATA BUILD");
            allyTurnOrder = CombatService.Instance.roundController.allyTurnOrder;
            enemyTurnOrder = CombatService.Instance.roundController.enemyTurnOrder;
            allyPosData.Clear();
            enemyPosData.Clear();
            for (int i = 0; i < allyTurnOrder.Count; i++)
            {
                CharPosData charPosData = new CharPosData(allyTurnOrder[i], i);
                allyPosData.Add(charPosData);
            }

            for (int i = 0; i < enemyTurnOrder.Count; i++)
            {
                CharPosData charPosData = new CharPosData(enemyTurnOrder[i], i);
                enemyPosData.Add(charPosData);
            }
        }

        void PopulateList()
        {
            allAllyPortraitMovement.Clear();
            allEnemyPortraitMovement.Clear();

            allAllyPortraitMovement =
                    allyPortraits.GetComponentsInChildren<TopPanelEvents>().ToList();
            allEnemyPortraitMovement =
                    enemyPortraits.GetComponentsInChildren<TopPanelEvents>().ToList();
        }

        void ClearPortraitEvents(CharController _charController)
        {
            CharMode charMode = _charController.charModel.charMode; 
            if(charMode == CharMode.Ally)
            {
                
                allAllyPortraitMovement.Find(t => t.charController.charModel.charID == _charController.charModel.charID)
                                                                    .charController = null; 
            }
            if (charMode == CharMode.Enemy)
            {
                foreach (TopPanelEvents enemyPortrait in allEnemyPortraitMovement)
                {

                    if (enemyPortrait.charController == null) continue;
                    if (enemyPortrait.charController.charModel.charID == _charController.charModel.charID)
                        enemyPortrait.charController = null; 
                }             
            }
        }
        void PlaceAsPerCharData()
        {

            foreach (var TopPanel in allAllyPortraitMovement)
            {
                if (TopPanel.charController == null)
                {
                    TopPanel.gameObject.SetActive(false);
                    continue;

                }
                CharController charController = TopPanel.charController;

                CharModel charModel = charController.charModel;
                GameObject portraitGO = TopPanel.gameObject;

                CharPosData charPosData = allyPosData.Find(t => t.charController.charModel.charID
                                                                                == charModel.charID);
                //TopPanel.transform.GetChild(0).gameObject.SetActive(true);
                if (charPosData == null)
                    continue;

                portraitGO.SetActive(true);

                MoveAlly2Pos(portraitGO, charPosData.pos);
            }

            foreach (var TopPanel in allEnemyPortraitMovement)
            {
                if (TopPanel.charController == null)
                {
                    TopPanel.gameObject.SetActive(false);
                    continue;

                }

                CharController charController = TopPanel.charController;

                CharModel charModel = charController.charModel;
                GameObject portraitGO = TopPanel.gameObject;

                CharPosData charPosData = enemyPosData.Find(t => t.charController.charModel.charID
                                                                       == charModel.charID);
                if (charPosData == null)  // setactive false 
                    continue; 
                // TopPanel.transform.GetChild(0).gameObject.SetActive(true);
                portraitGO.SetActive(true);

                MoveEnemy2Pos(portraitGO, charPosData.pos);
            }
        }


        public void ShufflePortraits()
        {       
            PopulateList();
            PlaceAsPerCharData();
        }

        void MoveAlly2Pos(GameObject GO, int pos)
        {
            Debug.Log("TOP" + pos + "Game object" + GO.name);
            GO.transform.DOMove(allyWayPoints[pos].position, 0.4f);
            if(pos ==3)
                GO.transform.DOScale(1.25f, 0.2f);
            else
                GO.transform.DOScale(1f, 0.2f);
        }

        void MoveEnemy2Pos(GameObject GO, int pos)
        {
            GO.transform.DOMove(enemyWayPoints[pos].position, 0.4f);
            if (pos == 3)
                GO.transform.DOScale(1.25f, 0.2f);
            else
                GO.transform.DOScale(1f, 0.2f);
        }

        void OnCharSetPortrait(CharController charController)
        {
            ShufflePortraits();
            Debug.Log("Enemy Portraits" + allEnemyPortraitMovement.Count);
            MoveOnCharSet(charController);
        }

        void RemoveFromCharDataEOT(CharController charController)
        {
            int index = -1; 
            CharMode charMode = charController.charModel.charMode;
            if (charMode == CharMode.Ally)
            {

                 index = allyPosData.FindIndex(t => t.charController.charModel.charID
                                               == charController.charModel.charID);
                if(index !=-1)
                allyPosData.RemoveAt(index);

            }
            else
            {
                 index = enemyPosData.FindIndex(t => t.charController.charModel.charID
                                      == charController.charModel.charID);
                if (index != -1)
                    enemyPosData.RemoveAt(index);
            }
        }

        void MoveOnCharSet(CharController charController)
        {
            if (charController.charModel.charMode == CharMode.Ally)
            {
                foreach (var TopPanel in allAllyPortraitMovement)
                {
                    CharController portraitCharCtrl = TopPanel.charController;
                    CharPosData charPosData = allyPosData.Find(t => t.charController.charModel.charID
                                                                       == portraitCharCtrl.charModel.charID);
                    if (portraitCharCtrl == null || charPosData == null) continue;

                    if (portraitCharCtrl.charModel.charID == charController.charModel.charID)   // SET ON TURN
                    {
                        MoveAlly2Pos(TopPanel.gameObject, 3);
                        portraitObj = TopPanel.gameObject;
                       // TopPanel.gameObject.transform.DOScale(1.25f, 0.2f);
                    }
                    else   // SHUFFLE FORWARD
                    {
                        int newPos = charPosData.pos - 1;
                        if (newPos >= 0)
                        {
                            MoveAlly2Pos(TopPanel.gameObject, newPos);
                            charPosData.pos = newPos;
                        }
                    }
                }
            }
            if (charController.charModel.charMode == CharMode.Enemy)
            {
                foreach (var TopPanel in allEnemyPortraitMovement)
                {
                    CharController portraitCharCtrl = TopPanel.charController;
                    CharPosData charPosData = enemyPosData.Find(t => t.charController.charModel.charID
                                                                    == portraitCharCtrl.charModel.charID);
                    if (portraitCharCtrl == null || charPosData == null) continue; 

                    if (portraitCharCtrl.charModel.charID == charController.charModel.charID)
                    {
                        MoveEnemy2Pos(TopPanel.gameObject, 3);
                        portraitObj = TopPanel.gameObject;
                    }
                    else   // SHUFFLE FORWARD
                    {
                        int newPos = charPosData.pos - 1;
                        if ((newPos) >= 0)
                        {
                            MoveEnemy2Pos(TopPanel.gameObject, newPos);
                            charPosData.pos = newPos;
                        }
                    }
                }
            }
        }

        void EOTRemovePortrait()
        {
            portraitObj.SetActive(false);
            CharController charController = portraitObj.GetComponent<TopPanelEvents>()
                                                .charController;
            RemoveFromCharDataEOT(charController);
        }

    }

}


 


//CharMode charMode = charController.charModel.charMode; 
//if(charMode == CharMode.Ally)
//{
//    CharPosData charPosData = allyPosData.Find(t => t.charController.charModel.charID
//                                                == charController.charModel.charID);
//    if(charPosData != null)
//    allyPosData.Remove(charPosData); 

//}else if (charMode == CharMode.Enemy)
//{
//    CharPosData charPosData = enemyPosData.Find(t => t.charController.charModel.charID
//                                    == charController.charModel.charID);
//    if (charPosData != null)
//        enemyPosData.Remove(charPosData);


//}
// get reference to the top portraits 
//// define paths vector3 

//[Header("PORTRAITS PARENT")]
//public GameObject allyPortraits;
//public GameObject enemyPortraits;

//[Header("WAY POINTS")]
//public List<Transform> allyWayPoints = new List<Transform>();
//public List<Transform> enemyWayPoints = new List<Transform>();


//public List<CharPosData> allyPosData = new List<CharPosData>();
//public List<CharPosData> enemyPosData = new List<CharPosData>();


////[Header("Turn Completed List")]
////public List<CharController> allyTurnCompleted = new List<CharController>();
//////public List<CharController> EnemyTurnCompleted = new List<CharController>();
////[Header("List from the round Controller")]
////[SerializeField]
////List<CharController> allyTurnOrder = new List<CharController>();

//[SerializeField]
//List<CharController> enemyTurnOrder = new List<CharController>();

//// create a tweening path 
//// run the Shuffle Method before start of the "TURN "
////move the charOnTurn to pos 1 once shuffle to is complete 

//List<TopPanelEvents> allAllyPortraitMovement = new List<TopPanelEvents>();
//List<TopPanelEvents> allEnemyPortraitMovement = new List<TopPanelEvents>();

////List<CharPosData> turnCompleteAlly = new List<CharPosData>();
////List<CharPosData> turnCompleteEnemy = new List<CharPosData>();

//[SerializeField] CharController nextCharController;

// [Header("In Game Parameters")]

// ok the funda 
// SOR=> build first charPosData 
// SOT => Set as per charPostData 
// EOT => Substract as per turn complete from charPosData



//void Start()
//{
//    // CombatEventService.Instance.OnSOR += BuildCharPosData;
//    CombatEventService.Instance.OnCharOnTurnSet += OnCharSetPortrait;
//    //CombatEventService.Instance.OnEOT += EOTRemovePortrait;
//}

//public void BuildCharPosData()
//{

//    Debug.Log("PROGRAM HAS SOR ");
//    allyTurnOrder = CombatService.Instance.roundController.allyTurnOrder;
//    enemyTurnOrder = CombatService.Instance.roundController.enemyTurnOrder;
//    allyPosData.Clear();
//    enemyPosData.Clear();



//    for (int i = 0; i < allyTurnOrder.Count; i++)
//    {
//        CharPosData charPosData = new CharPosData(allyTurnOrder[i], i);
//        allyPosData.Add(charPosData);
//    }

//    for (int i = 0; i < enemyTurnOrder.Count; i++)
//    {
//        CharPosData charPosData = new CharPosData(enemyTurnOrder[i], i);
//        enemyPosData.Add(charPosData);
//    }
//}

//void PopulateList()
//{
//    allAllyPortraitMovement.Clear();
//    allEnemyPortraitMovement.Clear();

//    allAllyPortraitMovement =
//            allyPortraits.GetComponentsInChildren<TopPanelEvents>().ToList();
//    allEnemyPortraitMovement =
//            enemyPortraits.GetComponentsInChildren<TopPanelEvents>().ToList();
//}

//void PlaceAsPerCharData()
//{

//    foreach (var TopPanel in allAllyPortraitMovement)
//    {
//        if (TopPanel.charGO == null)
//        {
//            TopPanel.gameObject.SetActive(false);
//            continue;

//        }

//        CharController charController = TopPanel.charGO?.GetComponent<CharController>();




//        CharModel charModel = charController.charModel;
//        GameObject portraitGO = TopPanel.gameObject;

//        CharPosData charPosData = allyPosData.Find(t => t.charController.charModel.charID
//                                                                        == charModel.charID);

//        portraitGO.SetActive(true);
//        MoveAlly2Pos(portraitGO, charPosData.pos);

//    }

//    foreach (var TopPanel in allEnemyPortraitMovement)
//    {

//        if (TopPanel.charGO == null)
//        {
//            TopPanel.gameObject.SetActive(false);
//            continue;
//        }
//        CharController charController = TopPanel.charGO.GetComponent<CharController>();
//        CharModel charModel = charController.charModel;
//        GameObject portraitGO = TopPanel.gameObject;

//        CharPosData charPosData = enemyPosData.Find(t => t.charController.charModel.charID
//                                                               == charModel.charID);

//        portraitGO.SetActive(true);
//        MoveEnemy2Pos(portraitGO, charPosData.pos);
//    }
//}


//public void ShufflePortraits()
//{
//    // BuildCharPosData();
//    PopulateList();
//    PlaceAsPerCharData();
//}

//void MoveAlly2Pos(GameObject GO, int pos)
//{
//    GO.transform.DOMove(allyWayPoints[pos].position, 0.4f);
//}

//void MoveEnemy2Pos(GameObject GO, int pos)
//{
//    GO.transform.DOMove(enemyWayPoints[pos].position, 0.4f);
//}

//void OnCharSetPortrait(CharController charController)
//{
//    ShufflePortraits();
//    Debug.Log("Enemy Portraits" + allEnemyPortraitMovement.Count);
//    MoveOnCharSet(charController);
//}

//void RemoveFromCharData(CharController charController)
//{
//    CharMode charMode = charController.charModel.charMode;
//    if (charMode == CharMode.Ally)
//    {
//        CharPosData charPosData = allyPosData.Find(t => t.charController.charModel.charID
//                                       == charController.charModel.charID);
//        allyPosData.Remove(charPosData);
//    }
//    else
//    {
//        CharPosData charPosData = enemyPosData.Find(t => t.charController.charModel.charID
//                              == charController.charModel.charID);
//        enemyPosData.Remove(charPosData);
//    }
//}

//void MoveOnCharSet(CharController charController)
//{
//    if (charController.charModel.charMode == CharMode.Ally)
//    {
//        foreach (var TopPanel in allAllyPortraitMovement)
//        {
//            CharController portraitCharCtrl = TopPanel.charGO.GetComponent<CharController>();
//            CharPosData charPosData = allyPosData.Find(t => t.charController.charModel.charID
//                                                               == portraitCharCtrl.charModel.charID);
//            if (portraitCharCtrl.charModel.charID == charController.charModel.charID)
//            {
//                MoveAlly2Pos(TopPanel.transform.parent.gameObject, 3);
//                nextCharController = charController;
//                TopPanel.gameObject.transform.DOScale(1.25f, 0.2f);
//            }
//            else   // SHUFFLE 
//            {
//                int newPos = charPosData.pos - 1;
//                if (newPos >= 0)
//                    MoveAlly2Pos(TopPanel.gameObject, newPos);
//            }
//        }
//    }
//    if (charController.charModel.charMode == CharMode.Enemy)
//    {
//        foreach (var TopPanel in allEnemyPortraitMovement)
//        {
//            CharController portraitCharCtrl = TopPanel.charGO.GetComponent<CharController>();
//            CharPosData charPosData = enemyPosData.Find(t => t.charController.charModel.charID
//                                                            == portraitCharCtrl.charModel.charID);
//            if (portraitCharCtrl.charModel.charID == charController.charModel.charID)
//            {
//                MoveEnemy2Pos(TopPanel.transform.parent.gameObject, 3);
//                nextCharController = charController;
//                TopPanel.gameObject.transform.DOScale(1.25f, 0.2f);
//            }
//            else   // SHUFFLE FORWARD
//            {
//                int newPos = charPosData.pos - 1;
//                if ((newPos) >= 0)
//                {
//                    MoveEnemy2Pos(TopPanel.gameObject, charPosData.pos - 1);
//                    charPosData.pos = newPos;
//                }
//            }
//        }
//    }
//}

//void EOTRemovePortrait()
//{
//    nextCharController.gameObject.SetActive(false);
//    RemoveFromCharData(nextCharController);
//}