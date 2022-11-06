using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class BarkEnum
    {

    }

    public enum BarkType
    {
        None,
        Multiline_MultiChar, 
        MultiOptions_SingleChar, 
        MultiChar_MultiLines, 
        // singular multi Options -----Minami 
        // multichar and multiLines



    }

    public enum BarkTrigger
    {
        None,
        Quest_Barks,
        Prep_Quest_Barks,
        dead_Barks,
        Curio_Barks,
        Town_Barks,
        NPC_Barks,
        Combat_Barks,

    }



}
