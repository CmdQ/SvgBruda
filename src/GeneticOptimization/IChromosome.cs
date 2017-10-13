using System;

namespace CmdQ.GeneticOptimization
{
    public interface IChromosome
    {
        byte[] Encode();

        void Decode(byte[] code);

        void Decode(Random random);
    }
}
