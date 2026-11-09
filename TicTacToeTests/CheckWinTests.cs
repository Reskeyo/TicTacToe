using NUnit.Framework;
using TicTacToe2;

namespace TicTacToeTests
{
    public class CheckWinTests
    {
        [SetUp]
        public void Setup()
        {
            Program.gameOver = false;
            Program.spielfeld = new Spielfeld();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void CheckWin_ErkenntGewinn_InAllenZeilen(int zeile)
        {
            Program.spielfeld[zeile, 0] = 1;
            Program.spielfeld[zeile, 1] = 1;
            Program.spielfeld[zeile, 2] = 1;

            Program.CheckWin();

            Assert.That(Program.gameOver, Is.True);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void CheckWin_ErkenntGewinn_InAllenSpalten(int spalte)
        {
            Program.spielfeld[0, spalte] = 2;
            Program.spielfeld[1, spalte] = 2;
            Program.spielfeld[2, spalte] = 2;

            Program.CheckWin();

            Assert.That(Program.gameOver, Is.True);
        }

        [Test]
        public void CheckWin_ErkenntGewinn_InHauptdiagonale()
        {
            Program.spielfeld[0, 0] = 1;
            Program.spielfeld[1, 1] = 1;
            Program.spielfeld[2, 2] = 1;

            Program.CheckWin();

            Assert.That(Program.gameOver, Is.True);
        }

        [Test]
        public void CheckWin_ErkenntGewinn_InNebendiagonale()
        {
            Program.spielfeld[0, 2] = 2;
            Program.spielfeld[1, 1] = 2;
            Program.spielfeld[2, 0] = 2;

            Program.CheckWin();

            Assert.That(Program.gameOver, Is.True);
        }

        [Test]
        public void CheckWin_ErkenntUnentschieden()
        {
            // Volles Spielfeld ohne 3er-Reihe
            // X O X
            // X X O
            // O X O
            Program.spielfeld[0, 0] = 1; Program.spielfeld[0, 1] = 2; Program.spielfeld[0, 2] = 1;
            Program.spielfeld[1, 0] = 1; Program.spielfeld[1, 1] = 1; Program.spielfeld[1, 2] = 2;
            Program.spielfeld[2, 0] = 2; Program.spielfeld[2, 1] = 1; Program.spielfeld[2, 2] = 2;

            Program.CheckWin();

            Assert.That(Program.gameOver, Is.True);
        }

        [Test]
        public void CheckWin_SpielLaeuftNoch_WennWederGewinnNochVoll()
        {
            Program.spielfeld[0, 0] = 1;
            Program.spielfeld[1, 1] = 2;

            Program.CheckWin();

            Assert.That(Program.gameOver, Is.False);
        }
    }
}

