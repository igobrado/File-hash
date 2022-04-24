using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Buffers;
using System.IO;

namespace FileHashBackend
{
    public enum HasherType
    {
        SHA256,
        MD5,
        SHA1,
        CRC32,
        Invalid
    }
    /// <summary>
    /// Custom event arguments which provides a progress of the current operation.
    /// </summary>
    public class IncreasedPercentage : EventArgs
    {
        public IncreasedPercentage(float percentage)
        {
            Percentage = percentage;
        }
        public float Percentage { get; set; }
    }

    /// <summary>
    /// Provides an abstraction around hashing functions.
    /// </summary>
    public class Hasher : IDisposable
    {
        public EventHandler<IncreasedPercentage> Handler;
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

        public string GetHash(List<System.IO.Stream> streams, ulong streamSize)
        {
            var block = ArrayPool<byte>.Shared.Rent(0);

            foreach (var stream in streams)
            {
                int length;
                while ((length = stream.Read(block, 0, block.Length)) > 0)
                {
                    _hashAlgorithm.TransformBlock(block, 0, length, null, 0);

                    float progress = length;
                    float completedPercentage = (progress / streamSize) * 100;
                    Handler?.Invoke(this, new IncreasedPercentage(completedPercentage));
                }
            }
            _hashAlgorithm.TransformFinalBlock(block, 0, 0);

            return BitConverter.ToString(_hashAlgorithm.Hash).Replace("-", "");
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

            return new Tuple<string, float>(GetHash(streams, streamSize), (streamSize / 1048576F));
        }

        public void Dispose()
        {
            _hashAlgorithm.Dispose();
        }

        private HashAlgorithm _hashAlgorithm;
    }
}
 