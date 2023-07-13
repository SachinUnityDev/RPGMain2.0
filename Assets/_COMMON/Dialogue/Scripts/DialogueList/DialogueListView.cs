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
        public void InitDialogueView(List<DialogueModel> lsModel)
        {
            int i = 0; 
            for (int j = 0; j < lsModel.Count; j++)
            {
                if (lsModel[j].isLocked) continue;
                TextMeshProUGUI txt =  container.GetChild(i).GetComponent<TextMeshProUGUI>();
                container.GetChild(i)
                    .GetComponent<DialogueListPtrEvents>().InitDialogueLsPtr(lsModel[j]); 
                txt.text = $"{i+1}. " +lsModel[j].dialogueTitle;
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