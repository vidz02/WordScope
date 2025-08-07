
using UnityEngine;
using System.Text.RegularExpressions;

public static class ValidationUtils
{   
    public static bool IsValidWord(string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            Debug.LogWarning("Input is null or empty.");
            return false;
        }
        if (!Regex.IsMatch(word, "^[A-Za-z]+$"))
        {
            Debug.LogWarning("Input contains invalid characters. Only alphabetic characters are allowed.");
            return false;
        }
        return true;
    }
}
