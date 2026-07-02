using UnityEngine;

namespace Application.Player
{
    public class PlayerBehaviour : MonoBehaviour, IPlayer
    {
        public Transform Transform => transform;
    }
}
