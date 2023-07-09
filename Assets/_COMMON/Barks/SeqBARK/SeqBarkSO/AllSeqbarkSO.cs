using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Town
{
    [CreateAssetMenu(fileName = "AllSeqBarkSO", menuName = "Common/BarkService/AllSeqBarkSO")]
    public class AllSeqbarkSO : ScriptableObject
    {
        public List<SeqBarkSO> allSeqBark= new List<SeqBarkSO>();

        public SeqBarkSO GetBarkSeqSO(SeqBarkNames seqbarkName)
        {
            int index = allSeqBark.FindIndex(t=>t.seqbarkName== seqbarkName);
            if(index != -1)
            {
               return allSeqBark[index]; 
            }
            else
            {
                Debug.Log("Seq bark SO" + seqbarkName);
            }
            return null; 
        }

    }
}
