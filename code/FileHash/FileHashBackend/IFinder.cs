using System;
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
        Tuple<FindStatus, List<string>> Find(List<string> foldersToSearch, string checksum, Hasher hasher);
        List<string> GetAllFilesInDirectory(List<string> foldersToSearch);
    }
}
