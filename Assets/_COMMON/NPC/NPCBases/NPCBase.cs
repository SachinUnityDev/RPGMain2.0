using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public abstract class NPCBase
    {
        public abstract NPCNames nPCNames { get; }
        public abstract void NPCInit();


    }
}