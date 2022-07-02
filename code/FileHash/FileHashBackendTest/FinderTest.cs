using NUnit.Framework;
using FileHashBackend;
using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

namespace FileHashBackendTest
{
    public class FinderTest
    {
        [SetUp]
        public void Setup()
        {
        }

        void CreateAndFill(string path, string content)
        {
            using (var file = File.Create(path))
            {
                byte[] text = new UTF8Encoding(true).GetBytes(content);
                file.Write(text, 0, text.Length);
            }
        }

        [Test]
        public void FindFilesWithEmptyFolderList()
        {
            Finder finder = new Finder();
            var result = finder.Find(new List<string>(), "1234", null);

            Assert.AreEqual(FindStatus.InvalidArguments, result.findStatus);
        }

        [Test]
        public void FindFilesWithNotExistingChecksum()
        {
            var systemPath = System.Environment.
                                         GetFolderPath(
                                             Environment.SpecialFolder.CommonApplicationData
                                         );


            for (int i = (int)HasherType.MD5; i < (int)HasherType.Invalid; ++i)
            {
                var folderToSearch = new List<string>();
                folderToSearch.Add(systemPath);

                var hasherType = (HasherType)i;
                using (Hasher hasher = new Hasher(hasherType))
                {
                    var finder = new Finder();
                    var findResult = finder.Find(folderToSearch, "myinvalidchecksum3129348124", hasher);

                    // find status must be ok
                    Assert.AreEqual(findResult.findStatus, FindStatus.FilesNotFound);

                    // file size must be same as hasher one
                    Assert.AreEqual(0, findResult.filesSize);

                    // file ordering must be the same as one used for calculating the hash
                    Assert.AreEqual(null, null);
                }
            }
        }

        [Test]
        public void FindFilesWithExistingChecksumAndCombination()
        {
            var files = new List<string>();
            var systemPath = System.Environment.
                                         GetFolderPath(
                                             Environment.SpecialFolder.CommonApplicationData
                                         );
            var fileOne = Path.Combine(systemPath, "file.txt");
            var fileTwo = Path.Combine(systemPath, "fileTwo.txt");
            files.Add(fileOne);
            files.Add(fileTwo);

            CreateAndFill(fileOne, "This is some text in the file");
            CreateAndFill(fileTwo, "This is some text in the file");

            for (int i = (int)HasherType.MD5; i < (int)HasherType.Invalid; ++i)
            {
                var hasherType = (HasherType)i;
                using (Hasher hasher = new Hasher(hasherType))
                {
                    System.Tuple<string, float> result = hasher.GetHash(files);
                    var foldersToSearch = new List<string>();
                    foldersToSearch.Add(systemPath);

                    var finder = new Finder();
                    var findResult = finder.Find(foldersToSearch, result.Item1, hasher);

                    // find status must be ok
                    Assert.AreEqual(findResult.findStatus, FindStatus.FilesFound);
                    
                    // file size must be same as hasher one
                    Assert.AreEqual(findResult.filesSize, result.Item2);

                    // file ordering must be the same as one used for calculating the hash
                    Assert.AreEqual(files, findResult.files);
                }
            }

            File.Delete(fileOne);
            File.Delete(fileTwo);
        }
    }
}