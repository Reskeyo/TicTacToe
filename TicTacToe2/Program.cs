namespace TicTacToe2
{
    internal class Program
    {

        static int playerCharId;

        static bool gameOver;
        static Spielfeld spielfeld;

        static void Main(string[] args)
        {
            bool playAgain = true;

            while (playAgain)
            {
                gameOver = false;

                spielfeld = new Spielfeld();
                Bot bot = null;

                Console.WriteLine("Wähle deinen Spielstein (X oder O): ");
                string input = Console.ReadLine();
                if (input == "X" || input == "O")
                {
                    playerCharId = (char.Parse(input) == 'X' ? 1 : 2);
                    bot = new Bot(playerCharId == 1 ? 2 : 1);
                }
                else
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte wähle X oder O.");
                    Main(args);
                }

                while (!gameOver)
                {
                    Console.WriteLine("Gib eine Position ein (z.B. 11): ");
                    string position = Console.ReadLine();
                    if (position.Length < 2)
                    {
                        Console.WriteLine("Falsche Eingabe! Zulässig: 11, 12, ... , 32, 33  ");
                        continue;
                    }

                    int[] pos = { Convert.ToInt16(position[0].ToString()) - 1, Convert.ToInt16(position[1].ToString()) - 1 };

                    if (pos[0] < 0 || pos[0] > 2 || pos[1] < 0 || pos[1] > 2)
                    {
                        Console.WriteLine("Ungültige Position. Bitte wähle eine Position zwischen 11 und 33.");
                        continue;
                    }

                    if (spielfeld[pos[0], pos[1]] == 0)
                    {
                        spielfeld[pos[0], pos[1]] = playerCharId;
                    }
                    else
                    {
                        Console.WriteLine("Diese Position ist bereits belegt. Wähle eine andere Position.");
                        continue;
                    }


                    spielfeld.DrawSpielfeld();
                    CheckWin();
                    if (gameOver)
                    {
                        PlayAgain(playAgain);
                        break;
                    }

                    Console.WriteLine("Bot ist am Zug.");
                    bot.zugSpielen(spielfeld);


                    spielfeld.DrawSpielfeld();
                    CheckWin();
                    if (gameOver)
                    {
                        PlayAgain(playAgain);
                        break;
                    }

                }
            }
        }

        static void PlayAgain(bool playAgain)
        {
            Console.Write("Weiter spielen? (y/n):\t");
            if (Console.ReadLine() == "y") { playAgain = true; }
            else { playAgain = false; }
        }

        static void CheckWin()
        {
            // Überprüfen der Zeilen
            for (int i = 0; i < 3; i++)
            {
                if (spielfeld[i, 0] != 0 && spielfeld[i, 0] == spielfeld[i, 1] && spielfeld[i, 1] == spielfeld[i, 2])
                {
                    gameOver = true;
                    Console.WriteLine($"Spieler {GetPlayerChar(spielfeld[i, 0])} hat gewonnen!");
                    return;
                }
            }
            // Überprüfen der Spalten
            for (int j = 0; j < 3; j++)
            {
                if (spielfeld[0, j] != 0 && spielfeld[0, j] == spielfeld[1, j] && spielfeld[1, j] == spielfeld[2, j])
                {
                    gameOver = true;
                    Console.WriteLine($"Spieler {GetPlayerChar(spielfeld[0, j])} hat gewonnen!");
                    return;
                }
            }
            // Überprüfen der Diagonalen
            if (spielfeld[0, 0] != 0 && spielfeld[0, 0] == spielfeld[1, 1] && spielfeld[1, 1] == spielfeld[2, 2])
            {
                gameOver = true;
                Console.WriteLine($"Spieler {GetPlayerChar(spielfeld[0, 0])} hat gewonnen!");
                return;
            }
            if (spielfeld[0, 2] != 0 && spielfeld[0, 2] == spielfeld[1, 1] && spielfeld[1, 1] == spielfeld[2, 0])
            {
                gameOver = true;
                Console.WriteLine($"Spieler {GetPlayerChar(spielfeld[0, 2])} hat gewonnen!");
                return;
            }
            // Überprüfen auf Unentschieden
            bool isDraw = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (spielfeld[i, j] == 0)
                    {
                        isDraw = false;
                        break;
                    }
                }
            }
            if (isDraw)
            {
                gameOver = true;
                Console.WriteLine("Das Spiel ist unentschieden!");
            }
        }

        static char GetPlayerChar(int playerCharId)
        {
            if (playerCharId == 1)
            {
                return 'X';
            }
            else if (playerCharId == 2)
            {
                return 'O';
            }
            else if (playerCharId == 0)
            {
                return ' ';
            }
            else
            {
                throw new ArgumentException("Ungültige Spieler-ID. Bitte wähle 1 für X oder 2 für O.");
            }
        }
    }
}
