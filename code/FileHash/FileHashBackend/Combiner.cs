using System.Collections.Generic;
using System.Linq;
using Combinatorics.Collections;
using System.Threading.Tasks;
using System;
using System.IO;

namespace FileHashBackend
{
    class Combiner
    {
        public Combiner(List<string> listOfFilesToCombine)
        {
            _listOfFilesToCombine = listOfFilesToCombine;
        }

        /// <summary>
        /// Finds a files which were used to combine checksum.
        /// </summary>
        /// <param name="checksum"></param>
        /// <param name="hasher"></param>
        /// <returns></returns>
        public FindResult FindInCombinations(string checksum, Hasher hasher)
        {
            var listOfAllCombinations = GetCombinations();

            foreach (var combinaton in listOfAllCombinations)
            {
                Permutations<string> permutations = new Permutations<string>(combinaton);
                foreach (var permutation in permutations)
                {
                    var hash = hasher.GetHash(permutation.ToList());

                    if (hash.Item1 == checksum)
                    {
                        return new FindResult { findStatus = FindStatus.FilesFound, files = permutation.ToList(), filesSize = hash.Item2};
                    }
                }
            }

            return new FindResult { findStatus = FindStatus.FilesNotFound, files = null, filesSize = 0};
        }

        /// <summary>
        /// Calculates all of the combinations of the given files in constructor.
        /// </summary>
        /// <returns></returns>
        List<List<string>> GetCombinations()
        {
            var listOfAllCombinations = new List<List<string>>();
            for (int i = 1; i <= _listOfFilesToCombine.Count; ++i)
            {
                Combinations<string> c = new Combinations<string>(_listOfFilesToCombine, i);
                foreach (var item in c)
                {
                    listOfAllCombinations.Add(item.ToList());
                }
            }

            return listOfAllCombinations;
        }

        private readonly List<string> _listOfFilesToCombine;
    }
}
