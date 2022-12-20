using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Combat;
using Cinemachine;
using UnityEngine.UI;

public class DemoCurio : MonoBehaviour
{

    [SerializeField] Sprite HLSprite;
    [SerializeField]SpriteRenderer curioSprite;
    [SerializeField] GameObject popUp;

    public Image interact;
    public Image result;

    public Button tick;
    public Button Continue; 
    void Start()
    {
        curioSprite = GetComponent<SpriteRenderer>();
        interact.gameObject.SetActive(true);
        result.gameObject.SetActive(false);
        tick.onClick.AddListener(OnTickPressed);
        Continue.onClick.AddListener(OnContinuePressed);
    }
    void OnTickPressed()
    {
        interact.gameObject.SetActive(false);
        result.gameObject.SetActive(true);
    }

    void OnContinuePressed()
    {
        UIControlServiceCombat.Instance.ToggleUIStateScale(popUp, UITransformState.Close);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {             
            curioSprite.sprite = HLSprite;
            Debug.Log("Inside on triggerherhe");
            StartCoroutine(Wait()); 
            UIControlServiceCombat.Instance.ToggleUIStateScale(popUp, UITransformState.Open); 
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f); 

    }
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
