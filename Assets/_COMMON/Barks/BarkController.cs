using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;

namespace Common
{
    public class BarkController : MonoBehaviour
    {
        // The Content Size Fitter does work but you have to make sure you are using 
        //the TextMeshPro UGUI Component which is located in Create - UI - TextMeshPro - Text


        public BarkSO barkSO;  // will contain all the bask of a character 
        public GameObject barkPopup;
        // Start is called before the first frame update

        void Start()
        {
            ShowBarksWithTime();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Barkable")  // use a interface if needed 
            {
                // match ID of barklines with the gameobject collider



            }
        }


        void ShowBarksWithTriggers()
        {

        }


        async void ShowBarksWithTime()
        {

            //foreach (var bark in barkSO.barkLines)
            //{
            //    float time = UnityEngine.Random.Range(1.0f, 4.0f);
            //    await Task.Delay(TimeSpan.FromSeconds(time));
            //    barkPopup.GetComponent<TextMesh>().text = bark.txt.ToString();
            //}

        }



    }


}

