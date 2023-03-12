using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


namespace Town
{
    public class HouseProvSlotView : MonoBehaviour
    {

        public void Init(Iitems item)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = GetSprite(item);
        }
        Sprite GetSprite(Iitems item)
        {
            Sprite sprite = InvService.Instance.InvSO.GetSprite(item.itemName, item.itemType);
            if (sprite != null)
                return sprite;
            else
                Debug.Log("SPRITE NOT FOUND");
            return null;
        }

    }
}