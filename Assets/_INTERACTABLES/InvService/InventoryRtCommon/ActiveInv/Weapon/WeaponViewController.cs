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
        [SerializeField] Image gemImg; 
        void Awake()
        {
            
                   
            gemImg = transform.GetChild(1).GetChild(0).GetComponent<Image>();
           
        }
        private void OnEnable()
        {
            InvService.Instance.OnCharSelectInvPanel += PopulateWeaponPanel;
            ItemService.Instance.OnItemEnchanted += PopulateGemEnchanted;
        }
        private void OnDisable()
        {
            InvService.Instance.OnCharSelectInvPanel -= PopulateWeaponPanel;
            ItemService.Instance.OnItemEnchanted -= PopulateGemEnchanted;
        }
        private void Start()
        {
            gameObject.SetActive(false);
        }
        public void Load()
        {         
            Init();           
           //  PopulateGemEnchanted(); 
        }
        void PopulateWeaponPanel(CharModel charModel)
        {
            charSelect = charModel.charName;
            Sprite sprite = WeaponService.Instance.allWeaponSO.GetWeaponSO(charSelect).weaponSprite;                 
            transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = sprite;
        }

        void PopulateGemEnchanted(CharController charController1, Iitems iitem)
        {          
            // use char Controller 1 on test
            CharController charController = InvService.Instance.charSelectController;
            if (charController == null) return;
            ItemModel itemModel = charController.itemController.itemModel;
            GemChargeData charge = itemModel.gemChargeData; 
            if(charge == null)
            {
                gemImg.gameObject.SetActive(false);
                return; 
            }
            GemSO gemSO =
                     ItemService.Instance.GetGemSO(charge.gemName);
            gemImg.gameObject.SetActive(true);
            gemImg.sprite = gemSO.iconSprite; 
        }

        public void UnLoad()
        {
           
        }

        public void Init()
        {
            charSelect = InvService.Instance.charSelectController.charModel.charName;
        }

    }

}

