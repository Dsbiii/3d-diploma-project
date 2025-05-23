using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenReport : MonoBehaviour
{
    [SerializeField] private float _scrolbarValueScroll;
    [SerializeField] private bool _isOnce;
    public Camera Camera;

    public GameObject[] _Obj;

    public Camera[] _Cameras;

    public Mask[] _Mask;

    public Scrollbar[] _Scrollbars;

    public int resWidth = 2550;
    public int resHeight = 3300;


    private bool takeHiResShot = false;

    public string _Ref;
    private bool pl;

    private void Start()
    {
        System.IO.Directory.CreateDirectory(Application.dataPath + "/�����");
        System.IO.Directory.CreateDirectory(Application.dataPath + "/������������");

    }

    public static string ScreenShotName(int width, int height, string Path)
    {


        return string.Format("{0}/{1}/Act_{2}x{3}_{4}.png",
                             Application.dataPath,
                             Path,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));

    }

    public void TakeHiResShot()
    {
        takeHiResShot = true;
    }


    public void Print()
    {
        _Scrollbars[0].value = 1f;

        for (int i = 0; i < _Obj.Length; i++)
        {
            _Obj[i].SetActive(false);
        }

        _Cameras[0].enabled = false;
        _Cameras[1].enabled = true;


        //_Cameras[0].enabled = false;
        //_Cameras[1].enabled = true;

        _Mask[0].enabled = false;

        if (!_isOnce)
        {
            for (float i = _Scrollbars[0].value; i > 0; i -= _scrolbarValueScroll)
            {
                _Scrollbars[0].value = i;

                RenderTexture rt1 = new RenderTexture(Screen.width, Screen.height, 24);
                Camera.targetTexture = rt1;
                Texture2D screenShot1 = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
                Camera.Render();
                RenderTexture.active = rt1;
                screenShot1.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
                Camera.targetTexture = null;
                RenderTexture.active = null; // JC: added to avoid errors
                Destroy(rt1);
                byte[] bytes1 = screenShot1.EncodeToPNG();
                string filename1 = ScreenShotName(resWidth, resHeight, _Ref);
                System.IO.File.WriteAllBytes(filename1, bytes1);
                Debug.Log(string.Format("Took screenshot to: {0}", filename1));
            }
        }


        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        Camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        Camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        Camera.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = ScreenShotName(resWidth, resHeight, _Ref);
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));


        _Mask[0].enabled = true;


        _Cameras[0].enabled = true;
        _Cameras[1].enabled = false;

        for (int i = 0; i < _Obj.Length; i++)
        {
            _Obj[i].SetActive(true);
        }


    }
}
