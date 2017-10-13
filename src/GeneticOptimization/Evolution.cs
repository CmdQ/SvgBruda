using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CmdQ.GeneticOptimization
{
    public class Evolution<G, C>
        where G : IGenome<C>, new()
        where C : IChromosome, new()
    {
        readonly Random _rand = new Random();
        readonly int _populationSize;

        List<G> _population;

        public int Generation { get; private set; }

        public Evolution(int populationSize)
        {
            if (populationSize < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(populationSize),
                    "For mating to occur, the population size must be at least 2.");
            }

            _populationSize = populationSize;
        }

        public void Start()
        {
            if (_population == null || _population.Count == 0)
            {
                _population = CreateInitialPopulation().ToList();
            }
            for (Generation = 0; ; ++Generation)
            {
                ScoreGenomes();
                var nextPopulation = BringOver();
                Breeding(nextPopulation);
                _population = nextPopulation;
            }
        }

        public void Pause()
        {
        }

        void Breeding(List<G> nextPopulation)
        {
        }

        IEnumerable<G> CreateInitialPopulation()
        {
            for (int i = 0; i < _populationSize; ++i)
            {
                var genome = new G();
                genome.Decode(_rand);
                yield return genome;
            }
        }

        void ScoreGenomes()
        {
        }

        List<G> BringOver()
        {
            var re = new List<G>(_populationSize);
            return re;
        }
    }
}
