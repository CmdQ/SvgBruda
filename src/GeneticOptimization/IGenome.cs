using System;

namespace CmdQ.GeneticOptimization
{
    public interface IGenome<T>
        where T : IChromosome, new()
    {
        byte[] Encode();

        void Decode(byte[] code);

        void Decode(Random random);
    }

    public interface IFixedLengthGenome<T> : IGenome<T>
        where T : IChromosome, new()
    {
        int Length { get; }
    }

    public interface IVariableLengthGenome<T> : IGenome<T>
        where T : IChromosome, new()
    {
        int Count { get; }

        int MinimumChromosomes { get; }

        int MaximumChromosomes { get; }
    }
}
