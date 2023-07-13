using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Common
{
    public class DialogueListView : MonoBehaviour
    {
        [SerializeField] Transform container;
        private void Awake()
        {
            container = transform.GetChild(1);
        }
        public void InitDialogueView(List<DialogueModel> allDiaModel)
        {
            int i = 0; 
            for (int j = 0; j < allDiaModel.Count; j++)
            {
                if (allDiaModel[j].isLocked) continue;                

                TextMeshProUGUI txt =  container.GetChild(i).GetComponent<TextMeshProUGUI>();
                container.GetChild(i)
                    .GetComponent<DialogueListPtrEvents>().InitDialogueLsPtr(allDiaModel[j]); 
                txt.text = $"{i+1}. " +allDiaModel[j].dialogueTitle;
                i++; 
            }
            for(int k =i; k< container.childCount; k++)
            {
                TextMeshProUGUI txt = container.GetChild(k).GetComponent<TextMeshProUGUI>();
                txt.text = "";
            }
        }
    }
}