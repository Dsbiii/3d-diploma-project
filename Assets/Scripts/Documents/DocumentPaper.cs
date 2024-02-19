using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MVC.Documents
{
    public class DocumentPaper : MonoBehaviour
    {
        [SerializeField] private Image _paper;

        public void SetPaper(Sprite sprite)
        {
            _paper.sprite = sprite;
        }
    }
}