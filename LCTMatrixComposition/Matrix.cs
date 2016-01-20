using Symbolism;
using Symbolism.Substitute;

namespace LCTMatrixComposition
{
    public class Matrix1D
    {
        public MathObject A { get; set; }
        public MathObject B { get; set; }
        public MathObject C { get; set; }
        public MathObject D { get; set; }

        public Matrix1D(MatrixType type, string symbol)
        {
            var sym = new Symbol(symbol);
            if (type == MatrixType.Magnification)
            {
                A = A.Substitute(A, sym);
                B = B.Substitute(B, 0);
                C = C.Substitute(C, 0);
                D = D.Substitute(D, 1 / sym);    
            }
            else if (type == MatrixType.FourParameter)
            {
                A = A.Substitute(A, new Symbol(symbol + "1"));
                B = B.Substitute(B, new Symbol(symbol + "2"));
                C = C.Substitute(C, new Symbol(symbol + "3"));
                D = D.Substitute(D, new Symbol(symbol + "4"));
            }
            else if (type == MatrixType.ChirpMultiplication)
            {
                A = A.Substitute(A, 1);
                B = B.Substitute(B, 0);
                C = C.Substitute(C, sym);
                D = D.Substitute(D, 1);
            }
            else if (type == MatrixType.Identity)
            {
                A = A.Substitute(A, 1);
                B = B.Substitute(B, 0);
                C = C.Substitute(C, 0);
                D = D.Substitute(D, 1);
            }
            else if (type == MatrixType.Zero)
            {
                A = A.Substitute(A, 0);
                B = B.Substitute(B, 0);
                C = C.Substitute(C, 0);
                D = D.Substitute(D, 0);
            }
            else
            {
                A = A.Substitute(A, 0);
                B = B.Substitute(B, 1);
                C = C.Substitute(C, -1);
                D = D.Substitute(D, 0);
            }
        }

        public Matrix1D(MathObject a, MathObject b, MathObject c, MathObject d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }
    }

    public static class MatrixExtensions
    {
        public static Matrix1D Multiply(this Matrix1D m, Matrix1D matrix)
        {
            return new Matrix1D(m.A * matrix.A + m.B * matrix.C, m.A * matrix.B + m.B * matrix.D, m.C * matrix.A + m.D * matrix.C, m.C * matrix.B + m.D * matrix.D);
        }
        public static Matrix1D Multiply(this Matrix1D m, int constant)
        {
            return new Matrix1D(m.A*constant, m.B*constant, m.C*constant, m.D*constant);
        }
        public static Matrix1D Transpose(this Matrix1D m)
        {
            return new Matrix1D(m.A, m.C, m.B, m.D);
        }
        public static MathObject Determinant(this Matrix1D m)
        {
            return m.A * m.D - m.B * m.C;
        }
        public static Matrix1D Inverse(this Matrix1D m)
        {
            return new Matrix1D(m.D / Determinant(m), -m.B / Determinant(m), -m.C / Determinant(m), m.A / Determinant(m));
        }
        public static Matrix1D Add(this Matrix1D m, Matrix1D matrix)
        {
            return new Matrix1D(m.A + matrix.A, m.B + matrix.B, m.C + matrix.C, m.D + matrix.D);
        }
    }

    public enum MatrixType
    {
        Magnification,
        FourierTransform,
        ChirpMultiplication,
        Identity,
        Zero,
        FourParameter
    }
}

