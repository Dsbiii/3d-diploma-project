using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ActReportScreenShoot : MonoBehaviour
    {
        [SerializeField] private GameObject[] _objects;
        [SerializeField] private Camera _screenCamera;
        [SerializeField] private Mask _mask;
        [SerializeField] private ScrollRect _scrollView;
        public int resWidth = 2550;
        public int resHeight = 3300;

        public void TakeScreenshot()
        {
            _mask.enabled = false;
            float value = _scrollView.verticalNormalizedPosition;
            _scrollView.verticalNormalizedPosition = 1;
            foreach (var obj in _objects)
            {
                obj.SetActive(false);
            }
            _screenCamera.gameObject.SetActive(true);

            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            _screenCamera.targetTexture = rt;
            Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            _screenCamera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            _screenCamera.targetTexture = null;
            RenderTexture.active = null;
            Destroy(rt);

            byte[] bytes = screenShot.EncodeToPNG();

            File.WriteAllBytes(ScreenShotName("Акт", "Судьи"), bytes);
            foreach (var obj in _objects)
            {
                obj.SetActive(true);
            }
            _screenCamera.gameObject.SetActive(false);
            _scrollView.verticalNormalizedPosition = value;
            _mask.enabled = true;
        }

        public string ScreenShotName(string name, string Path)
        {


            return string.Format("{0}/{1}/{2} - {3}.png",
                                 Application.dataPath,
                                 Path,
                                 name,
                                 System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));

        }
    }
}