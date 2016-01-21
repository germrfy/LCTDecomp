using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCTMatrixComposition
{
    public class MatrixDecomposition2D
    {
        private List<string> MagnificationSymbols = new List<string> { "S1", "S2", "S3", "S4", "S5" };
        private List<string> ChirpSymbols = new List<string> { "C1", "C2", "C3", "C4", "C5" };
        public Matrix2D M1 { get; private set; }
        public Matrix2D M2 { get; private set; }
        public Matrix2D M3 { get; private set; }
        public Matrix2D M4 { get; private set; }
        public Matrix2D M5 { get; private set; }
        public Matrix2D Multiplication { get; private set; }
        public string Permutation { get; private set; }

        public MatrixDecomposition2D(string permutation)
        {
            Permutation = permutation;
            M1 = EstablishMatrixType(permutation[4]);
            M2 = EstablishMatrixType(permutation[3]);
            M3 = EstablishMatrixType(permutation[2]);
            M4 = EstablishMatrixType(permutation[1]);
            M5 = EstablishMatrixType(permutation[0]);
            MultiplicationOfMatrices();
        }

        private Matrix2D EstablishMatrixType(char type)
        {
            Matrix2D returnMatrix;
            if (type == 'M')
            {
                returnMatrix = new Matrix2D(Matrix2DType.AffineTransformation, MagnificationSymbols.First());
                MagnificationSymbols.Remove(MagnificationSymbols.First());
            }
            else if (type == 'C')
            {
                returnMatrix = new Matrix2D(Matrix2DType.ChirpMultiplication, ChirpSymbols.First());
                ChirpSymbols.Remove(ChirpSymbols.First());
            }
            else
            {
                returnMatrix = new Matrix2D(Matrix2DType.FourierTransform, string.Empty);
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
