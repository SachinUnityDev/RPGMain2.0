using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class LoreParentViewController : MonoBehaviour, IPanel
    {
        [SerializeField] Transform lorePanel;
        [SerializeField] Transform receipePanel;

        public LoreViewController loreViewController; 
        public ReceipeViewController receipeViewController;
        void Awake()
        {
            lorePanel = transform.GetChild(0);
            receipePanel = transform.GetChild(1);   
            lorePanel.gameObject.SetActive(true);
            receipePanel.gameObject.SetActive(true);
            loreViewController = lorePanel.GetComponent<LoreViewController>(); 
            receipeViewController = receipePanel.GetComponent<ReceipeViewController>();

        }
        public void Init()
        {
            // init both sub panels here 
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
    }
}


