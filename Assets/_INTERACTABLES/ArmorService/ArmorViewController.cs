using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.UI;

namespace Interactables
{
    public class ArmorViewController : MonoBehaviour, IPanel
    {
        [SerializeField] CharNames charSelect;
     
        void Start()
        {
            InvService.Instance.OnCharSelectInvPanel += PopulateArmorPanel;
            UnLoad();
        }
        void OnEnable()
        {
            Load(); 
        }


        #region Load UnLoad
        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject, true);
            Init();
            PopulateArmorPanel(); 
        }
        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject, false);
        }

        public void Init()
        {
            charSelect = InvService.Instance.charSelect;
        }

        #endregion

        #region Populate Armor Content

        void PopulateArmorPanel(CharModel charModel)
        {
            charSelect = charModel.charName; 
            Sprite sprite = ArmorService.Instance.armorSO.GetSprite(charSelect);
            transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite
                = sprite;
        }
        void PopulateArmorPanel()
        {
            Sprite sprite = ArmorService.Instance.armorSO.GetSprite(charSelect);
            transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite
                = sprite;
        }

        #endregion

        #region Populate Gem Content

        #endregion


        #region OnDragged and Drop 

        // gemservice on panel update


        #endregion

        #region Socket Controls
        // is socketable with the divine and support gems 
        // 
        // if socketed inform to the Gem Service 

        #endregion 




    }

}

