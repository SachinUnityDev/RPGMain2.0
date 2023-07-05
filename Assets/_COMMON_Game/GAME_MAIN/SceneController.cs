using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] GameScene currGameScene;
        void Start()
        {

        }
        public void LoadScene(GameScene gameScene)
        {
           StartCoroutine(LoadNewScene(gameScene));    
           //StartCoroutine(UnLoadCurrScene());
        }
        
        
        IEnumerator LoadNewScene(GameScene gameScene)
        {
            //AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync((int)currGameScene);
            //while (!asyncUnload.isDone)
            //{
            //    yield return null;
            //}
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)gameScene, LoadSceneMode.Single);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            if (asyncLoad.isDone)
            {
                GameEventService.Instance.On_TownEnter(LocationName.Nekkisari);
                GameService.Instance.
                GameServiceInit(GameState.InTown, GameDifficulty.Easy, LocationName.Nekkisari);
            }                
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