using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class AnswerButton : MonoBehaviour
{
    
    private void OnEnable()
    {
       this.gameObject.GetComponent<Button>().onClick.AddListener(OnAnswerBtnClicked);
        
    }
    public void OnAnswerBtnClicked()
    {       
        Debug.Log(gameObject.name);
        Debug.Log("Sibling Index : " + transform.GetSiblingIndex());
        DialogueServDeprecated.Instance.ProcessResponse(transform.GetSiblingIndex()); 
    }



}
