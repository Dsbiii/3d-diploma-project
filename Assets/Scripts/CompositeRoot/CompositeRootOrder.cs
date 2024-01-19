using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CompositeRoot
{
    public class CompositeRootOrder : MonoBehaviour
    {
        [SerializeField] private List<CompositeRoot> _compositeRoots;

        private void Awake()
        {
            foreach (var item in _compositeRoots)
                item.Composite();
        }
    }
}