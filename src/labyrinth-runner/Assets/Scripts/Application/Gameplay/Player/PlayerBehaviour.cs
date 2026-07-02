using UnityEngine;

namespace Application.Gameplay.Player
{
    public class PlayerBehaviour : MonoBehaviour, IPlayer
    {
        public Transform Transform => transform;
    }
}

