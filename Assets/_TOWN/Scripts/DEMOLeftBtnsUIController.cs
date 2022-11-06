using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;
using UnityEngine.UI; 

public class DEMOLeftBtnsUIController : MonoBehaviour
{

    public GameObject openingPanel;
    public GameObject character1;
    public GameObject character2;
    public Button WOTL1;
    public Button WOTL2;
    public Button Candle; 
    
    // Start is called before the first frame update
    void Start()
    {
        WOTL1.onClick.AddListener(ShowText1);
        WOTL2.onClick.AddListener(ShowText2);
        Candle.onClick.AddListener(OnPanelExit); 

    }
    public void OnPanelEnter(GameObject panel)
    {
            openingPanel.transform.DOLocalMoveY(1, 2).SetEase(Ease.OutQuint);

        ChangeAlpha(WOTL1, 0.0f);
        ChangeAlpha(WOTL2, 0.0f);
        character1.SetActive(false);
        character2.SetActive(false);
    }
    public void OnPanelExit()
    {
        openingPanel.transform.DOLocalMoveY(-1200, 1).SetEase(Ease.OutQuint);
        
    }
    public void OnGraveyardBtnClicked()
    {
        // show the opening scripts 
        // as per weeks show the list of characters the died on a perticular week
        OnPanelEnter(openingPanel); 

    }

    public void ShowText1()
    {

        Debug.Log("character 1 clicked");
        ChangeAlpha(WOTL1, 0.5f);

        character1.SetActive(true);
        character2.SetActive(false); 


    }

    void ChangeAlpha(Button BtnGO, float alpha)
    {
        Image image = BtnGO.GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = alpha;
        image.color = tempColor;


    }

    public void ShowText2()
    {

        ChangeAlpha(WOTL2, 0.5f);
        character2.SetActive(true);
        character1.SetActive(false);



    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
