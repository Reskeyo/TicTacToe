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
            int randomIndex = rnd.Next(0, s.getFreePositions().Count);
            String randomFreePos = s.getFreePositions().ElementAt(randomIndex);

            return new int[]{ Convert.ToInt32(randomFreePos[0].ToString()), Convert.ToInt32(randomFreePos[1].ToString()) };
        }

        public void zugSpielen(Spielfeld s)
        {
            int[] pos = pickRandomFreePosition(s);
            s[pos[0], pos[1]] = botCharId;
        }

    }
}
