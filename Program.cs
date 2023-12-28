using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using SilkDotNetLearning.DrawObjects;
using System.Drawing;

namespace SilkDotNetLearning
{
    internal class Program
    {
        private static IWindow _window;
        private static GL _gl;
        private static uint _vertexArrayObjectPointer;
        private static uint _vertexBufferObjectPointer;

        private static void Main(string[] args)
        {
            Console.WriteLine("Silk.NET Learning Project + GLSL Learning.");

            Quad quad = Quad.Create(
                new Vector2D<float>(0.5f, 0.5f),
                new Vector2D<float>(-0.5f, -0.5f));

            Tri[] tris = quad.ToTris();

            Console.WriteLine(quad);
            Quad reCreated = Quad.FromTris(tris);
            Console.WriteLine(reCreated);
            Console.WriteLine(quad == reCreated);
            Console.WriteLine(quad.Equals(reCreated));


            foreach (Tri tri in tris)
            {
                Console.WriteLine(tri);
            }







            WindowOptions options = WindowOptions.Default with
            {
                Size = new Vector2D<int>(800, 600),
                Title = "Silk.NET Learning"
            };
            _window = Window.Create(options);
            _window.Load += OnLoad;
            _window.Update += OnUpdate;
            _window.Render += OnRender;

            _window.Run();
        }

        private static unsafe void OnLoad()
        {
            /*
            
            Vector2D<float>[] coords =
            [
                new Vector2D<float>(0.5f, 0.5f),
                new Vector2D<float>(0.5f, -0.5f),
                new Vector2D<float>(-0.5f, -0.5f),
                new Vector2D<float>(-0.5f, 0.5f)
            ];
            float[] vertices = VertexDataUtils.Vec2ToVertexDataArray(coords);
            */
            Quad quad = Quad.Create(
                new Vector2D<float>(0.5f, 0.5f),
                new Vector2D<float>(-0.5f, -0.5f)
                );

            _gl = _window.CreateOpenGL();
            _vertexArrayObjectPointer = _gl.GenVertexArray();
            _gl.BindVertexArray(_vertexArrayObjectPointer);
            _vertexBufferObjectPointer = _gl.GenBuffer();
            _gl.BindBuffer(
                BufferTargetARB.ArrayBuffer,
                _vertexBufferObjectPointer);

            fixed (float* buf = quad.Vertices)
            {
                _gl.BufferData(
                    BufferTargetARB.ArrayBuffer,
                    (nuint)(quad.Vertices.Length * sizeof(float)),
                    buf,
                    BufferUsageARB.StaticDraw);
            }
            _gl.ClearColor(Color.Aquamarine);
            IInputContext input = _window.CreateInput();
            for (int i = 0; i < input.Keyboards.Count; i++)
                input.Keyboards[i].KeyDown += KeyDown;
        }

        private static unsafe void OnRender(double deltaTime)
        {

            _gl.Clear(ClearBufferMask.ColorBufferBit);
        }
        private static void OnUpdate(double deltaTime)
        {

        }
        private static void KeyDown(IKeyboard keyboard, Key key, int keyCode)
        {
            if (key == Key.Escape)
            {
                _window.Close();
            }
        }


    }
}
