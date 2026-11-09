using NUnit.Framework;
using TicTacToe2;

namespace TicTacToeTests
{
    public class BotTests
    {
        // ==========================================
        // 1. TESTS DER INTELLIGENTEN STRATEGIE
        // ==========================================

        [Test]
        public void IntelligenterBot_SollteGewinnzugNutzen()
        {
            // Situation vorbereiten (Arrange):
            // Bot ist 1 ('X') und hat 2 in der oberen Zeile: [X, X, leer]
            Spielfeld feld = new Spielfeld();
            feld[0, 0] = 1;
            feld[0, 1] = 1;
            feld[0, 2] = 0; // hier MUSS der Bot hinsetzen

            Bot bot = new Bot(1);

            // Zug ausführen (Act):
            int[] zug = bot.pickPosWithAlgo(feld);

            // Überprüfen (Assert):
            Assert.That(zug[0], Is.EqualTo(0));
            Assert.That(zug[1], Is.EqualTo(2));
        }

        [Test]
        public void IntelligenterBot_SollteGegnerBlockieren()
        {
            // Situation vorbereiten:
            // Bot ist 2 ('O'). Der Gegner ('X' = 1) droht in Spalte 0 zu gewinnen:
            Spielfeld feld = new Spielfeld();
            feld[0, 0] = 1;
            feld[1, 0] = 1;
            feld[2, 0] = 0; // hier muss der Bot blockieren!

            Bot bot = new Bot(2);

            int[] zug = bot.pickPosWithAlgo(feld);

            Assert.That(zug[0], Is.EqualTo(2));
            Assert.That(zug[1], Is.EqualTo(0));
        }

        [Test]
        public void IntelligenterBot_WaehltMitteBeiLeeremSpielfeld()
        {
            // Bei leerem Feld soll der Bot sofort die Mitte [1, 1] besetzen
            Spielfeld feld = new Spielfeld();
            Bot bot = new Bot(1);

            int[] zug = bot.pickPosWithAlgo(feld);

            Assert.That(zug[0], Is.EqualTo(1));
            Assert.That(zug[1], Is.EqualTo(1));
        }

        [Test]
        public void IntelligenterBot_WaehltLetztesFreiesFeld_WennFastVoll()
        {
            // Spielfeld fast komplett voll, nur noch [2, 2] ist frei
            Spielfeld feld = new Spielfeld();
            feld[0, 0] = 1; feld[0, 1] = 2; feld[0, 2] = 1;
            feld[1, 0] = 2; feld[1, 1] = 1; feld[1, 2] = 2;
            feld[2, 0] = 2; feld[2, 1] = 1; feld[2, 2] = 0; // letztes freies Feld

            Bot bot = new Bot(1);

            int[] zug = bot.pickPosWithAlgo(feld);

            Assert.That(zug[0], Is.EqualTo(2));
            Assert.That(zug[1], Is.EqualTo(2));
        }

        // ==========================================
        // 2. TESTS DER ZUFALLSSTRATEGIE
        // ==========================================

        [Test]
        public void ZufallsBot_WaehltNurFreieFelder()
        {
            Spielfeld feld = new Spielfeld();
            // Belege fast alles, nur [1, 2] und [2, 1] bleiben frei:
            feld[0, 0] = 1; feld[0, 1] = 2; feld[0, 2] = 1;
            feld[1, 0] = 2; feld[1, 1] = 1;
            feld[2, 0] = 2; feld[2, 2] = 1;

            Bot bot = new Bot(2);

            // 20-mal testen: Der Zufall darf niemals ein belegtes Feld wählen
            for (int i = 0; i < 20; i++)
            {
                int[] zug = bot.pickRandomFreePosition(feld);
                Assert.That(feld[zug[0], zug[1]], Is.EqualTo(0), "Zufallsbot hat ein bereits belegtes Feld gewählt!");
            }
        }

        [Test]
        public void ZufallsBot_SetztGenauEinenStein()
        {
            Spielfeld feld = new Spielfeld();
            Bot bot = new Bot(1);

            int freieFelderVorher = feld.getFreePositions().Count; // 9

            bot.zugSpielen(feld);

            int freieFelderNachher = feld.getFreePositions().Count; // 8
            Assert.That(freieFelderNachher, Is.EqualTo(freieFelderVorher - 1));
        }
    }
}
