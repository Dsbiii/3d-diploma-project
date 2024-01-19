using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.FirstStage
{
    public class ItemPreview : MonoBehaviour
    {

        [SerializeField] private Transform _previewPosition;
        [SerializeField] private Transform _priborPreviewPosition;

        private Vector3 _baseItemPosition;
        private Quaternion _baseItemRotation;

        private GameState _firstStageState;

        private Item _seletedItem;

        public Item SelectedItem => _seletedItem;

        public void Init(GameState firstStageState)
        {
            _firstStageState = firstStageState;
        }

        public void SelectItemInstrument(Item item)
        {
            _seletedItem = item;
        }

        public void HideCurrentInstrumentItem()
        {
            _seletedItem = null;
        }

        public void PreviewItem(Item item)
        {
            item.gameObject.SetActive(true);
            _seletedItem = item;

            _baseItemPosition = _seletedItem.transform.position;
            _baseItemRotation = _seletedItem.transform.rotation;

            if(!item.IsPribor)
                _seletedItem.transform.position = _previewPosition.position;
            else
                _seletedItem.transform.position = _priborPreviewPosition.position;
        }

        public void BackFromPreview(bool disableItem = false)
        {
            FindObjectOfType<ResultReplaceNotify>().CloseNotifyText();

            _seletedItem.transform.position = _baseItemPosition;
            _seletedItem.transform.rotation = _baseItemRotation;

            if (disableItem)
                _seletedItem.gameObject.SetActive(false);
            _seletedItem = null;
        }


        public void Update()
        {
            if (_firstStageState.CurrentState != State.Preview || _seletedItem == null)
                return;
            if (Input.GetMouseButton(1))
            {
                _seletedItem.transform.Rotate(Vector3.left, -Input.GetAxis("Mouse Y") * 2, Space.World);
                _seletedItem.transform.Rotate(Vector3.up, -Input.GetAxis("Mouse X") * 2, Space.World);
            }

            if (Input.GetMouseButtonDown(2))
            {
                _seletedItem.ActionInPreview();
            }

            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            Camera.main.fieldOfView -= scrollInput * 8;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 2, 60);

            //float zoom = Input.mouseScrollDelta.y;
            //if (zoom == 1 && _seletedItem != null)
            //{
            //    if (_seletedItem.transform.localPosition.y < 1.33)
            //    {
            //        Vector3 dir = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
            //        _seletedItem.transform.Translate(-dir * zoom * 0.1f, Space.World);
            //    }
            //}
            //else if (zoom == -1)
            //{
            //    if (!_seletedItem.IsPribor)
            //        _seletedItem.transform.position = Vector3.MoveTowards(_seletedItem.transform.position, _previewPosition.position, Time.deltaTime * 4);
            //    else
            //        _seletedItem.transform.position = Vector3.MoveTowards(_seletedItem.transform.position, _priborPreviewPosition.position, Time.deltaTime * 4);
            //}

        }

    }
}