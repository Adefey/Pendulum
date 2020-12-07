using SharpGL;
using SharpGL.SceneGraph;
using System;
using System.Diagnostics;
using System.Drawing;

namespace Pendulum
{
    internal class Physics
    {
        private Drawings GL;
        private const float g = 9.8145f;
        private float rot;
        private float A; //В градусах
        private float T;
        private float w; //ммм... Наверное в радианах в секунду.
        private float phi0; //В радианах
        private float t;
        private float length = 1;
        private float w1Coord = 0.4f;
        private float w2Coord = 0.8f;
        public Stopwatch stopWath;
        public Physics(OpenGLControl Control, Vertex eyePos, Vertex centrePos, Vertex topPos)
        {
            A = 45;
            T = 2 * (float)Math.PI * (float)Math.Sqrt((w1Coord + w2Coord) / (2 * g));
            w = 2 * (float)Math.PI / T;
            phi0 = A / 2 * (float)Math.PI / 180; //автонахождение начальной фазы. Амплитуду перевести в радианы и делить на 2.
            stopWath = new Stopwatch();
            GL = new Drawings(Control, eyePos, centrePos, topPos);
        }

        public void SetParams(float w1, float w2, float l, float r)
        {
            A = r;
            length = l / 10;
            w1Coord = length * w1 / 10;
            w2Coord = length * w2 / 10;
            T = 2 * (float)Math.PI * (float)Math.Sqrt((w1Coord + w2Coord) / (2 * g));
            w = 2 * (float)Math.PI / T;
        }

        public float GetAngle()
        {
            return rot;
        }

        public float GetT()
        {
            return T;
        }

        public void ProcessPhysics()
        {
            //Система имеет одну степень свободы (грузы прочно закреплены). Параметр - rot - угол отклонения
            //rot вычисляется по классической формуле гармонических колебаний
            //phi = A * sin(w * t + phi0), где phi - угол отклонения, A - амплитуда, w - угловая частота, t - время в секундах, phi0 - начальное отклонение
            t = (float)stopWath.ElapsedMilliseconds / 1000;
            rot = A * (float)Math.Sin(w * t + phi0);
            GL.DrawRod(new Vertex(0, 0, 0), new Vertex(0, -length, 0), 0.2, Color.Blue);
            GL.DrawRod(new Vertex(0, -w1Coord - 0.1f, 0), new Vertex(0, -w1Coord + 0.1f, 0), 0.3, Color.Red);
            GL.DrawRod(new Vertex(0, -w2Coord - 0.1f, 0), new Vertex(0, -w2Coord + 0.1f, 0), 0.3, Color.Green);
            GL.Render(rot);
        }
    }
}
