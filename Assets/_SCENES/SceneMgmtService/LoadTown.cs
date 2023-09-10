using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadTown : MonoBehaviour
{
    [SerializeField] Button townBtn; 
    void Start()
    {
        townBtn.onClick.AddListener(OnTownLoadPressed); 
    }
    void OnTownLoadPressed()
    {
        SceneManager.LoadScene("TOWN"); 
    }
  
}
