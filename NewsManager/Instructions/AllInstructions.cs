using NewsManager.Instructions.Available;

namespace NewsManager.Instructions
{
    public static class AllInstructions
    {
        public static readonly SpecificInstruction[] List =
            new SpecificInstruction[]
        {
            new SpecificInstruction_Partisan(),
            new SpecificInstruction_Telegraf()
        };
    }

}
