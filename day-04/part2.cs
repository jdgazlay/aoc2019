using System;
using System.Linq;
using System.Collections.Generic;

class Password
{
    string password;

    public Password(string password)
    {
        this.password = password;
    }

    public bool MeetsRequirements()
    {
        return password.Length == 6 && MeetsDigitRequirements();
    }

    private bool MeetsDigitRequirements()
    {
        Dictionary<char,int> charCount = new Dictionary<char, int>();
        bool digitsOnlyIncrease = true;
        Action<char, char> isMatching = delegate(char prevChar, char currentChar)
        {
            if (prevChar == currentChar)
                if (!charCount.ContainsKey(currentChar))
                    charCount.Add(currentChar, 2);
                else
                    charCount[currentChar]++;
            if (Convert.ToInt32(prevChar) > Convert.ToInt32(currentChar))
                digitsOnlyIncrease = false;
        };

        for (int i = 1; i < password.Length; i++)
        {
            isMatching(password[i-1], password[i]);
        }

        return charCount.Count > 0 && charCount.ContainsValue(2) && digitsOnlyIncrease;
    }
}

class PasswordCruncher
{
    static void Main()
    {
        int min = 171309;
        int max = 643603;
        // int min = 223448;
        // int max = 223454;

        List<Password> passwordCandidates = new List<Password>();

        for (int counter = min; counter < max; counter++)
        {
            Password password = new Password(counter.ToString());
            if (password.MeetsRequirements())
                passwordCandidates.Add(password);
        }

        Console.WriteLine(passwordCandidates.Count());
    }
}
