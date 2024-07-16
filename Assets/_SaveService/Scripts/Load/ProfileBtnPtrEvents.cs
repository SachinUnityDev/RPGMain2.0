using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Common
{


    public class ProfileBtnPtrEvents : MonoBehaviour
    {
        [SerializeField] Image abbasImg;
        [SerializeField] TextMeshProUGUI dateTxt;
        [SerializeField] TextMeshProUGUI weektxt;
        [SerializeField] TextMeshProUGUI profileTxt;
        [SerializeField] Image lodImg;

        [Header("ref")]
        LoadView loadView; 
        ProfilePgView profilePgView;

        public void Init(GameModel gameModel, ProfilePgView profilePgView, LoadView loadView)
        {
            this.loadView = loadView; 
            this.profilePgView = profilePgView;


            profileTxt.text = gameModel.GetProfileName();
            Sprite spriteAbbas =
                CharService.Instance.allCharSO.GetAbbasClasstypeData(gameModel.abbasClassType).spriteN;
            abbasImg.sprite = spriteAbbas;

            Sprite spriteLOD =
                GameService.Instance.gameController.allGameDiffSO.GetGameDiffSO(gameModel.gameDifficulty).spriteN;
            lodImg.sprite = spriteLOD;

            abbasImg.gameObject.SetActive(true);
            lodImg.gameObject.SetActive(true);
        }
    }
}