namespace star_battle_project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

        }
    }
}
public class Cell
{
    public string State { get; set; } = " ";
    public int Group { get; set; }
}
public class Board
{
    public static int board_size = 9;
    public Cell[,] game_board = new Cell[board_size, board_size];
    // tutaj dac zczytywanie z pliku grup
}