using MessagePipe;
using UnityEngine;
using Zenject;

namespace Application.Collectibles
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

            _publisher.Publish(new DiamondCollectedMessage(diamond));
            diamond.gameObject.SetActive(false);
        }
    }
}
