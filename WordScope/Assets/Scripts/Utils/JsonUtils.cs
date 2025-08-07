using System;
using Newtonsoft.Json.Linq;
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

    public static void PrintAllJsonValues(string json)
    {
        var token = JToken.Parse(json);
        PrintTokenRecursive(token);
    }

    private static void PrintTokenRecursive(JToken token)
    {
        switch (token.Type)
        {
            case JTokenType.Object:
                foreach (var prop in (JObject)token)
                {
                    Debug.Log($"{prop.Key}:");
                    PrintTokenRecursive(prop.Value);
                }
                break;

            case JTokenType.Array:
                foreach (var item in (JArray)token)
                {
                    PrintTokenRecursive(item);
                }
                break;

            default:
                Debug.Log($"{token}");
                break;
        }
    }
}