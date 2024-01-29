using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    [System.Serializable]
    public class AdjMatrixData
    {
        public int[,] adjMatrix = new int[8, 8];
        //public int[,] trisAdjMatrix = new int[7,7];
        //public int[] trisPos = { 124, 245, 457, 134, 346, 467 }; 
        public AdjMatrixData()
        {
                 
            InitAdjMatrix();
        }

        public List<int> GetAdjacency(int cellNoFwd)
        {
            List<int> singleAdj = new List<int>(); 
            for (int i = 1; i <= 7; ++i)
            {              
                if (adjMatrix[cellNoFwd, i] == 1)
                {
                    singleAdj.Add(i);
                }
            }
            return singleAdj; 
        }

        void InitAdjMatrix()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    adjMatrix[i, j] = 0;
                }
            }

            adjMatrix[1, 2] = 1; adjMatrix[1, 3] = 1; adjMatrix[1, 4] = 1;
            adjMatrix[2, 1] = 1; adjMatrix[2, 4] = 1; adjMatrix[2, 5] = 1;
            adjMatrix[3, 1] = 1; adjMatrix[3, 4] = 1; adjMatrix[3, 6] = 1;
            adjMatrix[4, 1] = 1; adjMatrix[4, 2] = 1; adjMatrix[4, 3] = 1;
            adjMatrix[4, 5] = 1; adjMatrix[4, 6] = 1; adjMatrix[4, 7] = 1;
            adjMatrix[5, 2] = 1; adjMatrix[5, 4] = 1; adjMatrix[5, 7] = 1;
            adjMatrix[6, 3] = 1; adjMatrix[6, 4] = 1; adjMatrix[6, 7] = 1;
            adjMatrix[7, 4] = 1; adjMatrix[7, 5] = 1; adjMatrix[7, 6] = 1;

        }
    }
}

//void FillTrisMatrix(int colValue, int row1, int row2, int row3 = -2)
//{
//    for (int i = 0; i < 7; i++)
//    {
//        for (int j = 0; j < 7; j++)
//        {
//            if (trisAdjMatrix[i, 0] == colValue)
//            {
//                if (trisAdjMatrix[0, j] == row1 || trisAdjMatrix[0, j] == row2
//                    || trisAdjMatrix[0, j] == row3)
//                {
//                    trisAdjMatrix[i, j] = 1;
//                }
//            }
//        }
//    }
//}
//public List<int> TrisAjacency(int tris)
//{
//    List<int> trisAdj = new List<int>();

//    for (int i = 0; i < 7; i++)
//    {
//        for (int j = 0; j < 7; j++)
//        {
//            if (trisAdjMatrix[i, 0] == tris)
//            {
//                if (trisAdjMatrix[i, j] == 1)
//                {
//                    trisAdjMatrix[i, j] = 1;
//                    trisAdj.Add(trisAdjMatrix[0, j]);
//                }
//            }
//        }
//    }
//    return trisAdj;
//}


//for (int i = 0; i < 7; i++)
//{
//    for (int j = 0; j < 7; j++)
//    {
//        if ((i == 0) && (j != 0))
//            trisAdjMatrix[i, j] = trisPos[j - 1];
//        else if ((j == 0) && (i != 0))
//            trisAdjMatrix[i, j] = trisPos[i - 1];
//        else if (i == 0 && j == 0)
//            trisAdjMatrix[i, j] = -1;
//        else
//            trisAdjMatrix[i, j] = 0;
//    }
//}
//FillTrisMatrix(124, 245, 134);
//FillTrisMatrix(245, 124, 457, 346);
//FillTrisMatrix(457, 245, 467);
//FillTrisMatrix(134, 124, 346);
//FillTrisMatrix(346, 245, 134, 467);
//FillTrisMatrix(467, 457, 346);
