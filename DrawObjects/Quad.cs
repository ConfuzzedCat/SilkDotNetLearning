using Silk.NET.Maths;
using SilkDotNetLearning.Utils;

namespace SilkDotNetLearning.DrawObjects
{
    public struct Quad
    {
        public Vector3D<float> TopLeft { get; private set; }
        public Vector3D<float> TopRight { get; private set; }
        public Vector3D<float> BottomLeft { get; private set; }
        public Vector3D<float> BottomRight { get; private set; }

        private float[] _verticesData = [];
        public float[] Vertices
        {
            get
            {
                if (_verticesData.Length != 0u)
                {
                    return _verticesData;
                }
                _verticesData = VertexDataUtils.Vec3ToVertexDataArray(ToArray());
                return _verticesData;
            }
        }

        public uint[] Indices = [
            0u,
            1u,
            3u,
            1u,
            2u,
            3u
            ];


        private Quad(
            Vector3D<float> topLeft,
            Vector3D<float> topRight,
            Vector3D<float> bottomLeft,
            Vector3D<float> bottomRight)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomLeft = bottomLeft;
            BottomRight = bottomRight;
        }

        public readonly Tri[] ToTris()
        {
            return [
                Tri.Create(TopLeft, TopRight, BottomLeft),
                Tri.Create(TopRight, BottomRight, BottomLeft)
            ];
        }

        public readonly Vector3D<float>[] ToArray()
        {
            return
            [
                TopLeft,
                TopRight,
                BottomLeft,
                BottomRight
            ];
        }

        public override readonly string ToString()
        {
            return $"TopLeft: {TopLeft}, " +
                   $"TopRight: {TopRight}, " +
                   $"BottomLeft: {BottomLeft}, " +
                   $"BottomRight: {BottomRight}";
        }
        #region Static methods
        #region Create methods
        public static Quad Create(
            Vector3D<float> topLeft,
            Vector3D<float> topRight,
            Vector3D<float> bottomLeft,
            Vector3D<float> bottomRight
            )
        {
            return new Quad(topLeft, topRight, bottomLeft, bottomRight);
        }

        public static Quad Create(
            Vector3D<float> topLeft,
            Vector3D<float> bottomRight,
            float depth
            )
        {
            Vector3D<float> topRight = new Vector3D<float>(bottomRight.X, topLeft.Y, depth);
            Vector3D<float> bottomLeft = new Vector3D<float>(topLeft.X, bottomRight.Y, depth);
            return Create(topLeft, topRight, bottomLeft, bottomRight);
        }

        public static Quad Create(
            Vector3D<float> topLeft,
            Vector3D<float> bottomRight
            )
        {
            float z = (topLeft.Z + bottomRight.Y) / 2;
            return Create(topLeft, bottomRight, z);
        }

        public static Quad Create(
            Vector3D<float> topLeft,
            float length,
            float height
            )
        {
            Vector3D<float> bottomRight = new Vector3D<float>(
                topLeft.X + length,
                topLeft.Y + height,
                topLeft.Z
                );
            return Create(topLeft, bottomRight);
        }

        public static Quad Create(
            Vector3D<float> topLeft,
            float size
            )
        {
            Vector3D<float> bottomRight = new Vector3D<float>(
                topLeft.X + size,
                topLeft.Y + size,
                topLeft.Z
                );
            return Create(topLeft, bottomRight);
        }

        public static Quad Create(
            Vector3D<float> topLeft
            )
        {
            return Create(topLeft, 1f);
        }

        public static Quad Create()
        {
            return Create(new Vector3D<float>(1f, 1f, 0f));
        }

        public static Quad Create(
            Vector2D<float> topLeft,
            Vector2D<float> topRight,
            Vector2D<float> bottomLeft,
            Vector2D<float> bottomRight
            )
        {
            Vector3D<float> topLeft3D = new Vector3D<float>(topLeft.X, topLeft.Y, 0f);
            Vector3D<float> topRight3D = new Vector3D<float>(topRight.X, topRight.Y, 0f);
            Vector3D<float> bottomLeft3D = new Vector3D<float>(bottomLeft.X, bottomLeft.Y, 0f);
            Vector3D<float> bottomRight3D = new Vector3D<float>(bottomRight.X, bottomRight.Y, 0f);
            return Create(topLeft3D, topRight3D, bottomLeft3D, bottomRight3D);
        }

        public static Quad Create(
            Vector2D<float> topLeft,
            Vector2D<float> bottomRight
            )
        {
            Vector2D<float> topRight = new Vector2D<float>(bottomRight.X, topLeft.Y);
            Vector2D<float> bottomLeft = new Vector2D<float>(topLeft.X, bottomRight.Y);
            return Create(topLeft,
                topRight,
                bottomLeft,
                bottomRight
                );
        }

        public static Quad Create(
            float x,
            float y,
            float length,
            float height
            )
        {
            Vector3D<float> topLeft = new Vector3D<float>(x, y, 0f);
            return Create(topLeft, length, height);
        }

        public static Quad Create(
            float x,
            float y,
            float size
            )
        {
            return Create(x, y, size, size);
        }
        #endregion
        #region Operators overloading
        public static Quad operator +(Quad quad, Vector3D<float> vector)
        {
            return Create(
                quad.TopLeft + vector,
                quad.TopRight + vector,
                quad.BottomLeft + vector,
                quad.BottomRight + vector);
        }

        public static Quad operator -(Quad quad, Vector3D<float> vector)
        {
            return Create(
                quad.TopLeft - vector,
                quad.TopRight - vector,
                quad.BottomLeft - vector,
                quad.BottomRight - vector);
        }

        public static Quad operator *(Quad quad, Vector3D<float> vector)
        {
            return Create(
                quad.TopLeft * vector,
                quad.TopRight * vector,
                quad.BottomLeft * vector,
                quad.BottomRight * vector);
        }

        public static Quad operator /(Quad quad, Vector3D<float> vector)
        {
            return Create(
                quad.TopLeft / vector,
                quad.TopRight / vector,
                quad.BottomLeft / vector,
                quad.BottomRight / vector);
        }

        public static Quad operator *(Quad quad, float value)
        {
            return Create(
                quad.TopLeft * value,
                quad.TopRight * value,
                quad.BottomLeft * value,
                quad.BottomRight * value);
        }
        public static Quad operator /(Quad quad, float value)
        {
            return Create(
                quad.TopLeft / value,
                quad.TopRight / value,
                quad.BottomLeft / value,
                quad.BottomRight / value);
        }

        public static Quad operator +(Quad quad1, Quad quad2)
        {
            return Create(
                quad1.TopLeft + quad2.TopLeft,
                quad1.TopRight + quad2.TopRight,
                quad1.BottomLeft + quad2.BottomLeft,
                quad1.BottomRight + quad2.BottomRight);
        }

        public static Quad operator -(Quad quad1, Quad quad2)
        {
            return Create(
                quad1.TopLeft - quad2.TopLeft,
                quad1.TopRight - quad2.TopRight,
                quad1.BottomLeft - quad2.BottomLeft,
                quad1.BottomRight - quad2.BottomRight);
        }

        public static Quad operator *(Quad quad1, Quad quad2)
        {
            return Create(
                quad1.TopLeft * quad2.TopLeft,
                quad1.TopRight * quad2.TopRight,
                quad1.BottomLeft * quad2.BottomLeft,
                quad1.BottomRight * quad2.BottomRight);
        }

        public static Quad operator /(Quad quad1, Quad quad2)
        {
            return Create(
                quad1.TopLeft / quad2.TopLeft,
                quad1.TopRight / quad2.TopRight,
                quad1.BottomLeft / quad2.BottomLeft,
                quad1.BottomRight / quad2.BottomRight);
        }

        public static bool operator ==(Quad quad1, Quad quad2)
        {
            return quad1.TopLeft == quad2.TopLeft &&
                   quad1.TopRight == quad2.TopRight &&
                   quad1.BottomLeft == quad2.BottomLeft &&
                   quad1.BottomRight == quad2.BottomRight;
        }

        public static bool operator !=(Quad quad1, Quad quad2) => !(quad1 == quad2);
        #endregion

        public static Quad FromTris(Tri[] tris)
        {
            if (tris.Length != 2)
            {
                throw new ArgumentException("Quad can only be created from 2 tris");
            }
            Vector3D<float>[] points = Tri.RemoveDuplicateVertices(tris);
            return Create(points[0], points[1], points[2], points[3]);
        }

        public static Vector3D<float>[] RemoveDuplicateVertices(params Quad[] quads)
        {
            List<Vector3D<float>> vertices = [];

            for (int i = 0; i < quads.Length; i++)
            {
                for (int j = 0; j < quads[i].ToArray().Length; j++)
                {
                    if (!vertices.Contains(quads[i].ToArray()[j]))
                    {
                        vertices.Add(quads[i].ToArray()[j]);
                    }
                }
            }
            return [.. vertices];
        }
        #endregion
    }
}
