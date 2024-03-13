using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Interactables;
using TMPro;
using UnityEngine.EventSystems;


public class CharSlotPlankView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Transform img1;
    [SerializeField] Transform img2;
    [SerializeField] Transform orTrans;

    [SerializeField] TextMeshProUGUI earningShareTxt;

    [SerializeField] CharModel charModel;

    [Header("OnHover")]
    [SerializeField] Sprite spriteHL;
    [SerializeField] Sprite spriteN;

    [SerializeField] Image img;
    [SerializeField] int index;
    [SerializeField] string str;
    [SerializeField] GameObject toolTipPlank;



    void Awake()
    {
     
    }

    public void InitPlankView(CharModel charModel)
    {
        this.charModel = charModel; 
        int index = transform.GetSiblingIndex();
        if (index == 0)
            FillPreReq();
        else if (index == 1)         
            FillProvision();
        else if(index == 2)
            FillEarning();

        img = transform.GetChild(0).GetComponent<Image>();
        img.sprite = spriteN;
      
        toolTipPlank.SetActive(false);
    }
 
  
    void FillPreReq()
    {
        List<ItemDataWithQty> preReqItems = charModel.GetPrereqsItem();
              

        if(preReqItems.Count == 2)
        {
            FillItemCell(preReqItems[0], img1);
            FillItemCell(preReqItems[1], img2);
            orTrans.gameObject.SetActive(true);
           
        }
        if (preReqItems.Count == 1)
        {
            FillItemCell(preReqItems[0], img1);
            FillItemCell(null, img2);
            orTrans.gameObject.SetActive(false);
        }
        if (preReqItems.Count == 0)
        {
            FillItemCell(null, img1);
            FillItemCell(null, img2);
            orTrans.gameObject.SetActive(false);
        }
        if (preReqItems.Count == 2)
        {
            string itemNameStr = InvService.Instance.InvSO.GetItemName(preReqItems[0].itemData);
            string charNameStr = charModel.charNameStr;
            string itemNameStr2 = InvService.Instance.InvSO.GetItemName(preReqItems[1].itemData);            
            str = $"{charNameStr} wants <style=States>{itemNameStr}</style> and <style=States>{itemNameStr2}</style> as incentives to join the party.";
        }
        if (preReqItems.Count == 1)
        {
            string itemNameStr = InvService.Instance.InvSO.GetItemName(preReqItems[0].itemData);
            string charNameStr = charModel.charNameStr;
            str = $"{charNameStr} wants <style=States>{itemNameStr}</style> as incentives to join the party.";

        }
        if (preReqItems.Count == 0)
        {
            string charNameStr = charModel.charNameStr;
            str = $"{charNameStr} wants no items as incentives to join the party.";
        }
        
    }

    void FillItemCell(ItemDataWithQty itemDataQty,  Transform cellTran)
    {
       // 0,1,2
       if(itemDataQty == null)
       {
           cellTran.gameObject.SetActive(false);          
            return; 
       }            
       else
       {
          cellTran.gameObject.SetActive(true);           
       }
        cellTran.GetComponent<Image>().sprite = GetSprite(itemDataQty.itemData); 
        if(itemDataQty.quantity > 1)
            cellTran.GetComponentInChildren<TextMeshProUGUI>().text = itemDataQty.quantity.ToString();
        else
            cellTran.GetComponentInChildren<TextMeshProUGUI>().text = "";
    }
   
    void FillEarning()
    {
        ItemDataWithQty itemQty = charModel.GetEarningItem();
        if(itemQty == null)
        {
            str = "";
            FillItemCell(itemQty, img1);
            earningShareTxt.text = ""; 
            img2.gameObject.SetActive(false);   
        }
        else
        {
            img2.gameObject.SetActive(true);
            string itemNameStr = InvService.Instance.InvSO.GetItemName(itemQty.itemData);
            string charNameStr = charModel.charNameStr;
            str = $"Every <style=States>{itemNameStr}</style> and 50 % of money found in quest will go to {charNameStr}.";

            itemQty.quantity = 0;
            FillItemCell(itemQty, img1);
            // fill earning share
            earningShareTxt.text = charModel.earningsShare.ToString() + " %";
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTipPlank.SetActive(true);
        toolTipPlank.GetComponentInChildren<TextMeshProUGUI>().text = str;
        img.sprite = spriteHL;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTipPlank.SetActive(false);
        img.sprite = spriteN;
    }
    Sprite GetSprite(ItemData itemData)
    {
        Sprite sprite = InvService.Instance.InvSO.GetSprite(itemData.itemName, itemData.itemType);
        if (sprite != null)
            return sprite;
        else
            Debug.Log("SPRITE NOT FOUND");
        return null;
    }
    void FillProvision()
    {
        ItemDataWithQty provItem = charModel.GetProvisionItem();
        if(provItem != null )
        {
            string itemNameStr = InvService.Instance.InvSO.GetItemName(provItem.itemData);
            str = $"Provision slot: <style=States>{itemNameStr}</style>. Replenishes upon completing quest.";
            FillItemCell(provItem, img1);
        }
        else
        {
            str = "";
            CharController charController = CharService.Instance.GetAbbasController(CharNames.Abbas); 
            int charID = charController.charModel.charID;
            ActiveInvData activeInvData = InvService.Instance.invMainModel.allActiveInvData.Find(t => t.CharID == charID);

            Iitems item = activeInvData?.potionActiveInv[2];

            
            if (item != null)
            {
                ItemData itemdata = new ItemData(item.itemType, item.itemName);
                ItemDataWithQty itemDataWithQty = new ItemDataWithQty(itemdata, 1);
                string itemNameStr = InvService.Instance.InvSO.GetItemName(itemdata);
                str = $"Provision slot: <style=States>{itemNameStr}</style>. Replenishes upon completing quest.";
                FillItemCell(itemDataWithQty, img1);
            }
            else
            {
                str = $"";
                FillItemCell(null, img1);
            }
        }
        
    }
}
