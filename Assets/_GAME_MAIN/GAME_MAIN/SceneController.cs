using Intro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] Scene currScene;
        [SerializeField] Scene lastScene;
        [SerializeField] GameScene currGameScene;
        void Start()
        {

        }
        public void LoadScene(GameScene newScene)
        {
            this.currGameScene= newScene;
           // lastScene = SceneManager.GetActiveScene();  
           StartCoroutine(LoadNewScene(newScene));    
        }
        
        
        IEnumerator LoadNewScene(GameScene newScene)
        {         
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)newScene, LoadSceneMode.Additive);          
            asyncLoad.allowSceneActivation = false;
            
            while (!asyncLoad.isDone)
            {
                Debug.Log("Progress" + asyncLoad.progress); 
                yield return null;
            }
            if (asyncLoad.isDone)
            {
                asyncLoad.allowSceneActivation = true;
                currScene = SceneManager.GetSceneByBuildIndex((int)newScene);
                SceneManager.SetActiveScene(currScene);
                //GameEventService.Instance.On_TownEnter(LocationName.Nekkisari);
                //GameService.Instance.
                //    GameServiceInit(GameState.InTown, GameDifficulty.Easy, LocationName.Nekkisari);
            }                
        }      

        public void UnLoadSceneAsync()
        {
            // check on the conflicts....
            // 

        }
    }

    public enum GameScene
    {
        Intro, 
        Town, 
        Quest, 
        Combat,
    }

}