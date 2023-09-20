using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class DontDestroy : MonoBehaviour
{
    public string objID;
    private void Awake()
    {
        objID = gameObject.name + transform.position.ToString() + gameObject.tag;
    }
    void Start()
    {
        for (int i = 0; i < FindObjectsOfType<DontDestroy>().Length; i++)
        {
            if (FindObjectsOfType<DontDestroy>()[i] != this)
            {
                if (FindObjectsOfType<DontDestroy>()[i].objID == gameObject.GetComponent<DontDestroy>().objID)
                {
                    Destroy(gameObject);
                }
            }
        }
        DontDestroyOnLoad(gameObject);
    }

}









    // // Start is called before the first frame update
    // private static GameObject instance= null;
    // public static GameObject Instance { get { return instance; } }
    //// public string objID;
    // private void Awake()
    // {
    //     // objID = gameObject.name + transform.position.ToString(); 
    // }
    // void Start()
    // {
    //     if(instance == null)
    //     {
    //         instance = this.gameObject;
    //     }   
    //     else if (instance != null && instance.name != gameObject.name)
    //     {
    //         Destroy(gameObject);
    //         return;
    //     }

    //     // Set the instance to this gameObject


    //     // Ensure that the Singleton persists between scenes
    //     DontDestroyOnLoad(gameObject);
    //     //foreach (Object obj in Object.FindObjectsOfType<DontDestroy>())
    //     //{
    //     //    if(obj != this)
    //     //    {
    //     //        if(obj.GetInstanceID() == gameObject.GetInstanceID())
    //     //        {
    //     //            Destroy(obj);
    //     //        }
    //     //    }                
    //     //}
    //     //DontDestroyOnLoad(gameObject);
    // }

    // // Update is called once per frame
    // void Update()
    // {

    // }
//}
