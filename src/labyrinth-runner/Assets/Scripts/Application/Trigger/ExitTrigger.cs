using Application.Messages;
using Application.Player;
using MessagePipe;
using UnityEngine;
using Zenject;

namespace Application.Trigger
{
    public class ExitTrigger : MonoBehaviour
    {
        private IPublisher<ExitReachedMessage> _publisher;

        [Inject]
        private void Construct(IPublisher<ExitReachedMessage> publisher) =>
            _publisher = publisher;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IPlayer>(out _))
                _publisher.Publish(default);
        }
    }
}
