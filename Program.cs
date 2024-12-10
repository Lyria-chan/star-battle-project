﻿namespace star_battle_project
{
    using System.Text.Json;
    using System.Text.RegularExpressions;

    // Star Battle in CMD
    class StarBattle
    {
        static int GameSize; // Dynamically set based on the imported board
        static List<Cell> GameBoard = new List<Cell>();

        public class Cell
        {
            //Przypisanie koordynatów, grupy i stanu domyślnego kwadratu
            public int X { get; set; }
            public int Y { get; set; }
            public int Group { get; set; }
            public char State { get; set; }

            public Cell(int x, int y, int group, char state)
            {
                X = x;
                Y = y;
                Group = group;
                State = state;

            }
        }

        static void PrintBoard()
        {
            foreach (var cell in GameBoard)
            {

                Console.Write(cell.State);
                if (cell.Y == GameSize - 1)
                {
                    //Przypisanie kazdej grupie kwadratow(bloku) innego koloru

                    if (cell.Group == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (cell.Group == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (cell.Group == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (cell.Group == 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else if (cell.Group == 5)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                    else if (cell.Group == 6)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    else if (cell.Group == 7)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    }
                    else if (cell.Group == 8)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }
                    else if (cell.Group == 9)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine();
                }
            }
        }

        static List<Cell> ImportBoard(string boardFile)
        {
            //Import Tablicy z pliku JSON
            var board = new List<Cell>();
            var groups = new List<List<int>>();

            string json = File.ReadAllText(boardFile);
            // throw error if no file found
            
            try { groups = JsonSerializer.Deserialize<Dictionary<string, List<List<int>>>>(json)["game_board"]; }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                Environment.Exit(0);
            }

            // Determine GameSize from the size of the board
            GameSize = groups.Count;

            for (int rowNumber = 0; rowNumber < groups.Count; rowNumber++)
            {
                for (int yNumber = 0; yNumber < groups[rowNumber].Count; yNumber++)
                {
                    int cellGroup = groups[rowNumber][yNumber];
                    board.Add(new Cell(rowNumber, yNumber, cellGroup, 'x'));
                }
            }

            return board;
        }
        
        static void CheckPlacement(int xpos, int ypos)
        {
            static bool Validate(int xcheck, int ycheck)
            {
                if (xcheck <= 0 || ycheck <= 0) return false;
                else if (xcheck > GameSize || ycheck > GameSize) return false;
                else return true;

            }    
        }
        




        static void Main(string[] args)
        {
            //Czesc glowna, narazie wypiuje plansze i jej rozmiar
            GameBoard = ImportBoard("game_board.json");
            Console.WriteLine($"Game Size: {GameSize}x{GameSize}");
            PrintBoard();
            Console.ReadLine();
        }

    }
}
