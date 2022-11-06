using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;

namespace Intro
 { 
    public class IntroUIController : MonoBehaviour
    {
        [Header("CANVAS")]

        [SerializeField]Canvas canvas;
        [SerializeField] float zoomSpeed = 0.24f;
        [SerializeField] float zoomScaler = 1.80f; 
        bool isZoomOn = false; 
        [Header("PANELS")]
        [SerializeField] GameObject openingPanel;  
        [SerializeField] GameObject mountainPanel;
        [SerializeField] GameObject mainLogo;       
        [SerializeField] Button comeIn;

        [Header("MOUNTAIN PANEL Buttons")]
        [SerializeField] CanvasGroup buttonParent; 
        [SerializeField] Button newGame;
        [SerializeField] Button loadGame;
        [SerializeField] Button exitGame;
        [SerializeField] Button credits;
        [SerializeField] Button options;
        [SerializeField] Button cutScenes;

        
       [SerializeField] CinemachineVirtualCamera vcam;
        [SerializeField] Camera cam; 
        Vector3 prevScale =  Vector3.zero; 

        // Start is called before the first frame update
        void Start()
        {            
            comeIn.onClick.AddListener(ComeInClick);
            newGame.onClick.AddListener(NewGame);
            loadGame.onClick.AddListener(LoadGame);
            exitGame.onClick.AddListener(ExitGame);
            credits.onClick.AddListener(CreditsDisplay);
            cutScenes.onClick.AddListener(ShowCutScenes); 
           StartCoroutine(Seq()); 
        }

        void NewGame()
        {
            Debug.Log("NewGame"); 
        }
        void LoadGame()
        {
            Debug.Log("LoadGame");
        }
        void ExitGame()
        {
            Debug.Log("ExitGame");
        }
        void CreditsDisplay()
        {
            Debug.Log("Credits");
        }
        void ShowCutScenes()
        {
            Debug.Log("ShowCutScenes");
        }

        private IEnumerator Seq()
        {
            yield return StartCoroutine(OnSceneOpen());
            yield return StartCoroutine(MountainSceneOpen());
          
        }
        IEnumerator OnSceneOpen()
        {
            openingPanel.SetActive(true); 
            FadingImages(openingPanel, 1.0f, 0, 2.0f);
            yield return new WaitForSeconds(2.5f);        

        }
        
        IEnumerator MountainSceneOpen()
        {
            openingPanel.SetActive(false);
            mountainPanel.SetActive(true);
            FadingImages(mountainPanel, 1.0f, 1.0f, 2.0f);
            yield return new WaitForSeconds(2.5f);            

        }

        void FadingImages(GameObject _goPanel, float _alpha, float _color , float _duration)
        {
            Image[] imgs = _goPanel.GetComponentsInChildren<Image>();
            Color finalColor = new Color(_color, _color, _color, _alpha);
            foreach (Image i in imgs)
            {
                i.DOColor(finalColor, _duration);
            }   
        }     
      
        void ComeInClick()
        {
            comeIn.gameObject.SetActive(false);
            mainLogo.GetComponent<Image>().DOColor(new Color(1f, 1f, 1f, 0f), 1.0f);            
            Debug.Log("CanvasScale" + canvas.scaleFactor);        
            
            buttonParent.DOFade(1.0f, 3.0f); // button appears
          //  cam.orthographicSize = 5.0f; 
            isZoomOn = true;
        }
        void FixedUpdate()
        {
            if ( isZoomOn)
            {

                // change to perspective and zoom
               // cam.fieldOfView -= .1f;
                // canvas.scaleFactor += canvas.scaleFactor * Time.deltaTime * zoomSpeed; 
                canvas.scaleFactor = Mathf.Lerp(canvas.scaleFactor, 2, Time.deltaTime * zoomSpeed);
               // vcam.m_Lens.FieldOfView += vcam.m_Lens.FieldOfView * Time.deltaTime * zoomSpeed; 

            }
            else
            {
                isZoomOn = false; 
            }
        }
    }
 }


//int childCount = ButtonParent.transform.childCount;

//  for (int i = 0; i < childCount; i++)
//      ButtonParent.transform.GetChild(i).gameObject.SetActive(true); 


//NewGame.gameObject.SetActive(true);
//LoadGame.gameObject.SetActive(true);
//ExitGame.gameObject.SetActive(true);
//Credits.gameObject.SetActive(true);
//Options.gameObject.SetActive(true);
//CutScenes.gameObject.SetActive(true);