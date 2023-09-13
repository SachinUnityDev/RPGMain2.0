using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class LoreParentViewController : MonoBehaviour, IPanel, iHelp
    {
        [Header("help")]
        [SerializeField] HelpName helpName;

       

        public LoreViewController loreViewController; 
        public RecipeViewController receipeViewController;
        void OnEnable()
        {     
            loreViewController = FindObjectOfType<LoreViewController>(true); 
            receipeViewController = FindObjectOfType<RecipeViewController>(true);
            loreViewController.gameObject.SetActive(true); 
            receipeViewController.gameObject.SetActive(true);  
        }
        public void Init()
        {
            //// init both sub panels here 
            loreViewController = FindObjectOfType<LoreViewController>(true);
            receipeViewController = FindObjectOfType<RecipeViewController>(true);
            loreViewController.GetComponent<IPanel>().Init();
            receipeViewController.GetComponent<IPanel>().Init();
        }

        public void Load()
        {
            // load both sub panels here 
            loreViewController.GetComponent<IPanel>().Load();
            receipeViewController.GetComponent<IPanel>().Load();
        }
        public void UnLoad()
        {
            // unload both subPanels here
            loreViewController.GetComponent<IPanel>().UnLoad();
            receipeViewController.GetComponent<IPanel>().UnLoad();
        }

        public HelpName GetHelpName()
        {
            return helpName;
        }
    }
}


