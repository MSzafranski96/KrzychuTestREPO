using Day1;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
/*string abc;
abc = "";
for (int j = 1; j <= 3; j++)
{
    abc = abc + " ------- ";
}
abc = abc + System.Environment.NewLine;
for (int j = 1; j <= 3; j++)
{
    abc = abc + "|       |";
}
Console.WriteLine(abc);
Console.WriteLine(1/2);
Field_O_X testObj = new Field_O_X("X");
Console.WriteLine(testObj.Display());


foreach (KeyValuePair<int, string> kvp in testBoard.BoardDisplay)
{
    Console.WriteLine("Key = {0}, Value = {1}",
        kvp.Key, kvp.Value);
}

testBoard.BoardDisplay[1] = "O";
Console.WriteLine(testBoard.BoardDisplay[1]);

testBoard.MarkOorX(2, "X");
Console.WriteLine(testBoard.BoardStage());
if("a" == "a")
{
    Console.WriteLine("No i gitara");
}
*/

Console.Write("Enter X if you want to play with X and O if you want to play with O: ");
string OorX = Console.ReadLine();
Console.WriteLine("Great! You want to play with " + OorX);
Board testBoard = new Board(3,OorX);
int round = 0;
Console.WriteLine(testBoard.BoardStage());
while (round != testBoard.Size * testBoard.Size || testBoard.WeHaveAWinner == false)
{
    Console.Write("Select a  field (1 - " + testBoard.Size * testBoard.Size + ") where you'd like to put your " + OorX + ": ");
    string FieldNumber = Console.ReadLine();
    int.TryParse(FieldNumber, out int Field);
    testBoard.BoardDisplay[Field] = OorX;
    round++;
    testBoard.WinnerCheck();
    if (testBoard.WeHaveAWinner == true)
    {
        Console.WriteLine(testBoard.BoardStage());
        Console.WriteLine("We have  a winner -> " + testBoard.Winner);
        break;
    }
    else if (testBoard.WeHaveAWinner == false && round == testBoard.Size * testBoard.Size)
    {
        Console.WriteLine(testBoard.BoardStage());
        Console.WriteLine("Draw");
        break;
    }
    else
    {
        testBoard.PCMove();
        round++;
        testBoard.WinnerCheck();
        if (testBoard.WeHaveAWinner == true)
        {
            Console.WriteLine(testBoard.BoardStage());
            Console.WriteLine("We have  a winner -> " + testBoard.Winner);
            break;
        }
        else if (testBoard.WeHaveAWinner == false && round == testBoard.Size * testBoard.Size)
        {
            Console.WriteLine(testBoard.BoardStage());
            Console.WriteLine("Draw");
            break;
        }
        else
        {
            Console.WriteLine("Oponent has replied with:");
            Console.WriteLine(testBoard.BoardStage());
        }
        ;
    }
}

public class Field_O_X
{
    public string Sign { get; set; }
    public virtual string Display()
    {
        return
            @" -------
|       |
|   "+Sign+@"   |
|       |
 -------";
    }
    public Field_O_X(string inputSign)
    {
        Sign = inputSign;
    }
}
public class Board
{
    public Dictionary<int, string> BoardDisplay = new Dictionary<int, string>();
    public int Size; //3 or 4
    public bool WeHaveAWinner;
    public string PlayerOorX;
    public string PCOorX;
    public string Winner;

    public Board(int inputSize, string InputPlayerOorX)
    {
        Size = inputSize;
        PlayerOorX = InputPlayerOorX;
        if (PlayerOorX == "O")
        {
            PCOorX = "X";
        }
        else PCOorX = "O";
        for (int i = 1; i <= Size * Size; i++)
        {
            string asdf = Convert.ToString(i);
            BoardDisplay.Add(i, asdf);
        }
        WeHaveAWinner = false;
    }
    public virtual string BoardStage()
    {
        string linestring;
        linestring = "";
        Field_O_X Tile = new Field_O_X("N");
        for (int i = 1; i <= Size; i++) {
            for (int j = 1; j <= Size; j++)
            { linestring = linestring + " ------- "; }
            linestring = linestring + System.Environment.NewLine;
            for (int j = 1; j <= Size; j++)
            { linestring = linestring + "|       |"; }
            linestring = linestring + System.Environment.NewLine;
            foreach (KeyValuePair<int, string> kvp in BoardDisplay)
            {
                if (kvp.Key <= Size * i & kvp.Key > (i - 1) * Size) {
                    linestring = linestring + "|   " + kvp.Value + "   |";
                }
            }
            linestring = linestring + System.Environment.NewLine;
            for (int j = 1; j <= Size; j++)
            { linestring = linestring + "|       |"; }
            linestring = linestring + System.Environment.NewLine;
            for (int j = 1; j <= Size; j++)
            { linestring = linestring + " ------- "; }
            linestring = linestring + System.Environment.NewLine;

        }

        return linestring;
    }
    public void MarkOorX(int Field, string OorX)
    {
        BoardDisplay[Field] = OorX;
    }
    public void WinnerCheck()
    {
        //checking columns
        for (int i = 1; i <= Size; i++)
        {
            for (int j = 1; j < Size; j++)
            {
                if (BoardDisplay[i] != BoardDisplay[i + j * Size])
                {
                    break;
                }
                if (j == Size - 1)
                {
                    WeHaveAWinner = true;
                    Winner = BoardDisplay[i];
                }
            }
        }
        //checking rows
        for (int i = 1; i <= Size*(Size - 1) + 1 ; i = i+Size)
        {
            for (int j = 1; j < Size; j++)
            {
                if (BoardDisplay[i] != BoardDisplay[i + j])
                {
                    break;
                }
                ;
                if (j == Size - 1)
                {
                    WeHaveAWinner = true;
                    Winner = BoardDisplay[i];
                }
            }
        }
        //checking cross
        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                for (int j = 1; j < Size; j++)
                {
                    if (string.Compare(BoardDisplay[1], BoardDisplay[1 + j * (Size + 1)], StringComparison.OrdinalIgnoreCase) != 0)
                    {
                        break;
                    }
                    ;
                    if (j == Size - 1)
                    {
                        WeHaveAWinner = true;
                        Winner = BoardDisplay[1];
                    }
                }
            }
            else
            {
                for (int j = 1; j < Size; j++)
                {
                    if (BoardDisplay[Size] != BoardDisplay[Size + j * (Size - 1)])
                    {
                        break;
                    }
                    ;
                    if (j == Size - 1)
                    {
                        WeHaveAWinner = true;
                        Winner = BoardDisplay[Size];
                    }
                }
            }
        }

    }

    public bool IsCorner(int field)
    {
        return field == 1 || field == Size || field == Size * (Size - 1) + 1 || field == Size * Size;
    }
    public  void PCMove()
    {
        Dictionary<int, double> FieldValue = new Dictionary<int, double>();
        List<int> EmptyFieldsList = new List<int>();


        double PCpoints;
        double PlayerPoints;
        int FreeField = 0;
        PCpoints = 0;
        PlayerPoints = 0;

        double CornerValue;
        double MiddleValue;
        double IncrementValue;
        double SideValue;
        CornerValue = 0;
        MiddleValue = 1.5;
        SideValue = 0.2;
        IncrementValue = 1;

        bool IsPlaceChosen = false;
        int ChosenPlace = 0;

        foreach (var item in BoardDisplay)
        {
            if (IsEmpty(item))
            {

                if (IsCorner(item.Key))
                {
                    FieldValue.Add(item.Key, CornerValue);
                }

                //middles have  even higher value
                else if (Size == 3 && item.Key == 5)
                {
                    FieldValue.Add(5, MiddleValue);
                }
                else if (Size == 4)
                {
                    FieldValue.Add(6, MiddleValue);
                    FieldValue.Add(7, MiddleValue);
                    FieldValue.Add(10, MiddleValue);
                    FieldValue.Add(11, MiddleValue);
                }
                else FieldValue.Add(item.Key, SideValue);
            }
        }

        //checking columns
        for (int i = 1; i <= Size; i++)
        {
            FreeField = 0;
            PlayerPoints = 0;
            PCpoints = 0;
            if (BoardDisplay[i] == PCOorX) { PCpoints = IncrementValue; }
            else if (BoardDisplay[i] == PlayerOorX) { PlayerPoints = IncrementValue; }
            else
            {
                FreeField = i;
                EmptyFieldsList.Add(i);
            }
            ;

                for (int j = 1; j < Size; j++)
            {
                if (BoardDisplay[i + j * Size] == PlayerOorX)
                {
                    PlayerPoints = PlayerPoints + IncrementValue;
                }
                else if (BoardDisplay[i + j * Size] == PCOorX)
                {
                    PCpoints = PCpoints + IncrementValue;
                }
                else
                {
                    FreeField = i + j * Size;
                    EmptyFieldsList.Add(i + j * Size);
                }
                ;
                
            }
            if (PCpoints == 2 * IncrementValue && FreeField != 0)
            {
                IsPlaceChosen = true;
                ChosenPlace = FreeField;
                break;
            }
            if (PlayerPoints == 2 * IncrementValue && FreeField != 0)
            {
                ChosenPlace = FreeField;
            }
            foreach (int Field in EmptyFieldsList)
            {
                FieldValue[Field] = FieldValue[Field] + PCpoints;
            };
            EmptyFieldsList = [];
        }
        //checking rows
        FreeField = 0;
        PlayerPoints = 0;
        PCpoints = 0;
        
        if  (IsPlaceChosen == false)
        {
            for (int i = 1; i <= Size * (Size - 1) + 1; i = i + Size)
            {
                FreeField = 0;
                PlayerPoints = 0;
                PCpoints = 0;
                if (BoardDisplay[i] == PCOorX) { PCpoints = IncrementValue; }
                else if (BoardDisplay[i] == PlayerOorX) { PlayerPoints = IncrementValue; }
                else
                {
                    FreeField = i;
                    EmptyFieldsList.Add(i);
                }
                ;

                for (int j = 1; j < Size; j++)
                {
                    if (BoardDisplay[i + j] == PlayerOorX)
                    {
                        PlayerPoints = PlayerPoints + IncrementValue;
                    }
                    else if (BoardDisplay[i + j] == PCOorX)
                    {
                        PCpoints = PCpoints + IncrementValue;
                    }
                    else
                    {
                        FreeField = i + j;
                        EmptyFieldsList.Add(i + j);
                    }
                    ;
                }
                if (PCpoints == 2 * IncrementValue && FreeField != 0)
                {
                    IsPlaceChosen = true;
                    ChosenPlace = FreeField;
                    break;
                }
                if (PlayerPoints == 2 * IncrementValue && FreeField != 0)
                {
                    ChosenPlace = FreeField;
                }
                foreach (int Field in EmptyFieldsList)
                {
                    FieldValue[Field] = FieldValue[Field] + PCpoints;
                }
                EmptyFieldsList = [];
            }
                
        }

        //checking cross
        FreeField = 0;
        PlayerPoints = 0;
        PCpoints = 0;
        
        if (IsPlaceChosen == false)
        {
            for (int i = 0; i < 2; i++)
            {
                FreeField = 0;
                PlayerPoints = 0;
                PCpoints = 0;
                if (i == 0)
                {
                    if (BoardDisplay[1] == PCOorX) { PCpoints = IncrementValue; }
                    else if (BoardDisplay[1] == PlayerOorX) { PlayerPoints = IncrementValue; }
                    else
                    {
                        FreeField = 1;
                        EmptyFieldsList.Add(1);
                    }
                    ;
                    for (int j = 1; j < Size; j++)
                    {
                        if (BoardDisplay[1 + j * (Size + 1)] == PlayerOorX)
                        {
                            PlayerPoints = PlayerPoints + IncrementValue;
                        }
                        else if (BoardDisplay[1 + j * (Size + 1)] == PCOorX)
                        {
                            PCpoints = PCpoints + IncrementValue;
                        }
                        else
                        {
                            FreeField = 1 + j * (Size + 1);
                            EmptyFieldsList.Add(1 + j * (Size + 1));
                        }
                        ;
                    }

                    if (PCpoints == 2 * IncrementValue && FreeField != 0)
                    {
                        IsPlaceChosen = true;
                        ChosenPlace = FreeField;
                        break;
                    }
                    if (PlayerPoints == 2 * IncrementValue && FreeField != 0)
                    {
                        ChosenPlace = FreeField;
                    }
                    foreach (int Field in EmptyFieldsList)
                    {
                        FieldValue[Field] = FieldValue[Field] + PCpoints;
                    }
                    EmptyFieldsList = [];
                }
                else
                {
                    if (BoardDisplay[Size] == PCOorX) { PCpoints = IncrementValue; }
                    else if (BoardDisplay[Size] == PlayerOorX) { PlayerPoints = IncrementValue; }
                    else
                    {
                        FreeField = Size;
                        EmptyFieldsList.Add(Size);
                    }
                    ;
                    for (int j = 1; j < Size; j++)
                    {
                        if (BoardDisplay[Size + j * (Size - 1)] == PlayerOorX)
                        {
                            PlayerPoints = PlayerPoints + IncrementValue;
                        }
                        else if (BoardDisplay[Size + j * (Size - 1)] == PCOorX)
                        {
                            PCpoints = PCpoints + IncrementValue;
                        }
                        else
                        {
                            FreeField = Size + j * (Size - 1);
                            EmptyFieldsList.Add(Size + j * (Size - 1));
                        }
                        ;
                    }
                    if (PCpoints == 2 * IncrementValue && FreeField != 0)
                    {
                        IsPlaceChosen = true;
                        ChosenPlace = FreeField;
                        break;
                    }
                    if (PlayerPoints == 2 * IncrementValue && FreeField != 0)
                    {
                        ChosenPlace = FreeField;
                    }

                    foreach (int Field in EmptyFieldsList)
                    {
                        FieldValue[Field] = FieldValue[Field] + PCpoints;
                    }
                    EmptyFieldsList = [];
                }
            }
        }
        if (IsPlaceChosen == false && ChosenPlace != 0)
        {
            IsPlaceChosen = true;
        }
        if (IsPlaceChosen == false)
        {
            double MaxValue = -1;
            foreach (var item in FieldValue)
            {
                if (item.Value > MaxValue)
                {
                    MaxValue = item.Value;
                    ChosenPlace = item.Key;
                }
            }

        }

        BoardDisplay[ChosenPlace] = PCOorX;

        static bool IsEmpty(KeyValuePair<int, string> item)
        {
            return item.Value != "O" && item.Value != "X";
        }
    }
}
