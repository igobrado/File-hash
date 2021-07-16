using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;

namespace FileHashBackend
{
    public enum HasherType
    {
        SHA256,
        MD5,
        SHA1,
        Invalid
    }

    /// <summary>
    /// Provides an abstraction around hashing functions.
    /// </summary>
    public class Hasher : IDisposable
    {
        public Hasher(HasherType wantedHasherType)
        {
            switch (wantedHasherType)
            {
                case HasherType.SHA256:
                    _hashAlgorithm = SHA256.Create();
                    break;
                case HasherType.MD5:
                    _hashAlgorithm = MD5.Create();
                    break;
                case HasherType.SHA1:
                    _hashAlgorithm = SHA1.Create();
                    break;
                default:
                    Debug.Assert(false, "Invalid hasher type received");
                    break;
            }
        }

        public string GetHash(List<System.IO.Stream> streams)
        {
            return BitConverter.ToString(_hashAlgorithm.ComputeHash(new CombinationStream.CombinationStream(streams))).Replace("-", "");
        }

        /// <summary>
        /// Gets the hash of the providen files.
        /// </summary>
        /// <param name="files"></param>
        /// 
        /// <returns> Tuple which contains pair - <HASH, Size of files combined in MB> </returns>
        public Tuple<string, float> GetHash(List<string> files)
        {
            if (files.Count == 0)
            {
                return new Tuple<string, float>("", 0);
            }

            long streamSize = 0;
            var streams = new List<System.IO.Stream>();
            foreach (var item in files)
            {
                var file = System.IO.File.OpenRead(item);
                streamSize += file.Length;
                streams.Add(file);
            }

            return new Tuple<string, float>(GetHash(streams), (streamSize / 1048576F));
        }

        public void Dispose()
        {
            _hashAlgorithm.Dispose();
        }

        private HashAlgorithm _hashAlgorithm;
    }
}
 