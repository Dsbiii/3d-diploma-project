using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class AutoResizePanel : MonoBehaviour
    {
        public Text textToMeasure;
        public float padding = 10f;

        private RectTransform rectTransform;
        private TextGenerator textGenerator;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            if (rectTransform == null)
            {
                Debug.LogError("AutoResizePanel script requires a RectTransform component on the GameObject.");
                return;
            }

            if (textToMeasure == null)
            {
                Debug.LogError("Please assign a Text component to the textToMeasure field in the Inspector.");
                return;
            }

            textGenerator = new TextGenerator();
        }

        private void Update()
        {
            if (textGenerator == null)
                return;

            // Create a new TextGenerationSettings object based on current text and rect size
            TextGenerationSettings generationSettings = textToMeasure.GetGenerationSettings(new Vector2(rectTransform.rect.width, 0));

            // Measure preferred height of the text
            float preferredHeight = textGenerator.GetPreferredHeight(textToMeasure.text, generationSettings) + padding;

            // Set panel height to match text height
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, preferredHeight);
        }
    }
}