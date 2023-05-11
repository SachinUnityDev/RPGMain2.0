using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;



namespace Common
{
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
        //All,
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
        public int fameAdded;
        public CauseType causeType;
        public int causeName;

        public FameChgData( CauseType causeType, int causeName, int fameAdded)
        {
            this.fameAdded = fameAdded;
            this.causeType = causeType;
            this.causeName = causeName;
        }
    }

    public class FameViewController : MonoBehaviour
    {

        FameModel fameModel;
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
            FameChgData fcD1 = new FameChgData (CauseType.CharSkill, 2, -30);

            FameService.Instance.fameController.ApplyFameChg(CauseType.CharSkill, 2, -12);
            DisplayFamePanel();
        }

    
        public void DisplayFamePanel()
        {
            List<FameChgData> fameList = FameService.Instance.GetFameChgList();
            fameModel = FameService.Instance.fameController.fameModel;
            if (fameList.Count < 1) return; 
            pagetext.text = currPage.ToString(); 
            Debug.Log("*********" + GetFameNameStr(FameService.Instance.GetFameType()));

            fameName.transform.GetComponentInChildren<TextMeshProUGUI>().text 
                                = GetFameNameStr(FameService.Instance.GetFameType());
            ChgFameSprite();
            fameVal.text = FameService.Instance.GetFameValue().ToString();
            fameModVal.text = FameService.Instance.GetFameYieldValue().ToString();
            int currFameVal = FameService.Instance.GetFameValue(); 
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
                AddPlankPanel(f.fameAdded, f.causeType,  f.causeName); 
            }    

        }

        public void AddPlankPanel(int _scoreAdded, CauseType causeType, int causeName)
        {
            string preOperator;

            plankPrefab = FameService.Instance.fameSO.famePlank;            
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
            string desc = "Fame gained" + causeType.ToString().CreateSpace();  

            plankPrefab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = desc;
            float ht = plankPrefab.GetComponent<RectTransform>().rect.height; 
            RectTransform rt= ScrollViewFame.GetComponent<RectTransform>();
            rt.sizeDelta += new Vector2(0, ht+5f);  // its height, width :) 
        }
       
        void ChgFameSprite()
        {
            int currFameVal = FameService.Instance.GetFameValue(); 
            if(currFameVal < -29f)
            {
                FameImg.sprite = negSprite; 
            }
            else if (currFameVal > 30f)
            {
                FameImg.sprite = posSprite;
            }
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

