using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Interactables;
using TMPro;

public class CharSlotPlankView : MonoBehaviour
{
    [SerializeField] Image img1;
    [SerializeField] Image img2;
    [SerializeField] Transform orTrans; 

    [SerializeField] CharModel charModel; 
    void Start()
    {
        
    }

    public void InitPlankView(CharModel charModel)
    {
        int index = transform.GetSiblingIndex();
        if (index == 0)
            FillPreReq();
        else if (index == 1)         
            FillProvision();
        else if(index == 2)
            FillEarning();
    }
 
  
    void FillPreReq()
    {
        List<ItemDataWithQty> preReqItems = charModel.GetPrereqsItem();

        //if (preReqItems[0].itemData.itemType == ItemType.None
        //    && )
        //{
        //    img1.gameObject.SetActive(false);
        //    img2.gameObject.SetActive(false);
        //    orTrans.gameObject.SetActive(false);    
        //}
        //img1.sprite = GetSprite(preReqItems[0].itemData);
        //img1.GetComponentInChildren<TextMeshProUGUI>().text = preReqItems.quantity.ToString();

        //orTrans.gameObject.SetActive(false);
        img2.gameObject.SetActive(false);
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
        img1.sprite = GetSprite(provItem.itemData);
        img1.GetComponentInChildren<TextMeshProUGUI>().text = provItem.quantity.ToString(); 

        orTrans.gameObject.SetActive(false);
        img2.gameObject.SetActive(false);

        //get provision
        // get image 
        // check whether it has provision 
        // change color etc and ta daa...
    }
    void FillEarning()
    {

    }
}
