
namespace LCTMatrixComposition
{
    public class Matrix2D
    {
        public Matrix1D A { get; private set; }
        public Matrix1D B { get; private set; }
        public Matrix1D C { get; private set; }
        public Matrix1D D { get; private set; }
        
        public Matrix2D(Matrix2DType type, string symbol)
        {
            if(type == Matrix2DType.AffineTransformation)
            {
                A = new Matrix1D(MatrixType.FourParameter, symbol);
                B = new Matrix1D(MatrixType.Zero, string.Empty);
                C = new Matrix1D(MatrixType.Zero, string.Empty);
                D = A.Transpose().Inverse();
            }
            else if (type == Matrix2DType.ChirpMultiplication)
            {
                A = new Matrix1D(MatrixType.Identity, string.Empty);
                B = new Matrix1D(MatrixType.Zero, string.Empty);
                C = new Matrix1D(MatrixType.FourParameter, symbol);
                D = new Matrix1D(MatrixType.Identity, string.Empty);
            }
            else
            {
                A = new Matrix1D(MatrixType.Zero, string.Empty);
                B = new Matrix1D(MatrixType.Identity, string.Empty);
                C = B.Multiply(-1);
                D = new Matrix1D(MatrixType.Zero, string.Empty);
            }
        }
        public Matrix2D(Matrix1D a, Matrix1D b, Matrix1D c, Matrix1D d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }
    }

    public enum Matrix2DType
    {
        AffineTransformation,
        ChirpMultiplication,
        FourierTransform
    }

    public static class MatrixExtensions2D
    {
        public static Matrix2D Multiply(this Matrix2D m, Matrix2D n)
        {            
            return new Matrix2D(m.A.Multiply(n.A).Add(m.B.Multiply(n.C)), m.A.Multiply(n.B).Add(m.B.Multiply(n.D)), m.C.Multiply(n.A).Add(m.D.Multiply(n.C)), m.C.Multiply(n.B).Add(m.D.Multiply(n.D)));
        }

        //public static Matrix2D Multiply(this Matrix2D m, Matrix2D n)
        //{
        //    var a11 = m.A.A * n.A.A + m.A.B * n.A.C + m.B.A * n.C.A + m.B.B * n.C.C;
        //    var a12 = m.A.A * n.A.B + m.A.B * n.A.D + m.B.A * n.C.B + m.B.B * n.C.D;
        //    var a21 = m.A.C * n.A.A + m.A.D * n.A.C + m.B.C * n.C.A + m.B.D * n.C.C;
        //    var a22 = m.A.C * n.A.B + m.A.D * n.A.D + m.B.C * n.C.B + m.B.D * n.C.D;

        //    var b11 = m.A.A * n.B.A + m.A.B * n.B.C + m.B.A * n.D.A + m.B.B * n.D.C;
        //    var b12 = m.A.A * n.B.B + m.A.B * n.B.D + m.B.A * n.D.B + m.B.B * n.D.D;
        //    var b21 = m.A.C * n.B.A + m.A.B * n.B.C + m.B.A * n.D.A + m.B.B * n.D.C;
        //    return new Matrix2D(new Matrix1D(a11, a12, a21, a22), new Matrix1D(), new Matrix1D(), new Matrix1D());
        //}
    }
}
