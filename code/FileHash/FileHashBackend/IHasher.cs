using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHashBackend
{
    public enum HasherType
    {
        SHA256,
        MD5,
        SHA1,
        CRC32,
        CRC64,
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
    public interface IHasher :  IDisposable
    {
        /// <summary>
        /// Gets the hash of the providen files.
        /// </summary>
        /// <param name="files"></param>
        ///
        /// <returns> Tuple which contains pair - <HASH, Size of files combined in MB> </returns>
        public Tuple<string, float> GetHash(List<string> files);

        /// <summary>
        /// Registers event handler for user update
        /// </summary>
        /// <param name="eventHandler">Event handler</param>
        public void RegisterEventHandler(EventHandler<IncreasedPercentage> eventHandler);

    }
}
