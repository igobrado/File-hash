using System.Collections.Generic;
using System.Linq;
using Combinatorics.Collections;
using System.Threading.Tasks;
using System;

namespace FileHashBackend
{
    class Combiner<T>
    {
        public Combiner(List<T> listOfFilesToCombine)
        {
            _listOfFilesToCombine = listOfFilesToCombine;
        }

        /// <summary>
        /// Gets the all possible combinations of the list providen inside of constructor.
        /// </summary>
        /// <returns></returns>
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
            var combinationListSplitted = SplitList(listOfAllCombinations, 5);

            List<Task> tasks = new List<Task>();

            foreach (var combinationList in combinationListSplitted)
            {
                tasks.Add(Task.Run(() => CalculatePermutations(combinationList, ref listOfAllPermutations)));
            }

            Task.WaitAll(tasks.ToArray());

            return listOfAllPermutations;
        }

        /// <summary>
        /// Calculates all of the permutations of the parameter combinationList.
        /// 
        /// Context:
        ///         This method is providing a threadsafe access to the out param outputList.
        ///         And this method shall be executed in the multiple contexts.
        /// </summary>
        /// <param name="combinationList"></param>
        /// <param name="outputList"></param>
        void CalculatePermutations(List<List<T>> combinationList, ref List<List<T>> outputList)
        {
            foreach (var combination in combinationList)
            {
                Permutations<T> permutations = new Permutations<T>(combination);
                foreach (var permutation in permutations)
                {
                    lock(outputList)
                    {
                        outputList.Add(permutation.ToList());
                    }
                }
            }
        }

        /// <summary>
        /// Splits the list to the N equal pieces.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bigList"></param>
        /// <param name="nSize"></param>
        /// <returns></returns>
        public static IEnumerable<List<List<T>>> SplitList<T>(List<List<T>> bigList, int nSize = 3)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        private readonly List<T> _listOfFilesToCombine;
    }
}
