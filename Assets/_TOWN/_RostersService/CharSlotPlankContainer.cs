using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSlotPlankContainer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitPlankView(CharModel charModel)
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<CharSlotPlankView>().InitPlankView(charModel);
        }
    }
}
