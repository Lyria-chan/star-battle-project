﻿namespace star_battle_project
{
    using System.Text.Json;
    using System;

    // Star Battle in CMD
    class StarBattle
    {
        static int GameSize; // Dynamically set based on the imported board
        static List<Cell> GameBoard = new List<Cell>();

        public class Cell
        {
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
                    Console.WriteLine();
                }
            }
        }

        static List<Cell> ImportBoard(string boardFile)
        {
            var board = new List<Cell>();
            var groups = new List<List<int>>();

            string json = File.ReadAllText(boardFile);
            // throw error if no file found
            
            try { var groups = JsonSerializer.Deserialize<Dictionary<string, List<List<int>>>>(json)["game_board"]; }
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

        static void Main(string[] args)
        {
            GameBoard = ImportBoard("game_board.json");
            Console.WriteLine($"Game Size: {GameSize}x{GameSize}");
            PrintBoard();
            Console.ReadLine();
        }

    }
}
