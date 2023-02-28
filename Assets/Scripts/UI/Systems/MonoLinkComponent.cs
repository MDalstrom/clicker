using UnityEngine;

namespace Clicker.UI.Systems
{
    public struct MonoLinkComponent<T> where T : MonoBehaviour
    {
        public T Instance { get; set; }
    }
}