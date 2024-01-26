using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PermaTraitModel 
{
    public int permaTraitID;

    public PermaTraitName permaTraitName;
    public ClassType classType;
    public CultureType cultureType;
    public RaceType raceType;
    public TraitBehaviour traitBehaviour;
    
    [Header("Description")]
    public string traitNameStr = "";

    public PermaTraitModel(PermaTraitSO permaTraitSO, int permaTraitID)
    {
        permaTraitName = permaTraitSO.permaTraitName; 
        classType= permaTraitSO.classType;
        cultureType= permaTraitSO.cultureType;
        raceType = permaTraitSO.raceType;
        traitBehaviour = permaTraitSO.traitBehaviour;

        traitNameStr = permaTraitSO.traitNameStr;

        this.permaTraitID = permaTraitID;
    }
}