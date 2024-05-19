using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System; 

namespace Common
{
    public class FameService : MonoSingletonGeneric<FameService>, ISaveable
    {
        public FameSO fameSO;
        public FameController fameController;
        public FameViewController fameViewController;

        public event Action <int> OnFameYieldChg;
        public event Action<int> OnFameChg;

        public ServicePath servicePath => ServicePath.FameService;

        [Header("Game Init")]
        public bool isNewGInitDone = false;
        void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "TOWN")
            {
                fameViewController = FindObjectOfType<FameViewController>(true);
            }
        }
        public void Init()
        {
            // save service integration here pending
            fameController = gameObject.GetComponent<FameController>();
            fameController.InitFameController(fameSO);
            isNewGInitDone = true;
        }
        public FameType GetFameType()
        {
            float currentFame = FameService.Instance.GetFameValue();
            if (currentFame >= 30 && currentFame < 60) return FameType.Respectable;
            else if (currentFame >= 60 && currentFame < 120) return FameType.Honorable;
            else if (currentFame >= 120) return FameType.Hero;
            else if (currentFame > -60 && currentFame <= -30) return FameType.Despicable;
            else if (currentFame > -120 && currentFame <= -60) return FameType.Notorious;
            else if (currentFame <= -120) return FameType.Villain;
            else if (currentFame > -30 && currentFame < 30) return FameType.Unknown;
            else return FameType.None;
        }
        public int GetFameValue()
        {
            return (int)fameController.fameModel.fameVal;
        }
        public int GetFameYieldValue()
        {
            return (int)fameController.fameModel.fameYield;          
        }

        public void On_FameChg(int val)
        {
            OnFameChg?.Invoke(val); 
        }
        public void On_FameYieldChg(int val)
        {
            OnFameYieldChg?.Invoke(val);
        }
        public List<FameChgData> GetFameChgList()
        {
            return fameController.fameModel.allFameData;        
        }
        public void LoadState()
        {

        }
        public void ClearState()
        {

        }
        public void SaveState()
        {

        }

        public void RestoreState(string basePath)
        {
            throw new NotImplementedException();
        }
    }




}
