using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using Common;
using Town;
using Ink.Runtime;

namespace Combat
{
    public class SkillFxMoveController : MonoBehaviour
    {
        [Header("OFFSETS")]
        [SerializeField] Vector3 striker2TargetOffset = new Vector3(1.5f, 0, 0);
        [SerializeField] Vector3 strikerMainFXOffset = new Vector3(1.5f,1.5f,0); 
        [SerializeField] Transform strikerTransform;
        [SerializeField] Transform targetTransform;
        [Header("Global var")]
        [SerializeField] List<DynamicPosData> mainTargets = new List<DynamicPosData>();

        //  Vector3 endPos;
        Vector3 startPos;
        Vector3 centerPos;
        Vector3 endPos; 
        CharMode targetCharMode;

        [SerializeField]SkillModel skillModel; 

        [SerializeField] GameObject ImpactFX;
        [SerializeField] GameObject mainFX;
        [SerializeField] GameObject selfFX;
        [SerializeField] PerkType currPerkType = PerkType.None;
        [SerializeField] StrikeNos strikeNos; 

        [Header("Buffer variables")]
        [SerializeField] CharController prevCharController;
        [SerializeField] bool lastStatus = false;

        [SerializeField] StrikeType strikeType;
        
        void Start()
        {
            //targetDynasFX = new List<DynamicPosData>(); 
        }
        void SetMoveParams()
        {
            SkillService.Instance.currStrikerDyna = GridService.Instance.GetDyna4GO(CombatService.Instance.currCharOnTurn.gameObject);

            strikerTransform = SkillService.Instance.currStrikerDyna.charGO.transform;

            targetTransform = SkillService.Instance.currentTargetDyna.charGO.transform;
            targetCharMode = CharMode.None;
            targetCharMode = SkillService.Instance.currentTargetDyna.charMode;

            startPos = strikerTransform.position + strikerMainFXOffset;
            mainTargets.Clear();
            mainTargets.AddRange(CombatService.Instance.mainTargetDynas);

            strikeType = targetTransform.GetComponent<DamageController>().strikeType;

            //GridService.Instance.GetDynaWorldPos(SkillService.Instance.currentTargetDyna)
            //                                                - striker2TargetOffset;
            //targetDynasFX.AddRange(GridService.Instance.GetAllTargets()); 
            //centerPos = GridService.Instance.GetCenterPos4CurrHLTargets();
            UpdateSkillPose();

        }
        public GameObject RemoteSkillFX(PerkType perkType, CellPosData cellPosData, SkillBase skillbase)
        {       
            currPerkType = perkType;
            Vector3Int tilePos = GridService.Instance.gridMovement.GetTilePos4Pos(cellPosData.charMode, cellPosData.pos);
            Vector3 worldPos = GridService.Instance.gridMovement.GetWorldPosSingle(tilePos);
            Quaternion quat = GridService.Instance.gridLayout.transform.rotation;
            skillModel = skillbase.skillModel;

            SkillPerkFXData fxData = SkillService.Instance.GetSkillPerkFXData(perkType, CharMode.Enemy); 

            GameObject fxRemote = Instantiate(fxData.mainSkillFX, worldPos, quat);
            CharController charController = CombatService.Instance.currCharOnTurn;
            fxRemote.AddComponent<RemoteView>(); 
            fxRemote.GetComponent<RemoteView>().InitRemoteView(skillbase, cellPosData, charController, fxData);

            SkillService.Instance.On_PostSkill(skillModel);
            return fxRemote;

        }

      


        public void RangedStrike(PerkType perkType, SkillModel skillModel)
        {
            SetMoveParams();
            currPerkType = perkType;
            this.skillModel = skillModel;
            strikeNos = skillModel.strikeNos; 

            Sequence singleSeq = DOTween.Sequence();
            Sequence singleRev = DOTween.Sequence();

            singleSeq
                .PrependCallback(() => ToggleSprite(strikerTransform, true))
                .AppendCallback(()=> ApplyFXOnSelf())
                .AppendCallback(() => CharService.Instance.ToggleCharColliders(targetTransform.gameObject, false))
                .AppendCallback(()=> ApplyGabMainFXOnTarget())   
                .AppendCallback(()=> ApplyFXOnCollatralTargets())
                .AppendCallback(()=> ApplyImpactFXTarget())
                ;
            singleRev
                .AppendInterval(0.90f)
                .AppendCallback(() => ToggleSprite(strikerTransform, false))                
                ;

            singleSeq.Play()
                .OnComplete(() => singleRev.Play())
                .OnComplete(() => CharService.Instance.TurnOnAllCharColliders())
                //.OnComplete(() => singleSeq = null); 
                ;

        }

      

        //public void ImpactFXOnCurrTarget()
        //{
        //    if (ChkIfAttackIsDodged(targetTransform.gameObject)) return;

        //    GameObject impactFXGO;

        //    SkillPerkFXData skillPerkdataFX = SkillService.Instance.GetSkillPerkFXData(currPerkType, targetCharMode);

        //    impactFXGO = skillPerkdataFX.impactFX;
        //    if (impactFXGO == null) return;
        //    ImpactFX = Instantiate(impactFXGO, targetTransform.position, Quaternion.identity).gameObject;
        //    PlayParticleSystem(impactFXGO);
        //    //ImpactFX.GetComponentInChildren<ParticleSystem>().Play();
        //    Destroy(ImpactFX, 2.5f);

        //}

        public void MeleeStrike(PerkType perkType, SkillModel skillModel)
        {
            SetMoveParams();
            this.skillModel = skillModel;
            strikeNos = skillModel.strikeNos;
            currPerkType = perkType; 

            Sequence meleeSeq = DOTween.Sequence();
            Sequence meleeRev = DOTween.Sequence();
           
            Vector3 END = new Vector3(0, strikerTransform.position.y, strikerTransform.position.z);
            Vector3 START = new Vector3(strikerTransform.position.x, strikerTransform.position.y
                                    , strikerTransform.position.z);
          //  Debug.Log("END " + END + " START POS " + startPos);
            meleeSeq
                .AppendCallback(() => CharService.Instance.ToggleCharColliders(targetTransform.gameObject, false))
                //.AppendCallback(() => CharService.Instance.ToggleCharColliders(strikerTransform.gameObject, false))
                .AppendCallback(() => ToggleSprite(strikerTransform, true))
                .Append(strikerTransform.DOMove(END, 0.16f * SkillService.Instance.combatSpeed))
                .AppendCallback(()=>ApplyDefensePose(true))
                .AppendCallback(() => ApplyImpactFXTarget())
                ;

            meleeRev
                .AppendInterval(0.5f)
                .AppendCallback(() => ApplyDefensePose(false))
                .AppendCallback(() => ToggleSprite(strikerTransform, false))                
                .Append(strikerTransform.DOMove(START, 0.16f * SkillService.Instance.combatSpeed))                
                ;

            meleeSeq.Play().OnComplete(() => meleeRev.Play()
                       .OnComplete(() => Destroy(ImpactFX))
                       .OnComplete(() => meleeRev = null)
                       .OnComplete(() => meleeSeq = null)
                       .OnComplete(() => CharService.Instance.TurnOnAllCharColliders())
                       );

        }

        public void JumpStrike(PerkType perkType, SkillModel skillModel)
        {
            SetMoveParams();
            this.skillModel = skillModel;
            strikeNos = skillModel.strikeNos;
            currPerkType = perkType;

            Sequence meleeSeq = DOTween.Sequence();
            Sequence meleeRev = DOTween.Sequence();

            Vector3 END = new Vector3(0, strikerTransform.position.y, strikerTransform.position.z);
            Vector3 START = new Vector3(strikerTransform.position.x, strikerTransform.position.y
                                    , strikerTransform.position.z);
            //  Debug.Log("END " + END + " START POS " + startPos);
            meleeSeq
                .AppendCallback(() => CharService.Instance.ToggleCharColliders(targetTransform.gameObject, false))
                //.AppendCallback(() => CharService.Instance.ToggleCharColliders(strikerTransform.gameObject, false))
                .AppendCallback(() => ToggleSprite(strikerTransform, true))
                .Append(strikerTransform.DOJump(END, 0.75f, 1, 0.20f))
                //.Append(strikerTransform.DOMove(END, 0.16f * SkillService.Instance.combatSpeed))
                .AppendCallback(() => ApplyDefensePose(true))
                .AppendCallback(() => ApplyImpactFXTarget())
                ;

            meleeRev
                .AppendInterval(0.5f)
                .AppendCallback(() => ApplyDefensePose(false))
                .AppendCallback(() => ToggleSprite(strikerTransform, false))
                .Append(strikerTransform.DOMove(START, 0.16f * SkillService.Instance.combatSpeed))
                ;

            meleeSeq.Play().OnComplete(() => meleeRev.Play()
                       .OnComplete(() => Destroy(ImpactFX))
                       .OnComplete(() => meleeRev = null)
                       .OnComplete(() => meleeSeq = null)
                       .OnComplete(() => CharService.Instance.TurnOnAllCharColliders())
                       );
        }

        //****************TARGET FX APPLIERS *****************************************
        #region TARGET FX APPLIERS
        public void ApplyImpactFXTarget()
        {
            GameObject impactFXGO;      
            SkillPerkFXData skillPerkdataFX = SkillService.Instance.GetSkillPerkFXData(currPerkType, targetCharMode);
              
            impactFXGO = skillPerkdataFX.impactFX;            
            if (impactFXGO == null) return;
            if(strikeNos == StrikeNos.Multiple)
            {
                if (CombatService.Instance.mainTargetDynas.Count > 0)
                {
                    Debug.Log("Multi target FX" + mainTargets.Count);
                    foreach (DynamicPosData dyna in mainTargets)
                    {
                        Transform targetTrans = dyna.charGO.transform;
                        if (ChkIfAttackIsDodged(targetTrans.gameObject)) continue;
                        ImpactFX = Instantiate(impactFXGO, targetTrans.position, Quaternion.identity).gameObject;
                        ParticleSystem ps = ImpactFX.GetComponentInChildren<ParticleSystem>();
                        ps.GetComponent<Renderer>().sortingOrder = dyna.GetLayerOrder();
                        ps.Play();

                        Destroy(ImpactFX, 2.5f);
                    }
                }
            }          
            else
            {
                ImpactFX = Instantiate(impactFXGO, targetTransform.position, Quaternion.identity).gameObject;
                ParticleSystem ps = ImpactFX.GetComponentInChildren<ParticleSystem>();
                ps.GetComponent<Renderer>().sortingOrder = 3;
                Destroy(ImpactFX, 2.5f);
            }
            //ClearPreviousParams();


        }
        public void ApplyOnSkillSelect()
        {
            strikerTransform = CombatService.Instance.currCharOnTurn.gameObject.transform;
            SkillPerkFXData skillPerkdataFX = SkillService.Instance.GetSkillFXDataOnSkillSelect(currPerkType); 

            GameObject SelfFXGO = skillPerkdataFX.selfFX;
            Debug.Log("INSIDE Self base11111" + SelfFXGO.name);
            if (SelfFXGO != null)
            {
                selfFX = Instantiate(SelfFXGO, strikerTransform.position, Quaternion.identity).gameObject;
               PlayParticleSystem(SelfFXGO);    
                Destroy(selfFX, 2.5f);
            }
        }
        void PlayParticleSystem(GameObject FxGO)
        {
            foreach (ParticleSystem ps in FxGO.GetComponentsInChildren<ParticleSystem>())
            {
                ps.Play();      
            }
        }
        public void ApplyFXOnCollatralTargets()
        {
            GameObject colFXGO;
           
            SkillPerkFXData skillPerkdataFX = SkillService.Instance.GetSkillPerkFXData(currPerkType, targetCharMode);

            colFXGO = skillPerkdataFX.colImpactFX;
            if (colFXGO == null) return;
            foreach (DynamicPosData dyna in CombatService.Instance.colTargetDynas)
            {
                Transform targetTrans = dyna.charGO.transform;
                if (ChkIfAttackIsDodged(targetTrans.gameObject)) continue;

                GameObject ColFX = Instantiate(colFXGO, targetTrans.position, Quaternion.identity).gameObject;
                PlayParticleSystem(ColFX);
                //ColFX.GetComponentInChildren<ParticleSystem>().Play();               
                Destroy(ColFX, 2.5f);
            }
        }
        public void ApplyFXOnSelf()
        {
            SkillPerkFXData skillPerkdataFX = SkillService.Instance.GetSkillPerkFXData(currPerkType, targetCharMode);
           
            GameObject SelfFXGO = skillPerkdataFX.selfFX;
            Debug.Log("INSIDE Self base11111" + SelfFXGO.name);
            // GameObject selfFXBaseGO = skillPerkdataFX.selfFXBase;
            if (SelfFXGO != null)
            {
                selfFX = Instantiate(SelfFXGO, strikerTransform.position, Quaternion.identity).gameObject;
                PlayParticleSystem(selfFX);
              //  selfFX.GetComponentsInChildren<ParticleSystem>().ToList().ForEach(t => t.Play());
                Destroy(selfFX, 2.5f);
            }
        }
        void ApplyGabMainFXOnTarget()
        {
            GameObject mainFXGO; 
            SkillPerkFXData skillPerkdataFX = SkillService.Instance.GetSkillPerkFXData(currPerkType, targetCharMode);
            mainFXGO = skillPerkdataFX.mainSkillFX;

            if (mainFXGO == null) 
            {
                OnTargetReached(); 
                return;
            }
            if (CombatService.Instance.mainTargetDynas.Count > 0 && strikeNos == StrikeNos.Multiple)
            {
                foreach (DynamicPosData dyna in mainTargets)
                {
                    Vector3 targetPos = dyna.charGO.transform.position - striker2TargetOffset; 
                    GameObject mainFX = Instantiate(mainFXGO, startPos, Quaternion.identity);
                    mainFX.transform.DOLookAt(targetPos, 0.01f);
                    mainFX.GetComponent<VFXMoveScript>().targetPos = targetPos;                
                }
            }else
            {
                Vector3 targetPos = targetTransform.position - striker2TargetOffset;
                GameObject mainFX = Instantiate(mainFXGO, startPos, Quaternion.identity);
                mainFX.transform.DOLookAt(targetPos, 0.01f);
                mainFX.GetComponent<VFXMoveScript>().targetPos = targetPos;                
            }
           
        }

        #endregion
        public void OnTargetReached()
        {
            Sequence FXHitTargetSeq = DOTween.Sequence();

            FXHitTargetSeq
                .AppendCallback(() => SkillService.Instance.On_SkillApplyFXMove())                    
                .AppendCallback(() => ApplyDefensePose(true))
                .AppendInterval(1f)
                .AppendCallback(() => ApplyDefensePose(false))                
                ;
            FXHitTargetSeq.Play();

        }
        #region HELPERS 

        bool ChkIfAttackIsDodged(GameObject targetGO)
        {
            DamageController dmgController = targetGO.GetComponent<DamageController>();
            if (dmgController.strikeType == StrikeType.Dodged) return true;
            return false;
        }
        void UpdateSkillPose()
        {
            Transform poseTransform = strikerTransform.GetChild(1);            
            poseTransform.GetComponent<SpriteRenderer>().sprite = SkillService.Instance.GetCurrSkillSprite();
        }
    
        void ApplyDefensePose(bool showSprite)
        {
            if (strikeType == StrikeType.Dodged) return;
            if (skillModel.skillInclination == SkillInclination.Heal 
                || skillModel.skillInclination == SkillInclination.Guard
                || skillModel.skillInclination == SkillInclination.Buff
                ||skillModel.skillInclination == SkillInclination.Patience) return; 

            if(strikeNos == StrikeNos.Single)
            {
                CharController targetController = targetTransform.GetComponent<CharController>(); 
                AddDefensePose(targetController);
                ToggleSprite(targetTransform, showSprite); 
            }
            if(strikeNos == StrikeNos.Multiple)
            {
                foreach (DynamicPosData dyna in mainTargets)
                {
                    CharController targetController = dyna.charGO.GetComponent<CharController>(); 
                    AddDefensePose(targetController);
                    ToggleSprite(targetController.transform, showSprite);
                }
            }            
        }
        void AddDefensePose(CharController targetController)
        {
            Transform defenseTrans = targetTransform.GetChild(1);
            defenseTrans.GetComponent<SpriteRenderer>().sprite = SkillService.Instance.GetDefPoseSprite(targetController);
        }
   
        void SpawnMainAtACenterPos(Vector3 _pos)
        {
            GameObject FXGO;

            SkillPerkFXData skillPerkdataFX = SkillService.Instance.GetSkillPerkFXData(currPerkType, targetCharMode);

            FXGO = skillPerkdataFX.mainSkillFX;
            if (FXGO == null) return;

            mainFX = Instantiate(FXGO, _pos, Quaternion.identity).gameObject;
            PlayParticleSystem(mainFX);
          //  mainFX.GetComponentsInChildren<ParticleSystem>().ToList().ForEach(t => t.Play());
            Destroy(mainFX, 2.5f);
        }

        void ToggleSprite(Transform transform, bool transPoseON)   // to accomodate for once extra transition POSE 
        {
            //Debug.Log("Ranged Strike" + strikerTransform.GetChild(0).gameObject.activeInHierarchy
            //    + "GAMEPBJECT" + strikerTransform.name);
           // bool Status = strikerTransform.GetChild(0).gameObject.activeInHierarchy;
         //   if (lastStatus == Status) return;
            if (transPoseON)
            {
                transform.GetChild(1).gameObject.SetActive(true);              
                transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
#endregion

    }
}
//void ApplyImpactFXOnSingleTarget()
//{
//    GameObject impactFXGO;

//    SkillPerkFXData skillPerkdataFX = SkillService.Instance.GetSkillPerkFXData(currPerkType,targetCharMode);
//    if (ChkIfAttackIsDodged(targetTransform.gameObject)) return;

//    impactFXGO = skillPerkdataFX.impactFX;
//    if (impactFXGO == null) return;

//        ImpactFX = Instantiate(impactFXGO, targetTransform.position, Quaternion.identity).gameObject;
//        //ImpactFX.GetComponentInChildren<ParticleSystem>().Play();
//        PlayParticleSystem(impactFXGO);
//        Destroy(ImpactFX, 2.5f);           
//}
//public void MultiTargetRangeFX(PerkType perkType, SkillModel skillModel)
//{   
//    SetMoveParams();
//    currPerkType = perkType;
//    strikeNos = skillModel.strikeNos;

//    Sequence multiTargetSeq = DOTween.Sequence();
//    Sequence revMultiTargetSeq = DOTween.Sequence();

//    multiTargetSeq
//        .PrependCallback(() => ToggleSprite(strikerTransform, true))
//        .AppendCallback(() => ApplyFXOnSelf())
//        .AppendCallback(() => CharService.Instance.ToggleCharColliders(targetTransform.gameObject))
//        .AppendCallback(() => ApplyGabMainFXOnTarget())
//        .AppendCallback(() => ApplyImpactFXTarget())
//        ;
//    revMultiTargetSeq
//        .AppendInterval(0.90f)
//        .AppendCallback(() => ToggleSprite(strikerTransform, false))
//        .AppendCallback(() => CharService.Instance.TurnOnAllCharColliders())
//        ;

//    multiTargetSeq.Play()
//        .OnComplete(() => revMultiTargetSeq.Play())
//        .OnComplete(() => multiTargetSeq = null)
//       .OnComplete(() => revMultiTargetSeq = null)
//       ;
//}


//public void MultiTargetEnemyFX()
//{
//    SetMoveParams();
//    currPerkType = PerkType.None;

//    Sequence mySequence = DOTween.Sequence();

//    mySequence
//        .PrependCallback(() => ToggleSprite())
//        .AppendCallback(() => Debug.Log("HELLO u reached next stage"))
//        .AppendCallback(() => CharacterService.Instance.ToggleCharColliders(targetTransform.gameObject))
//        .AppendCallback(() => ApplyGabMainFXOnTarget())
//        //.AppendCallback(() => ApplyImpactFXOnAllTarget())
//        ;


//    mySequence.Play()
//        //.OnComplete(() => reverseSequence.Play())
//        ;

//}


//void ClearPreviousParams()
//{
//    targetDynasFX.Clear(); 
//}

//public void CenterFXOnMultiTargets(PerkType perkType)
//{
//    SetMoveParams();
//    currPerkType = perkType;
//    Sequence mySequence = DOTween.Sequence();
//    Sequence reverseSequence = DOTween.Sequence();
//    SpawnMainAtACenterPos(centerPos);
//    //mySequence
//    //    .PrependCallback(() => ToggleSprite())
//    //   // .AppendCallback(() => ApplyFXOnSelf())
//    //  //  .AppendCallback(() => CharacterService.Instance.ToggleCharColliders(targetTransform.gameObject))
//    //  //  .AppendCallback(() => SpawnOnAPoint(centerPos))
//    //  //  .AppendCallback(() => ApplyImpactFXOnTarget())
//    //    ;
//    //reverseSequence
//    //    .AppendInterval(0.90f)
//    //    .AppendCallback(() => ToggleSprite())
//    //    .AppendCallback(() => CharacterService.Instance.TurnOnAllCharColliders())
//    //    ;

//    //mySequence.Play()
//    //    .OnComplete(() => reverseSequence.Play());


//}

//void ApplyImpactFXOnMultiTarget()
//{
//    GameObject impactFXGO;

//    SkillPerkFXData skillPerkdataFX = SkillService.Instance.GetSkillPerkFXData(currPerkType);

//    impactFXGO = skillPerkdataFX.impactFX;
//    if (impactFXGO == null) return;

//    //if (targetDynasFX.Count > 0)
//    //{
//    //    foreach (DynamicPosData dyna in targetDynasFX)
//    //    {
//    //        Transform targetTrans = dyna.charGO.transform;
//    //        ImpactFX = Instantiate(impactFXGO, targetTrans.position, Quaternion.identity).gameObject;
//    //        ImpactFX.GetComponentInChildren<ParticleSystem>().Play();
//    //        Destroy(ImpactFX, 2.5f);
//    //    }
//    //}

//   // ClearPreviousParams();
//}



// mySequence.Rewind(); 

// Toggle Sprites

//do move back 

//void ApplyMainFXonTarget()
//{
//    GameObject mainFXGO;
//    SkillPerkFXData skillPerkdataFX = SkillService.Instance.GetSkillPerkFXData(currPerkType);
//    mainFXGO = skillPerkdataFX.mainSkillFX;

//    if (mainFXGO == null) return;
//    mainFX = Instantiate(mainFXGO, startPos, Quaternion.identity);
//    mainFX.GetComponentsInChildren<ParticleSystem>().ToList().ForEach(t => t.Play());

//    mainFX.transform.DOLookAt(endPos, 0.01f);
//    mainFX.transform.DOMove(endPos, 2f);    

//}

// change sprite=> toggle on and off ..  
// playFX(perks can have diff FX) .. shake camera ... after pop dmg number numberFX etc
// change Sprite again
// move the character back 

//mySequence.Append(transform.DOMoveX(45, 1));
//// Add a rotation tween as soon as the previous one is finished
//// mySequence.Append(transform.DORotate(new Vector3(0, 180, 0), 1));
//// Delay the whole Sequence by 1 second
//// mySequence.PrependInterval(1);
//// Insert a scale tween for the whole duration of the Sequence

////mySequence.Insert(0, transform.DOScale(new Vector3(3, 3, 3), mySequence.Duration()));

//mySequence.Play(); 


//public void ApplyFXSingleTarget(PerkType perkType)  // self, Main and Impact 
//{
//    SetMoveParams();
//    currPerkType = perkType;
//    Sequence mySequence = DOTween.Sequence();
//    Sequence reverseSequence = DOTween.Sequence();

//    mySequence
//        .PrependCallback(() => ToggleSprite())
//        .AppendCallback(() => ApplyFXOnSelf())
//        .AppendCallback(() => CharacterService.Instance.ToggleCharColliders(targetTransform.gameObject))
//        .AppendCallback(() => ApplyMainFXonTarget())
//        .AppendCallback(() => ApplyImpactFXOnTarget())
//        ;
//    reverseSequence
//        .AppendInterval(0.90f)
//        .AppendCallback(() => ToggleSprite())
//        .AppendCallback(() => CharacterService.Instance.TurnOnAllCharColliders())
//        ;

//    mySequence.Play()
//        .OnComplete(() => reverseSequence.Play());

//}


//public void CenterOfTargetsStrike(PerkType perkType)
//{
//    SetMoveParams();
//    currPerkType = perkType;
//    Sequence mySequence = DOTween.Sequence();
//    Sequence reverseSequence = DOTween.Sequence();

//    mySequence
//        .PrependCallback(() => ToggleSprite())
//        .AppendCallback(() => ApplyFXOnSelf())
//        .AppendCallback(() => CharacterService.Instance.ToggleCharColliders(targetTransform.gameObject))
//        .AppendCallback(() => SpawnMainAtACenterPos(centerPos))
//        .AppendCallback(() => ApplyImpactFXOnTarget())
//        ;
//    reverseSequence
//        .AppendInterval(0.90f)
//        .AppendCallback(() => ToggleSprite())
//        .AppendCallback(() => CharacterService.Instance.TurnOnAllCharColliders())
//        ;

//    mySequence.Play()
//        .OnComplete(() => reverseSequence.Play());
//}