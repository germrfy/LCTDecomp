using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LCTMatrixComposition
{
    class Program2D
    {
        private static List<string> availableMatrices = new List<string> { "M", "C", "F" };
        static void Main(string[] args)
        {
            var permutations = GenerateAllPermutations();
            permutations = RemoveRedundantPermutations(permutations);
            var decompositions = new List<MatrixDecomposition2D>();
            foreach (var p in permutations)
            {
                decompositions.Add(new MatrixDecomposition2D(p));
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
            permutations = permutations.Where(p => p.Count(c => c == 'M') == 1);  
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

        private static void OutputToFile(List<MatrixDecomposition2D> decomp)
        {
            string[] output = new string[15 * decomp.Count()];
            var i = 0;
            foreach (var m in decomp)
            {
                output[i] = "\n [" + (decomp.IndexOf(m) + 1).ToString() + "]";
                output[i + 1] = "****** '" + m.Permutation + "' ******";
                output[i + 2] = "**********************************************************************";
                output[i + 3] = "A11 = " + m.Multiplication.A.A.ToString() + "\t\t\t\t A12 = " + m.Multiplication.A.B.ToString();
                output[i + 4] = "A21 = " + m.Multiplication.A.C.ToString() + "\t\t\t\t A22 = " + m.Multiplication.A.D.ToString();
                output[i + 5] = "**********************************************************************";
                output[i + 6] = "B11 = " + m.Multiplication.B.A.ToString() + "\t\t\t\t B12 = " + m.Multiplication.B.B.ToString();
                output[i + 7] = "B21 = " + m.Multiplication.B.C.ToString() + "\t\t\t\t B22 = " + m.Multiplication.B.D.ToString();
                output[i + 8] = "**********************************************************************";
                output[i + 9] = "C11 = " + m.Multiplication.C.A.ToString() + "\t\t\t\t C12 = " + m.Multiplication.C.B.ToString();
                output[i + 10] = "C21 = " + m.Multiplication.C.C.ToString() + "\t\t\t\t C22 = " + m.Multiplication.C.D.ToString();
                output[i + 11] = "**********************************************************************";
                output[i + 12] = "D11 = " + m.Multiplication.D.A.ToString() + "\t\t\t\t D12 = " + m.Multiplication.D.B.ToString();
                output[i + 13] = "D21 = " + m.Multiplication.D.C.ToString() + "\t\t\t\t D22 = " + m.Multiplication.D.D.ToString();
                output[i + 14] = "**********************************************************************";
                i += 15;
            }

            using (StreamWriter file = new StreamWriter(@"C:\Users\Ger\Desktop\Output2DNSLCT.txt"))
            {
                foreach (string ln in output)
                {
                    file.WriteLine(ln);
                }
            }
        }

        private static void TestFunction()
        {
            var x = new Matrix2D(new Matrix1D(4, 5, 0, 3), new Matrix1D(1, 8, 6, 1), new Matrix1D(3, 5, 2, 4), new Matrix1D(0, 9, 6, 1));
            var y = new Matrix2D(new Matrix1D(1, 5, 0, 3), new Matrix1D(1, 0, 6, 1), new Matrix1D(3, 5, 2, 0), new Matrix1D(7, 2, 6, 1));
            var z = x.Multiply(y);
            var a = x.A.Transpose();
            var b = x.A.Inverse();
        }
    }
}
