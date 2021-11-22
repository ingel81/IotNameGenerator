using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IotNameGenerator
{
  class Program
  {
    const string SEPERATOR = " ";
    const string JOINSEPERATOR = ", ";

    static void Main(string[] args)
    {
      Console.WriteLine(@$"
         _____              ______                    
        (_____)     _      |  ___ \                   
           _   ___ | |_    | |   | | ____ ____   ____ 
          | | / _ \|  _)   | |   | |/ _  |    \ / _  )
         _| || |_| | |__   | |   | ( ( | | | | ( (/ / 
        (_____)___/ \___)  |_|   |_|\_||_|_|_|_|\____)

          ______                                              
         / _____)                             _               
        | /  ___  ____ ____   ____  ____ ____| |_  ___   ____ 
        | | (___)/ _  )  _ \ / _  )/ ___) _  |  _)/ _ \ / ___)
        | \____/( (/ /| | | ( (/ /| |  ( ( | | |_| |_| | |    
         \_____/ \____)_| |_|\____)_|   \_||_|\___)___/|_|
                                                              v0.1
        ");

      PrintOuput(BuildVariants(Input()));
    }

    private static (string[] Groups, string[] Places, string[] Floors) Input()
    {
      Console.WriteLine("\nKommagetrennte Liste der Geräteart (Was wird geschaltet, z.B.: Licht,Lampe,Beleuchtung,Spots)");
      Console.Write(">> ");
      var groupInput = Console.ReadLine();

      Console.WriteLine("\nKommagetrennte Liste der Bezeichner eingeben (Wo befindet es sich, z.B.: Esstisch,Esszimmer)");
      Console.Write(">> ");
      var placesInput = Console.ReadLine();

      Console.Write(">> ");
      Console.WriteLine("\n(Optional) Kommagetrennten Etagenbezeichner eingeben (Etage, z.B.: EG,Erdgeschoss)");
      var floorInput = Console.ReadLine();

      var groups = groupInput.Split(",", StringSplitOptions.RemoveEmptyEntries);
      var places = placesInput.Split(",", StringSplitOptions.RemoveEmptyEntries);
      var floors = new List<string>();

      if (floorInput != string.Empty)
      {
        floors.AddRange(floorInput.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList());

        Console.WriteLine("\nAuch Varianten ohne Etagenbezeichner generieren? (\"true\"/\"false\" - default: \"true\" = Enter)");
        Console.Write(">> ");
        if (bool.TryParse(Console.ReadLine(), out var doEmptyFloor) == false)
          doEmptyFloor = true;

        if (doEmptyFloor)
          floors.Add(string.Empty);
      }

      return (groups, places, floors.ToArray());
    }

    private static List<string> BuildVariants((string[] Groups, string[] Places, string[] Floors) Input)
    {
      var result = new List<string>();

      foreach (var floor in Input.Floors)
        foreach (var place in Input.Places)
          foreach (var group in Input.Groups)
            AddMixed(result, group, place, floor, SEPERATOR);

      return result.Distinct().ToList();
    }

    private static void AddMixed(List<string> variants, string group, string place, string floor, string seperator)
    {
      if (floor == string.Empty)
      {
        variants.Add($"{group}{seperator}{place}");
        variants.Add($"{place}{seperator}{group}");
      }
      else
      {
        variants.Add($"{floor}{seperator}{group}{seperator}{place}");
        variants.Add($"{place}{seperator}{floor}{seperator}{group}");
        variants.Add($"{place}{seperator}{group}{seperator}{floor}");

        variants.Add($"{group}{seperator}{floor}{seperator}{place}");
        variants.Add($"{group}{seperator}{place}{seperator}{floor}");
      }
    }

    private static void PrintOuput(List<string> output)
    {
      Console.WriteLine($"\n#########################");
      Console.WriteLine($"Varianten: {output.Count}\n");
      Console.WriteLine(string.Join(JOINSEPERATOR, output.ToArray()));
      Console.WriteLine("\n\nBitte beliebige Taste drücken um zu beenden.");
      Console.Write(">> ");
      Console.ReadKey();
    }

  }
}
