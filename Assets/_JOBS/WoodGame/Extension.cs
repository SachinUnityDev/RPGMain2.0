using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class Extension 
{
    public static T DeepClone<T>(this T obj)
    {
        if (obj == null) 
        {           
            return default(T); // Return default value for type T if obj is null
        }

        using (var ms = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Seek(0, SeekOrigin.Begin);
            //ms.Position = 0;
            return (T)formatter.Deserialize(ms);
        }
    }
}
