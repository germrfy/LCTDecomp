using Symbolism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCTMatrixComposition
{
    public class MatrixDecomposition
    {
        private List<string> MagnificationSymbols = new List<string> { "M1", "M2", "M3", "M4", "M5" };
        private List<string> ChirpSymbols = new List<string> { "C1", "C2", "C3", "C4", "C5" };

        public Matrix1D M1 { get; private set; }
        public Matrix1D M2 { get; private set; }
        public Matrix1D M3 { get; private set; }
        public Matrix1D M4 { get; private set; }
        public Matrix1D M5 { get; private set; }
        public Matrix1D Multiplication { get; private set; }
        public MathObject DeterminantMultiplication { get; private set; }
        public string Permutation { get; private set; }

        public MatrixDecomposition(string permutation)
        {
            Permutation = permutation;
            M1 = EstablishMatrixType(permutation[4]);
            M2 = EstablishMatrixType(permutation[3]);
            M3 = EstablishMatrixType(permutation[2]);
            M4 = EstablishMatrixType(permutation[1]);
            M5 = EstablishMatrixType(permutation[0]);
            MultiplicationOfMatrices();
            DeterminantMultiplication = Multiplication.Determinant();
        }

        private Matrix1D EstablishMatrixType(char type)
        {
            Matrix1D returnMatrix;
            if(type == 'M')
            {
                returnMatrix = new Matrix1D(MatrixType.Magnification, MagnificationSymbols.First());
                MagnificationSymbols.Remove(MagnificationSymbols.First());
            }
            else if (type == 'C')
            {
                returnMatrix = new Matrix1D(MatrixType.ChirpMultiplication, ChirpSymbols.First());
                ChirpSymbols.Remove(ChirpSymbols.First());
            }
            else
            {
                returnMatrix = new Matrix1D(MatrixType.FourierTransform, string.Empty);
            }
            return returnMatrix;
        }

        private void MultiplicationOfMatrices()
        {
            Multiplication = M5.Multiply(M4);
            Multiplication = Multiplication.Multiply(M3);
            Multiplication = Multiplication.Multiply(M2);
            Multiplication = Multiplication.Multiply(M1);
        }
    }
}
