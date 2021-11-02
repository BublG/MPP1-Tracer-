using System;

namespace Main.Output.Impl
{
    public class ConsoleOutput : IOutput
    {
        public void Print(string result)
        {
            Console.WriteLine(result);
        }
    }
}