using Assets.Scripts.Stages.FirstStage;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class ButtonsMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _documentsButton;

        private Transform[] _items;

        private void Awake()
        {
            _items = GetComponentsInChildren<Transform>();
        }

        private void Update()
        {
            if (SceneManager.GetActiveScene().name == "Menu")
            {
                _documentsButton.SetActive(false);
                foreach (var item in _items)
                {
                    item.gameObject.SetActive(false);
                }
            }
            else
            {
                _documentsButton.SetActive(true);
                foreach (var item in _items)
                {
                    item.gameObject.SetActive(true);
                }
            }

        }

    }
}