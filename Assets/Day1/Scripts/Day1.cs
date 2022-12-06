using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ElfCalories : IComparable
{
    private List<int> m_AllCalories = new List<int>();
    
    public int TotalCalories { private set; get; } = 0;
    public int ElfID { private set; get; }
    
    public ElfCalories(int id)
    {
        ElfID = id;
    }

    public void AddCalories(int calories)
    {
        m_AllCalories.Add(calories);
        TotalCalories += calories;
    }
    
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder($"Elf {ElfID}\n");

        foreach (int calories in m_AllCalories)
        {
            builder.Append($"{calories}\n");
        }

        string log = builder.ToString();
        
        Debug.Log(log);
        return log;
    }

    public int CompareTo(object obj)
    {
        ElfCalories other = obj as ElfCalories;

        return TotalCalories.CompareTo(other.TotalCalories);
    }
}

public class Day1 : MonoBehaviour
{
    [SerializeField] private TextAsset m_Input;

    [SerializeField] private int m_NumberOfTopElfs = 3;
    
    private void Start()
    {
        Debug.Assert(m_Input != null, $"Input null {nameof(m_Input)} in {name}");

        List<string> inputLines = StringOperations.ReadFileLines(m_Input);

        List<ElfCalories> caloriePerElfGroup = new List<ElfCalories>();

        ElfCalories currentElf = new ElfCalories(0);
        
        foreach (string line in inputLines)
        {
            if (string.IsNullOrEmpty(line))
            {
                currentElf.ToString();
                caloriePerElfGroup.Add(currentElf);
                currentElf = new ElfCalories(currentElf.ElfID + 1);
                continue;
            }
            
            currentElf.AddCalories(int.Parse(line));
        }

        caloriePerElfGroup.Sort();
        caloriePerElfGroup.Reverse();

        int topCalories = 0;
        
        for (int i = 0; i < m_NumberOfTopElfs; i++)
        {
            topCalories += caloriePerElfGroup[i].TotalCalories;
        }
        
        Debug.LogWarning($"Max calories carried -> {topCalories}");
    }
}
