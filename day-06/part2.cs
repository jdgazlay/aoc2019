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

        // compile orbits
        foreach (string item in map)
        {
            string[] satellites = item.Split(")");
            orbits.Add(satellites[1], new Orbit(satellites[0], satellites[1]));
        }

        Func<Orbit, List<string>> flattenPath = delegate(Orbit orbit)
        {
            List<string> flatOrbits = new List<string>();
            while (orbit.Name != "COM")
            {
                flatOrbits.Add(orbit.Name);
                orbit = orbits[orbit.Parent];
            }
            return flatOrbits;
        };

        Func<List<string>, List<string>, int> getOrbitHops = delegate(List<string> orbitA, List<string> orbitB)
        {
            for (int i = 0; i < orbitA.Count; i++)
            {
                for (int j = 0; j < orbitB.Count; j++)
                {
                    if (orbitA[i] == orbitB[j])
                        return i + j;
                }
            }
            return 0;
        };

        // find orbit path from COM )))) YOU && COM )))) SAN

        Orbit ourOrbitPath = orbits[orbits["YOU"].Parent];
        Orbit santaOrbitPath = orbits[orbits["SAN"].Parent];
        List<string> ourOrbits = flattenPath(ourOrbitPath);
        List<string> santaOrbits = flattenPath(santaOrbitPath);

        Console.WriteLine(getOrbitHops(ourOrbits, santaOrbits));

    }
}
