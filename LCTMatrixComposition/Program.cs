using Symbolism;
using Symbolism.Substitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCTMatrixComposition
{
    class Program
    {
        private static List<string> availableMatrices = new List<string> { "M","C","F" };
        static void Main(string[] args)
        {
            var spectralMethodDecomp = new MatrixDecomposition("CMFCF");
            var permutations = GenerateAllPermutations();
            permutations = RemoveRedundantPermutations(permutations);
            PrintPermutations(permutations);
        }

        private static IEnumerable<string> RemoveRedundantPermutations(IEnumerable<string> permutations)
        {
            permutations = permutations.Where(p => p.Contains("F"));    // Must contain at least one of F,M,C
            permutations = permutations.Where(p => p.Contains("M"));
            permutations = permutations.Where(p => p.Contains("C"));
            permutations = permutations.Where(p => !p.Contains("MM"));  // Cannot contain two 'M' in row
            permutations = permutations.Where(p => !p.Contains("FFF")); //Cannot contain 3 or more C or F in a row
            permutations = permutations.Where(p => !p.Contains("CCC"));
            return permutations;
        }

        private static IEnumerable<string> GenerateAllPermutations()
        {
            var permutations = availableMatrices.Select(x => x.ToString());
            int size = 5;
            for (int i = 0; i < size - 1; i++)
                permutations = permutations.SelectMany(x => availableMatrices, (x, y) => x + y);
            Console.WriteLine(permutations.Count());
            return permutations;
        }

        private static void PrintPermutations(IEnumerable<string> permutations)
        {
            foreach (var p in permutations)
                Console.WriteLine(p);
            Console.WriteLine(permutations.Count());
        }
    }
}
