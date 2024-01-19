using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectVector : MonoBehaviour
{
    [SerializeField] private MeteringParma _meteringParma;

    public Dropdown DD;
    public Image _Ia;
    public Image _Ib;
    public Image _Ic;


    float b;
    float Ib;
    float c;
    float Ic;
    float a;
    float Ia;
    public  Act _Act;

    public int[] Ar;
    public int[] Br;
    public int[] Cr;




    void Start()
    {     
        
        b = -30f;
        Ib = b += float.Parse(_Act.Instrumental._Fb) * -1;
        c = 30f;
        Ic = c += float.Parse(_Act.Instrumental._Fc) * -1;
        
        if (_Act.Instrumental._Va == -2)
        {
            a = float.Parse(_Act.Instrumental._Fa) * 1;
        }
        else
        {
            a = float.Parse(_Act.Instrumental._Fa) * -1;
        }
        
        float Ta = 0f;
        Ia = Ta += a;
        for (int i = 0; i < Ar.Length; i++)
        {
            Ar[i] = Random.Range(-117, 0);
            Br[i] = Random.Range(-150, -30);
            Cr[i] = Random.Range(30, -50);
        }

    }

    private void Update()
    {
        if (DD.value == 0)
        {
            _Ia.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
            _Ib.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -30);
            _Ic.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 30);
        }

        if (DD.value == 1)
        {
            _Ia.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Ar[0]);
            _Ib.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Br[0]);
            _Ic.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Cr[0]);
        }
        if (DD.value == 2)
        {
            //_Ia.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -_meteringParma.Phase_A[2]);
            //_Ib.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 90 - _meteringParma.Phase_B[2]);
            //_Ic.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -(90 + _meteringParma.Phase_C[2]));
            if (Ia == 0)
            {
                _Ia.enabled = false;
            }
            else
            {
                _Ia.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Ia);
            }
            _Ib.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Ib);
            _Ic.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Ic);
        }
        if (DD.value == 3)
        {
            _Ia.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Ar[1]);
            _Ib.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Br[1]);
            _Ic.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Cr[1]);
        }
        if (DD.value == 4)
        {
            _Ia.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Ar[2]);
            _Ib.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Br[2]);
            _Ic.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Cr[2]);
        }
    }

}
