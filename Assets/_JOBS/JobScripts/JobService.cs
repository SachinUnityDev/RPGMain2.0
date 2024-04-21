using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class JobService : MonoSingletonGeneric<JobService>
    {

        public WoodGameSO woodGameSO;
        public WoodGameController1 woodGameController;



        [Header(" Job Panel View")]
        public GameObject jobViewPrefab; 
        public GameObject jobViewGO;
        public void JobServiceInit()
        {
            woodGameController = GetComponent<WoodGameController1>();
           
        }

        public void ShowJobView()
        {
            JobModel jobModel =
                    woodGameController.GetLoadGameData();

            Canvas canvas = FindObjectOfType<Canvas>();
            if (jobViewGO == null)
            {
                jobViewGO = Instantiate(jobViewPrefab);
            }
            jobViewGO.transform.SetParent(canvas.transform);
            int index = jobViewGO.transform.parent.childCount - 1;
            jobViewGO.transform.SetSiblingIndex(index);
            RectTransform jobViewRect = jobViewGO.GetComponent<RectTransform>();

            jobViewRect.anchorMin = new Vector2(0, 0);
            jobViewRect.anchorMax = new Vector2(1, 1);
            jobViewRect.pivot = new Vector2(0.5f, 0.5f);
            jobViewRect.localScale = Vector3.one;
            jobViewRect.offsetMin = new Vector2(0, 0); // new Vector2(left, bottom);
            jobViewRect.offsetMax = new Vector2(0, 0); // new Vector2(-right, -top);

            jobViewGO.GetComponent<JobView>().InitJobView(jobModel); 
        
        }


        public void StartJob(JobNames jobName)
        {
            // find view prefab from the So and start the game
            switch (jobName)
            {
                case JobNames.None:
                    break;
                case JobNames.Woodcutter:
                    woodGameController.StartGame(); 
                    break;
                case JobNames.Poacher:
                    break;
                default:
                    break;
            }

        }
        //private void Update()
        //{
        //    if(Input.GetKeyUp(KeyCode.W))
        //    {
        //        StartJob(JobNames.WoodCutting);    

        //    }
        //}
    }
    

    public enum JobNames
    {
        None, 
        Woodcutter, 
        Poacher, 
    }


}
