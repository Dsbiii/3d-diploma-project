using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Stages.FifthStage
{
    [ExecuteInEditMode]
    public class ResizePortPanels : MonoBehaviour
    {
        private void OnEnable()
        {
            transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().fontSize = 19;

            //transform.GetChild(0).transform.GetChild(0).GetComponent<RectTransform>().sizeDelta =
            //    new Vector2(transform.GetChild(0).transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x,
            //    30);
            //transform.GetChild(1).transform.GetChild(0).GetComponent<RectTransform>().sizeDelta =
            //    new Vector2(transform.GetChild(1).transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x,
            //    30);
            //transform.GetChild(2).transform.GetChild(1).GetComponent<RectTransform>().sizeDelta =
            //    new Vector2(transform.GetChild(2).transform.GetChild(1).GetComponent<RectTransform>().sizeDelta.x,
            //    30);
            //transform.GetChild(3).transform.GetChild(0).GetComponent<RectTransform>().sizeDelta =
            //    new Vector2(transform.GetChild(3).transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x,
            //    30);

            Debug.Log("Enabled");
        }
    }
}