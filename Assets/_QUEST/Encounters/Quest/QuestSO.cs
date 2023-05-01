using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSO : MonoBehaviour
{
    public CityENames encounterName;
    public int encounterSeq;
    public CityEState state;

    [TextArea(5, 10)]
    public string descTxt;
    public string choiceAStr;
    public string choiceBStr;
}
