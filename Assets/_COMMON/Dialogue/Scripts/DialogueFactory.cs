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
        public Dictionary<DialogueNames, Type> allDialogueControllers;
        [SerializeField] int dialogueCount = 0;
        void Start()
        {
            allDialogueControllers = new Dictionary<DialogueNames, Type>();
            InitDialogues();  // start of the game
        }

        public void InitDialogues()
        {

            if (allDialogueControllers.Count > 0) return;

            var getDiaControllers = Assembly.GetAssembly(typeof(IDialogue)).GetTypes()
                                 .Where(myType => myType.IsClass && !myType.IsAbstract 
                                 && myType.GetInterfaces().Contains(typeof(IDialogue)));

            foreach (var diaCtrl in getDiaControllers)
            {
                var t = Activator.CreateInstance(diaCtrl) as IDialogue;
                allDialogueControllers.Add(t.dialogueNames, diaCtrl);
            }
            dialogueCount = allDialogueControllers.Count;
        }

        public IDialogue GetDiaController(DialogueNames _dialogueNames)
        {
            foreach (var diaCtrl in allDialogueControllers)
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
        RetrieveDebt, // 
        DebtIsClear, // 
        AttendJob,
        GoVisitTemple,
        AltGoVisitTemple,        
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
