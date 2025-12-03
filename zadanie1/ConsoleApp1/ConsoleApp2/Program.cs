using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string inputFile = "ranking_raw.txt";
        string outputFile = "ranking_clean.txt";

        if (!File.Exists(inputFile))
        {
            Console.WriteLine("Plik nie istnieje.");
            return;
        }

        List<string> lines = new List<string>(File.ReadAllLines(inputFile));

        if (lines.Count == 0)
        {
            Console.WriteLine("Plik jest pusty.");
            return;
        }

        List<string> cleanLines = new List<string>();
        cleanLines.Add(lines[0]);

        for (int i = 1; i < lines.Count; i++)
        {
            string line = lines[i];
            string[] pola = line.Split(';');

            if (pola.Length != 5)
                continue;

            string nick = pola[0].Trim();
            string czas = pola[1].Trim();
            string punktyStr = pola[2].Trim();
            string status = pola[3].Trim();
            string opis = pola[4].Trim();

            if (status.Equals("HACKER", StringComparison.OrdinalIgnoreCase))
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

        foreach (string l in cleanLines)
        {
            Console.WriteLine(l);
        }
    }
}