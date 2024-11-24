using Common;
using Quest;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;

namespace Quest
{
    public class HuntInTheWildernessPath : PathBase 
    {
        public override QuestNames questName => QuestNames.HuntInTheWilderness; 

        public override ObjNames objName => ObjNames.TravelIntoTheWilderness;


        public override void OnNode0Enter()
        {
        }

        public override void OnNode0Exit()
        {


        }

        public override void OnNode1Enter()
        {
            EncounterService.Instance.mapEController.ShowMapE(MapENames.BandOfBanditsOne, pathModel);
            Debug.Log("Entered node 1");   
            //QuestMode questMode = QuestMissionService.Instance.currQuestMode; 
            //if(questMode == QuestMode.Stealth)
            //{
            //    if (10f.GetChance())
            //    {
            //        EncounterService.Instance.mapEController.ShowMapE(MapENames.BandOfBanditsOne);                    
            //    }else if (60f.GetChance())
            //    {
            //        EncounterService.Instance.mapEController.ShowMapE(MapENames.MigratoryBirds);
            //    }
            //    else
            //    {
            //        EncounterService.Instance.mapEController.ShowMapE(MapENames.BuffaloStampede);
            //    }

            //}else if(questMode == QuestMode.Exploration)
            //{
            //    if (30f.GetChance())
            //    {
            //        EncounterService.Instance.mapEController.ShowMapE(MapENames.BandOfBanditsOne);
            //    }
            //    else if (30f.GetChance())
            //    {
            //        EncounterService.Instance.mapEController.ShowMapE(MapENames.MigratoryBirds);
            //    }
            //    else
            //    {
            //        EncounterService.Instance.mapEController.ShowMapE(MapENames.BuffaloStampede);
            //    }
            //}
            //else if(questMode== QuestMode.Taunt)
            //{
            //    if (60f.GetChance())
            //    {
            //        EncounterService.Instance.mapEController.ShowMapE(MapENames.BandOfBanditsOne);
            //    }
            //    else if (10f.GetChance())
            //    {
            //        EncounterService.Instance.mapEController.ShowMapE(MapENames.MigratoryBirds);
            //    }
            //    else
            //    {
            //        EncounterService.Instance.mapEController.ShowMapE(MapENames.BuffaloStampede);
            //    }
            //}
        }

        public override void OnNode1Exit()
        {
            
        }

        public override void OnNode2Enter()
        {
            EncounterService.Instance.mapEController.ShowMapE(MapENames.Hunt1, pathModel);
        }

        public override void OnNode2Exit()
        {
            Debug.Log(" ON NODE 2 EXIT");
        }

        public override void OnNode3Enter()
        {
        
            QuestMode questMode = QuestMissionService.Instance.currQuestMode;
            if (questMode == QuestMode.Stealth)
            {
                if (30f.GetChance())
                {
                    EncounterService.Instance.mapEController.ShowMapE(MapENames.BandOfBanditsTwo,pathModel);
                }
                else
                {
                    MapService.Instance.pathController.NodeResult(true);
                }

            }
            else if (questMode == QuestMode.Exploration)
            {
                if (60f.GetChance())
                {
                    EncounterService.Instance.mapEController.ShowMapE(MapENames.BandOfBanditsTwo, pathModel);
                }
                else
                {
                    MapService.Instance.pathController.NodeResult(true);
                }
            }
            else if (questMode == QuestMode.Taunt)
            {
                if (90f.GetChance())
                {
                    EncounterService.Instance.mapEController.ShowMapE(MapENames.BandOfBanditsTwo, pathModel);
                }
                else
                {
                    MapService.Instance.pathController.NodeResult(true);
                }
            }
        }

        public override void OnNode3Exit()
        {
            
        }

        public override void OnNode4Enter()
        {
            
        }

        public override void OnNode4Exit()
        {
            
        }

        public override void OnNode5Enter()
        {
            
        }

        public override void OnNode5Exit()
        {
            
        }

    }
}