using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    public class RatInfestPath : PathBase
    {
        public override QuestNames questName => QuestNames.RatInfestation; 

        public override ObjNames objName => ObjNames.CleanseTheSewers;
            

        public override void OnNode0Enter()
        {
            Debug.Log("Rat Infest  Node 0 Enter"); 
        }

        public override void OnNode0Exit()
        {
            Debug.Log("Rat Infest  Node 0 Exit");

        }

        public override void OnNode1Enter()
        {
            // transition scene to quest then run the code below


            //QRoomService.Instance.On_QuestSceneStart(QuestNames.RatInfestation);
            EncounterService.Instance.mapEController.ShowMapE(MapENames.BandOfBanditsOne, pathModel);

        }

        public override void OnNode1Exit()
        {
            Debug.Log("Rat Infest  Node 1 Exit");

        }

        public override void OnNode2Enter()
        {
            Debug.Log("Rat Infest  Node 2 Enter");

        }

        public override void OnNode2Exit()
        {
            Debug.Log("Rat Infest  Node 2 Exit");

        }

        public override void OnNode3Enter()
        {
            Debug.Log("Rat Infest  Node 3 Enter");

        }

        public override void OnNode3Exit()
        {
            Debug.Log("Rat Infest  Node 3 Exit");

        }

        public override void OnNode4Enter()
        {
            Debug.Log("Rat Infest  Node 4 Enter");

        }

        public override void OnNode4Exit()
        {
            Debug.Log("Rat Infest  Node 4 Exit");

        }

        public override void OnNode5Enter()
        {
            
        }
        public override void OnNode5Exit()
        {
            
        }
    }
}