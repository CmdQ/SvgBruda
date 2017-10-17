using System;

namespace CmdQ.GeneticOptimization
{
    public abstract class FixedLengthGenome<T> : IFixedLengthGenome<T>
        where T : IChromosome, new()
    {
        readonly T[] _chromosomes;

        protected FixedLengthGenome(int length)
        {
            _chromosomes = new T[length];
            for (int i = 0; i < _chromosomes.Length; ++i)
            {
                _chromosomes[i] = new T();
            }
        }

        public int Length => _chromosomes.Length;

        protected T[] Chromosomes => _chromosomes;

        public abstract void Decode(byte[] code);

        public abstract void Decode(Random random);

        public abstract byte[] Encode();
    }
}
