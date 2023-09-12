using CheckerDiff.Models;
using CheckerDiff.Repository;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace CheckerDiff.Services
{
    internal class CheckerDiffService
    {
        private readonly FileRepository _repository = new FileRepository();



        internal void Diff(string root)
        {
            Console.WriteLine("파일을 수집합니다.");
            var fileTree = _repository.GetFileTree(root);

            List<string> filePaths = new List<string>();
            Dictionary<string, CompareCheckerInfo> checkerInfos = new Dictionary<string, CompareCheckerInfo>();
            foreach (var entry in fileTree)
            {
                var directory = entry.Key;
                var files = entry.Value;

                

                Console.WriteLine("비교를 위해 파일을 교정합니다.");
                files.ForEach(file =>
                {
                    var filePath = Path.Combine(directory, file);
                    this.Replace(filePath);
                });

                var parentDirectoryName = Path.GetFileName(Path.GetDirectoryName(directory));
                var directoryName = Path.GetFileName(directory);

            }

            //Console.WriteLine("분석 중입니다.");
            //using (var reader = new StreamReader($@"C:\Users\Alice\Downloads\Windows\jdk17\ASP.NET\sourcepath\202308041309_checker.csv"))
            //{
            //    using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            //    {
            //        var list = csvReader.GetRecords<CheckerInfo>();
            //        var l = list.ToList();
            //        int i = 0;
            //    }
            //}

        }

        private void Replace(List<string> filePaths)
        {

            foreach (var filePath in filePaths)
            {
                this.Replace(filePath);
            }
        }

        private void Replace(string filePath)
        {
            this.Replace(filePath, $@",,C:\", $@",C:\");

            var oldHeader = "Canon,File,Func,StartLine,StartCol,EndLine,EndCol,->,File,Func,StartLine,StartCol,EndLine,EndCol,Debug";
            var newHeader = "Canon,BeforeFile,BeforeFunc,BeforeStartLine,BeforeStartCol,BeforeEndLine,BeforeEndCol,->,AfterFile,AfterFunc,AfterStartLine,AfterStartCol,AfterEndLine,AfterEndCol,Debug";
            this.Replace(filePath, oldHeader, newHeader);
        }

        private void Replace(string filePath, string oldValue, string newValue)
        {
            string text = File.ReadAllText(filePath, Encoding.UTF8);
            text = text.Replace(oldValue, newValue);
            File.WriteAllText(filePath, text);
        }
    }
}
