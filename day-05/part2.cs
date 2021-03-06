using System;


class GravityAssist
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

    private static int input;
    private static int output;

    public static int Intcode(int[] memory)
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
                Console.WriteLine("Please give an input");
                input = Convert.ToInt32(Console.ReadLine());
                instructions[instructions[instructionPointer + 1]] = input;
                instructionSet = 2;
            }
            else if (opcode == OUTPUT_OPCODE)
            {
                Console.WriteLine("output: {0}", GetValue(instructions, instructionPointer + 1, parameterModes[1]).ToString());
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
        return instructions[0];
    }

    private static int GetValue(int[] instructions, int position, char parameterMode)
    {
        if (parameterMode == Convert.ToChar("1"))
            return instructions[position];

        return instructions[instructions[position]];
    }

    static void Main()
    {
        int[] memory = new int[] {3,225,1,225,6,6,1100,1,238,225,104,0,1102,88,66,225,101,8,125,224,101,-88,224,224,4,224,1002,223,8,223,101,2,224,224,1,224,223,223,1101,87,23,225,1102,17,10,224,101,-170,224,224,4,224,102,8,223,223,101,3,224,224,1,223,224,223,1101,9,65,225,1101,57,74,225,1101,66,73,225,1101,22,37,224,101,-59,224,224,4,224,102,8,223,223,1001,224,1,224,1,223,224,223,1102,79,64,225,1001,130,82,224,101,-113,224,224,4,224,102,8,223,223,1001,224,7,224,1,223,224,223,1102,80,17,225,1101,32,31,225,1,65,40,224,1001,224,-32,224,4,224,102,8,223,223,1001,224,4,224,1,224,223,223,2,99,69,224,1001,224,-4503,224,4,224,102,8,223,223,101,6,224,224,1,223,224,223,1002,14,92,224,1001,224,-6072,224,4,224,102,8,223,223,101,5,224,224,1,223,224,223,102,33,74,224,1001,224,-2409,224,4,224,1002,223,8,223,101,7,224,224,1,223,224,223,4,223,99,0,0,0,677,0,0,0,0,0,0,0,0,0,0,0,1105,0,99999,1105,227,247,1105,1,99999,1005,227,99999,1005,0,256,1105,1,99999,1106,227,99999,1106,0,265,1105,1,99999,1006,0,99999,1006,227,274,1105,1,99999,1105,1,280,1105,1,99999,1,225,225,225,1101,294,0,0,105,1,0,1105,1,99999,1106,0,300,1105,1,99999,1,225,225,225,1101,314,0,0,106,0,0,1105,1,99999,107,677,677,224,1002,223,2,223,1006,224,329,101,1,223,223,108,677,677,224,1002,223,2,223,1005,224,344,101,1,223,223,1007,677,677,224,1002,223,2,223,1006,224,359,101,1,223,223,1107,226,677,224,1002,223,2,223,1006,224,374,1001,223,1,223,8,677,226,224,1002,223,2,223,1006,224,389,101,1,223,223,1108,677,677,224,1002,223,2,223,1005,224,404,1001,223,1,223,7,226,226,224,1002,223,2,223,1006,224,419,101,1,223,223,1107,677,677,224,1002,223,2,223,1005,224,434,101,1,223,223,107,226,226,224,102,2,223,223,1005,224,449,101,1,223,223,107,677,226,224,1002,223,2,223,1006,224,464,1001,223,1,223,8,226,677,224,102,2,223,223,1006,224,479,1001,223,1,223,108,677,226,224,102,2,223,223,1005,224,494,1001,223,1,223,1108,677,226,224,1002,223,2,223,1005,224,509,1001,223,1,223,1107,677,226,224,1002,223,2,223,1005,224,524,101,1,223,223,1008,226,226,224,1002,223,2,223,1006,224,539,101,1,223,223,1008,226,677,224,1002,223,2,223,1005,224,554,1001,223,1,223,7,226,677,224,1002,223,2,223,1005,224,569,101,1,223,223,1007,677,226,224,1002,223,2,223,1006,224,584,1001,223,1,223,7,677,226,224,102,2,223,223,1006,224,599,101,1,223,223,1007,226,226,224,102,2,223,223,1006,224,614,101,1,223,223,1008,677,677,224,1002,223,2,223,1006,224,629,101,1,223,223,108,226,226,224,102,2,223,223,1006,224,644,101,1,223,223,1108,226,677,224,1002,223,2,223,1005,224,659,101,1,223,223,8,226,226,224,1002,223,2,223,1005,224,674,101,1,223,223,4,223,99,226};
        // int[] memory = new int[] {3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99};
        Intcode(memory);
    }
}
