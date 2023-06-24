using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{


    public class DontDestroy : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < Object.FindObjectsOfType<DontDestroy>().Length; i++)
            {
                DontDestroy dontDestroy = Object.FindObjectsOfType<DontDestroy>()[i]; 
                if (dontDestroy != this)
                {
                    if(dontDestroy.gameObject.name == this.name)
                    {
                        Destroy(dontDestroy.gameObject);
                    }
                }
            }
            DontDestroyOnLoad(this.gameObject);
        }


    }
}