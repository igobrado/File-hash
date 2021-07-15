using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace FileHashBackend
{
    public class Finder : IFinder
    {
        public Tuple<FindStatus, List<string>> Find(List<string> foldersToSearch, string checksum, Hasher hasher)
        {
            var resultTuple = new Tuple<FindStatus, List<string>>(FindStatus.FilesFound, new List<string>());

            if (foldersToSearch.Count == 0 || checksum.Length == 0)
            {
                return new Tuple<FindStatus, List<string>>(FindStatus.InvalidArguments, new List<string>());
            }

            var fileList = new List<string>();

            foreach (var folderPath in foldersToSearch)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
                if (!directoryInfo.Exists)
                {
                    continue;
                }

                foreach (var file in directoryInfo.GetFiles())
                {
                    fileList.Add(file.FullName);
                }
            }

            var combiner = new Combiner<string>(fileList);
            var allCombinationsOfTheStreams = combiner.GetAllCombinations();

            foreach (var combination in allCombinationsOfTheStreams)
            {
                var combinedChecksumStreamSizeTuple = hasher.GetHash(combination.ToList());

                if (combinedChecksumStreamSizeTuple.Item1.Equals(checksum))
                {
                    foreach (var file in combination.ToList())
                    {
                        resultTuple.Item2.Add(file);
                    }

                    return resultTuple;
                }
            }

            return new Tuple<FindStatus, List<string>>(FindStatus.FilesNotFound, new List<string>());
        }

        public List<string> GetAllFilesInDirectory(List<string> foldersToSearch)
        {
            var fileList = new List<string>();

            foreach (var folderPath in foldersToSearch)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
                if (!directoryInfo.Exists)
                {
                    continue;
                }

                foreach (var file in directoryInfo.GetFiles())
                {
                    fileList.Add(file.FullName);
                }
            }

            return fileList;
        }
    }
}
