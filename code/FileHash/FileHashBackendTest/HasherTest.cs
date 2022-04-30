using NUnit.Framework;
using FileHashBackend;
using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

namespace FileHashBackendTest
{
    public class HasherTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetHashOnEmptyFileReturnsEmptyHash()
        {
            for (int i = (int)HasherType.MD5; i < (int)HasherType.Invalid; ++i)
            {
                var hasherType = (HasherType)i;

                Hasher hasher = new Hasher(hasherType);
                System.Tuple<string, float> result = hasher.GetHash(new List<string>());

                Assert.AreEqual(result.Item1, "");
                Assert.AreEqual(result.Item2, 0);
            }
        }

        [Test]
        public void GetHashOnExistingFileReturnsHashThatIsNotNull()
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

            using(var file = File.Create(fileOne))
            {
                byte[] text = new UTF8Encoding(true).GetBytes("This is some text in the file.");
                file.Write(text, 0, text.Length);
            }

            using (var file = File.Create(fileTwo))
            {
                byte[] text = new UTF8Encoding(true).GetBytes("This is some text in the file.");
                file.Write(text, 0, text.Length);
            }
            
            for (int i = (int)HasherType.MD5; i < (int)HasherType.Invalid; ++i)
            {
                var hasherType = (HasherType)i;
                using (Hasher hasher = new Hasher(hasherType))
                {
                    System.Tuple<string, float> result = hasher.GetHash(files);

                    Assert.IsNotEmpty(result.Item1);
                    Assert.AreNotEqual(result.Item2, 0);
                }
            }

            File.Delete(fileOne);
            File.Delete(fileTwo);
        }

        [Test]
        public void GetHashOfTwoFilesCombinedToOneIsTheSame()
        {
            var files = new List<string>();
            var filesTwo = new List<string>();
            var systemPath = System.Environment.
                                         GetFolderPath(
                                             Environment.SpecialFolder.CommonApplicationData
                                         );
            var fileOne = Path.Combine(systemPath, "file.txt");
            var fileTwo = Path.Combine(systemPath, "fileTwo.txt");
            var fileThree = Path.Combine(systemPath, "fileThree.txt"); // represents combination of 2 above
            
            files.Add(fileOne);
            files.Add(fileTwo);
            filesTwo.Add(fileThree);

            using (var file = File.Create(fileOne))
            {
                byte[] text = new UTF8Encoding(true).GetBytes("1");
                file.Write(text, 0, text.Length);
            }

            using (var file = File.Create(fileTwo))
            {
                byte[] text = new UTF8Encoding(true).GetBytes("2");
                file.Write(text, 0, text.Length);
            }

            using (var file = File.Create(fileThree))
            {
                byte[] text = new UTF8Encoding(true).GetBytes("12");
                file.Write(text, 0, text.Length);
            }

            for (int i = (int)HasherType.MD5; i < (int)HasherType.Invalid; ++i)
            {
                var hasherType = (HasherType)i;
                using (Hasher hasher = new Hasher(hasherType))
                {
                    System.Tuple<string, float> result = hasher.GetHash(files);
                    System.Tuple<string, float> resultOfCombined = hasher.GetHash(filesTwo);

                    Assert.IsNotEmpty(result.Item1);
                    Assert.AreNotEqual(result.Item2, 0);

                    Assert.IsNotEmpty(resultOfCombined.Item1);
                    Assert.AreNotEqual(resultOfCombined.Item2, 0);

                    Assert.AreEqual(result.Item1, resultOfCombined.Item1);
                    Assert.AreEqual(result.Item2, resultOfCombined.Item2);
                }
            }
        }
    }
}