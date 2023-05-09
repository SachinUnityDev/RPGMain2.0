using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Quest
{
    [CreateAssetMenu(fileName = "AllQuestMainSO", menuName = "Quest/AllQuestMainSO")]

    public class AllQuestSO : ScriptableObject
    {
        public List<QuestSO>  allQuestSO = new List<QuestSO>();

        [Header("QuestMain SO")]

        public Sprite mainQIcon;
        public Sprite sideQIcon;
        public Sprite companionQIcon;
        public Sprite bountyQIcon;

        [Header("Quest info SpriteN")]
        [SerializeField] Sprite stealthInfo;
        [SerializeField] Sprite explorationInfo;
        [SerializeField] Sprite tauntInfo;

        [Header("Quest info SpriteLit")]
        [SerializeField] Sprite stealthInfoLit;
        [SerializeField] Sprite explorationInfoLit;
        [SerializeField] Sprite tauntInfoLit;


        [Header("Quest Mode")]
        [SerializeField] Sprite stealthMode;
        [SerializeField] Sprite explorationMode;
        [SerializeField] Sprite tauntMode;

        public Sprite GetQuestTypeSprite(QuestType questType)
        {
            switch (questType)
            {                
                case QuestType.Main:
                    return mainQIcon;                     
                case QuestType.Side:
                    return sideQIcon;                    
                case QuestType.Bounty:
                    return bountyQIcon;
                case QuestType.Companion:
                    return companionQIcon;
                default:
                    return null;                     
            }
        }    
        public Sprite GetQuestModeSprite(QuestMode questMode)
        {
            switch (questMode)
            {
                case QuestMode.Stealth:
                    return stealthMode;
                case QuestMode.Exploration:
                    return explorationMode;
                case QuestMode.Taunt:
                    return tauntMode;
                default:
                    return null;
            }
        }
        public Sprite GetQuestInfoSpriteN(QuestMode questMode) 
        {
            switch (questMode)
            {
                case QuestMode.Stealth:
                    return stealthInfo; 
                case QuestMode.Exploration:
                    return explorationInfo;
                case QuestMode.Taunt:
                    return tauntInfo;
                default:
                    return null; 
            }
        }
        public Sprite GetQuestInfoSpriteLit(QuestMode questMode)
        {
            switch (questMode)
            {
                case QuestMode.Stealth:
                    return stealthInfoLit;
                case QuestMode.Exploration:
                    return explorationInfoLit;
                case QuestMode.Taunt:
                    return tauntInfoLit;
                default:
                    return null;
            }
        }
        public QuestSO GetQuestSO(QuestNames questName)
        {
            int index = 
                    allQuestSO.FindIndex(t=>t.questName== questName);
            if(index != -1)
            {
                return allQuestSO[index];   
            }
            Debug.Log("Quest SO not found "); 
            return null;
        }
        public List<QuestSO> GetAllQuestTypes(QuestType questType)
        {
            List<QuestSO> questSOs = new List<QuestSO>();   
            questSOs =
                 allQuestSO.Where(t => t.questType == questType).ToList();
            return questSOs;
        }


    }
}