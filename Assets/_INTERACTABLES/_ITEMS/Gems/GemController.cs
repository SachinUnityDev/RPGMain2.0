using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class GemController : MonoBehaviour
    {
        // WE : weapons enchant: divine gems ONly, charge 12, Enchantment can take place at temple
        // SELF Enchantment after consuming/reading scroll : 3 days

        // socket anywhere .. right click on the Comm Inv panel
        // if for current char It is not socketable then grey out the options
        void Start()
        {

        }
        List<GemModel> gemsSocketed = new List<GemModel>();
        public GemModel GetGemEnchanted2Weapon(CharNames charName)
        {
            foreach (GemModel gemModel in GemService.Instance.allGemModels)
            {
                if (gemModel.charName == charName && gemModel.gemState == GemState.Enchanted)
                {
                    return gemModel;
                }
            }
            Debug.Log("NO gem enchanted to the char's weapon");
            return null;
        }

        public List<GemModel> GetGemsSocketedWithArmor(CharNames charName)
        {
            gemsSocketed.Clear();

            foreach (GemModel gemModel in GemService.Instance.allGemModels)
            {
                if (gemModel.charName == charName && gemModel.gemState == GemState.Socketed)
                {
                    gemsSocketed.Add(gemModel);
                }
            }

            if (gemsSocketed.Count > 0)
                return gemsSocketed;
            else
                Debug.Log("NO gems socketed to char");
            return null;
        }

        public void OnGem_Socketed(GemModel gemModel)
        {
            if(gemModel.gemtype == GemType.Support)
            {
               // get other gems should know there thru Gembase
               // nullify there FX push a new value of val thru gembase

            }
            if(gemModel.gemtype == GemType.Divine)
            {


            }


            GemService.Instance.allGemModels.Add(gemModel);
        }

        public void OnGem_Enchanted(GemModel gemModel)
        {
            GemService.Instance.allGemModels.Add(gemModel);
        }






    }




}
