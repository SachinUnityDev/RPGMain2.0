using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunguDestroyOnCol : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        Destroy(this.gameObject, 4f);
    }

}