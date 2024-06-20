using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Common
{


    public class StatDsplyCharLvl : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI charTxt;
        void Start()
        {
            charTxt = GetComponentInChildren<TextMeshProUGUI>();  
            InvService.Instance.OnCharSelectInvPanel += InitCharLvlDsply;
            CharController charController = InvService.Instance.charSelectController;
            InitCharLvlDsply(charController.charModel);
        }
        void OnDisable()
        {
            InvService.Instance.OnCharSelectInvPanel -= InitCharLvlDsply;
        }
        public void InitCharLvlDsply(CharModel charModel)
        {
            charTxt.text = charModel.charLvl.ToString();
        }


    }
}