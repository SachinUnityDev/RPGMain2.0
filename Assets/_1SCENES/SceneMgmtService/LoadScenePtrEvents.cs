using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoadScenePtrEvents : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] SceneName sceneName; 
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneMgmtController sceneMgmtController = SceneMgmtService.Instance.sceneMgmtController;
        StartCoroutine(sceneMgmtController.LoadScene(sceneName));            
    }
}
