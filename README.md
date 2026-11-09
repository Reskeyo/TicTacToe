# Machine Learning Praktikum 1: TicTacToe

---

## Projektübersicht

* **`TicTacToe2`**: Hauptanwendung (Konsolenspiel Mensch gegen Bot).
* **`TicTacToeTests`**: Automatisierte Unit-Tests mit NUnit zur Qualitätskontrolle.
* **`TicTacToeSimulation`**: Automatisierter Strategievergleich (100 Partien) mit Ergebnistabelle.

---

## Übersicht der 4 Aufgaben

### Aufgabe 1: Basis-Spiel und Zufallsstrategie
* **Spielfeld:** 3x3-Array mit formatierter Konsolenausgabe nach jedem Zug (`Spielfeld.cs`).
* **Stein- und Startwahl:** Der Spieler kann frei zwischen `X` (Erster) und `O` (Zweiter) wählen. Wählt der Spieler `O`, eröffnet der Computer als `X` automatisch die Partie.
* **Eingabevalidierung:** Fehleingaben (bereits belegte Felder, falsche Längen, Zahlen außerhalb von 1–3) werden abgefangen.
* **Zufallsstrategie:** Der Bot wählt bei jedem Zug zufällig aus der Liste aller aktuell noch freien Felder (`pickRandomFreePosition`).
* **Spielstatus:** Nach jedem Zug wird auf Sieg, Unentschieden oder Fortsetzung geprüft. Am Ende jeder Runde kann direkt eine Revanche gestartet werden.

---

### Aufgabe 2: Entwicklung einer intelligenten Strategie
Ersetzung des Zufalls durch einen deterministischen, regelbasierten Algorithmus (`pickPosWithAlgo` in `Bot.cs`):

1. **Siegzug vollenden:** Hat der Bot 2 Steine in einer Reihe (Zeile, Spalte, Diagonale), wird das 3. Feld sofort belegt.
2. **Gegner blockieren:** Hat der Gegner 2 Steine in einer Reihe, blockiert der Bot die gegnerische Reihe.
3. **Mitte sichern:** Ist das Feld `[1, 1]` frei, wird die strategisch wertvolle Spielfeldmitte gewählt.
4. **Ecken besetzen:** Freie Ecken (`[0, 0]`, `[0, 2]`, `[2, 0]`, `[2, 2]`) werden bevorzugt.
5. **Kanten besetzen:** Zuletzt werden freie Randfelder gewählt.
* **Austauschbarkeit:** Über den Parameter `useSmartStrat` im Bot-Konstruktor kann jederzeit nahtlos zwischen intelligenter und Zufallsstrategie gewechselt werden.

---

### Aufgabe 3: Testen und Qualitätskontrolle
Automatisiertes Testprojekt (`TicTacToeTests`) mit **NUnit** (16 erfolgreiche Tests):

1. **Tests der Basisfunktionen (`CheckWinTests.cs`):**
   * Gewinnerkennung für alle 3 Zeilen (0, 1, 2)
   * Gewinnerkennung für alle 3 Spalten (0, 1, 2)
   * Gewinnerkennung für beide Diagonalen (Haupt- und Nebendiagonale)
   * Korrekte Erkennung eines Unentschiedens bei vollem Spielfeld
   * Laufender Spielstatus (`gameOver == false`), solange keine Entscheidung gefallen ist
2. **Tests der Zufallsstrategie (`BotTests.cs`):**
   * Sicherstellung, dass der Zufallsbot ausschließlich legale/freie Felder wählt.
   * Überprüfung, dass pro Zug exakt ein neuer Stein gesetzt wird.
3. **Tests der intelligenten Strategie (`BotTests.cs`):**
   * Bot nutzt vorhandene Gewinnchancen konsequent.
   * Bot blockiert drohende Siege des Gegners.
   * Bot wählt bei leerem Spielfeld automatisch das Zentrum `[1, 1]`.
   * Bot findet und besetzt bei fast vollem Spielfeld das letzte freie Feld.

---

### Aufgabe 4: Vergleich der Strategien
Konsolenanwendung `TicTacToeSimulation` lässt Strategie A (Intelligent) und Strategie B (Zufall) in **100 Partien** gegeneinander antreten (50x Startrecht A, 50x Startrecht B):

```text
Setting        | Siege A | Unentsch. | Siege B | Siege A (%) | Siege B (%) |  Ø Züge
---------------------------------------------------------------------------------------
Gesamt (100)   |      93 |         5 |       2 |       93,0 % |        2,0 % |     6,0
Start: A (50)  |      49 |         1 |       0 |       98,0 % |        0,0 % |     5,4
Start: B (50)  |      44 |         4 |       2 |       88,0 % |        4,0 % |     6,6
---------------------------------------------------------------------------------------
```

#### Reflexion (Erkenntnisse):
* **Startrecht:** Das Startrecht hat einen klaren Einfluss. Beginnt A, gewinnt die Strategie fast jedes Spiel (98 %) ohne eine einzige Niederlage. Beginnt B, kann der Zufall in seltenen Fällen (4 %) einen Sieg erringen, und die Partien dauern länger (Ø 6,6 statt 5,4 Züge).
* **Leistungsvorteil:** Über 93 % Gesamtsiege beweisen die deutliche Überlegenheit der heuristischen Regeln gegenüber dem Zufall.
* **Schwachstellen:** Zufällige Siege von B entstehen fast ausschließlich dann, wenn B durch Zufall eine Doppelbedrohung („Fork“) setzt, die von einfachen 1-Zug-Heuristiken nicht vorausschauend verhindert wird.
* **Optimierungspotenzial:** Einsatz des **Minimax-Algorithmus**, um den Bot mathematisch unschlagbar (0 % Niederlagen) zu machen.

