using System;


class GravityAssist
{
    private static int TERMINATE_OPCODE = 99;
    private static int ADD_OPCODE = 1;
    private static int MULTIPLY_OPCODE = 2;
    private static int INSTRUCTION_SET = 4;

    public static int Intcode(int[] memory, int[] inputs)
    {
        int instructionPointer = 0;
        int[] instructions = new int[memory.Length];
        Array.Copy(memory, instructions, memory.Length);
        instructions[1] = inputs[0];
        instructions[2] = inputs[1];

        while (instructions[instructionPointer] != TERMINATE_OPCODE)
        {
            if (instructions[instructionPointer] == ADD_OPCODE)
                instructions[instructions[instructionPointer + 3]] = instructions[instructions[instructionPointer + 1]] + instructions[instructions[instructionPointer + 2]];
            else if (instructions[instructionPointer] == MULTIPLY_OPCODE)
                instructions[instructions[instructionPointer + 3]] = instructions[instructions[instructionPointer + 1]] * instructions[instructions[instructionPointer + 2]];
            else if (instructions[instructionPointer] != 99)
                return 0;

            instructionPointer += INSTRUCTION_SET;
        }
        return instructions[0];
    }

    public static int findInputsForOutput(int[] memory, int requiredOutput)
    {
        int[] inputs = new int[2];

        Func<int> loop = delegate
        {
            for (int noun = 0; noun < 99; noun++)
            {
                inputs[0] = noun;

                for (int verb = 0; verb < 99; verb++)
                {
                    inputs[1] = verb;
                    int output = Intcode(memory, inputs);

                    if (output == requiredOutput)
                        return 100 * noun + verb;
                }
            }
            return 0;
        };

        return loop();
    }

    static void Main()
    {
        int[] memory = new int[] {1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,13,1,19,1,6,19,23,2,6,23,27,1,5,27,31,2,31,9,35,1,35,5,39,1,39,5,43,1,43,10,47,2,6,47,51,1,51,5,55,2,55,6,59,1,5,59,63,2,63,6,67,1,5,67,71,1,71,6,75,2,75,10,79,1,79,5,83,2,83,6,87,1,87,5,91,2,9,91,95,1,95,6,99,2,9,99,103,2,9,103,107,1,5,107,111,1,111,5,115,1,115,13,119,1,13,119,123,2,6,123,127,1,5,127,131,1,9,131,135,1,135,9,139,2,139,6,143,1,143,5,147,2,147,6,151,1,5,151,155,2,6,155,159,1,159,2,163,1,9,163,0,99,2,0,14,0};
        int requiredOutput = 19690720;

        Console.WriteLine(findInputsForOutput(memory, requiredOutput));
    }
}
