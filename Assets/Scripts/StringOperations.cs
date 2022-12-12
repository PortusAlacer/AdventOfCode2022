using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class StringOperations
{
    private static string delimitator = "\n";
    private static string space = " ";
    
    public static List<string> ReadFileLines(TextAsset file)
    {
        List<string> result = file.text.Split(delimitator).ToList();
        
        Debug.Log($"Number of words loaded: {result.Count}");

        return result;
    }

    public static string[] SplitSpaces(string input)
    {
        return input.Split(space);
    }
}
