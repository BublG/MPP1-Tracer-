using System.IO;

namespace Main.Output.Impl
{
    public class FileOutput : IOutput
    {
        public const string FilePath = "D:\\учеба\\СПП\\Tracer\\Main\\output.txt";

        public void Print(string result)
        {
            var stream = new StreamWriter(FilePath, true);
            stream.WriteLine(result);
            stream.Flush();
            stream.Close();
        }
    }
}