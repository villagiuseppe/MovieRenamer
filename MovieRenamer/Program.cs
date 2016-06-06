using System;
using System.Configuration;
using System.IO;
using System.Linq;


namespace MovieRenamer
{
    class Program
    {
        private static string _language = "en";

        static void Main(string[] args)
        {
            _language = Convert.ToString(ConfigurationManager.AppSettings["language"]);
            while (string.IsNullOrWhiteSpace(_language))
            {
                Console.WriteLine("With which language to run search ? (two digit, es. en = english)");
                _language = Console.ReadLine();
            }

            var directory = new DirectoryInfo(ConfigurationManager.AppSettings["from"]);
            var files = Enumerable.Empty<FileInfo>();
            files = files.Concat(directory.GetFiles("*.mp4"));
            files = files.Concat(directory.GetFiles("*.m4v"));
                       
            foreach (var file in files)
            {
                var task = Renamer.HandleFile(file,_language);
                task.Wait();
            }
            Console.WriteLine("No More Files to rename!");
            Console.ReadLine();
        }
    }
}
