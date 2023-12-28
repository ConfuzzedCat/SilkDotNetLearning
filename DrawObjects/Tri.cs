using Silk.NET.Maths;
using SilkDotNetLearning.Utils;

namespace SilkDotNetLearning.DrawObjects
{
    public struct Tri
    {
        public Vector3D<float> Top { get; private set; }
        public Vector3D<float> Left { get; private set; }
        public Vector3D<float> Right { get; private set; }

        public readonly Vector3D<float> A => Top;
        public readonly Vector3D<float> B => Left;
        public readonly Vector3D<float> C => Right;

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
            2u
            ];


        private Tri(
            Vector3D<float> top,
            Vector3D<float> left,
            Vector3D<float> right
            )
        {
            Top = top;
            Left = left;
            Right = right;
        }

        public readonly Vector3D<float>[] ToArray()
        {
            return
            [
                Top,
                Left,
                Right
            ];
        }

        public override readonly string ToString()
        {
            return $"Tri: A: {A}, B: {B}, C: {C}";
        }
        #region Static methods
        #region Create methods
        public static Tri Create(
            Vector3D<float> top,
            Vector3D<float> left,
            Vector3D<float> right
            )
        {
            return new Tri(top, left, right);
        }

        public static Tri Create(
            Vector3D<float> top,
            Vector3D<float> bottom
            )
        {
            Vector3D<float> left =
                new Vector3D<float>(
                    top.X,
                    bottom.Y,
                    top.Z
                );
            Vector3D<float> right =
                new Vector3D<float>(
                    bottom.X,
                    bottom.Y,
                    top.Z
                );
            return Create(top, left, right);
        }

        public static Tri Create(
            Vector3D<float> top,
            float width,
            float height
            )
        {
            Vector3D<float> bottom =
                new Vector3D<float>(
                    top.X + width,
                    top.Y + height,
                    top.Z
                );
            return Create(top, bottom);
        }

        public static Tri Create(
            Vector3D<float> top,
            float size
            )
        {
            return Create(top, size, size);
        }

        public static Tri Create(
            Vector3D<float> top
            )
        {
            return Create(top, 1f);
        }

        public static Tri Create()
        {
            return Create(new Vector3D<float>(1f, 1f, 0f));
        }
        #endregion
        #region Operators overloading
        public static Tri operator +(Tri tri, Vector3D<float> vec)
        {
            return Create(
                tri.A + vec,
                tri.B + vec,
                tri.C + vec
                );
        }

        public static Tri operator -(Tri tri, Vector3D<float> vec)
        {
            return Create(
                tri.A - vec,
                tri.B - vec,
                tri.C - vec
                );
        }

        public static Tri operator *(Tri tri, Vector3D<float> vec)
        {
            return Create(
                tri.A * vec,
                tri.B * vec,
                tri.C * vec
                );
        }

        public static Tri operator /(Tri tri, Vector3D<float> vec)
        {
            return Create(
                tri.A / vec,
                tri.B / vec,
                tri.C / vec
                );
        }

        public static Tri operator *(Tri tri, float val)
        {
            return Create(
                tri.A * val,
                tri.B * val,
                tri.C * val
                );
        }

        public static Tri operator /(Tri tri, float val)
        {
            return Create(
                tri.A / val,
                tri.B / val,
                tri.C / val
                );
        }

        public static Tri operator +(Tri tri1, Tri tri2)
        {
            return Create(
                tri1.A + tri2.A,
                tri1.B + tri2.B,
                tri1.C + tri2.C
                );
        }

        public static Tri operator -(Tri tri1, Tri tri2)
        {
            return Create(
                tri1.A - tri2.A,
                tri1.B - tri2.B,
                tri1.C - tri2.C
                );
        }

        public static Tri operator *(Tri tri1, Tri tri2)
        {
            return Create(
                tri1.A * tri2.A,
                tri1.B * tri2.B,
                tri1.C * tri2.C
                );
        }

        public static Tri operator /(Tri tri1, Tri tri2)
        {
            return Create(
                tri1.A / tri2.A,
                tri1.B / tri2.B,
                tri1.C / tri2.C
                );
        }

        public static bool operator ==(Tri tri1, Tri tri2)
        {
            return tri1.Equals(tri2);
        }

        public static bool operator !=(Tri tri1, Tri tri2)
        {
            return !tri1.Equals(tri2);
        }
        #endregion

        public static Vector3D<float>[] RemoveDuplicateVertices(params Tri[] tris)
        {
            List<Vector3D<float>> vertices = [];

            for (int i = 0; i < tris.Length; i++)
            {
                for (int j = 0; j < tris[i].ToArray().Length; j++)
                {
                    if (!vertices.Contains(tris[i].ToArray()[j]))
                    {
                        vertices.Add(tris[i].ToArray()[j]);
                    }
                }
            }
            return [.. vertices];
        }

        #endregion
    }
}
