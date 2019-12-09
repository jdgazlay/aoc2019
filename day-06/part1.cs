using System;
using System.Collections.Generic;
using PuzzleInput;

class Orbit
{
    public string Parent;
    public string Name;

    public Orbit(string parent, string name)
    {
        Parent = parent;
        Name = name;
    }
}

class Planets
{
    static void Main()
    {

        string[] map = Input.PuzzleInput;
        Dictionary<string, Orbit> orbits = new Dictionary<string, Orbit>();
        orbits.Add("COM", new Orbit("", "COM"));
        int total = 0;

        foreach (string item in map)
        {
            string[] satellites = item.Split(")");
            orbits.Add(satellites[1], new Orbit(satellites[0], satellites[1]));
        }

        foreach (KeyValuePair<string, Orbit> kvp in orbits)
        {
            Orbit orbit = kvp.Value;
            Console.WriteLine("name: {0} parent: {1}", kvp.Value.Name, kvp.Value.Parent);
            while (orbit.Name != "COM")
            {
                orbit = orbits[orbit.Parent];
                total++;
            }
        }
        Console.WriteLine(total);
    }
}
