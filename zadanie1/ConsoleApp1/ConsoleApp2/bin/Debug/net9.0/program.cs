using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string inputFile = "ranking_raw.txt";
        string outputFile = "ranking_clean.txt";

        List<string> lines = new List<string>(File.ReadAllLines(inputFile));
        List<string> cleanLines = new List<string>();

        if (lines.Count == 0)
        {
            Console.WriteLine("Plik jest pusty.");
            return;
        }

        cleanLines.Add(lines[0]);

        for (int i = 1; i < lines.Count; i++)
        {
            string line = lines[i];
            string[] pola = line.Split(';');

            if (pola.Length != 5)
                continue;

            string nick = pola[0];
            string czas = pola[1];
            string punktyStr = pola[2];
            string status = pola[3];
            string opis = pola[4];

            if (status == "HACKER")
                continue;

            if (czas == "00:00:01" || czas == "0:00:01")
                continue;

            int punkty;
            if (!int.TryParse(punktyStr, out punkty))
            {
                punkty = 0;
            }

            string newLine = $"{nick};{czas};{punkty};{status};{opis}";
            cleanLines.Add(newLine);
        }

        File.WriteAllLines(outputFile, cleanLines);

        Console.WriteLine("Wynikowy ranking (ranking_clean.txt):");
        foreach (string l in cleanLines)
        {
            Console.WriteLine(l);
        }
    }
}
