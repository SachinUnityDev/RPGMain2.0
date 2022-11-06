using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSmith : MonoBehaviour
{    

        int randomTrialNumber; // 0,1       
        int mouseInput = -1; 
        // Start is called before the first frame update
        void Start()
        {         
           randomTrialNumber = Random.Range(0, 2);
           StartCoroutine(Gameplay());
        }

        // Update is called once per frame
        void Update()
        {
            InputHandling(); 

        }

        void InputHandling()
        {
            if (Input.GetMouseButton(0))
            {
                mouseInput = 0; 
            }
            if(Input.GetMouseButton(1))
            {
                mouseInput = 1;
            }
        }

        IEnumerator Gameplay()
        {
            while (true)  /// put in here the number of trials
            {
                randomTrialNumber = Random.Range(0, 2);
                Debug.Log("Random click" + randomTrialNumber); 
                yield return new WaitForSeconds(2.0f); 
                if (randomTrialNumber == mouseInput)
                {
                    Debug.Log("Success");
                }
                else
                {
                    Debug.Log("Fail"); 
                }
                mouseInput = -1; 
            }
        }
    }





//Debug.Log("rand" + randomTrialNumber);
//if (Input.GetMouseButton(0))
//{
//    Debug.Log("left click ");
//    if (randomTrialNumber == 0)
//    {
//        Debug.Log("SUCCESS");

//    }
//    else
//    {

//        Debug.Log("FAIL");
//    }


//}
//else if (Input.GetMouseButton(1))
//{
//    Debug.Log("right click ");
//    if (randomTrialNumber == 1)
//    {
//        Debug.Log("SUCCESS");

//    }
//    else
//    {
//        Debug.Log("Fail");
//    }
//}

