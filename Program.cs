namespace star_battle_project
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
        static void UpdateGameState()
        {
            Console.WriteLine("Podaj współrzędne (X i Y), aby zmienić pole na '*'. Podaj wartości między 0 a 9.");
            int ruchPoziomy = -1, ruchPionowy = -1;

    while (true)
    {
       
            Console.Write("Podaj X (0-9): ");
            ruchPoziomy = int.Parse(Console.ReadLine());
            Console.Write("Podaj Y (0-9): ");
            ruchPionowy = int.Parse(Console.ReadLine());


            // Znalezienie odpowiedniej komórki i zmiana jej stanu
            var selectedCell = GameBoard.FirstOrDefault(c => c.X == ruchPoziomy && c.Y == ruchPionowy);
            if (selectedCell != null)
            {
                selectedCell.State = '*';
                Console.WriteLine($"Pole ({ruchPoziomy}, {ruchPionowy}) zostało zaktualizowane.");
                break;
            }
            else
            {
                Console.WriteLine("Nie znaleziono pola. Spróbuj ponownie.");
            }
        }
       
    }



        static void Gameloop()
        {
            //Funkcja zapętlająca grę
            while (true)
            {
                Console.Clear(); 
                PrintBoard(); 

                UpdateGameState();
            }
        }

        static void Main(string[] args)
        {
            //Czesc glowna, narazie wypiuje plansze i jej rozmiar
            GameBoard = ImportBoard("game_board.json");
            Console.WriteLine($"Game Size: {GameSize}x{GameSize}");
            PrintBoard();
            Console.ReadLine();
            Gameloop();
        }

    }
}
