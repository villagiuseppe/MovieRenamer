using System;
using System.Configuration;
using System.IO;
using System.Linq;


namespace MovieRenamer
{
    class Program
    {
        
        static void Main(string[] args)
        {

            #region Check Paramameters            
            string language = Convert.ToString(ConfigurationManager.AppSettings["language"]);
            while (string.IsNullOrWhiteSpace(language))
            {
                Console.WriteLine("With which language to run search ? (two digit, es. en = english)");
                language = Console.ReadLine();
            }

            string inputPath = ConfigurationManager.AppSettings["from"];
            while (string.IsNullOrWhiteSpace(inputPath) || ! Directory.Exists(inputPath))
            {
                Console.WriteLine("Insert a valid input directory");
                inputPath = Console.ReadLine();
            }
            ConfigurationManager.AppSettings.Set("from", inputPath);

            string outputPath = ConfigurationManager.AppSettings["to"];
            while (string.IsNullOrWhiteSpace(outputPath) || !Directory.Exists(outputPath))
            {
                Console.WriteLine("Insert a valid output directory ");
                Console.WriteLine("(enter empty to set equal to \"input directory\")");
                
                outputPath = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(outputPath))
                    outputPath = inputPath;
            }
            ConfigurationManager.AppSettings.Set("to", outputPath);
            #endregion

            var directory = new DirectoryInfo(inputPath);
            
            var files = Enumerable.Empty<FileInfo>();
            files = files.Concat(directory.GetFiles("*.mp4"));
            files = files.Concat(directory.GetFiles("*.m4v"));
                       
            foreach (var file in files)
            {
                var task = Renamer.HandleFile(file, language);
                task.Wait();
            }
            Console.WriteLine("No More Files to rename!");
            Console.ReadLine();
        }
    }
}
