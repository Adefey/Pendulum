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
        private float rot;
        private float A; //В градусах
        private float w; //ммм... Наверное в радианах в секунду.
        private float phi0; //В радианах
        private float t;
        private float length = 5;
        private float w1Coord = -2;
        private float w2Coord = -4;
        public Stopwatch stopWath;
        public Physics(OpenGLControl Control, Vertex eyePos, Vertex centrePos, Vertex topPos)
        {
            A = 45;
            w = 5;
            phi0 = A / 2 * (float)Math.PI / 180; //автонахождение начальной фазы. Амплитуду перевести в радианы и делить на 2.
            stopWath = new Stopwatch();
            GL = new Drawings(Control, eyePos, centrePos, topPos);
        }

        public void SetWeightCoords(float w1, float w2)
        {
            w1Coord = w1;
            w2Coord = w2;
        }

        public void ProcessPhysics()
        {
            //Система имеет одну степень свободы (грузы прочно закреплены). Параметр - rot - угол отклонения
            //rot вычисляется по классической формуле гармонических колебаний
            //phi = A * sin(w * t + phi0), где phi - угол отклонения, A - амплитуда, w - угловая частота, t - время в секундах, phi0 - начальное отклонение
            t = (float)stopWath.ElapsedMilliseconds / 1000;
            rot = A * (float)Math.Sin(w * t + phi0);
            GL.DrawRod(new Vertex(0, 0, 0), new Vertex(0, -length, 0), 0.2, Color.Blue);
            GL.DrawRod(new Vertex(0, w1Coord - 0.25f, 0), new Vertex(0, w1Coord + 0.25f, 0), 0.5, Color.Red);
            GL.DrawRod(new Vertex(0, w2Coord - 0.25f, 0), new Vertex(0, w2Coord + 0.25f, 0), 0.5, Color.Green);
            GL.Render(rot);
        }
    }
}
