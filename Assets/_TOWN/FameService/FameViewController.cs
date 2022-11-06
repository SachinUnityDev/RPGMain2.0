using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;



namespace Common
{
   
    public class FameData
    {
        public FameLoc fameClass;
        public FameType fameType;
        public float fameVal; 


        public FameData(FameLoc fameClass, FameType fameType, float fameVal)
        {
            this.fameClass = fameClass;
            this.fameType = fameType;
            this.fameVal = fameVal;
        }
    }


    public enum FameLoc
    {
        FameGlobal, //0 
        FameNekkisari,//1
        FameBluetown, //2
        FameSmaeru, //3   
    }
    
    
    public enum FameType
    {        
        None,
        Respectable,
        Honorable,
        Hero,
        Despicable, 
        Notorious,
        Villain,
        Unknown,
    }

    public enum FameBehavior
    {
        None,
        AttentionLover,
        ChaosLover,
        EasyGoing,
        Judicious,
        Lawful,
        SafeRider,
    }



    [System.Serializable]
    public class FameChgData
    {
        public FameLoc fameLoc; 
        public int scoreAdded;
        public string changeDesc;
        public MonthName monthName;

        public FameChgData(int _scoreAdded, string _changedesc, MonthName _monthName, FameLoc _fameLoc)
        {
            scoreAdded = _scoreAdded;
            changeDesc = _changedesc;
            monthName = _monthName;
            fameLoc = _fameLoc; 
        }
    }

    public class FameViewController : MonoBehaviour
    {
        [SerializeField] FameModel fameModel;  

        [Header("Fame UI... to be ref")]
        [SerializeField] GameObject fameName;
        [SerializeField] GameObject fameBar;
        [SerializeField] Image FameImg;
        [SerializeField] TextMeshProUGUI fameVal;
        [SerializeField] TextMeshProUGUI fameModVal; 

        //[SerializeField]CharacterController AbbasCharacterController;
        [SerializeField] GameObject plankPrefab;

        
        [SerializeField] GameObject ScrollViewFame;

        [Header("Page References")]
        [SerializeField] Button leftBtn;
        [SerializeField] Button rightBtn;
        [SerializeField] TextMeshProUGUI pagetext;

        [Header("Fame Color Sprite..REF")]

        [SerializeField] Sprite posSprite;
        [SerializeField] Sprite negSprite;

        [SerializeField] int currPage = 0; 
        void Start()
        {
            // change the fame symbol with depending upon the fame
            // fame value to be shown on the right side with modifier 
            // scroll bar pages to be added (prefab)
            fameModel = FameService.Instance.fameModel;
            leftBtn.onClick.AddListener(OnLeftPageBtnPressed);
            rightBtn.onClick.AddListener(OnRightPageBtnPressed);
        }
          
        void OnLeftPageBtnPressed()
        {
            currPage--; 
            if(currPage>=0 && currPage <= 1)
            {
                DisplayFamePanel(); 
            }
        }
        void OnRightPageBtnPressed()
        {
            currPage++;
           // ClearPlank();

            if (currPage >= 0 && currPage <= 1)
            {
                DisplayFamePanel();
            }
        }
        public void RunTestBtn()
        {
            FameChgData fcD1 = new FameChgData(-30, "Sacrificed a friend in altar"
                                        , MonthName.HunchOfTheCamel, FameLoc.FameGlobal);

            FameService.Instance.FameScoreUpdate(fcD1); 
            //FameChgData fcD2 = new FameChgData(6, "Bought drinks in the fest", MonthName.AntlersOfTheDeer);
            //FameScoreUpdate(fcD2);
            //FameChgData fcD3 = new FameChgData(3, "Completed Biloko quest", MonthName.AntlersOfTheDeer);
            //FameScoreUpdate(fcD3);
            //FameChgData fcD4 = new FameChgData(-24, "Dialogue with Ilona", MonthName.AntlersOfTheDeer);
            //FameScoreUpdate(fcD4);

            DisplayFamePanel();
        }

    
        public void DisplayFamePanel()
        {
            List<FameChgData> fameList = FameService.Instance.GetFameChgList((FameLoc)currPage);

            if (fameList.Count < 1) return; 
            pagetext.text = currPage.ToString(); 
            Debug.Log("*********" + GetFameNameStr(GetFameType()));

            fameName.transform.GetComponentInChildren<TextMeshProUGUI>().text = GetFameNameStr(GetFameType());
            ChgFameSprite();
            fameVal.text = FameService.Instance.GetFameValue(currPage).ToString();
            fameModVal.text = FameService.Instance.GetFameModValue(currPage).ToString();
            int currFameVal = FameService.Instance.GetFameValue(currPage); 
            if (currFameVal >= 0)
            {
                fameBar.transform.GetChild(0).GetComponent<Slider>().value = currFameVal;
                fameBar.transform.GetChild(1).GetComponent<Slider>().value = 0; 
            }
            else if (currFameVal <= 0)
            {
                fameBar.transform.GetChild(0).GetComponent<Slider>().value = 0;
                fameBar.transform.GetChild(1).GetComponent<Slider>().value = Mathf.Abs(currFameVal);
            }


            foreach ( FameChgData f in fameList)
            {
                AddPlankPanel(f.scoreAdded, f.changeDesc, f.monthName); 
            }    

        }

        //void ClearPlank()
        //{
        //    foreach (Transform child in ScrollViewFame.transform)
        //    {
        //       Destroy(child.gameObject);
        //    }
            
        //}
        public void AddPlankPanel(int _scoreAdded, string _displayStr, MonthName _monthName)
        {
            string preOperator;

           // Debug.Log("scoreadd" + _scoreAdded + "abs" + Mathf.Abs(_scoreAdded));
            string monthNameStr = CalendarService.Instance.GetMonthSO(_monthName).monthNameShort;  

            if (!plankPrefab) return;
            GameObject newPlank = Instantiate(plankPrefab, ScrollViewFame.transform.position , Quaternion.identity);
            newPlank.transform.SetParent(ScrollViewFame.transform, false);

            TextMeshProUGUI scoreTxt = plankPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>(); 

            if (_scoreAdded > 0)
            {
                preOperator = "+";
                scoreTxt.color = new Color32(102, 153, 255, 255);
            }
            else 
            {
                preOperator = "-";
                scoreTxt.color = new Color32(255, 204, 102, 255);
            } 

                scoreTxt.text = preOperator + Mathf.Abs(_scoreAdded).ToString();
           

            plankPrefab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _displayStr;
            plankPrefab.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = monthNameStr;
            float ht = plankPrefab.GetComponent<RectTransform>().rect.height; 
            RectTransform rt= ScrollViewFame.GetComponent<RectTransform>();
            rt.sizeDelta += new Vector2(0, ht+5f);  // its height, width :) 

        }
       
        void ChgFameSprite()
        {
            int currFameVal = FameService.Instance.GetFameValue(currPage); 
            if(currFameVal < -29f)
            {
                FameImg.sprite = negSprite; 
            }
            else if (currFameVal > 30f)
            {
                FameImg.sprite = posSprite;
            }
        }


        public FameType GetFameType()
        {
            float currentFame = FameService.Instance.GetFameValue(currPage);  
            //Debug.Log("fame" + currFameData.fameVal); 
            if (currentFame >= 30 && currentFame < 60) return FameType.Respectable;
            else if (currentFame >= 60 && currentFame < 120) return FameType.Honorable;
            else if (currentFame >= 120) return FameType.Hero;
            else if (currentFame > -60 && currentFame <= -30) return FameType.Despicable;
            else if (currentFame > -120 && currentFame <=-60) return FameType.Notorious;
            else if (currentFame <= -120) return FameType.Villain;
            else if (currentFame > -30 && currentFame < 30) return FameType.Unknown;
            else return FameType.None; 
        }
    
        public string GetFameNameStr(FameType _fameType)
        {
            switch (_fameType)
            {
                case FameType.Respectable: return "Respectable";
                case FameType.Honorable: return "Honorable";
                case FameType.Hero: return "Hero";
                case FameType.Despicable: return "Despicable";
                case FameType.Notorious: return "Notorious";
                case FameType.Villain: return "Villain";
                case FameType.Unknown: return "Unknown";

                case FameType.None: return "";
            }
            return null;

        }

    }


}

