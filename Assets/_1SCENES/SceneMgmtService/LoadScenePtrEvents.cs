using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoadScenePtrEvents : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameScene sceneName; 
    public void OnPointerClick(PointerEventData eventData)
    {
       SceneMgmtService.Instance.LoadGameScene(sceneName);
    }
}
