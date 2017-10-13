using System;
using CmdQ.GeneticOptimization;

namespace SvgBrudaGui
{
    public class Chromosome : IChromosome
    {
        public void Decode(byte[] code)
        {
            throw new NotImplementedException();
        }

        public void Decode(Random random)
        {
            throw new NotImplementedException();
        }

        public byte[] Encode()
        {
            throw new NotImplementedException();
        }
    }

    public class Genome : IFixedLengthGenome<Chromosome>
    {
        public int Length => throw new NotImplementedException();

        public void Decode(byte[] code)
        {
            throw new NotImplementedException();
        }

        public void Decode(Random random)
        {
            throw new NotImplementedException();
        }

        public byte[] Encode()
        {
            throw new NotImplementedException();
        }
    }
}
