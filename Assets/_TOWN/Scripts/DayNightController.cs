using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
namespace Common
{
    public class DayNightController : MonoBehaviour
    {
        // based on Distance 

        public event Action<TimeState> ONStartOfNight;
        public event Action<TimeState> ONStartOfDay;

        public TimeState timeState;
       
        [SerializeField] float cycleLength =10f;
        float prevTime;
       
        // Start is called before the first frame update
        void Start()
        {          
            prevTime = 0;           
            timeState = TimeState.Day; 
        }           

        public void Change2NextDay()
        {



        }

     
        void Update()
        {
            //if (timeState == TimeState.Day)
            //{
            //    float currentTime = Time.time;

            //    if ((currentTime - prevTime) > cycleLength )
            //    {
            //       // Debug.Log("ON Start of Night");

            //        ONStartOfDay?.Invoke(); 
            //        timeState = TimeState.Night; 
            //        prevTime = currentTime;
            //    }
            //}
            //else if (timeState == TimeState.Night)
            //{
            //    float currentTime = Time.time;

            //    if ((currentTime - prevTime) > cycleLength )
            //    {
            //       // Debug.Log("Night counter");
            //        timeState = TimeState.Day;
            //        ONStartOfNight?.Invoke();
                   
            //        prevTime = currentTime;
            //    }
            //}        
            

        }
    }




}
