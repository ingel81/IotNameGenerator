using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IotNameGenerator
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Bitte kommagetrennte Liste der Geräteart eingeben  (Was wird geschaltet, z.b.: Licht,Lampe,Beleuchtung,Spots)");
      var groupInput = Console.ReadLine();

      Console.WriteLine("Bitte kommagetrennte Liste der Bezeichner eingeben (Wo befindet es sich, z.B.: Esstisch,Esszimmer)");
      var placesInput = Console.ReadLine();

      Console.WriteLine("zusätzlichen kommagetrennte Etagenbezeichner eingeben (Etage, z.B.: EG,Erdgeschoss)");
      var floorInput = Console.ReadLine();

      Console.WriteLine("Auch Varianten ohne Etagenbezeichner generieren? (true/false - default true)");
      if (bool.TryParse(Console.ReadLine(), out var doEmptyFloor) == false)
        doEmptyFloor = true;

      var groups = groupInput.Split(",", StringSplitOptions.RemoveEmptyEntries);
      var places = placesInput.Split(",", StringSplitOptions.RemoveEmptyEntries);
      var floors = new List<string>();

      if (doEmptyFloor)
        floors.Add(string.Empty);

      if (floorInput != string.Empty)
        floors.AddRange(floorInput.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList());


      var variants = new List<string>();
      var seperator = " ";

      foreach (var floor in floors)
        foreach (var place in places)
          foreach (var group in groups)
            AddMixed(variants, group, place, floor, seperator);

      var deDupedVariants = variants.Distinct().ToList();
      Console.WriteLine($"Variants: {deDupedVariants.Count} deduped");
      Console.WriteLine();
      Console.WriteLine(string.Join(", ", deDupedVariants.ToArray()));
    }

    static void AddMixed(List<string> variants, string group, string place, string floor, string seperator)
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

  }
}
