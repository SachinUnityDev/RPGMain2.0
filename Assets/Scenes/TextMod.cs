using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class TextMod : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]Material m_sharedMaterial; 
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("on pointer enter");

        // m_sharedMaterial.SetFloat(ShaderUtilities.ID_LightAngle, 0.1f);
        StartCoroutine(FadeUp());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("on pointer exit");
        StartCoroutine(FadeDown());
       // m_sharedMaterial.SetFloat(ShaderUtilities.ID_LightAngle, 5.1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        m_sharedMaterial = text.fontMaterial; 
    }
    IEnumerator FadeDown()
    {

        for (float LAngle = 5f; LAngle >= 0.1f; LAngle -= 0.1f)
        {
            m_sharedMaterial.SetFloat(ShaderUtilities.ID_LightAngle, LAngle);
            yield return null;
        }
    }
    IEnumerator FadeUp()
    {
       
        for (float LAngle = 0.1f; LAngle <= 5; LAngle += 0.1f)
        {  
            m_sharedMaterial.SetFloat(ShaderUtilities.ID_LightAngle, LAngle);
            yield return null;
        }
    }
    [SerializeField] TextMeshProUGUI text; 
}
