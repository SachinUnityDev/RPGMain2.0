using Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement; 


namespace Quest
{
    public class QbarkController : MonoBehaviour
    {
        public QBarkSO qBarkSO;
        [SerializeField]List<BarkData> allBarkDataSelect = new List<BarkData>();

        public QbarkView qbarkViewGO;
     
        [SerializeField] QbarkView qBarkViewPrefab;
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;  
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (GameService.Instance.currGameModel.gameState == GameState.InQuestRoom)
            {
                GameObject canvasGO = FindObjectOfType<Canvas>().gameObject; 
                qbarkViewGO = FindObjectOfType<QbarkView>(true);
                //if (qbarkViewGO == null)
                //    qbarkViewGO =
                //          Instantiate(qBarkViewPrefab ,canvasGO.transform);
               // qbarkViewGO.gameObject.transform.SetParent(canvasGO.transform);
            }
        }

        public bool ShowBark(List<QBarkNames> barkNames, InteractEColEvents intColEvents)
        {
            allBarkDataSelect.Clear();
            allBarkDataSelect.AddRange(GetBarkData(barkNames));
            if (allBarkDataSelect.Count == 0)
                return false; 
            int index = UnityEngine.Random.Range(0,allBarkDataSelect.Count);
            qbarkViewGO.gameObject.SetActive(true);

            qbarkViewGO.InitBark(allBarkDataSelect[index].allCharData, intColEvents); 
            return true;
        }
        public List<BarkData> GetBarkData(List<QBarkNames> qBarkNames)
        {
            allBarkDataSelect.Clear(); 
            allBarkDataSelect.AddRange(qBarkSO.GetBarkData(qBarkNames));            
            RemoveOnQModeBasis();
            RemoveOnCharNameBasis();
            return allBarkDataSelect;
        }

        void RemoveOnCharNameBasis()
        {
            foreach (BarkData barkData in allBarkDataSelect.ToList())
            {
                foreach (BarkCharData lineData in barkData.allCharData)
                {
                    if (lineData.charName == CharNames.None)
                        continue;
                    if(!CharService.Instance.allCharsInPartyLocked
                        .Any(t=>t.charModel.charName == lineData.charName))
                    {
                        allBarkDataSelect.Remove(barkData);    break;                    
                    }
                }                
            }
        }

        void RemoveOnQModeBasis()
        {
            QuestMode questMode = QuestMissionService.Instance.currQuestMode;
            foreach (BarkData barkData in allBarkDataSelect.ToList())
            {
                if (barkData.InValidQMode[0] == QuestMode.None)
                    continue; 
                if(barkData.InValidQMode.Any(t=>t == questMode))
                    allBarkDataSelect.Remove(barkData);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                GetBarkData(new List<QBarkNames>() { QBarkNames.Qbark_001
                    , QBarkNames.Qbark_003, QBarkNames.Qbark_006, QBarkNames.Qbark_007,
                      QBarkNames.Qbark_012});
            }
        }

    }
}