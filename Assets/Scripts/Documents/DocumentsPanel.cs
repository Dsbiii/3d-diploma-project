using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MVC.Documents
{
    public class DocumentsPanel : MonoBehaviour
    {
        [SerializeField] private List<Document> _documents;
        [SerializeField] private GameObject _panelsWithButtons;


        public void OpenClose()
        {
            _panelsWithButtons.SetActive(!_panelsWithButtons.activeSelf);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                foreach(var document in _documents)
                {
                    document.gameObject.SetActive(false);
                }
            }
        }
    }
}