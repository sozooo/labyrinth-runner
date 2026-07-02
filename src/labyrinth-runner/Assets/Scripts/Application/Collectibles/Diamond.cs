using UnityEngine;
using Zenject;

namespace Application.Collectibles
{
    public class Diamond : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<Diamond> { }
    }
}
