using UnityEngine;
using UnityEngine.UI;


namespace Common
{
    public class ButtonHLIntro : MonoBehaviour
    {
        Image btnImg;
      
        void Start()
        {
            btnImg = GetComponent<Image>();
           // btnImg.alphaHitTestMinimumThreshold = 0.75f;
           
        }

       
    }
}


