using Assets.Scripts.Stages.SecondStage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportSelectObyDeystviy : MonoBehaviour
{
    public SecondStageController SecondStageController;
    public GameObject Panel;
    public GameObject[] _Cur;
    
    public int _CurInt;
    public int _ActivInt;
    private string _Select;
    
    
    public void Select()
    {
        for (int i = 0; i < Panel.transform.childCount; i++)
        {
            if (Panel.transform.GetChild(i).gameObject.activeSelf == true)
            {
                _Select = _Select + Panel.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text;
                _ActivInt++;
            }
            
        }

        for (int i = 0; i < _Cur.Length; i++)
        {
            if (_Cur[i].activeSelf == true)
            {
                _CurInt++;
            }
        }

        if (_ActivInt == 10 && _CurInt == 10)
        {
            SecondStageController.AddSecondStageExam("Обязательные действия перед ТП", "Правильно", "Отобрать только необходимые действия, перед осмотром ТП", 1, 0);
        }
        else
        {
            SecondStageController.AddSecondStageExam("Обязательные действия перед ТП", "Ошибка", "Отобрать только необходимые действия, перед осмотром ТП", 0, 0);

        }

        gameObject.SetActive(false);
    }


}
