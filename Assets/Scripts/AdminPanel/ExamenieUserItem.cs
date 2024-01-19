using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.AdminPanel
{
    public class ExamenieUserItem : MonoBehaviour
    {
        [SerializeField] private Text _indexText;
        [SerializeField] private Text _itemText;
        [SerializeField] private Text _userActionText;
        [SerializeField] private Text _idealActionText;
        [SerializeField] private Text _scoreText;


        public void SetExamenieItems(string index, string item, string userAction, string idealAction, string score)
        {
            _indexText.text = index;
            _itemText.text = item;
            _userActionText.text = userAction;
            _idealActionText.text = idealAction;
            _scoreText.text = score;
        }

    }
}