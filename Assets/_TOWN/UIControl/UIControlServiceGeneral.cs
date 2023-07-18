using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Intro;
using System.Linq; 

namespace Common
{

    public class UIControlServiceGeneral : MonoSingletonGeneric<UIControlServiceGeneral>
    {
        public GameObject townCanvas;
        public List<GameObject> currOpenPanels = new List<GameObject>();

        [Header("Escape related")]
        [SerializeField] float lastEscClick = 0f; 
        [SerializeField] GameObject escPanel;
        [SerializeField] bool isEscOpen = false;
        [SerializeField] bool isEscBlocked = false; 

        
        [Header("Notify SO")]
        public AllNotifySO allNotifySO;
        public NotifyController notifyController;
        
        [Header("HelpName")]
        public HelpName helpName;

        [Header("Game Init")]
        public bool isNewGInitDone = false;

        public void InitUIGeneral()
        {
            notifyController = GetComponent<NotifyController>();
            notifyController.InitController(allNotifySO);
            isNewGInitDone = true;

        }
        private void Start()
        {
            townCanvas = GameObject.FindWithTag("TownCanvas");
            TogglePanel(escPanel, false);
        }
        public void BlockEsc(bool blockEsc)
        {
            isEscBlocked= blockEsc;
        }
        public void CloselastPanel()
        {
            int count = currOpenPanels.Count; 
            if (count > 0)
            {
                // TogglePanel(currOpenPanels[count - 1], false); // this methods skips the other unloads
                IPanel ipanel =
                    currOpenPanels[count - 1].GetComponent<IPanel>();
                ipanel.UnLoad();
            }
            else
            {
                CalendarUIController calendarUIController = CalendarService.Instance.calendarUIController; 
                if (GameService.Instance.gameModel.gameState == GameState.InTown
                    && CalendarService.Instance.calendarUIController.panelInScene != PanelInScene.None)
                {
                    CalendarService.Instance.calendarUIController
                        .OnPanelExit(calendarUIController.GetPanelInScene(calendarUIController.panelInScene));
                }
                else
                {
                    if (!isEscOpen)
                        TogglePanel(escPanel, true);
                    else
                        TogglePanel(escPanel, false);
                    isEscOpen = !isEscOpen;
                }
              
            }
        }
        
        public void CloseAllPanels()
        {           
            foreach (GameObject go in currOpenPanels.ToList())
            {
               TogglePanel(go, false);
            }
            currOpenPanels.Clear();
        }
        public void TogglePanels(GameObject go, List<GameObject> allPanels)
        {
            foreach (GameObject panel in allPanels)
            {
                panel.SetActive(false); // use toggle panel here
            }
            go.SetActive(true);// use toggle panel here          
        }
        public void AddAnchorsToMaxExpansion(GameObject go)
        {
            RectTransform rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(1, 1);
            rect.pivot = new Vector2(0.5f, 0.5f);

            rect.offsetMin = Vector2.zero;
            // new Vector2(left, bottom);
            rect.offsetMax = Vector2.zero;
            // new Vector2(-right, -top);
           // rect.anchoredPosition = mainCanvas.transform.position;
            rect.localScale = Vector3.one;
            rect.sizeDelta =  Vector2.zero;
        }
     
        public void SetMaxSibling2Canvas(GameObject go)
        {
            go.transform.SetParent(townCanvas.transform);
            int childCount = townCanvas.transform.childCount - 1;
            go.transform.SetSiblingIndex(childCount);
            AddAnchorsToMaxExpansion(go);
        }

        public void SetSibling2Count(GameObject go, int count)
        {
            int childCount = go.transform.parent.childCount - 1;
            if (count <= childCount)
                go.transform.SetSiblingIndex(childCount);
            else
                Debug.Log("incorrect hierarchy set ordered " + go.name + "COUNT " + count +"   "+ childCount);
        }

        public void SetMaxSiblingIndex(GameObject GO)
        {
            int childCount = GO.transform.parent.childCount - 1;
            GO.transform.SetSiblingIndex(childCount);

        }

        public void TogglePanelNCloseOthers(GameObject go, bool turnOn)
        {
            if (turnOn)
                if (currOpenPanels.Any(t => t.gameObject.name == go.name)) return;
            CloseAllPanels();
            TogglePanel(go, turnOn);  
        }
        public void TogglePanel(GameObject go, bool turnON)
        {
            if(turnON)
                if (currOpenPanels.Any(t => t.gameObject.name == go.name)) return;
            iHelp help = go.GetComponent<iHelp>();
            if (turnON)
            {
                if (help != null)
                    helpName = help.GetHelpName();
                currOpenPanels.Add(go);
            }
            else
            {
                if (help != null)
                    helpName = GuideServices.Instance.GetDefaultHelp(); 
                currOpenPanels.Remove(go);
            }
                

            go.SetActive(turnON); 
            //go.transform.DOScale(1f, 0.01f);
            CanvasGroup canvasGrp = go?.GetComponent<CanvasGroup>();
            if (canvasGrp != null)
            {
                canvasGrp.interactable = turnON;
                canvasGrp.blocksRaycasts = turnON;
            }
            IPanel panel = go.GetComponentInChildren<IPanel>();
            if (panel != null)
            {
                if (turnON)                
                    panel.Load();               
            }
                
        }
        public void ToggleOffNotify()
        {
           // TogglePanel(go, turnOn);
            int index =
            currOpenPanels.FindIndex(t => t.GetComponent<NotifyBoxView>() != null);
            if (index != -1)
                currOpenPanels[index].GetComponent<NotifyBoxView>().UnLoad();
        }
        public void TogglePanelOnInGrp(GameObject go, bool turnON)  
        {
            Transform goParent = go.transform.parent;
            int  panelCount = goParent.childCount; 
            for (int i =0; i<panelCount; i++)
            {                
                TogglePanel(goParent.GetChild(i).gameObject, false); 
            }
            TogglePanel(go, turnON);         
            go.transform.SetAsLastSibling();
            IPanel panel = go.GetComponentInChildren<IPanel>();
            if (panel != null)
                panel.Load();

        }
        
        public void ToggleInteractionsOnUI(GameObject go, bool TurnOn)
        {
            CanvasGroup canvasGrp = go.GetComponent<CanvasGroup>();
            if (canvasGrp != null)
            {
                canvasGrp.interactable = TurnOn;
                canvasGrp.blocksRaycasts = TurnOn;
            }
        }

        // Fade all images of the GameObject
        public void Fade(GameObject GO, float alpha)
        {
            Image[] imgs = GO.transform.GetComponentsInChildren<Image>();
            for (int i = 0; i < imgs.Length; i++)
            {
                imgs[i].DOFade(alpha, 1.0f);
                if (imgs[i].GetComponent<Button>() != null)
                {
                    if (alpha > 0.5f)
                    {
                        imgs[i].GetComponent<Image>().DOFade(1.0f, 0.5f);
                        imgs[i].GetComponent<Button>().enabled = true;
                    }
                    else
                    {
                        imgs[i].GetComponent<Image>().DOFade(0.0f, 0.5f)
                            .OnComplete(() => imgs[i].GetComponent<Button>().enabled = false);
                    }
                }
            }
        }

        private void Update()
        {  
            if(isEscBlocked) return;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameState gameState = GameService.Instance.gameModel.gameState; 
                if(gameState == GameState.InTown)
                {
                    if ((Time.time - lastEscClick) < 0.5f) return;
                    CloselastPanel();
                    lastEscClick = Time.time;
                }
                
            }
        }
    }

}

//public void ToggleIPanel(GameObject go, bool turnON)
//{
//    if (turnON)
//        currOpenPanels.Add(go);
//    else
//        currOpenPanels.Remove(go);
//    //go.SetActive(turnON);
//    go.GetComponentInChildren<IPanel>().Load(); 

//    go.transform.DOScale(1f, 0.1f);
//    CanvasGroup canvasGrp = go.GetComponent<CanvasGroup>();
//    if (canvasGrp != null)
//    {
//        canvasGrp.interactable = turnON;
//        canvasGrp.blocksRaycasts = turnON;
//    }
//    AddPanel2OpenList(go);
//}