using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class StartView : MonoBehaviour
{

    [SerializeField] Button startBtn;
    [SerializeField] Button back2TownBtn;

    private void Start()
    {
        startBtn.onClick.AddListener(OnStartBtnPressed); 
        back2TownBtn.onClick.AddListener(OnBack2TownBtnPressed);
    }
    void OnStartBtnPressed()
    {

    }
    void OnBack2TownBtnPressed()
    {

    }
}
