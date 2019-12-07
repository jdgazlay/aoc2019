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

    /// <summary>Has two adjacent digits and digits never descrease</summary>
    private bool MeetsDigitRequirements()
    {
        int adjacentCount = 0;
        bool digitsOnlyIncrease = true;
        Action<char, char> isMatching = delegate(char prevChar, char currentChar)
        {
            if (prevChar == currentChar)
                adjacentCount++;
            if (Convert.ToInt32(prevChar) > Convert.ToInt32(currentChar))
                digitsOnlyIncrease = false;
        };

        for (int i = 1; i < password.Length; i++)
        {
            isMatching(password[i-1], password[i]);
        }

        return adjacentCount > 0 && digitsOnlyIncrease;
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
