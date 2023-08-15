using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class FileUtil
{
    public static async Task <string> ReadAllTextAsync(string filePath)
    {        
            using (StreamReader reader = new StreamReader(filePath))
            {
                return await reader.ReadToEndAsync();
            }
    
    }
}

