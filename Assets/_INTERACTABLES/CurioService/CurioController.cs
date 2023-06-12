using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 
namespace Quest
{
    public class CurioController : MonoBehaviour
    {
        public List<CurioModel> allCurioModel = new List<CurioModel>();
        public List<CurioBase> allCurioBases = new List<CurioBase>();
        void Start()
        {

        }

        public void InitCurioController(AllCurioSO allCurioSO)
        {
            foreach (CurioSO curioSO in allCurioSO.allCurioSO)
            {
                CurioModel curioModel = new CurioModel(curioSO);
                allCurioModel.Add(curioModel);
            }
            InitAllCurioBase(allCurioSO);
        }
        void InitAllCurioBase(AllCurioSO allCurioSO)
        {
            foreach (CurioSO curioSO in allCurioSO.allCurioSO)
            {
                CurioBase curioBase =
                CurioService.Instance.curioFactory.GetNewCurio(curioSO.curioName);
                allCurioBases.Add(curioBase);
            }
        }


        public CurioModel GetCurioModel(CurioNames curioName)
        {
            int index =
                        allCurioModel.FindIndex(t => t.curioName == curioName);
            if (index != -1)
                return allCurioModel[index];
            else
                Debug.Log("CurioModel Not FOUND");
            return null;
        }

        public CurioBase GetCurioBase(CurioNames curioName)
        {
            int index =
                        allCurioBases.FindIndex(t => t.curioName == curioName);
            if (index != -1)
                return allCurioBases[index];
            else
                Debug.Log("CurioBase Not FOUND");
            return null;
        }

    }
}