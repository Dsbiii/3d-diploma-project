using Assets.Scripts.Stages.ThirdStage.Objects.Counter;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.ThirdStage.Phases.TransformerPhase
{
    public class ItemPlacePoint : MonoBehaviour
    {
        public bool IsRight;
        [SerializeField] private GameObject[] _itemsOnGameObjects;
        [SerializeField] private GameObject[] _itemsOffGameObjects;
        [SerializeField] private GameObject _pointObjects;
        private Item _item;

        public bool IsPlanted { get; private set; }

        public void SetItem(Item transformer)
        {
            _item = transformer;
            _item.transform.position = transform.position;
            //_item.transform.rotation = Quaternion.Euler(-90, 0, 0);
            foreach (var item in _itemsOnGameObjects)
                item.SetActive(true);
            foreach (var item in _itemsOffGameObjects)
                item.SetActive(false);
            IsPlanted = true;
        }

        public void TemporaryItem(Item transformer)
        {
            _item = transformer;
            _item.transform.position = transform.position;
            IsPlanted = true;
        }

        public void UndoTemorartyItem()
        {
            _item = null;
            IsPlanted = false;
        }

        public void TryDisplayPoint()
        {
            if (IsPlanted)
                return;
            _pointObjects.SetActive(true);
        }

        public void OffPoint()
        {
            _pointObjects.SetActive(false);
        }

    }
}