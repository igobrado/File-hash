using System;
using System.Collections.Generic;
using System.IO;

namespace FileHashBackend
{
    public class Finder : IFinder
    {
        public EventHandler<IncreasedPercentage> FindProgress;

        public FindResult Find(List<string> foldersToSearch, string checksum, Hasher hasher)
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


            var combiner = new Combiner(fileList);
            combiner.FindProgress += new EventHandler<IncreasedPercentage>(OnUserUpdate);

            return combiner.FindInCombinations(checksum, hasher);
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

        protected virtual void OnUserUpdate(object sender, IncreasedPercentage percentage)
        {
            EventHandler<IncreasedPercentage> raiseEvent = FindProgress;
            if (raiseEvent != null)
            {
                raiseEvent(this, percentage);
            }
        }
    }
}
