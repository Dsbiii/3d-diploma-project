 using System;
 using UnityEngine;
 using System.Collections;
 using UnityEngine.UI;

 public class HiResScreenShots : MonoBehaviour
 {
    [SerializeField] private GameObject[] _akts;
    [SerializeField] private ScreenShootCanvas _screenShootCanvas;
    [SerializeField] private Camera _camera;
     public GameObject[] _Obj;

     public Camera[] _Cameras;

     public Mask[] _Mask;
     
     public int resWidth = 2550; 
     public int resHeight = 3300;
 
     private bool takeHiResShot = false;

     public string _Ref;
     private bool pl;
     
     private void Start()
     {
         System.IO.Directory.CreateDirectory( Application.dataPath + "/Судьи");
         System.IO.Directory.CreateDirectory( Application.dataPath + "/Пользователи");

     }

     public static string ScreenShotName(int width, int height, string Path) {
         
         
         return string.Format("{0}/{1}/Act_{2}x{3}_{4}_{5}.png", 
                              Application.dataPath,
                              Path,
                              width, height, 
                              System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"), UnityEngine.Random.Range(0f,1000f));
         
     }
 
     public void TakeHiResShot() {
         takeHiResShot = true;
     }


     public void Print()
     {
        gameObject.SetActive(true);
        foreach (var item in _akts)
            item.SetActive(false);
        foreach (var item in _akts)
        {
            item.SetActive(true);
            if (_screenShootCanvas.PrepareForScreenShoot())
            {

                for (int i = 0; i < _Obj.Length; i++)
                {
                    _Obj[i].SetActive(false);
                }
                //_Scrollbars[0].value = 1;
                //_Scrollbars[1].value = 1;
                _Cameras[0].enabled = false;
                _Cameras[1].enabled = true;

                _Mask[0].enabled = false;
                _Mask[1].enabled = false;

                RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
                _camera.targetTexture = rt;
                Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
                _camera.Render();
                RenderTexture.active = rt;
                screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
                _camera.targetTexture = null;
                RenderTexture.active = null; // JC: added to avoid errors
                Destroy(rt);
                byte[] bytes = screenShot.EncodeToPNG();
                string filename = ScreenShotName(resWidth, resHeight, _Ref);
                System.IO.File.WriteAllBytes(filename, bytes);
                Debug.Log(string.Format("Took screenshot to: {0}", filename));

                _Mask[0].enabled = true;
                _Mask[1].enabled = true;

                _Cameras[0].enabled = true;
                _Cameras[1].enabled = false;

                for (int i = 0; i < _Obj.Length; i++)
                {
                    _Obj[i].SetActive(true);
                }
            }
            item.SetActive(false);

        }
        gameObject.SetActive(false);
    }


}