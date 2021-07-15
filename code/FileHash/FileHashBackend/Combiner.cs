using System.Collections.Generic;
using System.Linq;
using Combinatorics.Collections;

namespace FileHashBackend
{
    class Combiner<T>
    {
        public Combiner(List<T> listOfFilesToCombine)
        {
            _listOfFilesToCombine = listOfFilesToCombine;
        }

        public List<List<T>> GetAllCombinations()
        {
            var listOfAllCombinations = new List<List<T>>();
            for (int i = 1; i <= _listOfFilesToCombine.Count; ++i)
            {
                Combinations<T> c = new Combinations<T>(_listOfFilesToCombine, i);
                foreach (var item in c)
                {
                    listOfAllCombinations.Add(item.ToList());
                }
            }

            var listOfAllPermutations = new List<List<T>>();

            foreach (var combination in listOfAllCombinations)
            {
                Permutations<T> permutations = new Permutations<T>(combination);
                foreach (var permutation in permutations)
                {
                    listOfAllPermutations.Add(permutation.ToList());
                }

            }

            return listOfAllPermutations;
        }

        private readonly List<T> _listOfFilesToCombine;
    }
}
