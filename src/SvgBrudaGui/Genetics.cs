using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using CmdQ.GeneticOptimization;

namespace SvgBrudaGui
{
    struct Point
    {
        internal static int _mySize = Marshal.SizeOf<Point>();

        public float x;
        public float y;

        public override string ToString() => $"{x} / {y}";
    }

    public class Chromosome : IChromosome
    {
        const int VERTEX_COUNT = 3;

        static readonly int _mySize = VERTEX_COUNT * Point._mySize + sizeof(int);

        readonly Point[] _points = new Point[VERTEX_COUNT];

        Color _color;

        public Chromosome() { }

        internal Chromosome(Point[] points, Color color)
        {
            for (int i = 0; i < _points.Length; ++i)
            {
                _points[i] = points[i];
            }
            _color = color;
        }

        public void Decode(byte[] code)
        {
            for (int i = 0; i < _points.Length; ++i)
            {
                _points[i] = new Point
                {
                    x = BitConverter.ToSingle(code, i * Point._mySize),
                    y = BitConverter.ToSingle(code, i * Point._mySize + sizeof(float))
                };
            }
            _color = Color.FromArgb(BitConverter.ToInt32(code, code.Length - sizeof(int)));
        }

        public void CreateRandom(Random random)
        {
            for (int i = 0; i < _points.Length; ++i)
            {
                _points[i] = new Point { x = (float)random.NextDouble(), y = (float)random.NextDouble() };
            }
            _color = Color.FromArgb(random.Next());
        }

        public byte[] Encode()
        {
            return _points
                .SelectMany(p => BitConverter.GetBytes(p.x).Concat(BitConverter.GetBytes(p.y)))
                .Concat(BitConverter.GetBytes(_color.ToArgb()))
                .ToArray();
        }
    }

    public class Genome : FixedLengthGenome<Chromosome>
    {
        const int TRIANGLES = 99;

        static readonly Chromosome _prototype = new Chromosome();
        static readonly int _chromosomeLength = _prototype.Encode().Length;

        public Genome()
            : base(TRIANGLES)
        { }

        public override void Decode(byte[] code)
        {
            for (int i = 0; i < Chromosomes.Length; ++i)
            {
                Chromosomes[i].Decode(code);
            }
        }

        public override void Decode(Random random)
        {
            for (int i = 0; i < Chromosomes.Length; ++i)
            {
                Chromosomes[i].CreateRandom(random);
            }
        }

        public override byte[] Encode()
        {
            return Chromosomes
                .SelectMany(c => c.Encode())
                .ToArray();
        }
    }
}
