using System;
using TicTacToe2;

namespace TicTacToeSimulation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("  AUFGABE 4: STRATEGIEVERGLEICH");
            Console.WriteLine("  Strategie A = Intelligent  vs.  Strategie B = Zufall");
            Console.WriteLine("=================================================");
            Console.WriteLine();

            int spieleProSetting = 50; // 50x A beginnt, 50x B beginnt -> Gesamt 100 Spiele
            int n = spieleProSetting * 2;

            // Zähler für "Start: A"
            int startA_SiegeA = 0;
            int startA_Unentsch = 0;
            int startA_SiegeB = 0;
            int startA_Zuege = 0;

            // Zähler für "Start: B"
            int startB_SiegeA = 0;
            int startB_Unentsch = 0;
            int startB_SiegeB = 0;
            int startB_Zuege = 0;

            Console.WriteLine($"Simuliere {n} Partien (bitte warten...)...");

            // 1. Spiele mit Startrecht A
            for (int i = 0; i < spieleProSetting; i++)
            {
                var (gewinner, zuege) = SimulierePartie(aBeginnt: true);
                startA_Zuege += zuege;
                if (gewinner == 1) startA_SiegeA++;
                else if (gewinner == 2) startA_SiegeB++;
                else startA_Unentsch++;
            }

            // 2. Spiele mit Startrecht B
            for (int i = 0; i < spieleProSetting; i++)
            {
                var (gewinner, zuege) = SimulierePartie(aBeginnt: false);
                startB_Zuege += zuege;
                if (gewinner == 1) startB_SiegeA++;
                else if (gewinner == 2) startB_SiegeB++;
                else startB_Unentsch++;
            }

            // Gesamt-Statistiken
            int gesamt_SiegeA = startA_SiegeA + startB_SiegeA;
            int gesamt_Unentsch = startA_Unentsch + startB_Unentsch;
            int gesamt_SiegeB = startA_SiegeB + startB_SiegeB;
            int gesamt_Zuege = startA_Zuege + startB_Zuege;

            Console.WriteLine("Simulation abgeschlossen!\n");

            // Ausgabe der geforderten Tabelle
            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine(string.Format("{0,-14} | {1,7} | {2,9} | {3,7} | {4,11} | {5,11} | {6,7}",
                "Setting", "Siege A", "Unentsch.", "Siege B", "Siege A (%)", "Siege B (%)", "Ø Züge"));
            Console.WriteLine("---------------------------------------------------------------------------------------");

            DruckeZeile($"Gesamt ({n})", gesamt_SiegeA, gesamt_Unentsch, gesamt_SiegeB, gesamt_Zuege, n);
            DruckeZeile($"Start: A ({spieleProSetting})", startA_SiegeA, startA_Unentsch, startA_SiegeB, startA_Zuege, spieleProSetting);
            DruckeZeile($"Start: B ({spieleProSetting})", startB_SiegeA, startB_Unentsch, startB_SiegeB, startB_Zuege, spieleProSetting);

            Console.WriteLine("---------------------------------------------------------------------------------------");
            if (!Console.IsInputRedirected)
            {
                Console.WriteLine("\nDrücke eine beliebige Taste zum Beenden...");
                Console.ReadKey();
            }
        }

        static void DruckeZeile(string label, int siegeA, int unentsch, int siegeB, int zuege, int anzahlSpiele)
        {
            double prozentA = (double)siegeA / anzahlSpiele * 100.0;
            double prozentB = (double)siegeB / anzahlSpiele * 100.0;
            double schnittZuege = (double)zuege / anzahlSpiele;

            Console.WriteLine(string.Format("{0,-14} | {1,7} | {2,9} | {3,7} | {4,10:0.0} % | {5,10:0.0} % | {6,7:0.0}",
                label, siegeA, unentsch, siegeB, prozentA, prozentB, schnittZuege));
        }

        /// <summary>
        /// Simuliert eine vollständige Partie zwischen Bot A (Intelligent) und Bot B (Zufall).
        /// Gibt (Gewinner, Züge) zurück: Gewinner = 1 für A, 2 für B, 0 für Unentschieden.
        /// </summary>
        static (int gewinner, int zuege) SimulierePartie(bool aBeginnt)
        {
            Spielfeld feld = new Spielfeld();

            // Der beginnende Bot bekommt Stein 1 ('X'), der zweite Stein 2 ('O')
            Bot botA = new Bot(aBeginnt ? 1 : 2, useSmartStrat: true);
            Bot botB = new Bot(aBeginnt ? 2 : 1, useSmartStrat: false);

            Bot aktuellerBot = aBeginnt ? botA : botB;
            int zuege = 0;

            while (true)
            {
                aktuellerBot.zugSpielen(feld);
                zuege++;

                int status = PruefeGewinner(feld);
                if (status != 0)
                {
                    if (status == -1)
                    {
                        return (0, zuege); // Unentschieden
                    }

                    // Hat Bot A oder Bot B gewonnen?
                    int gewinner = (status == botA.botCharId) ? 1 : 2;
                    return (gewinner, zuege);
                }

                // Spielerwechsel
                aktuellerBot = (aktuellerBot == botA) ? botB : botA;
            }
        }

        /// <summary>
        /// Prüft den Zustand des Spielfelds:
        /// Rückgabe: 1 (X hat gewonnen), 2 (O hat gewonnen), -1 (Unentschieden), 0 (Spiel läuft noch).
        /// </summary>
        static int PruefeGewinner(Spielfeld s)
        {
            // Zeilen
            for (int i = 0; i < 3; i++)
            {
                if (s[i, 0] != 0 && s[i, 0] == s[i, 1] && s[i, 1] == s[i, 2])
                    return s[i, 0];
            }

            // Spalten
            for (int j = 0; j < 3; j++)
            {
                if (s[0, j] != 0 && s[0, j] == s[1, j] && s[1, j] == s[2, j])
                    return s[0, j];
            }

            // Diagonalen
            if (s[0, 0] != 0 && s[0, 0] == s[1, 1] && s[1, 1] == s[2, 2])
                return s[0, 0];
            if (s[0, 2] != 0 && s[0, 2] == s[1, 1] && s[1, 1] == s[2, 0])
                return s[0, 2];

            // Unentschieden (keine freien Felder mehr)
            if (s.getFreePositions().Count == 0)
                return -1;

            return 0; // Spiel läuft noch
        }
    }
}
