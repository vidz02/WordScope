using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DictionaryModels;

public static class JsonUtils
{
    private static RootWrapper GetRoot(string json)
    {
        try
        {
            // Wrap the JSON string to match the expected structure
            string wrapped = "{\"items\":" + json + "}";

            // Deserialize the wrapped JSON string into the RootWrapper object
            var root = JsonUtility.FromJson<RootWrapper>(wrapped);
            return root;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error parsing JSON: " + ex.Message);
            return null;
        }
    }

    public static string GetFirstDefinition(string json)
    {
        RootWrapper root = GetRoot(json);

        // Null checks for root & its items.
        return root?.items?[0]?.meanings?[0]?.definitions?[0]?.definition;
    }
}
