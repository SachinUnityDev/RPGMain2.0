using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class WoodGameService : MonoBehaviour
{

    private static WoodGameService instance;
    public static WoodGameService Instance { get { return instance; } }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as WoodGameService;
        }
        else
        {
            Destroy(this);
        }
    }


   




}


