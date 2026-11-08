using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe2
{
    internal class Spielfeld
    {
        // 0 = leer, 1 = X, 2 = O 
        public int[,] spielfeldArray = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

        public int this[int i, int j]
        {
            get { return spielfeldArray[i, j]; }
            set { spielfeldArray[i, j] = value; }
        }

        public List<String> getFreePositions()
        {
            List<String> freePositions = new List<String>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (this.spielfeldArray[i, j] == 0)
                    {
                        freePositions.Add($"{i}{j}");
                    }
                }
            }

            return freePositions;
        }

        public char getCharAtPosition(int i, int j)
        {
            return convertIdToChar(spielfeldArray[i, j]);
        }

        public int convertCharToId(char playerChar)
        {
            if (playerChar == 'X' || playerChar == 'x')
            {
                return 1;
            }
            else if (playerChar == 'O' || playerChar == 'o')
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        public char convertIdToChar(int playerId)
        {
            if (playerId == 1)
            {
                return 'X';
            }
            else if (playerId == 2)
            {
                return 'O';
            }
            else
            {
                return ' ';
            }
        }

        public void DrawSpielfeld()
        {
            Console.Clear();
            Console.WriteLine("  1 2 3");
            for (int i = 0; i < 3; i++)
            {
                Console.Write((i + 1) + " ");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(getCharAtPosition(i, j));
                    if (j < 2)
                    {
                        Console.Write("|");
                    }
                }
                Console.WriteLine();
                if (i < 2)
                {
                    Console.WriteLine("  -----");
                }
            }
        }
    }
}
