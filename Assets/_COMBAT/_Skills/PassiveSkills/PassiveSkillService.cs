using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;
using UnityEngine.SceneManagement;

namespace Combat
{
    public class PassiveSkillService : MonoSingletonGeneric<PassiveSkillService>
    {
        public Action<PassiveSkillName> OnPSkillHovered; 


        public List<string>descLines = new List<string>();


        [SerializeField] GameObject pSkillCardPrefab;
        public GameObject pSkillCardGO;

        public PassiveSkillName currPSkillName;

        SkillView skillView;
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        public void On_PSkillHovered(PassiveSkillName passiveSkillName, CharController charClicked)
        {
            OnPSkillHovered = null; 
            currPSkillName = passiveSkillName;
            PassiveSkillsController pSkillController = charClicked.GetComponent<PassiveSkillsController>();
            PassiveSkillBase pSkillbase =  pSkillController.GetPassiveSkillBase(passiveSkillName);
            pSkillbase.PSkillHovered(); 
            OnPSkillHovered?.Invoke(passiveSkillName);
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (GameService.Instance.currGameModel.gameState == GameScene.InCombat)
            {
                skillView = FindObjectOfType<SkillView>();
                
            }
            GameObject canvasGO = GameObject.FindGameObjectWithTag("Canvas");
            if (pSkillCardGO == null)
            {
                pSkillCardGO = Instantiate(pSkillCardPrefab);
            }
            pSkillCardGO.transform.SetParent(canvasGO.transform);
            pSkillCardGO.transform.SetAsLastSibling();
            pSkillCardGO.transform.localScale = Vector3.one;
            pSkillCardGO.SetActive(false);
        }

    }
}