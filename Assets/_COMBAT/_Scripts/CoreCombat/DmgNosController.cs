using Combat;
using Common;
using DamageNumbersPro;
using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class DmgNosController : MonoBehaviour
{
    [Header("Dmg Crit Feeble")]
    [SerializeField] DamageNumber dmgPrefab;
    [SerializeField] DamageNumber critPrefab;
    [SerializeField] DamageNumber feeblePrefab;
    [SerializeField] DamageNumber dodgePrefab; 

    [Header("Morale haste Chks")]
    [SerializeField] DamageNumber moraleChkPrefab_Pos;
    [SerializeField] DamageNumber moraleChkPrefab_Neg;
    [SerializeField] DamageNumber hasteChkPrefab;

    [Header(" Char State Chk")]
    [SerializeField] DamageNumber bleedPrefab;
    [SerializeField] DamageNumber poisonPrefab;
    [SerializeField] DamageNumber burnPrefab;

    [Header("Heal Stm Fort")]
    [SerializeField] DamageNumber healPrefab;
    [SerializeField] DamageNumber stmPrefab;
    [SerializeField] DamageNumber fortPrefab;

// Start is called before the first frame update
    void Start()
    {
        //CharStatesService.Instance.OnCharStateStart += OnCharStateStart;
       // CombatEventService.Instance.OnDamageApplied += OnDmgApplied;

        //CombatEventService.Instance.OnDodge += OnDodge;
        CombatEventService.Instance.OnDamageApplied += OnDmgApplied;
        //CombatEventService.Instance.OnMisfire += OnMisFire;
    }
    void OnDisable()
    {
        CombatEventService.Instance.OnDamageApplied -= OnDmgApplied;
    }
    void OnCharStateStart(CharStateModData charStateModData)
    {
        // get prefab // 
    }

    //void OnDmgApplied(DmgAppliedData dmgAppliedData)
    //{
    //    GameObject targetGo = dmgAppliedData.targetController.gameObject;
    //    Vector3 pos = targetGo.transform.position;
    //    float y = targetGo.GetComponentInChildren<SpriteRenderer>().size.y;
    //    pos += new Vector3(0f, y/2, 0f);
    //    ShootNos(dmgPrefab, pos, dmgAppliedData.dmgValue);
    //}

    void OnDmgApplied(DmgAppliedData dmgAppliedData)
    {
        GameObject targetGo = dmgAppliedData.targetController.gameObject; 

        Vector3 pos = targetGo.transform.position;
        float y = targetGo.GetComponentInChildren<SpriteRenderer>().size.y;
      
        switch (dmgAppliedData.strikeType)
        {
            case StrikeType.None:
                break;
            case StrikeType.Feeble: // down 
                pos +=  new Vector3(0f, y/2, 0f);
                ShootNos(feeblePrefab, pos, dmgAppliedData.dmgValue); 
                break;
            case StrikeType.Crit: // up 
                pos += new Vector3(0f, y/2, 0f);
                ShootNos(critPrefab, pos, dmgAppliedData.dmgValue);
                break;
            case StrikeType.Normal:
                ShootNos(dmgPrefab, pos, dmgAppliedData.dmgValue);
                break;
            case StrikeType.Dodged: // up 
                pos += new Vector3(0f, y/2, 0f);
                ShootNos(dodgePrefab, pos, dmgAppliedData.dmgValue);
                break;
            default:
                break;
        }

    }
    void OnMisFire(DmgAppliedData dmgAppliedData)
    {

    }

    void ShootNos(DamageNumber dmgNos, Vector3 pos, float val)
    {   
        DamageNumber damageNumberGO = dmgNos.Spawn(pos, val);                
    }
  
}
