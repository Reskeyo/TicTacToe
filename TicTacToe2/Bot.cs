using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe2
{
    internal class Bot
    {
        int botCharId;

        public Bot(int botCharId)
        {
            this.botCharId = botCharId;
        }

        public int[] pickRandomFreePosition(Spielfeld s)
        {

            Random rnd = new Random();

            List<string> freePositions = s.getFreePositions();

            int randomIndex = rnd.Next(0, freePositions.Count);
            String randomFreePos = freePositions.ElementAt(randomIndex);

            return new int[] { Convert.ToInt32(randomFreePos[0].ToString()), Convert.ToInt32(randomFreePos[1].ToString()) };
        }

        public void zugSpielen(Spielfeld s)
        {
            int[] pos = pickPosWithAlgo(s);
            s[pos[0], pos[1]] = botCharId;
        }


        //SMART Algo: Der Bot versucht dem Spieler das nächte Feld zu blockieren, wenn der Spieler 2 Felder in einer Reihe hat.

        public int[] pickPosWithAlgo(Spielfeld s)
        {
            int opponentCharId = (botCharId == 1) ? 2 : 1;

            // 1. Zuerst prüfen, ob der Bot gewinnen kann (botCharId).
            // 2. Danach prüfen, ob der Gegner blockiert werden muss (opponentCharId).
            int[] spielerIds = { botCharId, opponentCharId };

            foreach (int id in spielerIds)
            {
                for (int i = 0; i < 3; i++)
                {
                    // Zeilen prüfen
                    if (s[i, 0] == id && s[i, 1] == id && s[i, 2] == 0) return new int[] { i, 2 };
                    if (s[i, 0] == id && s[i, 2] == id && s[i, 1] == 0) return new int[] { i, 1 };
                    if (s[i, 1] == id && s[i, 2] == id && s[i, 0] == 0) return new int[] { i, 0 };

                    // Spalten prüfen
                    if (s[0, i] == id && s[1, i] == id && s[2, i] == 0) return new int[] { 2, i };
                    if (s[0, i] == id && s[2, i] == id && s[1, i] == 0) return new int[] { 1, i };
                    if (s[1, i] == id && s[2, i] == id && s[0, i] == 0) return new int[] { 0, i };
                }

                // Diagonalen prüfen
                if (s[0, 0] == id && s[1, 1] == id && s[2, 2] == 0) return new int[] { 2, 2 };
                if (s[0, 0] == id && s[2, 2] == id && s[1, 1] == 0) return new int[] { 1, 1 };
                if (s[1, 1] == id && s[2, 2] == id && s[0, 0] == 0) return new int[] { 0, 0 };

                if (s[0, 2] == id && s[1, 1] == id && s[2, 0] == 0) return new int[] { 2, 0 };
                if (s[0, 2] == id && s[2, 0] == id && s[1, 1] == 0) return new int[] { 1, 1 };
                if (s[1, 1] == id && s[2, 0] == id && s[0, 2] == 0) return new int[] { 0, 2 };
            }

            // 3. Mitte wählen, wenn frei
            if (s[1, 1] == 0)
            {
                return new int[] { 1, 1 };
            }

            // 4. Ecken wählen, wenn frei
            if (s[0, 0] == 0) return new int[] { 0, 0 };
            if (s[0, 2] == 0) return new int[] { 0, 2 };
            if (s[2, 0] == 0) return new int[] { 2, 0 };
            if (s[2, 2] == 0) return new int[] { 2, 2 };

            // 5. Kanten/Seiten wählen, wenn frei
            if (s[0, 1] == 0) return new int[] { 0, 1 };
            if (s[1, 0] == 0) return new int[] { 1, 0 };
            if (s[1, 2] == 0) return new int[] { 1, 2 };
            if (s[2, 1] == 0) return new int[] { 2, 1 };

            return null;
        }
    }
}
