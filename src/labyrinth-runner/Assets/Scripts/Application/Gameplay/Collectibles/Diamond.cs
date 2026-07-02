using UnityEngine;
using Zenject;

namespace Application.Gameplay.Collectibles
{
    public class Diamond : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<Diamond> { }
    }
}

