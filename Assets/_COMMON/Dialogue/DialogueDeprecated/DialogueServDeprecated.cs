using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueServDeprecated : MonoSingletonGeneric<DialogueServDeprecated>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ProcessResponse(int option)
    {

        switch (option)
        {
            case 0:
                Debug.Log("Varun is a killer ");
                // code block
                break;
            case 1:
                Debug.Log("VP is a dumbo ");
                break;
            case 2:
                Debug.Log("VP is a super dumbo ");
                break;
            case 3:
                // code block
                break;
            default:
                // code block
                break;
        }
    }   // Update is called once per frame
  
}
