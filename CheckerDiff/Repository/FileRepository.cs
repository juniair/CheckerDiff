using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckerDiff.Repository
{
    internal class FileRepository
    {

        public Dictionary<string, List<string>> GetFileTree(string root)
        {
            var attribute = File.GetAttributes(root);

            var fileTree = new Dictionary<string, List<string>>();
            
            if(attribute.HasFlag(FileAttributes.Directory))
            {
                var fileEntries = Directory.GetFiles(root, "*", SearchOption.AllDirectories);

                foreach (var fileEntry in fileEntries)
                {
                    var fileInfo = GetFileInfo(fileEntry);

                    var directory = fileInfo.Item1;
                    var file = fileInfo.Item2;
                    if(fileTree.ContainsKey(directory))
                    {
                        fileTree[directory].Add(file);
                    }
                    else
                    {
                        fileTree.Add(directory, new List<string>() { file });
                    }

                }

            }
            else
            {
                if(File.Exists(root))
                {
                    var fileInfo = GetFileInfo(root);
                    fileTree.Add(fileInfo.Item1, new List<string>() { fileInfo.Item2 });
                }
            }

            return fileTree;
        }

        private Tuple<string, string> GetFileInfo(string file)
        {
            var directory = Path.GetDirectoryName(file);
            var fileName = Path.GetFileName(file);

#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return Tuple.Create(directory, fileName);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public string GetFileFullPath(string directory, string fileName)
        {
            return Path.Combine(directory, fileName);
        }

    }
}
