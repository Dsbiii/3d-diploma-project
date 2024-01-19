using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Instruments
{
    public class PlombPoint : MonoBehaviour
    {
        [SerializeField] private MeshRenderer[] _meshRenderers;
        
        public bool IsPlombed { get; private set; }
        public bool IsActivePlomb { get; private set; }

        public void SetupCap()
        {
            foreach (var item in _meshRenderers)
                item.enabled = true;
            IsPlombed = true;
            IsActivePlomb = true;
        }

        public void Close()
        {
            IsPlombed = false;
            foreach (var item in _meshRenderers)
                item.enabled = false;
        }

        public void OffColliders()
        {
            GetComponent<BoxCollider>().enabled = false;
        }

        public void OnColliders()
        {
            GetComponent<BoxCollider>().enabled = true;
        }
    }
}