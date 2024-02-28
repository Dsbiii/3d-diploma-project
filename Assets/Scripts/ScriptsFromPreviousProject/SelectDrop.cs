using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectDrop : MonoBehaviour
{
    public enum Phase
    {
        A,
        B,
        C
    }

    [SerializeField] private Phase _phase;
    [SerializeField] private bool _noCalculateSum;

    //public bool IsRight
    //{
    //    get
    //    {
    //        try
    //        {
    //            return _dropDown.options[_dropDown.value].text.Length > 0;
    //        }
    //        catch
    //        {
    //            return true;
    //        }
    //    }
    //}

    public bool IsRight
    {
        get
        {
            try
            {
                if (_any)
                    return true;
                return _dropDown.options[_dropDown.value].text.Length > 0;
                return _rightValue == _dropDown.options[_dropDown.value].text;
            }
            catch
            {
                return true;
            }
        }
    }
    public Type _Type;
    public Act _Act;
    [SerializeField] private string _rightValue;
    private Dropdown _dropDown;
    private bool _any;

    public Dropdown Dropdown => _dropDown;

    public enum Type
    {
        Kleshi,
        Tok,
        U,
        Uabc,
        F,
        Cos,
        Imp,
        Sec,
        P,
        Pa,
        PaIN,
        TokSila,
        FactPower,
        CalculatedSumPower,
        Error,
        LeadPlomb,
        PlombNumber
    }

    private void SetRightValue(string a , string b , string c)
    {

        switch (_phase)
        {
            case Phase.A:
                _rightValue = a;
                break;
            case Phase.B:
                _rightValue = b;
                break;
            case Phase.C:
                _rightValue = c;
                break;
        }
    }

    private void Awake()
    {
        _dropDown = GetComponent<Dropdown>();
    }

    public void Fill()
    {
        Start();
        GetComponent<Dropdown>().ClearOptions();
        GetComponent<Dropdown>().AddOptions(new List<Dropdown.OptionData> { new Dropdown.OptionData(_rightValue) });
        GetComponent<Dropdown>().value = 0;
        OnDropdownValueChanged();
    }

    void Start()
    {
        try
        {
            if (_Type == Type.LeadPlomb)
            {
                Dropdown dropdown = GetComponent<Dropdown>();
                dropdown.ClearOptions();
                var options = new List<Dropdown.OptionData>
                {
                    new Dropdown.OptionData(""),
                    new Dropdown.OptionData("Ѕумажна€"),
                    new Dropdown.OptionData("—винцова€"),
                    new Dropdown.OptionData("ѕластикова€"),
                };
                dropdown.AddOptions(options);
                //GetComponent<Dropdown>().options[4].text = "ѕластикова€";
                //GetComponent<Dropdown>().options[5].text = "ѕластикова€";

            }
            if (_Type == Type.PlombNumber)
            {
                _any = true;
                Dropdown dropdown = GetComponent<Dropdown>();
                dropdown.ClearOptions();
                var options = new List<Dropdown.OptionData>
                {
                    new Dropdown.OptionData(""),
                    new Dropdown.OptionData("JQ62841"),
                    new Dropdown.OptionData("JQ62823"),
                    new Dropdown.OptionData("JQ62845"),
                };
                dropdown.AddOptions(options);
                //GetComponent<Dropdown>().options[1].text = "JQ62841";
                //GetComponent<Dropdown>().options[2].text = "JQ62823";
                //GetComponent<Dropdown>().options[3].text = "JQ62845";

            }
            if (_Type == Type.FactPower)
            {
                float aPhase = (float.Parse(_Act.Instrumental._TokA) * float.Parse(_Act.Instrumental._Ua) * float.Parse(_Act.Instrumental._CosA)) / 1000;
                float bPhase = (float.Parse(_Act.Instrumental._TokB) * float.Parse(_Act.Instrumental._Ub) * float.Parse(_Act.Instrumental._CosB)) / 1000;
                float cPhase = (float.Parse(_Act.Instrumental._TokC) * float.Parse(_Act.Instrumental._Uc) * float.Parse(_Act.Instrumental._CosC)) / 1000;

                SetRightValue(aPhase.ToString(), bPhase.ToString(), cPhase.ToString());

                GetComponent<Dropdown>().options[1].text = System.Math.Round(aPhase, 3).ToString();
                GetComponent<Dropdown>().options[2].text = Random.Range(58, 67).ToString();
                GetComponent<Dropdown>().options[3].text = System.Math.Round(bPhase, 3).ToString();
                GetComponent<Dropdown>().options[4].text = Random.Range(58, 67).ToString();
                GetComponent<Dropdown>().options[5].text = System.Math.Round(cPhase, 3).ToString();
            }

            if (_Type == Type.Kleshi)
            {
                SetRightValue(_Act.Instrumental._KleshiA, _Act.Instrumental._KleshiB, _Act.Instrumental._KleshiC);
                GetComponent<Dropdown>().options[1].text = _Act.Instrumental._KleshiA;
                GetComponent<Dropdown>().options[2].text = Random.Range(58, 67).ToString();
                GetComponent<Dropdown>().options[3].text = _Act.Instrumental._KleshiB;
                GetComponent<Dropdown>().options[4].text = Random.Range(58, 67).ToString();
                GetComponent<Dropdown>().options[5].text = _Act.Instrumental._KleshiC;
            }

            if (_Type == Type.Tok)
            {
                SetRightValue("1.5мA", "1.0мA", "2.0мA");
                GetComponent<Dropdown>().options[1].text = "1.5мA";
                GetComponent<Dropdown>().options[2].text = Random.Range(1f, 3f).ToString("0.0") + "мA";
                GetComponent<Dropdown>().options[3].text = "1.0мA";
                GetComponent<Dropdown>().options[4].text = Random.Range(1f, 3f).ToString("0.0") + "мA";
                GetComponent<Dropdown>().options[5].text = "2.0мA";
            }
            if (_Type == Type.TokSila)
            {
                SetRightValue(_Act.Instrumental._TokA, _Act.Instrumental._TokB, _Act.Instrumental._TokC);
                GetComponent<Dropdown>().options[1].text = _Act.Instrumental._TokA + "A";
                GetComponent<Dropdown>().options[2].text = Random.Range(1f, 3f).ToString("0.0") + "A";
                GetComponent<Dropdown>().options[3].text = _Act.Instrumental._TokB + "A";
                GetComponent<Dropdown>().options[4].text = Random.Range(1f, 3f).ToString("0.0") + "A";
                GetComponent<Dropdown>().options[5].text = _Act.Instrumental._TokC + "A";
            }

            if (_Type == Type.U)
            {
                SetRightValue(_Act.Instrumental._Ua, _Act.Instrumental._Ub, _Act.Instrumental._Uc);
                GetComponent<Dropdown>().options[1].text = _Act.Instrumental._Ua;
                GetComponent<Dropdown>().options[2].text = Random.Range(220, 230).ToString();
                GetComponent<Dropdown>().options[3].text = _Act.Instrumental._Ub;
                GetComponent<Dropdown>().options[4].text = Random.Range(220, 230).ToString();
                GetComponent<Dropdown>().options[5].text = _Act.Instrumental._Uc;
            }
            if (_Type == Type.Uabc)
            {
                SetRightValue(_Act.Instrumental._Uab, _Act.Instrumental._Ubc, _Act.Instrumental._Uac);
                GetComponent<Dropdown>().options[1].text = _Act.Instrumental._Uab;
                GetComponent<Dropdown>().options[2].text = Random.Range(445, 450).ToString();
                GetComponent<Dropdown>().options[3].text = _Act.Instrumental._Ubc;
                GetComponent<Dropdown>().options[4].text = Random.Range(445, 450).ToString();
                GetComponent<Dropdown>().options[5].text = _Act.Instrumental._Uac;
            }
            if (_Type == Type.F)
            {

                if (_Act.Instrumental._Va == 1)
                {
                    GetComponent<Dropdown>().options[1].text = _Act.Instrumental._Fa + "L∞";
                }
                else
                {
                    GetComponent<Dropdown>().options[1].text = _Act.Instrumental._Fa + "C∞";
                }


                if (_Act.Instrumental._Fa == "0")
                {
                    GetComponent<Dropdown>().options[1].text = "";
                }

                GetComponent<Dropdown>().options[2].text = Random.Range(27, 40).ToString() + "L∞";
                if (_Act.Instrumental._Va == -1)
                {

                    GetComponent<Dropdown>().options[3].text = _Act.Instrumental._Fb + "C∞";
                }
                else
                {
                    GetComponent<Dropdown>().options[3].text = _Act.Instrumental._Fb + "L∞";
                }

                GetComponent<Dropdown>().options[4].text = Random.Range(27, 40).ToString() + "C∞";
                GetComponent<Dropdown>().options[5].text = _Act.Instrumental._Fc + "L∞";
            }

            if (_Type == Type.Cos)
            {
                if (_Act.Instrumental._CosA == "0")
                {
                    SetRightValue("0", _Act.Instrumental._CosB, _Act.Instrumental._CosC);
                    GetComponent<Dropdown>().options[1].text = "";
                }
                else
                {
                    SetRightValue(_Act.Instrumental._CosA, _Act.Instrumental._CosB, _Act.Instrumental._CosC);
                    GetComponent<Dropdown>().options[1].text = System.Math.Round(float.Parse(_Act.Instrumental._CosA), 3).ToString();
                }

                GetComponent<Dropdown>().options[2].text = Random.Range(0.7f, 0.10f).ToString("0.0");
                GetComponent<Dropdown>().options[3].text = System.Math.Round(float.Parse(_Act.Instrumental._CosB), 3).ToString();
                GetComponent<Dropdown>().options[4].text = Random.Range(0.7f, 0.10f).ToString("0.0");
                GetComponent<Dropdown>().options[5].text = System.Math.Round(float.Parse(_Act.Instrumental._CosC), 3).ToString();
            }
            if (_Type == Type.Imp)
            {
                _rightValue = _Act.Instrumental._Cout_Inp;
                GetComponent<Dropdown>().options[1].text = _Act.Instrumental._Cout_Inp;
                GetComponent<Dropdown>().options[2].text = Random.Range(5, 25).ToString();
                GetComponent<Dropdown>().options[3].text = Random.Range(6, 20).ToString();
                GetComponent<Dropdown>().options[4].text = Random.Range(6, 39).ToString();
                GetComponent<Dropdown>().options[5].text = Random.Range(6, 24).ToString();
            }
            if (_Type == Type.CalculatedSumPower)
            {
                float w = (float)System.Math.Round(_Act.Instrumental.W / 1000, 2);

                _rightValue = w.ToString();
                GetComponent<Dropdown>().options[1].text = "0,48к¬т";
                GetComponent<Dropdown>().options[2].text = "0,68к¬т";
                GetComponent<Dropdown>().options[3].text = w.ToString() + "к¬т";

            }

            if (_Type == Type.Sec)
            {
                GetComponent<Dropdown>().options[1].text = Random.Range(5, 13).ToString();
                GetComponent<Dropdown>().options[2].text = Random.Range(5, 13).ToString();
                GetComponent<Dropdown>().options[3].text = Random.Range(5, 13).ToString();
                GetComponent<Dropdown>().options[4].text = Random.Range(5, 13).ToString();
                float t = (float)System.Math.Round(2.88f / (_Act.Instrumental.W / 1000), 2);
                GetComponent<Dropdown>().options[5].text = t.ToString();

                _rightValue = t.ToString();
            }
            if (_Type == Type.P)
            {
                SetRightValue(_Act.Instrumental._PA, _Act.Instrumental._PB, _Act.Instrumental._PC);
                GetComponent<Dropdown>().options[1].text = Random.Range(10, 60).ToString();
                GetComponent<Dropdown>().options[2].text = _Act.Instrumental._PA;
                GetComponent<Dropdown>().options[3].text = Random.Range(10, 60).ToString();
                GetComponent<Dropdown>().options[4].text = _Act.Instrumental._PB;
                GetComponent<Dropdown>().options[5].text = _Act.Instrumental._PC;
            }

            if (_Type == Type.Pa)
            {
                GetComponent<Dropdown>().options[1].text = Random.Range(555f, 600f).ToString();
                GetComponent<Dropdown>().options[2].text = _Act.Instrumental._PaA;
                GetComponent<Dropdown>().options[3].text = Random.Range(555f, 600f).ToString();
                GetComponent<Dropdown>().options[4].text = Random.Range(555f, 600f).ToString();
                GetComponent<Dropdown>().options[5].text = Random.Range(555f, 600f).ToString();
                _rightValue = _Act.Instrumental._PaA;
            }
            if (_Type == Type.Error)
            {
                GetComponent<Dropdown>().options[1].text = "0,65";
                GetComponent<Dropdown>().options[2].text = "1,85";
                GetComponent<Dropdown>().options[3].text = "5,065";
                GetComponent<Dropdown>().options[4].text = "3,42";
                string error = System.Math.Round(float.Parse(_Act.Instrumental._Error), 3).ToString();
                GetComponent<Dropdown>().options[5].text = error;
                _rightValue = error;
            }


            if (_Type == Type.PaIN)
            {

                float r = float.Parse(_Act.Instrumental._Cout_Inp);
                float t = float.Parse(_Act.Instrumental._Sec_Time);

                float Pinp = (3600 * r) / (1250 * t);


                GetComponent<Dropdown>().options[1].text = Random.Range(1.3f, 1.6f).ToString();
                GetComponent<Dropdown>().options[2].text = Random.Range(1.3f, 1.6f).ToString();
                GetComponent<Dropdown>().options[3].text = Random.Range(1.3f, 1.6f).ToString();
                GetComponent<Dropdown>().options[4].text = Pinp.ToString();
                GetComponent<Dropdown>().options[5].text = Random.Range(1.3f, 1.6f).ToString();
                _rightValue = Pinp.ToString();

            }
            if (_Type == Type.LeadPlomb || _Type == Type.PlombNumber)
                return;

            GetComponent<Dropdown>().ClearOptions();
            GetComponent<Dropdown>().AddOptions(new List<Dropdown.OptionData> { new Dropdown.OptionData(_rightValue) });
            GetComponent<Dropdown>().value = 0;
            OnDropdownValueChanged();
        }
        catch
        {

        }
        //transform.GetChild(0).GetComponent<Text>().text = _rightValue.ToString();
    }

    public void OnDropdownValueChanged()
    {
        // ѕолучаем выбранный текст из Dropdown
        string selectedText = GetComponent<Dropdown>().options[GetComponent<Dropdown>().value].text;

        // ¬ыводим информацию о выбранной опции (можете изменить логику в соответствии с вашими нуждами)
    }

}
