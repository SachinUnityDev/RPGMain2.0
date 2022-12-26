using Common;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;


namespace Interactables
{
    public abstract class GenGewgawBase
    {
        public abstract GenGewgawNames genGewgawNames { get; }
        public CharController charController { get; protected set; }
        public  SuffixBase suffixBase { get; set; }  // they are set in Item Factory
        public  PrefixBase prefixBase { get; set; } 

        public GenGewgawQ genGewgawQ = GenGewgawQ.None;


        ILyric lyricSuffix = null;
        ILyric lyricPrefix = null;

        IFolkloric folkLoricSuffix = null;
        IFolkloric folkLoricPrefix = null;

        IEpic epicSuffix = null;
        IEpic epicPrefix = null;
        public virtual void GenGewgawInit(GenGewgawQ genGewgawQ)
        {

            switch (genGewgawQ)
            {
                case GenGewgawQ.None:
                    break;             
                case GenGewgawQ.Lyric:
                     lyricSuffix = suffixBase as ILyric;
                     lyricPrefix = prefixBase as ILyric;                   
                        lyricSuffix?.LyricInit(); 
                        lyricPrefix?.LyricInit();
                    break;   
                case GenGewgawQ.Folkloric:
                     folkLoricSuffix = suffixBase as IFolkloric;
                     folkLoricPrefix = prefixBase as IFolkloric;                    
                        folkLoricSuffix?.FolkloricInit();
                        folkLoricPrefix?.FolkloricInit();
                    break; 
                     
                case GenGewgawQ.Epic:
                     epicSuffix = suffixBase as IEpic;
                     epicPrefix = prefixBase as IEpic;                 
                        epicSuffix?.EpicInit();
                        epicPrefix?.EpicInit();
                    break;                    
                default:
                    break;
            }

        }

        public virtual void EquipGenGewgawFX()
        {
            switch (genGewgawQ)
            {
                case GenGewgawQ.None:
                    Debug.LogError("FALSE gewgaws created"); 
                    break;
                case GenGewgawQ.Lyric:
                    lyricPrefix?.ApplyFXLyric(); 
                    lyricSuffix?.ApplyFXLyric();
                    break;
                case GenGewgawQ.Folkloric:
                    folkLoricPrefix?.ApplyFXFolkloric(); 
                    folkLoricSuffix?.ApplyFXFolkloric();
                    break;
                case GenGewgawQ.Epic:
                    epicPrefix?.ApplyFXEpic();
                    epicSuffix?.ApplyFXEpic();
                    break;
                default:
                    break;
            }


        }
        public virtual void UnEquipGenGewgawFX()
        {
            // remove loop here buffID 

            switch (genGewgawQ)
            {
                case GenGewgawQ.None:
                    Debug.LogError("FALSE gewgaws created");
                    break;
                case GenGewgawQ.Lyric:
                    lyricPrefix.RemoveFXLyric(); 
                    lyricSuffix.RemoveFXLyric();
                    break;
                case GenGewgawQ.Folkloric:
                    folkLoricPrefix.RemoveFXFolkloric();
                    folkLoricSuffix.RemoveFXFolkloric();
                    break;
                case GenGewgawQ.Epic:
                    epicPrefix.RemoveFXEpic();
                    epicSuffix?.RemoveFXEpic();
                    break;
                default:
                    break;
            }


        }

    }

    public interface IEpic
    {
        void EpicInit();
        void ApplyFXEpic();
        void RemoveFXEpic(); 
    }
    public interface IFolkloric
    {
        void FolkloricInit();
        void ApplyFXFolkloric();
        void RemoveFXFolkloric();
    }
    public interface ILyric
    {
        void LyricInit(); 
        void ApplyFXLyric();    
        void RemoveFXLyric(); 
    }



}

