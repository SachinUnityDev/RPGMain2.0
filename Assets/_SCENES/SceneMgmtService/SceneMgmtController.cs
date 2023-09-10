using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Common
{


    public class SceneMgmtController : MonoBehaviour
    {
        [SerializeField] Button loadQ;
        [SerializeField] Button loadC;


        private void Start()
        {
            loadQ.onClick.AddListener(OnQuestLoadPressed); 
            loadC.onClick.AddListener(OnCombatLoadPressed);

            SceneManager.sceneLoaded += OnSceneLoaded; 
        }

        void OnQuestLoadPressed()
        {
            //SceneMgmtService.Instance.LoadNewScene("QUEST"); 
            SceneManager.LoadScene("QUEST"); 
        }
        void OnCombatLoadPressed()
        {
            SceneMgmtService.Instance.LoadNewScene("COMBAT");

        }
        
        void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            // toggle off camera, event System and canvas  of previous scene
            // toggle on ...
            // allowscene activation  here 
        }


    }
}