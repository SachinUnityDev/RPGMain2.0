using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;

namespace Common
{
    public class DialogueFactory : MonoBehaviour
    {
        public Dictionary<DialogueNames, Type> allDialogBases;
        [SerializeField] int dialogueCount = 0;
        void Start()
        {
            allDialogBases = new Dictionary<DialogueNames, Type>();
            InitDialogues();  // start of the game
        }

        public void InitDialogues()
        {

            if (allDialogBases.Count > 0) return;

            var getDiaControllers = Assembly.GetAssembly(typeof(IDialogue)).GetTypes()
                                 .Where(myType => myType.IsClass && !myType.IsAbstract 
                                 && myType.GetInterfaces().Contains(typeof(IDialogue)));

            foreach (var diaCtrl in getDiaControllers)
            {
                var t = Activator.CreateInstance(diaCtrl) as IDialogue;
                allDialogBases.Add(t.dialogueNames, diaCtrl);
            }
            dialogueCount = allDialogBases.Count;
        }

        public IDialogue GetDialogBase(DialogueNames _dialogueNames)
        {
            foreach (var diaCtrl in allDialogBases)
            {
                if (diaCtrl.Key == _dialogueNames)
                {
                    var t = Activator.CreateInstance(diaCtrl.Value) as IDialogue;
                    return t;
                }
            }
            Debug.Log("IDialogue class Not found");
            return null;
        }

    }

    public enum DialogueNames
    {
        None,
        MeetKhalid,  // top panel 
        XX, // 
        DebtIsClear, // 
        AttendJob,
        GoVisitTemple,
        XXX,        
        MeetMinami,
        ProofOfPower,
        MeetElder,
        ElderQuestDone,
        CompleteBilokoQuest,
        JourneyBegins,
        SwampReport,
        CityHallCalls,
        CityHallMeeting,
        Casualties,
        StoryMinami, 
        StoryRayyan, 
        StoryCahyo, 
        StoryBaran, 
        StoryAmadi, 
        StoryAmish, 
        StoryKamila, 
        StoryOmobolanle, 
        StoryGreybrow,
        StoryBelal, 
        StoryAdalberto,   
        MeetRayyan, 
        MeetCahyo, 
        MeetBaran,
    }

}
