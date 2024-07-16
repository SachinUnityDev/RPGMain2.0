using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.Linq;
public class TestSave : MonoBehaviour
{
    void ListAllMonoSingletonClasses()
    {
        // Get all types in the current assembly
        string endName = "Service";
        Type[] types = Assembly.GetExecutingAssembly().GetTypes();
        var getAllMono = types.Where(myType => myType.IsClass
                                   && !myType.IsAbstract && myType.Name.EndsWith(endName));
        //..Assembly.GetAssembly(typeof(MonoBehaviour)).GetTypes()
        //                    .Where(myType => myType.IsClass
        //                    && !myType.IsAbstract && myType.Name.EndsWith(endName));
        // Create a list to store the classes that inherit from MonoSingleton
      // List<Type> monoSingletonClasses = new List<Type>();

        // Iterate through each type
        
        // Print the names of the classes that inherit from MonoSingleton
        foreach (Type monoSingletonClass in getAllMono)
        {
            Debug.Log(monoSingletonClass.Name);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("###");
        //ListAllMonoSingletonClasses();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
