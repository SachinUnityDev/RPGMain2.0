using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using TMPro;

namespace Interactables
{
    public class CompCharParaViewController : MonoBehaviour
    {

        const float SIZE_Multiplier = 4.5f;
        string contentStr ="";

        [SerializeField] TextMeshProUGUI contentTxt; 

        void Start()
        {
            contentStr = ""; 
        }
        public void SetPara(CharNames charSelect)
        {
            contentStr = "";
                    CharComplimentarySO charCompSO = CharService.Instance.charComplimentarySO;

            CharStoryPara charStoryPara =
                        charCompSO.allCharStoryPara.Find(t => t.charName == charSelect);
            if(charStoryPara == null)
            {
                Debug.Log("Char Story para is NULL");
            }

            int index = charStoryPara.unLockedIndex;
            
            for (int i = 0; i <= index; i++)
            {
                contentStr += charStoryPara.allParastr[i]; 
            }
            SetContentSize(contentStr);
            contentTxt.text = contentStr;

        }

        void SetContentSize(string mainTxtSize)
        {   
            string[] strSplt = mainTxtSize.Split(' ');
            int wordLen = strSplt.Length;
            float height = wordLen * SIZE_Multiplier;
            RectTransform rect = contentTxt.GetComponent<RectTransform>();
            float prevWidth = rect.sizeDelta.x;
            rect.sizeDelta = new Vector2(prevWidth, height);


        }


    }


}
