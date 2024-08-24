using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Town;
using UnityEngine;
using UnityEngine.UI;

namespace Quest
{
    public class QuestEmbarkView : MonoBehaviour, IPanel
    {
        [SerializeField] Transform questLogo;
        [SerializeField] TextMeshProUGUI headingTxt;

        [SerializeField] TextMeshProUGUI descTxt;
        [SerializeField] Transform questMenu;
        [SerializeField] Transform LLDTrans;
        [SerializeField] Button embarkBtn;

        [SerializeField] Button exitBtn;

        [SerializeField] QuestModel questModel;
        [SerializeField] QuestSO questSO;
        [SerializeField] QuestBase questBase;
        [SerializeField] ObjModel objModel;
        [SerializeField] ObjSO objSO;
        QMarkView qMarkView;

       // [Header("Start Node")]
        //[SerializeField] NodeData startNode; 
        void Awake()
        {
            exitBtn.onClick.AddListener(OnExitBtnPressed); 
            embarkBtn.onClick.AddListener(OnEmbarkBtnPressed);    
        }

        // to be called only from QuestController
        public void ShowQuestEmbarkView(QuestModel questModel, QuestSO questSO
                    , QuestBase questBase, ObjModel objModel, QMarkView qMarkView)           
        {
            this.questModel = questModel;
            this.questSO = questSO;
            this.questBase= questBase;
            this.objModel = objModel;
            this.objSO = questSO.GetObjSO(objModel.objName);
            this.qMarkView = qMarkView;           
            Load();
            FillQuestPanel(); 
        }
        void OnExitBtnPressed()
        {
            MapService.Instance.pathView.OnPathExit(); 
            UnLoad();
        }
        void OnEmbarkBtnPressed()
        {
            UnLoad();
                
            qMarkView.OnPathEmbark();
        }
        void FillQuestPanel()
        {
            headingTxt.text = questSO.questNameStr;
            questLogo.GetComponent<Image>().sprite
                = QuestMissionService.Instance.allQuestSO.GetQuestTypeSprite(questModel.questType);
            descTxt.text = objSO.desc;
            FillLLD();
            InitQuestMenuBoard();
        }
        void FillLLD()
        {
            LLDTrans.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text
                                        = objSO.locationName.ToString().CreateSpace();
            LLDTrans.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text
                                        = objSO.landscapeName.ToString().CreateSpace();

            if (objSO.distanceInDays == 0)
                LLDTrans.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text
                                            = "Instant";
            if (objSO.distanceInDays == 0.5)
                LLDTrans.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text
                                            = "Half day";
            if (objSO.distanceInDays >= 1.0)
                LLDTrans.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text
                                            = $"{objSO.distanceInDays} day";
        }
        void InitQuestMenuBoard()
        {            
            questMenu.GetComponent<QuestMenuEmbarkView>().InitQuestMenuEmbark(this, questModel, questSO);
        }

        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject,  true); 
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }

        public void Init()
        {
            UnLoad();    
        }
    }
}