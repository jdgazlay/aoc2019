using System;
using PuzzleInput;

class Intcode
{
    private static int TERMINATE_OPCODE = 99;
    private static int ADD_OPCODE = 1;
    private static int MULTIPLY_OPCODE = 2;
    private static int INPUT_OPCODE = 3;
    private static int OUTPUT_OPCODE = 4;
    private static int JUMP_IF_TRUE_OPCODE = 5;
    private static int JUMP_IF_FALSE_OPCODE = 6;
    private static int LESS_THAN_OPCODE = 7;
    private static int EQUALS_OPCODE = 8;

    private bool phaseSettingUsed = false;
    private int[] memory;
    private int output;

    public Intcode(int[] memory)
    {
        this.memory = memory;
    }

    public int Run(int input, int phaseSetting)
    {
        int instructionPointer = 0;
        int[] instructions = new int[memory.Length];
        int opcode;
        string parameterModes;
        int instructionSet = 4;
        Array.Copy(memory, instructions, memory.Length);

        while (instructions[instructionPointer] != TERMINATE_OPCODE)
        {
            opcode = instructions[instructionPointer] % 10;
            parameterModes = (instructions[instructionPointer] / 100).ToString();
            if (parameterModes.Length == 1)
                parameterModes = "0" + parameterModes;

            if (opcode == ADD_OPCODE)
            {
                instructions[instructions[instructionPointer + 3]] = GetValue(instructions, instructionPointer + 1, parameterModes[1]) + GetValue(instructions, instructionPointer + 2, parameterModes[0]);
                instructionSet = 4;
            }
            else if (opcode == MULTIPLY_OPCODE)
            {
                instructions[instructions[instructionPointer + 3]] = GetValue(instructions, instructionPointer + 1, parameterModes[1]) * GetValue(instructions, instructionPointer + 2, parameterModes[0]);
                instructionSet = 4;
            }
            else if (opcode == INPUT_OPCODE)
            {
                int value = phaseSettingUsed ? input : phaseSetting;
                phaseSettingUsed = true;
                instructions[instructions[instructionPointer + 1]] = value;
                instructionSet = 2;
            }
            else if (opcode == OUTPUT_OPCODE)
            {
                Console.WriteLine("output: {0}", GetValue(instructions, instructionPointer + 1, parameterModes[1]).ToString());
                output = GetValue(instructions, instructionPointer + 1, parameterModes[1]);
                instructionSet = 2;
            }
            else if (opcode == JUMP_IF_TRUE_OPCODE)
            {
                if (GetValue(instructions, instructionPointer + 1, parameterModes[1]) != 0)
                {
                    int value = GetValue(instructions, instructionPointer + 2, parameterModes[0]);
                    instructions[instructionPointer] = value;
                    instructionPointer = value;
                    instructionSet = 0;
                } else
                    instructionSet = 3;
            }
            else if (opcode == JUMP_IF_FALSE_OPCODE)
            {
                if (GetValue(instructions, instructionPointer + 1, parameterModes[1]) == 0)
                {
                    int value = GetValue(instructions, instructionPointer + 2, parameterModes[0]);
                    instructions[instructionPointer] = value;
                    instructionPointer = value;
                    instructionSet = 0;
                } else
                    instructionSet = 3;
            }
            else if (opcode == LESS_THAN_OPCODE)
            {
                bool isLessThan = GetValue(instructions, instructionPointer + 1, parameterModes[1]) < GetValue(instructions, instructionPointer + 2, parameterModes[0]);
                instructions[instructions[instructionPointer + 3]] = isLessThan ? 1 : 0;
                instructionSet = 4;
            }
            else if (opcode == EQUALS_OPCODE)
            {
                bool isEqual = GetValue(instructions, instructionPointer + 1, parameterModes[1]) == GetValue(instructions, instructionPointer + 2, parameterModes[0]);
                instructions[instructions[instructionPointer + 3]] = isEqual ? 1 : 0;
                instructionSet = 4;
            }
            else if (opcode != 99)
                return 0;

            Console.WriteLine("instructionPointer: {0} opcode: {1} instructionSet: {2}", instructionPointer, opcode, instructionSet);
            instructionPointer += instructionSet;
        }
        phaseSettingUsed = false;
        return output;
    }

    private static int GetValue(int[] instructions, int position, char parameterMode)
    {
        if (parameterMode == Convert.ToChar("1"))
            return instructions[position];

        return instructions[instructions[position]];
    }
}


class GravityAssist
{
    static void Main()
    {
        int[] memory = Input.PuzzleInput;
        string[] phaseSequences = Input.allPhases;
        int maxOutput = 0;

        Intcode ampA = new Intcode(memory);
        Intcode ampB = new Intcode(memory);
        Intcode ampC = new Intcode(memory);
        Intcode ampD = new Intcode(memory);
        Intcode ampE = new Intcode(memory);

        foreach (string phaseSequence in phaseSequences)
        {
            int[] phaseSettings = Array.ConvertAll(phaseSequence.ToCharArray(), x => int.Parse(x.ToString()));

            int outputA = ampA.Run(0, phaseSettings[0]);
            int outputB = ampB.Run(outputA, phaseSettings[1]);
            int outputC = ampC.Run(outputB, phaseSettings[2]);
            int outputD = ampD.Run(outputC, phaseSettings[3]);
            int finalOutput = ampE.Run(outputD, phaseSettings[4]);

            maxOutput = Math.Max(maxOutput, finalOutput);
        }



        Console.WriteLine(maxOutput);
    }
}
