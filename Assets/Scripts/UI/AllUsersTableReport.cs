using Assets.Scripts.AdminPanel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AllUsersTableReport : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private float _scrolbarValueScroll;
    [SerializeField] private Transform _content;
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
        System.IO.Directory.CreateDirectory(Application.dataPath + "/Судьи");
        System.IO.Directory.CreateDirectory(Application.dataPath + "/Пользователи");

    }
    public static string ScreenShotName(string name, string Path)
    {


        return string.Format("{0}/{1}/Act_{2}_{3}.png",
                             Application.dataPath,
                             Path,
                             name,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));

    }

    public void TakeHiResShot()
    {
        takeHiResShot = true;
    }

    public void Print()
    {
        StartCoroutine(PrintCoroutine());
    }

    public IEnumerator PrintCoroutine()
    {
        float rectNormalizedY = _scrollRect.verticalNormalizedPosition;
        _scrollRect.verticalNormalizedPosition = 1;
        yield return new WaitForEndOfFrame();

        foreach (var obj in _Obj)
        {
            obj.SetActive(false);
        }

        _Cameras[0].enabled = false;
        _Cameras[1].enabled = true;

        _Mask[0].enabled = false;

        List<UserItem> userItems = _content.GetComponentsInChildren<UserItem>().ToList();
        List<UserItem> userItems2 = _content.GetComponentsInChildren<UserItem>().ToList();

        Debug.Log("userItems.Count " + userItems.Count);
        while (userItems.Count > 0)
        {
            RenderTexture rt1 = new RenderTexture(Screen.width, Screen.height, 24);
            Camera.targetTexture = rt1;
            Texture2D screenShot1 = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            Camera.Render();
            RenderTexture.active = rt1;
            screenShot1.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            Camera.targetTexture = null;
            RenderTexture.active = null;
            Destroy(rt1);
            byte[] bytes1 = screenShot1.EncodeToPNG();
            string filename1 = ScreenShotName(userItems.Count.ToString(), _Ref);
            System.IO.File.WriteAllBytes(filename1, bytes1);

            Debug.Log($"Took screenshot to: {filename1}");

            for (int i = 0; i < Mathf.Min(7, userItems.Count); i++)
            {
                if (userItems[i] != null)
                {
                    userItems[i].gameObject.SetActive(false);
                }
            }
            userItems = userItems.Skip(Mathf.Min(7, userItems.Count)).ToList();

            // Даем время на обновление кадра после деактивации объектов
            yield return null;
        }

        foreach (var item in _content.GetComponentsInChildren<UserItem>())
            item.gameObject.SetActive(true);

        _Mask[0].enabled = true;

        _Cameras[0].enabled = true;
        _Cameras[1].enabled = false;

        foreach (var obj in _Obj)
        {
            obj.SetActive(true);
        }

        foreach (var item in userItems2)
            item.gameObject.SetActive(true);

        _scrollRect.verticalNormalizedPosition = rectNormalizedY;
    }
}
