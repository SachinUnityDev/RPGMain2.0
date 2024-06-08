using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Intro
{

    public class IntroController : MonoBehaviour
    {
        int num = 5; 
        

        private void Start() {
            num += 2; 


        }
        private void Update() {
            num += 2; 
        }
    }

}
