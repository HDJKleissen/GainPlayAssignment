using System;
using System.Collections.Generic;
using UnityEngine;

static class ResourceLoader
{
    static Dictionary<string, UnityEngine.Object> loadedResources = new Dictionary<string, UnityEngine.Object>();
    static Dictionary<string, UnityEngine.Object[]> loadedResourceCollections = new Dictionary<string, UnityEngine.Object[]>();

    public static T GetResource<T>(string name, bool canBeNull = false) where T : UnityEngine.Object
    {
        string fullPath = GetFullPath<T>(name);

        string collectionPath = GetFullPath<T>("");

        if (loadedResourceCollections.ContainsKey(collectionPath))
        {
            int resourceIndexInLoadedCollection = Array.FindIndex(loadedResourceCollections[collectionPath], x => x.name == name);
            if (resourceIndexInLoadedCollection != -1)
            {
                loadedResources[fullPath] = loadedResourceCollections[collectionPath][resourceIndexInLoadedCollection];
            }
        }
        if (!loadedResources.ContainsKey(fullPath))
        {
            bool success = LoadResource<T>(name);
            if (!success)
            {
                if (canBeNull)
                {
                    return null;
                }
                else
                {
                    throw new Exception("The requested resource " + fullPath + " could not be found. Check if the name is wrong or if it is the wrong filetype.");
                }
            }
        }
        return loadedResources[fullPath] as T;
    }

    public static bool LoadResource<T>(string name) where T : UnityEngine.Object
    {
        string fullPath = GetFullPath<T>(name);

        T resource = Resources.Load<T>(fullPath);

        if (resource != null)
        {
            loadedResources[fullPath] = resource;
            return true;
        }
        else
        {
            return false;
        }
    }
    public static T[] GetAll<T>() where T : UnityEngine.Object
    {
        string fullPath = GetFullPath<T>("");

        if (!loadedResourceCollections.ContainsKey(fullPath))
        {
            Debug.Log(fullPath);
            loadedResourceCollections[fullPath] = Resources.LoadAll<T>(fullPath);

        }

        return loadedResourceCollections[fullPath] as T[];
    }



    static string GetFullPath<T>(string name) where T : UnityEngine.Object
    {
        string typeString = typeof(T).ToString();

        string[] splitTypeString = typeString.Split('.');

        return splitTypeString[splitTypeString.Length - 1] + "/" + name;
    }
}