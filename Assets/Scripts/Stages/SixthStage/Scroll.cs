using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stages.SixthStage
{
    public class Scroll : MonoBehaviour
    {
        [SerializeField] private Scrollbar scrollbar;
        private void OnEnable()
        {
            scrollbar.value = 1;
        }
    }
}