using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCTMatrixComposition
{
    public class Matrix2D
    {
        public Matrix1D A { get; private set; }
        public Matrix1D B { get; private set; }
        public Matrix1D C { get; private set; }
        public Matrix1D D { get; private set; }


    }

    public enum Matrix2DType
    {
        AffineTransformation,
        ChirpMultiplication,
        FourierTransform
    }
}
