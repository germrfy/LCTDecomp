using Symbolism;
using Symbolism.Substitute;
using System;
using System.Collections.Generic;
using System.IO;
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
            var decompositions = new List<MatrixDecomposition>();
            foreach (var p in permutations)
            {
                decompositions.Add(new MatrixDecomposition(p));
            }

            PrintPermutations(permutations);
            OutputToFile(decompositions);
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

        private static void OutputToFile(List<MatrixDecomposition> decomp)
        {
            string[] output = new string[6*decomp.Count()];
            var i = 0;
            foreach(var m in decomp)
            {
                output[i] = "\n [" +(decomp.IndexOf(m) + 1).ToString()+ "]";
                output[i + 1] = "****** '" + m.Permutation + "' ******";
                output[i + 2] = "A = " + m.Multiplication.A.ToString() + "\t\t\t\t B = " + m.Multiplication.B.ToString();
                output[i + 3] = "C = " + m.Multiplication.C.ToString() + "\t\t\t\t D = " + m.Multiplication.D.ToString();
                output[i + 4] = "Det : " + m.Multiplication.Determinant().ToString();
                output[i + 5] = "**********************************************************************";
                i += 6;
            }

            using (StreamWriter file = new StreamWriter(@"C:\Users\Ger\Desktop\Output.txt"))
            {
                foreach (string ln in output)
                {
                    file.WriteLine(ln);
                }
            }
        }
    }
}
