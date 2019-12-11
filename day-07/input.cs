using System;
using System.Collections.Generic;


namespace PuzzleInput
{
    class Input
    {
        public static Dictionary<string, int[]> TestInputPart1 = new Dictionary<string, int[]> {
            // output = 43210
            {"memory", new int[] {3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0}},
            {"phases", new int[] {4,3,2,1,0}},
        };
        public static Dictionary<string, int[]> TestInputPart2 = new Dictionary<string, int[]> {
            // output = 54321
            {"memory", new int[] {3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0}},
            {"phases", new int[] {0,1,2,3,4}},
        };
        public static Dictionary<string, int[]> TestInputPart3 = new Dictionary<string, int[]> {
            // output = 65210
            {"memory", new int[] {3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0}},
            {"phases", new int[] {1,0,4,3,2}},
        };
        public static int[] PuzzleInput = new int[] {3,8,1001,8,10,8,105,1,0,0,21,34,59,68,89,102,183,264,345,426,99999,3,9,102,5,9,9,1001,9,5,9,4,9,99,3,9,101,3,9,9,1002,9,5,9,101,5,9,9,1002,9,3,9,1001,9,5,9,4,9,99,3,9,101,5,9,9,4,9,99,3,9,102,4,9,9,101,3,9,9,102,5,9,9,101,4,9,9,4,9,99,3,9,1002,9,5,9,1001,9,2,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,99,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,99};
        public static string[] allPhases = new PermutePhases().Run("01234");
    }
}


class PermutePhases
{ // thanks to https://www.geeksforgeeks.org/c-program-to-print-all-permutations-of-a-given-string-2/ for the help on this one
    /**
    * permutation function
    * @param str string to
       calculate permutation for
    * @param l starting index
    * @param r end index
    */
    public static List<string> permutations = new List<string>();
    private static void permute(String str,
                                int l, int r)
    {
        if (l == r)
        {
            Console.WriteLine(str);
            permutations.Add(str);
        }
        else {
            for (int i = l; i <= r; i++) {
                str = swap(str, l, i);
                permute(str, l + 1, r);
                str = swap(str, l, i);
            }
        }
    }

    /**
    * Swap Characters at position
    * @param a string value
    * @param i position 1
    * @param j position 2
    * @return swapped string
    */
    public static String swap(String a,
                              int i, int j)
    {
        char temp;
        char[] charArray = a.ToCharArray();
        temp = charArray[i];
        charArray[i] = charArray[j];
        charArray[j] = temp;
        string s = new string(charArray);
        return s;
    }

    public string[] Run(string str)
    {
        int n = str.Length;
        permute(str, 0, n - 1);
        return permutations.ToArray();
    }
}
