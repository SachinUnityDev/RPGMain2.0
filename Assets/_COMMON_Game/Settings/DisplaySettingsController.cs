using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System; 

namespace Common
{
    public class DisplaySettingsController : MonoBehaviour
    {
        [SerializeField] TMP_Dropdown resolutionDropDown;
        [SerializeField] Toggle fullScreenToggle;
        [SerializeField] Toggle vSyncToggle;
        [SerializeField] Toggle cameraToggle;
        [SerializeField] TMP_Dropdown bloomDropDown;
        [SerializeField] TMP_Dropdown qualityDropDown;
        [SerializeField] TMP_Dropdown colorBlindDropDown;
        [SerializeField] TMP_Dropdown brightnessDropDown;

        public List<string> resolutionList = new List<string>() { "1024x576 (16:9)","1280x720 (16:9)", "1280x800 (16:10)","1376x774 (16:9)", "1440x900 (16:10)",
            "1600x900 (16:9)", "1680x1050 (16:10)", "1920x1080 (16:9)", "1920x1200 (16:10)", "2560x1440 (16:9)", "2560x1600 (16:10)", "3840x2160 (16:9)"
                }; 

        void Start()
        {
           // resolutionDropDown.AddOptions("hello world");
        }

        public void ResolutionChg()
        {

        }

        public void FullScreenOn(bool  fullScreenOn)
        {
            Screen.fullScreen = fullScreenOn;
        }

        public void VSyncOn(bool vSyncOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        public void CameraShakeToggle(bool cameraShakeOn)
        {
            // to be fixed later
        }

        public void BloomChg()
        {
            // get hold of global vol every scene and cut the bloom 
        }

        public void QualityChg()
        {
            QualitySettings.SetQualityLevel(qualityDropDown.value, true);
        }

        public void ColorBlindnessDropDown()
        {
            // to be done 


        }
        public void BrightnessDropDown()
        {
            switch (brightnessDropDown.value)
            {


                default:
                    break;
            }

        }



        //    Resolution
        //    Fullscreen
        //Bloom
        //Camera Shake
        //Vsync
        //Quality
        //Brightness
        //Color Blind
        //Blood fx ?
    }





}
