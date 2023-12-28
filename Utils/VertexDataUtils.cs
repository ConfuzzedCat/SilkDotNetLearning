using Silk.NET.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkDotNetLearning.Utils
{
    public class VertexDataUtils
    {
        public static float[] Vec2ToVertexDataArray(Vector2D<float>[] points)
        {
            float[] floats = new float[points.Length * 3];
            for (int i = 0; i < points.Length; i++)
            {
                floats[i * 3] = points[i].X;
                floats[i * 3 + 1] = points[i].Y;
                floats[i * 3 + 2] = 0;
            }
            return floats;
        }

        public static float[] Vec3ToVertexDataArray(Vector3D<float>[] points)
        {
            float[] floats = new float[points.Length * 3];
            for (int i = 0; i < points.Length; i++)
            {
                floats[i * 3] = points[i].X;
                floats[i * 3 + 1] = points[i].Y;
                floats[i * 3 + 2] = points[i].Z;
            }
            return floats;
        }
    }
}
