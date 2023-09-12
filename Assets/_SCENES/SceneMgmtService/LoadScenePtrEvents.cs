using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadScenePtrEvents : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] string sceneName; 
    public void OnPointerClick(PointerEventData eventData)
    {
       SceneMgmtController sceneMgmtController = FindAnyObjectByType<SceneMgmtController>();
        sceneMgmtController.StartSceneTransit(); 
        SceneManager.LoadSceneAsync(sceneName);

    }

  
}
