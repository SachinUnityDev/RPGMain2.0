using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.UI;


namespace Interactables
{
    public class WeaponViewController : MonoBehaviour, IPanel
    {
        [SerializeField] CharNames charSelect;

        void Start()
        {
            InvService.Instance.OnCharSelectInvPanel += PopulateWeaponPanel;
            UnLoad();
        }
        void OnEnable()
        {
            Load();
        }
        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject, true);
            Init();
            PopulateArmorPanel();
        }

        void PopulateWeaponPanel(CharModel charModel)
        {
            charSelect = charModel.charName;
            Sprite sprite = WeaponService.Instance.weaponSO.GetSprite(charSelect);
            transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite
                = sprite;
        }
        void PopulateArmorPanel()
        {
            Sprite sprite = WeaponService.Instance.weaponSO.GetSprite(charSelect);
            transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite
                                                                  = sprite;
        }
        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject, false);
        }

        public void Init()
        {
            charSelect = InvService.Instance.charSelect;
        }

    }

}

