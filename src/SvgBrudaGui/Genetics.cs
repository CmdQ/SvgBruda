using System;
using System.Linq;
using CmdQ.GeneticOptimization;

namespace SvgBrudaGui
{
    struct Point
    {
        public float x;
        public float y;

        public override string ToString() => $"{x} / {y}";
    }

    public class Chromosome : IChromosome
    {
        readonly Point[] _points = new Point[3];

        public void Decode(byte[] code)
        {
            for (int i = 0; i < _points.Length; ++i)
            {
                _points[i] = new Point
                {
                    x = BitConverter.ToSingle(code, (2 * i + 0) * sizeof(float)),
                    y = BitConverter.ToSingle(code, (2 * i + 1) * sizeof(float))
                };
            }
        }

        public void CreateRandom(Random random)
        {
            for (int i = 0; i < _points.Length; ++i)
            {
                _points[i] = new Point { x = (float)random.NextDouble(), y = (float)random.NextDouble() };
            }
        }

        public byte[] Encode()
        {
            return _points
                .SelectMany(p => BitConverter.GetBytes(p.x).Concat(BitConverter.GetBytes(p.y)))
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
