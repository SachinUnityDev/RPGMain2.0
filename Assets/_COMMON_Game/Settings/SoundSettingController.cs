using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Common
{



    public class SoundSettingController : MonoBehaviour
    {
        [SerializeField] GameObject soundPanel;

        [SerializeField] GameObject masterVol;
        [SerializeField] GameObject musicVol;
        [SerializeField] GameObject ambienceVol;
        [SerializeField] GameObject soundFxVol;
        [SerializeField] GameObject voiceVol; 



     
        void Start()
        {
            masterVol = transform.GetChild(0).gameObject;  
            musicVol = transform.GetChild(1).gameObject;
            ambienceVol = transform.GetChild(2).gameObject;
            soundFxVol = transform.GetChild(3).gameObject;
            voiceVol = transform.GetChild(4).gameObject;


            masterVol.GetComponentInChildren<Slider>().onValueChanged.AddListener(OnMasterVolChg);
            musicVol.GetComponentInChildren<Slider>().onValueChanged.AddListener(OnMusicVolChg);
            ambienceVol.GetComponentInChildren<Slider>().onValueChanged.AddListener(OnAmbienceVolChg);
            soundFxVol.GetComponentInChildren<Slider>().onValueChanged.AddListener(OnSoundFXVolChg);
            voiceVol.GetComponentInChildren<Slider>().onValueChanged.AddListener(OnVoiceVolChg);

        }

        void OnMasterVolChg(float val)
        {
            int percentVal = (int)(val * 100);
            masterVol.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = percentVal.ToString() + "%"; 
           
        }
        void OnMusicVolChg(float val)
        {
            int percentVal = (int)(val * 100);
            musicVol.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = percentVal.ToString() + "%";
        }
        void OnAmbienceVolChg(float val)
        {
            int percentVal = (int)(val * 100);
            ambienceVol.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = percentVal.ToString() + "%";
        }
        void OnSoundFXVolChg(float val)
        {
            int percentVal = (int)(val * 100);
            soundFxVol.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = percentVal.ToString() + "%";
        }
        void OnVoiceVolChg(float val)
        {
            int percentVal = (int)(val * 100);
            voiceVol.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = percentVal.ToString() + "%";
        }

    }
}