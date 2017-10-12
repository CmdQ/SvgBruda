using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading;

namespace SvgBrudaGui
{
    [DataContract]
    class Settings : IDisposable
    {
        const string CAT_EVOLUTION = "Evolution";

        ReaderWriterLockSlim _lock;

        public Settings() => Initialize();

        [OnDeserializing]
        void OnDeserialize(StreamingContext _) => Initialize();

        void Initialize()
        {
            _imagePath = "";
            _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            _mutationProbability = 0;
            _populationSize = 2;
        }

        string _imagePath;

        [Browsable(false)]
        [DataMember]
        public string TargetImagePath
        {
            get => GetLocked(() => _imagePath);
            set
            {
                if (value != null)
                {
                    SetLocked(ref _imagePath, value);
                }
            }
        }

        float _mutationProbability;

        [Category(CAT_EVOLUTION)]
        [Description("Set the probability (i. e. 0 to 1) that a random bit flip occurs for each bit.")]
        [DataMember]
        public float MutationProbability
        {
            get => GetLocked(() => _mutationProbability);
            set
            {
                if (value >= 0 && value <= 1)
                {
                    SetLocked(ref _mutationProbability, value);
                }
            }
        }

        int _populationSize;

        [Category(CAT_EVOLUTION)]
        [Description("How many individuals are in one generation? Bigger populations mean more mating possibilities.")]
        [DataMember]
        public int PopulationSize
        {
            get => GetLocked(() => _populationSize);
            set
            {
                if (value >= 2)
                {
                    SetLocked(ref _populationSize, value);
                }
            }
        }

        T GetLocked<T>(Func<T> read)
        {
            _lock.EnterReadLock();
            try
            {
                return read();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        void SetLocked<T>(ref T field, T value)
        {
            _lock.EnterWriteLock();
            try
            {
                field = value;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Save2Json(FileInfo path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            var json = new DataContractJsonSerializer(GetType());

            using (var stream = path.OpenWrite())
            {
                _lock.EnterReadLock();
                try
                {
                    json.WriteObject(stream, this);
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            }
        }

        public static Settings ReadFromJson(FileInfo path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            var json = new DataContractJsonSerializer(typeof(Settings));

            using (var stream = path.OpenRead())
            {
                return (Settings)json.ReadObject(stream);
            }
        }

        #region IDisposable Support

        bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                if (disposing)
                {
                    _lock.Dispose();
                }
            }
        }

        public void Dispose() => Dispose(true);

        #endregion
    }
}