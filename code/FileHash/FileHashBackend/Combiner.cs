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
