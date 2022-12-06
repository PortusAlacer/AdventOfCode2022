using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class StringOperations
{
    private static string delimitator = "\n";
    
    public static List<string> ReadFileLines(TextAsset file)
    {
        List<string> result = file.text.Split(delimitator).ToList();
        
        Debug.Log($"Number of words loaded: {result.Count}");

        return result;
    }
}
