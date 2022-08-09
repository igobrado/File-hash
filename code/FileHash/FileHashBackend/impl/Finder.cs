using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileHashBackend.Impl
{
    internal class Finder : IFinder
    {
        private Hasher _hasher;
        Finder() : this(null)
        {
        }

        public Finder(Hasher hasher)
        {
            _hasher = hasher;
        }
        public EventHandler<IncreasedPercentage> FindProgress;
        
        public FindResult Find(List<string> foldersToSearch, string checksum)
        {
            if (foldersToSearch.Count == 0)
            {
                return new FindResult { findStatus = FindStatus.InvalidArguments, files = null, filesSize = 0 };
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

            return FindInCombination(fileList, checksum);
        }
        public void RegisterEventHandler(EventHandler<IncreasedPercentage> eventHandler)
        {
            FindProgress += eventHandler;
        }

        /// <summary>
        /// Finds list of files that are matching certain checksum
        /// </summary>
        /// <param name="listOfFilesToFind"></param>
        /// <param name="checksum"></param>
        /// <param name="hasher"></param>
        /// <returns></returns>
        protected FindResult FindInCombination(List<string> fileList, string checksum)
        {
            var listOfAllCombinations = GetCombinations(fileList);

            ulong combinationCount = (ulong)listOfAllCombinations.Count;
            float progress = 0;
            foreach (var combinaton in listOfAllCombinations)
            {
                Permutations<string> permutations = new Permutations<string>(combinaton);
                foreach (var permutation in permutations)
                {
                    var hash = _hasher.GetHash(permutation.ToList());

                    if (checksum == hash.Item1)
                    {
                        var result = new FindResult { findStatus = FindStatus.FilesFound, files = permutation.ToList(), filesSize = hash.Item2 };
                        OnUserUpdate(new IncreasedPercentage(100));
                        return result;
                    }
                    else
                    {
                        ++progress;
                        float completedPercentage = (progress / combinationCount) * 100;
                        OnUserUpdate(new IncreasedPercentage(completedPercentage));
                    }
                }
            }

            OnUserUpdate(new IncreasedPercentage(100));
            return new FindResult { findStatus = FindStatus.FilesNotFound, files = null, filesSize = 0 };

        }

        /// <summary>
        /// Calculates all of the combinations of the given files in constructor.
        /// </summary>
        /// <returns></returns>
        protected List<List<string>> GetCombinations(List<string> listOfFilesToCombine)
        {
            var listOfAllCombinations = new List<List<string>>();
            for (int i = 1; i <= listOfFilesToCombine.Count; ++i)
            {
                Combinations<string> c = new Combinations<string>(listOfFilesToCombine, i);
                foreach (var item in c)
                {
                    listOfAllCombinations.Add(item.ToList());
                }
            }

            return listOfAllCombinations;
        }
        protected virtual void OnUserUpdate(IncreasedPercentage percentage)
        {
            EventHandler<IncreasedPercentage> raiseEvent = FindProgress;
            if (raiseEvent != null)
            {
                raiseEvent(this, percentage);
            }
        }
    }
}
