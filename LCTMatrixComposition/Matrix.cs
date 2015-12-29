using Symbolism;
using Symbolism.Substitute;

namespace LCTMatrixComposition
{
    public class Matrix
    {
        public MathObject A { get; set; }
        public MathObject B { get; set; }
        public MathObject C { get; set; }
        public MathObject D { get; set; }

        public Matrix(MatrixType type, string symbol)
        {
            var sym = new Symbol(symbol);
            if (type == MatrixType.Magnification)
            {
                A = A.Substitute(A, sym);
                B = B.Substitute(B, 0);
                C = C.Substitute(C, 0);
                D = D.Substitute(D, 1 / sym);    
            }
            else if (type == MatrixType.ChirpMultiplication)
            {
                A = A.Substitute(A, 1);
                B = B.Substitute(B, 0);
                C = C.Substitute(C, sym);
                D = D.Substitute(D, 1);
            }
            else
            {
                A = A.Substitute(A, 0);
                B = B.Substitute(B, 1);
                C = C.Substitute(C, -1);
                D = D.Substitute(D, 0);
            }
        }

        public Matrix(MathObject a, MathObject b, MathObject c, MathObject d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }
    }

    public static class MatrixExtensions
    {
        public static Matrix Multiply(this Matrix m, Matrix matrix)
        {
            return new Matrix(m.A * matrix.A + m.B * matrix.C, m.A * matrix.B + m.B * matrix.D, m.C * matrix.A + m.D * matrix.C, m.C * matrix.B + m.D * matrix.D);
        }
        public static MathObject Determinant(this Matrix m)
        {
            return m.A * m.D - m.B * m.C;
        }
    }

    public enum MatrixType
    {
        Magnification,
        FourierTransform,
        ChirpMultiplication
    }
}

