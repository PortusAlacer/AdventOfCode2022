using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shape : IComparable
{
    public enum ShapeType
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

    public ShapeType Type { protected set; get; }
    
    public int CompareTo(object obj)
    {
        Shape other = obj as Shape;

        if (other.Type == this.Type)
        {
            return 0;
        }

        if (this.Type == ShapeType.Scissors && other.Type == ShapeType.Paper)
        {
            return 1;
        }
        
        if (this.Type == ShapeType.Paper && other.Type == ShapeType.Rock)
        {
            return 1;
        }
        
        if (this.Type == ShapeType.Rock && other.Type == ShapeType.Scissors)
        {
            return 1;
        }

        return -1;
    }

    public static Shape ShapeFactory(string input)
    {
        switch (input)
        {
            case "A":
            case "X":
                return new Rock();
            case "B":
            case "Y":
                return new Paper();
            case "C":
            case "Z":
                return new Scissors();
            default:
                return null;
        }
    }

    public Shape OtherWin()
    {
        switch (Type)
        {
            case ShapeType.Paper:
                return new Scissors();
            case ShapeType.Rock:
                return new Paper();
            case ShapeType.Scissors:
                return new Rock();
            default:
                return null;
        }
    }
    
    public Shape OtherLoose()
    {
        switch (Type)
        {
            case ShapeType.Paper:
                return new Rock();
            case ShapeType.Rock:
                return new Scissors();
            case ShapeType.Scissors:
                return new Paper();
            default:
                return null;
        }
    }
    
    public Shape Draw()
    {
        switch (Type)
        {
            case ShapeType.Paper:
                return new Paper();
            case ShapeType.Rock:
                return new Rock();
            case ShapeType.Scissors:
                return new Scissors();
            default:
                return null;
        }
    }
}

public class Rock : Shape
{
    public Rock()
    {
        Type = ShapeType.Rock;
    }
}

public class Paper : Shape
{
    public Paper()
    {
        Type = ShapeType.Paper;
    }
}

public class Scissors : Shape
{
    public Scissors()
    {
        Type = ShapeType.Scissors;
    }
}

public class Game
{
    public enum OutcomeType
    {
        Win,
        Lost,
        Draw
    }
    
    private Shape m_Other;
    private Shape m_Me;

    public int WinPoints { private set; get; }
    public int ShapePoints { private set; get; }

    private void CalculatePoints()
    {
        ShapePoints = (int) m_Me.Type;

        int result = m_Me.CompareTo(m_Other);

        if (result == 0)
        {
            WinPoints = 3;
        }
        else if (result > 0)
        {
            WinPoints = 6;
        }
        else
        {
            WinPoints = 0;
        }
    }
    
    public Game(string other, string me)
    {
        m_Other = Shape.ShapeFactory(other);
        m_Me = Shape.ShapeFactory(me);

        CalculatePoints();
    }

    public Game(string other, OutcomeType outcome)
    {
        m_Other = Shape.ShapeFactory(other);

        switch (outcome)
        {
            case OutcomeType.Draw:
                m_Me = m_Other.Draw();
                break;
            case OutcomeType.Lost:
                m_Me = m_Other.OtherLoose();
                break;
            case OutcomeType.Win:
                m_Me = m_Other.OtherWin();
                break;
            default:
                m_Me = null;
                break;
        }
        
        CalculatePoints();
    }
}

public class Day2 : MonoBehaviour
{
    [SerializeField] private TextAsset m_Input;
    
    private void Start()
    {
        Debug.Assert(m_Input != null, $"Input null {nameof(m_Input)} in {name}");

        List<string> inputLines = StringOperations.ReadFileLines(m_Input);

        int totalPoints = 0;
        int totalPointsSecond = 0;
        
        foreach (string inputLine in inputLines)
        {
            if (string.IsNullOrEmpty(inputLine))
            {
                continue;
            }
            
            string[] gameString = StringOperations.SplitSpaces(inputLine);
            
            Game game = new Game(gameString[0], gameString[1]);
            totalPoints += game.ShapePoints + game.WinPoints;

            Game.OutcomeType outcome;

            switch (gameString[1])
            {
                case "X":
                    outcome = Game.OutcomeType.Lost;
                    break;
                case "Y":
                    outcome = Game.OutcomeType.Draw;
                    break;
                case "Z":
                    outcome = Game.OutcomeType.Win;
                    break;
                default:
                    outcome = Game.OutcomeType.Draw;
                    break;
            }
            
            Game game2 = new Game(gameString[0], outcome);
            totalPointsSecond += game2.ShapePoints + game2.WinPoints;
        }
        
        Debug.LogWarning($"Total Points -> {totalPoints}");
        Debug.LogWarning($"Total Points 2 -> {totalPointsSecond}");
    }


}
