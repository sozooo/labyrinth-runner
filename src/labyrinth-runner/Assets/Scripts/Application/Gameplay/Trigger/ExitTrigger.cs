using Application.Core.Messages;
using Application.Gameplay.Player;
using MessagePipe;
using UnityEngine;
using Zenject;

namespace Application.Gameplay.Trigger
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

