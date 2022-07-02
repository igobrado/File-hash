using System.Collections.Generic;

namespace FileHashBackend
{
    public enum FindStatus
    {
        FilesFound,
        FilesNotFound,
        InvalidArguments
    }

    public struct FindResult
    {
        public FindStatus findStatus;
        public List<string> files;
        public double filesSize;
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
        FindResult Find(List<string> foldersToSearch, string checksum, Hasher hasher);
    }
}
