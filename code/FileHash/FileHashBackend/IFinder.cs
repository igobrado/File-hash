﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHashBackend
{
    public enum FindStatus
    {
        FilesFound,
        FilesNotFound,
        InvalidArguments
    }

    public interface IFinder
    {
        /// <summary>
        /// Finds the files that are used to combine the given checksum.
        /// 
        /// Note:
        ///     Method does not ignore the file ordering.
        /// </summary>
        /// <param name="foldersToSearch"></param>
        /// <param name="checksum"></param>
        /// <param name="hasher"></param>
        /// <returns></returns>
        Tuple<FindStatus, List<string>> Find(List<string> foldersToSearch, string checksum, Hasher hasher);

        /// <summary>
        /// Methods is getting all the files in the directory.
        /// </summary>
        /// <param name="foldersToSearch"></param>
        /// <returns></returns>
        List<string> GetAllFilesInDirectory(List<string> foldersToSearch);
    }
}
