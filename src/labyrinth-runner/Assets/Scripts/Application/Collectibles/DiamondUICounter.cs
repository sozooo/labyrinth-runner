using System;
using MessagePipe;
using TMPro;
using UnityEngine;
using Zenject;

namespace Application.Collectibles
{
    public class DiamondUICounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counterText;

        private IBufferedSubscriber<DiamondsSpawnedMessage> _spawnSubscriber;
        private ISubscriber<DiamondCollectedMessage> _collectSubscriber;
        
        private IDisposable _spawnSub;
        private IDisposable _collectSub;
        
        private int _total;
        private int _collected;

        [Inject]
        private void Construct(
            IBufferedSubscriber<DiamondsSpawnedMessage> spawnSubscriber,
            ISubscriber<DiamondCollectedMessage> collectSubscriber)
        {
            _spawnSubscriber = spawnSubscriber;
            _collectSubscriber = collectSubscriber;
        }

        private void Start()
        {
            _spawnSub = _spawnSubscriber.Subscribe(msg => { _total = msg.TotalCount; UpdateUI(); });
            _collectSub = _collectSubscriber.Subscribe(_ => { _collected++; UpdateUI(); });
        }

        private void UpdateUI() => 
            _counterText.text = $"{_collected} / {_total}";

        private void OnDestroy()
        {
            _spawnSub?.Dispose();
            _collectSub?.Dispose();
        }
    }
}
