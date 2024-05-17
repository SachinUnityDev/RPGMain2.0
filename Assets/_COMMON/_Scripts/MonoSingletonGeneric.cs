using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;

public class MonoSingletonGeneric<T> : SaveBase where T : MonoSingletonGeneric<T>
{
    private static T instance;
    public static T Instance { get { return instance; } }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
}
public class SaveBase : MonoBehaviour
{

    protected void CreateAFolder(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }
    public bool DirectoryExists(string path)
    {
        return Directory.Exists(path);
    }
    protected bool ChkIfFileExist(string fileName)
    {
        return File.Exists(Application.persistentDataPath + "/" + fileName);
    }
    protected bool ChkIfFileIsEmpty(string fileName)
    {
        return new FileInfo(Application.persistentDataPath + "/" + fileName).Length == 0;
    }
    public void ClearFile(string fileName)
    {
        File.Delete(Application.persistentDataPath + "/" + fileName);
    }
    public void ClearFileTxt(string filePath)
    {
        File.WriteAllText(filePath, string.Empty);
    }

    public void CreateTextFile(string path, string content)
    {
        using (StreamWriter sw = File.CreateText(path))
        {
            sw.Write(content);
        }
    }
    public void ReadAllFilesInDirectory(string directoryPath)
    {
        string[] fileNames = Directory.GetFiles(directoryPath);

        foreach (string fileName in fileNames)
        {
            string contents = File.ReadAllText(fileName);            
            Debug.Log(contents);
        }
    }
    public bool IsDirectoryEmpty(string directoryPath)
    {
        return !Directory.EnumerateFileSystemEntries(directoryPath).Any();
    }
    public void DeleteAllFilesInDirectory(string directoryPath)
    {
        string[] fileNames = Directory.GetFiles(directoryPath);

        foreach (string fileName in fileNames)
        {
            File.Delete(fileName);
        }
    }
}

