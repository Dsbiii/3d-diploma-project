using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class ScrollViewScreenshot : MonoBehaviour
    {
        public RectTransform scrollViewContent; // Перетащите сюда RectTransform вашего ScrollView
        public Camera renderCamera; // Камера для рендеринга ScrollView
        public int width = 1080; // Ширина скриншота

        public void CaptureScreenshot()
        {
            StartCoroutine(CaptureScreenshotCoroutine());
        }

        public static string ScreenShotName(int width, int height, string Path)
        {


            return string.Format("{0}/{1}/Act_{2}x{3}_{4}.png",
                                 Application.dataPath,
                                 Path,
                                 width, height,
                                 System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));

        }

        private IEnumerator CaptureScreenshotCoroutine()
        {
            // Вычисляем высоту содержимого ScrollView
            float contentHeight = scrollViewContent.sizeDelta.y;
            int height = Mathf.CeilToInt(contentHeight * (width / scrollViewContent.rect.width));

            // Создаем новую RenderTexture
            RenderTexture rt = new RenderTexture(width, height, 24);
            renderCamera.targetTexture = rt;
            Texture2D screenShot = new Texture2D(width, height, TextureFormat.RGB24, false);

            // Устанавливаем камеру для захвата всего ScrollView
            Vector3 originalPosition = renderCamera.transform.position;
            renderCamera.orthographicSize = contentHeight * 0.5f;
            renderCamera.transform.position = new Vector3(scrollViewContent.position.x, scrollViewContent.position.y, renderCamera.transform.position.z);

            // Даем время на обновление камеры
            yield return new WaitForEndOfFrame();

            // Рендерим содержимое в текстуру
            renderCamera.Render();

            // Восстанавливаем камеру
            renderCamera.transform.position = originalPosition;
            renderCamera.orthographicSize = 5; // Верните камеру к исходному размеру
            renderCamera.targetTexture = null;

            // Читаем пиксели из текстуры и сохраняем их
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            screenShot.Apply();

            // Переводим изображение в формат PNG
            byte[] bytes = screenShot.EncodeToPNG();
            string filename = ScreenShotName(width, height, "Судьи");
            System.IO.File.WriteAllBytes(filename, bytes);
            Debug.Log($"Screenshot saved to {filename}");

            // Очищаем за собой
            RenderTexture.active = null;
            Destroy(rt);
        }
    }
}