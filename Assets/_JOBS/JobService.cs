using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class JobService : MonoSingletonGeneric<JobService>
    {

        public WoodGameSO woodGameSO;
        public WoodGameController1 woodGameController; 


        public void JobServiceInit()
        {
            woodGameController = GetComponent<WoodGameController1>();
           


        }

        public void StartJob(JobNames jobName)
        {
            // find view prefab from the So and start the game
            switch (jobName)
            {
                case JobNames.None:
                    break;
                case JobNames.WoodCutting:
                    woodGameController.StartGame(); 
                    break;
                case JobNames.Hunting:
                    break;
                default:
                    break;
            }

        }
        private void Update()
        {
            if(Input.GetKeyUp(KeyCode.W))
            {
                StartJob(JobNames.WoodCutting);    

            }
        }
    }
    

    public enum JobNames
    {
        None, 
        WoodCutting, 
        Hunting, 
    }


}
