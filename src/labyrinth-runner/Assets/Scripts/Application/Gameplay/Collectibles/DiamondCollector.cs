using MessagePipe;
using UnityEngine;
using Zenject;

namespace Application.Gameplay.Collectibles
{
    public class DiamondCollector : MonoBehaviour
    {
        private IPublisher<DiamondCollectedMessage> _publisher;

        [Inject]
        private void Construct(IPublisher<DiamondCollectedMessage> publisher) =>
            _publisher = publisher;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Diamond>(out var diamond) == false) 
                return;

            _publisher.Publish(default(DiamondCollectedMessage));
            diamond.gameObject.SetActive(false);
        }
    }
}

