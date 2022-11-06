using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using DG.Tweening;



namespace Common
{

    [Serializable]
    public class BGSoundType
    {
        public BGAudioClipNames soundNames;
        public AudioClip audioClip;
    }
    public enum BGAudioClipNames
    {
        None,
        GatesAmbience,
        ShargadMusic,
        PoetryMusic,
        VideoDialogue,

    }
    public class SoundServices : MonoSingletonGeneric<SoundServices>
    {       
        public AudioSource soundFXSource;
        public AudioSource musicSource;
        public AudioSource ambienceSource;
        public AudioSource voiceSource; 

        public List<BGSoundType> allSounds = new List<BGSoundType>();

        //public AudioSource BGMusic;
        //public AudioSource BGMusic2; 
        // public SoundType[] Sounds;
        //private Dictionary<IntroSceneSoundList, float> soundTimerdictionary;
        [SerializeField] BGAudioClipNames currBGClip;
        [SerializeField] float fadeDuration; 

        int currSourceNo = 0; 
        void Start()
        {
            SoundServicesInit();
        }
        public void SoundServicesInit()
        {           
            currBGClip = BGAudioClipNames.None;
            fadeDuration = 3f;
        }
        
        public void StopAllBGSound(float fadeTime)
        {
            if (musicSource.isPlaying)
            {
                musicSource.DOFade(1.0f, fadeDuration).OnComplete(() => musicSource.Stop());
            }
                
            if (ambienceSource.isPlaying)
            {
                ambienceSource.DOFade(1.0f, fadeDuration).OnComplete(() => ambienceSource.Stop());
            }                
        }

        public void PlayBGSound(BGAudioClipNames newClipName)
        {
            AudioClip audioClip = GetBGSoundClip(newClipName);

            if (newClipName != currBGClip)
            {
                if (currBGClip == BGAudioClipNames.None)
                {
                    Debug.Log("Inside BG NONE  state");

                    musicSource.clip = audioClip;
                    musicSource.Play();
                    musicSource.DOFade(1.0f, fadeDuration);
                    ambienceSource.Stop();

                }else if (musicSource.isPlaying)
                {
                    Debug.Log("active GO tate" + ambienceSource.name + "CLIP " + newClipName);
                    ambienceSource.clip = audioClip;
                    ambienceSource.Play();                    
                    ambienceSource.DOFade(1.0f, fadeDuration);
                   
                    musicSource.DOFade(0f, fadeDuration).OnComplete(() => musicSource.Stop());

                }else if (ambienceSource.isPlaying)
                {                        
                    Debug.Log(" active GO BG1  state" +musicSource.name + "CLIP "+ newClipName);

                    musicSource.clip = audioClip;
                    musicSource.Play();
                    musicSource.DOFade(1.0f, fadeDuration);
                    ambienceSource.DOFade(0f, fadeDuration).OnComplete(() => ambienceSource.Stop());
                }
               
            }

            currBGClip = newClipName;

        }
        AudioClip GetBGSoundClip(BGAudioClipNames sound)
        {
            Debug.Log("Sound Name" + sound);
            AudioClip audio = allSounds.Find(t => t.soundNames == sound).audioClip;
            return audio;
        }

        //public void PlayBackGround(BGAudioClipNames newClipName)
        //{
        //    AudioClip audioClip = GetBGSoundClip(newClipName);
        //    int inActiveChild = GetInActiveChild();
        //    if (newClipName != currBGClip)
        //    {

        //        gameObject.transform.GetChild(inActiveChild).gameObject.SetActive(true);
        //        AudioSource fadeInObj = gameObject.transform.GetChild(inActiveChild).GetComponent<AudioSource>();


        //        fadeInObj.clip = audioClip;
        //        fadeInObj.volume = 1f; 

        //        StartCoroutine(StartFade(fadeInObj, 1f, 1f));

        //        AudioSource fadeOutObj = gameObject.transform.GetChild(currSourceNo).GetComponent<AudioSource>(); 
        //        StartCoroutine(StartFade(fadeOutObj, 1f, 0f));
        //    }
        //    else if (currBGClip == BGAudioClipNames.None)
        //    {
        //        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        //        AudioSource sourceObj = gameObject.transform.GetChild(currSourceNo).GetComponent<AudioSource>();

        //        sourceObj.clip = audioClip;
        //        StartCoroutine(StartFade(sourceObj, 1f, 1f)); 


        //    }
        //    currSourceNo = inActiveChild;
        //    currBGClip = newClipName;
        //}

        //int GetInActiveChild()
        //{
        //    if (currSourceNo == 0)
        //        return 1;
        //    else return 0; 
        //}


        //public  IEnumerator StartFade(AudioSource sourceClip, float duration, float targetVolume)
        //{
        //    float currentTime = 0;
        //    float start = sourceClip.volume;

        //    while (currentTime < duration)
        //    {
        //        currentTime += Time.deltaTime;
        //        sourceClip.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
        //        yield return null;
        //    }
        //    if (targetVolume == 0f)
        //    {

        //        sourceClip.gameObject.SetActive(false);
        //    }

        //    yield break;
        //}

        //public void PlayFX(IntroSceneSoundList sound)
        //{
        //    AudioClip clip = getSoundClip(sound);
        //    if ((clip != null) && CanPlaySound(sound))
        //    {
        //        soundEffect.PlayOneShot(clip);
        //    }
        //    else if (clip == null)
        //    {
        //        Debug.Log("Sound Effect clip not found");
        //    }
        //}

        //public bool CanPlaySound(IntroSceneSoundList name)
        //{
        //    switch (name)
        //    {
        //        case IntroSceneSoundList.None:

        //            if (soundTimerdictionary.ContainsKey(name))
        //            {
        //                float lastTimePlayed = soundTimerdictionary[name];
        //                float timeGap = 0.5f;
        //                if (lastTimePlayed + timeGap < Time.time)
        //                {
        //                    soundTimerdictionary[name] = Time.time;
        //                    //  Debug.Log("Player sound played"); 
        //                    return true;
        //                }
        //                else return false;
        //            }
        //            else return true;
        //        default:
        //            return true;
        //    }
        //}
        //private AudioClip getSoundClip(IntroSceneSoundList sound)
        //{
        //    SoundType item = Array.Find(Sounds, i => i.soundType == sound);
        //    return item?.soundClip;

        //}



    }


    //public class SoundType
    //{
    //    public IntroSceneSoundList soundType;
    //    public AudioClip soundClip;
    //}
    //public enum IntroSceneSoundList
    //{
    //   None, 
    //   Click, 
    //}
   
}






