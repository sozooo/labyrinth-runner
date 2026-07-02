using System;
using MessagePipe;
using Zenject;

namespace Application.Collectibles
{
    public class DoorOpener : IInitializable, IDisposable
    {
        private readonly Door[] _doors;
        
        private readonly IBufferedSubscriber<DiamondsSpawnedMessage> _spawnSubscriber;
        private readonly ISubscriber<DiamondCollectedMessage> _collectSubscriber;
        
        private IDisposable _spawnSub;
        private IDisposable _collectSub;
        
        private int _total;
        private int _collected;

        public DoorOpener(
            Door[] doors,
            IBufferedSubscriber<DiamondsSpawnedMessage> spawnSubscriber,
            ISubscriber<DiamondCollectedMessage> collectSubscriber)
        {
            _doors = doors;
            _spawnSubscriber = spawnSubscriber;
            _collectSubscriber = collectSubscriber;
        }

        public void Initialize()
        {
            _spawnSub = _spawnSubscriber.Subscribe(msg => _total = msg.TotalCount);
            _collectSub = _collectSubscriber.Subscribe(_ => TryOpenDoors());
        }

        public void Dispose()
        {
            _spawnSub?.Dispose();
            _collectSub?.Dispose();
        }

        private void TryOpenDoors()
        {
            if (++_collected < _total) 
                return;

            foreach (Door door in _doors)
                door.Open();
        }
    }
}
