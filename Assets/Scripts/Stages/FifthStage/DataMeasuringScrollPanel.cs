using Assets.Scripts.Stages.FourthStage;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Stages.FifthStage
{
    public class DataMeasuringScrollPanel : MonoBehaviour
    {
        [Inject] private FifthStageExam _fifthStageExam;
        public ScrollRect scrollRect;

        private void Update()
        {
            Debug.Log("scrollRect.normalizedPosition.y " + scrollRect.normalizedPosition.y);
            if (scrollRect.normalizedPosition.y <= 0f)
            {
                _fifthStageExam.IsScrolledDataPanel = true;
                Debug.Log("ScrollView доскролен до верхней части.");
            }

            if (scrollRect.normalizedPosition.y >= 1f)
            {
                Debug.Log("ScrollView доскролен до нижней части.");
            }
        }
    }
}