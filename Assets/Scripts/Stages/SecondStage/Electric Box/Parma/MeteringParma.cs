using Assets.Scripts.Stages.SecondStage.Dismantling.Install_screw;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class PillerPhase
{
    [SerializeField] private MeshRenderer[] _items;
    [SerializeField] private GameObject _piller;

    public bool IsActivePiller => _piller.activeSelf;
    public int ActiveItems => _items.Where(item => item.enabled).ToArray().Length;
    public int ItemsCount => _items.Length;

    public bool CheckToActiveItems()
    {
        if (_items.Where(item => item.enabled).ToArray().Length > 0)
            return true;
        return false;
    }
}

[System.Serializable]
public class ProbePhase
{
    [SerializeField] private GameObject[] _probesOnPhases;

    public bool IsOnPhase => _probesOnPhases.Where(item => item.activeSelf).ToArray().Length > 0;
}

public class MeteringParma : MonoBehaviour
{
    [SerializeField] private GameObject[] _pliersOnIkk;
    [SerializeField] private Stopwatch _watch;
    [SerializeField] private GameObject[] _probes;
    [SerializeField] private ProbePhase[] _probesOnPhases;
    [SerializeField] private PillerPhase[] _pillerCounterPhases;
    
    [SerializeField] private GameObject _redProbe;
    [SerializeField] private GameObject _kleshi;
    [SerializeField] private GameObject[] _probesCounter;
    [SerializeField] private GameObject[] _probesIKK;
    [SerializeField] private MeshRenderer[] _ikkJumpers;
    [SerializeField] private GameObject[] _pliersCounter;
    [SerializeField] private GameObject[] _pliersIKK;
    [SerializeField] private GameObject[] _earthPhase;


    [SerializeField] private Screw[] _screws;
    [SerializeField] private Cable[] _cables;
    public string MeteringBlack1;
    public string MeteringBlack2;
    public string MeteringRed;
    public string Kleshi;

    public string FirstBlackProbe;
    public string SecondBlackProbe;
    public string RedProbe;
    public string Plier;

    public bool Nesootvetstvie_Toka;
    public bool Obratnoe_Cheredovanie_Faz;
    public bool Vstrechnoe_Vklyuchenie_Transformatorov_Toka;
    public bool Kontakty_Transformatorov;
    public bool Kontakty_Transformatorov_On;
    public bool Podklyuchenie_Dvuh_Transformatorov_Toka;
    public bool Nalichie_Zakorotok;
    public bool Obryv_Vnutrennih_Cepej_Napryazheniya;
    public bool Obryv_Vnutrennih_Cepej_Toka;

    public bool Probe_3;
    public bool GloreFail;
    public bool GloreUpload;
    public bool Probe_On;
    public bool Clamp_On;

    public List<string> _Сorrect = new List<string>();
    public int N;

    public float[] Phase_A = new float[4]; // 0 = U - напряжения, 1 =  I - сила Тока, 2 = Ф(F) - угол между ними, 3 = f(H) - частота
    public float[] Phase_B = new float[4];
    public float[] Phase_C = new float[4];

    public float Pa_A;
    public float Pa_B;
    public float Pa_C;


    public GameObject[] _Clamp;

    public float Pr_A;
    public float Pr_B;
    public float Pr_C;
    
    public float _Uab;
    public float _Ubc;
    public float _Uac;
    
    public float Gradus_Kont;

    private float _amperageA;
    private float _amperageB;
    private float _amperageC;

    public float W;
    public float X;
    public float P;

    public string _ActionParma;
    public string _ActionClanp;
    public TMP_Text _Info;
    public int[] Demo = new[] {0, 3, 4, 7};
    [SerializeField] private TMP_Text _tablo;
    [SerializeField] private Act _act;
    [SerializeField] private Setting _setting;

    public Act Act => _act;
    public bool IsUssed { get; private set; }
    public bool IsUssedStopWatch { get; private set; }
    public bool IsUssedClamps { get; private set; }
    public bool IsTakedPiller { get; set; }

    public string Tablo
    {
        set
        {
            _tablo.text = value;
        }
    }

    public void ResetUsingParma()
    {
        IsUssed = false;
        IsUssedStopWatch = false;
        IsUssedClamps = false;
    }

    public void ResetParma()
    {
        MeteringBlack1 = "";
        MeteringBlack2 = "";
        MeteringRed = "";
        Kleshi = "";
        FirstBlackProbe = "";
        SecondBlackProbe = "";
        RedProbe = "";
    }


    private bool CalculatePillersCounter()
    {

        for (int i = 0; i < _pillerCounterPhases.Length; i++)
        {
            if(!IsTakedPiller && _redProbe.activeSelf && _earthPhase[0].activeSelf && _pillerCounterPhases[i].ActiveItems == 1
                && _pillerCounterPhases[i].ItemsCount == 2 && _pillerCounterPhases[i].IsActivePiller)
            {
                Tablo = "           " + (Phase_A[3] / 3).ToString("0.0") + "Hz     " + "0V\n        " + "                    " + (Phase_A[1] / 3).ToString("0.0") + "A";
                return true;
            }else if (!IsTakedPiller && _redProbe.activeSelf && _earthPhase[0].activeSelf && _pillerCounterPhases[i].CheckToActiveItems() && _pillerCounterPhases[i].IsActivePiller)
            {
                Tablo = "                         0,V\n                       0mA";
                return true;
            }
            else if (!IsTakedPiller && _redProbe.activeSelf && _earthPhase[0].activeSelf && !_pillerCounterPhases[i].CheckToActiveItems() && _pillerCounterPhases[i].IsActivePiller)
            {
                Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + "0V\n        " + "                    " + Phase_A[1].ToString("0.0") + "A";
                return true;
            }
        }
        return false;
    }

   
    private bool CalculatePillersIKK()
    {

        if (!IsTakedPiller && _redProbe.activeSelf && _earthPhase[0].activeSelf && (_pliersIKK[0].activeSelf || _pliersIKK[1].activeSelf))
        {
            Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + "0V\n        " + "                    " + _amperageA.ToString("0.0") + "A";
            return true;
        }
        if (!IsTakedPiller && _redProbe.activeSelf && _earthPhase[0].activeSelf && (_pliersIKK[2].activeSelf || _pliersIKK[3].activeSelf))
        {
            Tablo = "           " + Phase_B[3].ToString("0.0") + "Hz     " + "0V\n        " + "                    " + _amperageB.ToString("0.0") + "A";
            return true;
        }
        if (!IsTakedPiller && _redProbe.activeSelf && _earthPhase[0].activeSelf && (_pliersIKK[4].activeSelf || _pliersIKK[5].activeSelf))
        {
            Tablo = "           " + Phase_C[3].ToString("0.0") + "Hz     " + "0V\n        " + "                    " + _amperageC.ToString("0.0") + "A";
            return true;
        }


        //if (!IsTakedPiller && (_pliersIKK[0].activeSelf || _pliersIKK[1].activeSelf))
        //{
        //    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + "0V\n        " + "                    " + Phase_A[1].ToString("0.0") + "A";
        //    return true;
        //}
        //if (!IsTakedPiller && (_pliersIKK[2].activeSelf || _pliersIKK[3].activeSelf))
        //{
        //    Tablo = "           " + Phase_B[3].ToString("0.0") + "Hz     " + "0V\n        " + "                    " + Phase_B[1].ToString("0.0") + "A";
        //    return true;
        //}
        //if (!IsTakedPiller && (_pliersIKK[4].activeSelf || _pliersIKK[5].activeSelf))
        //{
        //    Tablo = "           " + Phase_C[3].ToString("0.0") + "Hz     " + "0V\n        " + "                    " + Phase_C[1].ToString("0.0") + "A";

        //    return true;
        //}
        return false;
    }

    private void CalculateAmperange()
    {
        if (_ikkJumpers[0].enabled)
        {
            Phase_A[1] = _amperageA;
        }
        else
        {
            Phase_A[1] = 0;
        }

        if (_ikkJumpers[1].enabled)
        {
            Phase_B[1] = _amperageB;
        }
        else
        {
            Phase_B[1] = 0;
        }

        if (_ikkJumpers[2].enabled)
        {
            Phase_C[1] = _amperageC;
        }
        else
        {
            Phase_C[1] = 0;
        }
    }


    private bool CalculateProbeCounter()
    {
        if(!IsTakedPiller && _kleshi.activeSelf && _earthPhase[0].activeSelf && _probesCounter[1].activeSelf && _ikkJumpers[0].enabled)
        {
            Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[0].ToString("0.0") + "V\n        " + "                    " + "0A";

            return true;
        }
        if (!IsTakedPiller && _kleshi.activeSelf && _earthPhase[0].activeSelf && _probesCounter[4].activeSelf && _ikkJumpers[1].enabled)
        {
            Tablo = "           " + Phase_B[3].ToString("0.0") + "Hz     " + Phase_B[0].ToString("0.0") + "V\n        " + "                    " + "0A";

            return true;
        }
        if (!IsTakedPiller && _kleshi.activeSelf && _earthPhase[0].activeSelf && _probesCounter[7].activeSelf && _ikkJumpers[2].enabled)
        {
            Tablo = "           " + Phase_C[3].ToString("0.0") + "Hz     " + Phase_C[0].ToString("0.0") + "V\n        " + "                    " + "0A";

            return true;
        }


        if (!IsTakedPiller && _kleshi.activeSelf && _earthPhase[0].activeSelf && _probesCounter[1].activeSelf && !_ikkJumpers[0].enabled)
        {
            Tablo = "                         0,V\n                       0mA"; Debug.Log("фаза А");
            return true;
        }
        if (!IsTakedPiller && _kleshi.activeSelf && _earthPhase[0].activeSelf && _probesCounter[4].activeSelf && !_ikkJumpers[1].enabled)
        {
            Tablo = "                         0,V\n                       0mA"; Debug.Log("фаза А");
            return true;
        }
        if (!IsTakedPiller && _kleshi.activeSelf && _earthPhase[0].activeSelf && _probesCounter[7].activeSelf && !_ikkJumpers[2].enabled)
        {
            Tablo = "                         0,V\n                       0mA"; Debug.Log("фаза А");
            return true;
        }
        return false;
    }

    private bool CalculateProbeIKK()
    {
        if (!IsTakedPiller && _kleshi.activeSelf && _earthPhase[0].activeSelf  && _probesIKK[0].activeSelf)
        {
            Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[0].ToString("0.0") + "V\n        " + "                    " + "0A";
            return true;
        }
        if (!IsTakedPiller && _kleshi.activeSelf && _earthPhase[0].activeSelf && _probesIKK[1].activeSelf)
        {
            Tablo = "           " + Phase_B[3].ToString("0.0") + "Hz     " + Phase_B[0].ToString("0.0") + "V\n        " + "                    " + "0A";
            return true;
        }
        if (!IsTakedPiller && _kleshi.activeSelf && _earthPhase[0].activeSelf && _probesIKK[2].activeSelf)
        {
            Tablo = "           " + Phase_C[3].ToString("0.0") + "Hz     " + Phase_C[0].ToString("0.0") + "V\n        " + "                    " + "0A";
            return true;
        }

        return false;
    }

    private void Awake()
    {
        //Nesootvetstvie_Toka = _setting.Nesootvetstvie_Toka;

        //Vstrechnoe_Vklyuchenie_Transformatorov_Toka = _setting.Vstrechnoe_Vklyuchenie_Transformatorov_Toka;

        //Kontakty_Transformatorov = _setting.Kontakty_Transformatorov;

        //Podklyuchenie_Dvuh_Transformatorov_Toka = _setting.Podklyuchenie_Dvuh_Transformatorov_Toka;

        //Nalichie_Zakorotok = _setting.Nalichie_Zakorotok;

        //Obryv_Vnutrennih_Cepej_Toka = _setting.Obryv_Vnutrennih_Cepej_Toka;

        //Obryv_Vnutrennih_Cepej_Napryazheniya = _setting.Obryv_Vnutrennih_Cepej_Napryazheniya;

        //Obratnoe_Cheredovanie_Faz = _setting.Obratnoe_Cheredovanie_Faz;
       
        
        
        
        //N = Demo[Random.Range(0,Demo.Length)];
        // N = Random.Range(0,7);

        _act.Instrumental._Va = 1;
      
        
        

        int U = Random.Range(209, 237);
        float I = Mathf.Round(Random.Range(1.5f, 4.5f));
        int F = Random.Range(5, 40);
        float H = Random.Range(49.8f, 50.3f);

        Phase_A[0] = U;
        Phase_A[1] = I;
        Phase_A[2] = F;
        Phase_A[3] = H;

        Phase_B[0] = U + U / 100 * 2;
        Phase_B[1] = I + I / 100 * 5;
        Phase_A[2] = Random.Range(5, 40);
        //Phase_B[2] = F + F / 100.0f * 20;
        Phase_B[3] = H;

        Phase_C[0] = U + U / 100 * -2;
        Phase_C[1] = I + I / 100 * -5;
        Phase_A[2] = Random.Range(5, 40);
        //Phase_C[2] = F + F / 100.0f * -20;
        Phase_C[3] = H;

        float Cos_A = Mathf.Cos(Phase_A[2] * Mathf.Deg2Rad);
        float Cos_B = Mathf.Cos(Phase_B[2] * Mathf.Deg2Rad);
        float Cos_C = Mathf.Cos(Phase_C[2] * Mathf.Deg2Rad);

        float Sin_A = Mathf.Sin(Phase_A[2] * Mathf.Deg2Rad);
        float Sin_B = Mathf.Sin(Phase_B[2] * Mathf.Deg2Rad);
        float Sin_C = Mathf.Sin(Phase_C[2] * Mathf.Deg2Rad);



        Pa_A = Phase_A[1] * Phase_A[0] * Cos_A;
        Pa_B = Phase_B[1] * Phase_B[0] * Cos_B;
        Pa_C = Phase_C[1] * Phase_C[0] * Cos_C;

        Pr_A = Phase_A[1] * Phase_A[0] * Sin_A;
        Pr_B = Phase_B[1] * Phase_B[0] * Sin_B;
        Pr_C = Phase_C[1] * Phase_C[0] * Sin_C;

        _Uab = Random.Range(360 , 390); _Ubc = Random.Range(360 , 390);
        _Uac = Random.Range(360 , 390);
        
        
        
        Malfunctions();
        //Phase_A[2] = F;
        //Phase_A[2] = Random.Range(5, 40);
        //Phase_A[2] = Random.Range(5, 40);
        Debug.Log("Malfunctions");
    }


    private void Update()
    {

        CalculateAmperange();
        _Сorrect[0] = MeteringBlack1;
        _Сorrect[1] = MeteringBlack2;
        _Сorrect[2] = MeteringRed;
        _Сorrect[3] = Kleshi;
        _Сorrect.Sort();

        if (CalculatePillersCounter())
            return;
        if (CalculatePillersIKK())
            return;
        if (CalculateProbeCounter())
            return;
        if (CalculateProbeIKK())
            return;

        if (RedProbe == "8" && FirstBlackProbe == "2" && !_ikkJumpers[0].enabled||
                RedProbe == "8" && FirstBlackProbe == "2" && !_ikkJumpers[2].enabled ||

                RedProbe == "8" && FirstBlackProbe == "5" && !_ikkJumpers[1].enabled ||
                RedProbe == "8" && FirstBlackProbe == "5" && !_ikkJumpers[2].enabled ||

                RedProbe == "2" && FirstBlackProbe == "5" && !_ikkJumpers[0].enabled ||
                RedProbe == "2" && FirstBlackProbe == "5" && !_ikkJumpers[1].enabled ||

                RedProbe == "2" && FirstBlackProbe == "8" && !_ikkJumpers[0].enabled ||
                RedProbe == "2" && FirstBlackProbe == "8" && !_ikkJumpers[2].enabled ||


                RedProbe == "5" && FirstBlackProbe == "2" && !_ikkJumpers[0].enabled ||
                RedProbe == "5" && FirstBlackProbe == "2" && !_ikkJumpers[1].enabled ||

                RedProbe == "5" && FirstBlackProbe == "8" && !_ikkJumpers[1].enabled ||
                RedProbe == "5" && FirstBlackProbe == "8" && !_ikkJumpers[2].enabled)
        {
            Tablo = "                         0,V\n                       0mA";
            return;
        }

        if (RedProbe == "10" && FirstBlackProbe == "2" && !_ikkJumpers[0].enabled ||

        RedProbe == "10" && FirstBlackProbe == "5" && !_ikkJumpers[1].enabled ||

        RedProbe == "10" && FirstBlackProbe == "8" && !_ikkJumpers[2].enabled)
        {
            Tablo = "                         0,V\n                       0mA";
            return;
        }

        if(_pliersOnIkk[0].activeSelf || _pliersOnIkk[1].activeSelf)
        {
            Phase_A[1] = _amperageA;
        }
        if (_pliersOnIkk[2].activeSelf || _pliersOnIkk[3].activeSelf)
        {
            Phase_B[1] = _amperageB;
        }
        if (_pliersOnIkk[4].activeSelf || _pliersOnIkk[5].activeSelf)
        {
            Phase_C[1] = _amperageC;
        }


        if (Plier == "1" && _pillerCounterPhases[3].ActiveItems == 1 && !_pliersOnIkk[0].activeSelf)
        {
            Tablo = "           " + (Phase_A[3] / 3).ToString("0.0") + "Hz     " + "0V\n        " + "                    " + (Phase_A[1] / 3).ToString("0.0") + "A";
            return;
        }
        else if (Plier == "1" && _pillerCounterPhases[3].ActiveItems == 2 && !_pliersOnIkk[0].activeSelf)
        {
            Tablo = "                         0,V\n                       0mA";
            return;
        }

        if (Plier == "3" && _pillerCounterPhases[4].ActiveItems == 1 && !_pliersOnIkk[1].activeSelf)
        {
            Tablo = "           " + (Phase_A[3] / 3).ToString("0.0") + "Hz     " + "0V\n        " + "                    " + (Phase_A[1] / 3).ToString("0.0") + "A";
            return;
        }
        else if (Plier == "3" && _pillerCounterPhases[4].ActiveItems == 2 && !_pliersOnIkk[1].activeSelf)
        {
            Tablo = "                         0,V\n                       0mA";
            return;
        }

        if (Plier == "4" && _pillerCounterPhases[5].ActiveItems == 1 && !_pliersOnIkk[2].activeSelf)
        {
            Tablo = "           " + (Phase_B[3] / 3).ToString("0.0") + "Hz     " + "0V\n        " + "                    " + (Phase_B[1] / 3).ToString("0.0") + "A";
            return;
        }
        else if (Plier == "4" && _pillerCounterPhases[5].ActiveItems == 2 && !_pliersOnIkk[2].activeSelf)
        {
            Tablo = "                         0,V\n                       0mA";
            return;
        }

        if (Plier == "6" && _pillerCounterPhases[6].ActiveItems == 1 && !_pliersOnIkk[3].activeSelf)
        {
            Tablo = "           " + (Phase_B[3] / 3).ToString("0.0") + "Hz     " + "0V\n        " + "                    " + (Phase_B[1] / 3).ToString("0.0") + "A";
            return;
        }
        else if (Plier == "6" && _pillerCounterPhases[6].ActiveItems == 2 && !_pliersOnIkk[3].activeSelf)
        {
            Tablo = "                         0,V\n                       0mA";
            return;
        }

        if (Plier == "7" && _pillerCounterPhases[7].ActiveItems == 1 && !_pliersOnIkk[4].activeSelf)
        {
            Tablo = "           " + (Phase_C[3] / 3).ToString("0.0") + "Hz     " + "0V\n        " + "                    " + (Phase_C[1] / 3).ToString("0.0") + "A";
            return;
        }
        else if (Plier == "7" && _pillerCounterPhases[7].ActiveItems == 2 && !_pliersOnIkk[4].activeSelf)
        {
            Tablo = "                         0,V\n                       0mA";
            return;
        }

        if (Plier == "8" && _pillerCounterPhases[8].ActiveItems == 1 && !_pliersOnIkk[5].activeSelf)
        {
            Tablo = "           " + (Phase_C[3] / 3).ToString("0.0") + "Hz     " + "0V\n        " + "                    " + (Phase_C[1] / 3).ToString("0.0") + "A";
            return;
        }
        else if (Plier == "8" && _pillerCounterPhases[8].ActiveItems == 2 && !_pliersOnIkk[5].activeSelf)
        {
            Tablo = "                         0,V\n                       0mA";
            return;
        }


        Tablo = "                         0,V\n                       0mA";


        //if (CalculatePillersCounter() || CalculatePillersIKK() || CalculateProbeCounter() || CalculateProbeIKK())
        //return;

        if ( _probesOnPhases[0].IsOnPhase && RedProbe != "" && FirstBlackProbe == "" && SecondBlackProbe == "" && Probe_3 ||
            _probesOnPhases[0].IsOnPhase && RedProbe == "" && FirstBlackProbe != "" && SecondBlackProbe == "" && Probe_3||
            _probesOnPhases[0].IsOnPhase && RedProbe == "" && FirstBlackProbe == "" && SecondBlackProbe != "" && Probe_3||
            _probesOnPhases[0].IsOnPhase && RedProbe != "" && FirstBlackProbe != "" && SecondBlackProbe == "" && Probe_3||
            _probesOnPhases[0].IsOnPhase && RedProbe != "" && FirstBlackProbe == "" && SecondBlackProbe != "" && Probe_3||
            _probesOnPhases[0].IsOnPhase && RedProbe == "" && FirstBlackProbe != "" && SecondBlackProbe != "" && Probe_3||

            _probesOnPhases[1].IsOnPhase && RedProbe != "" && FirstBlackProbe == "" && SecondBlackProbe == "" && Probe_3||
            _probesOnPhases[1].IsOnPhase && RedProbe == "" && FirstBlackProbe != "" && SecondBlackProbe == "" && Probe_3||
            _probesOnPhases[1].IsOnPhase && RedProbe == "" && FirstBlackProbe == "" && SecondBlackProbe != "" && Probe_3||
            _probesOnPhases[1].IsOnPhase && RedProbe != "" && FirstBlackProbe != "" && SecondBlackProbe == "" && Probe_3||
            _probesOnPhases[1].IsOnPhase && RedProbe != "" && FirstBlackProbe == "" && SecondBlackProbe != "" && Probe_3||
            _probesOnPhases[1].IsOnPhase && RedProbe == "" && FirstBlackProbe != "" && SecondBlackProbe != "" && Probe_3||

            _probesOnPhases[2].IsOnPhase && RedProbe != "" && FirstBlackProbe == "" && SecondBlackProbe == "" && Probe_3||
            _probesOnPhases[2].IsOnPhase && RedProbe == "" && FirstBlackProbe != "" && SecondBlackProbe == "" && Probe_3||
            _probesOnPhases[2].IsOnPhase && RedProbe == "" && FirstBlackProbe == "" && SecondBlackProbe != "" && Probe_3||
            _probesOnPhases[2].IsOnPhase && RedProbe != "" && FirstBlackProbe != "" && SecondBlackProbe == "" && Probe_3||
            _probesOnPhases[2].IsOnPhase && RedProbe != "" && FirstBlackProbe == "" && SecondBlackProbe != "" && Probe_3 ||
            _probesOnPhases[2].IsOnPhase && RedProbe == "" && FirstBlackProbe != "" && SecondBlackProbe != "")
        {
            Tablo = "Подключите фазу";
        }

        if (((RedProbe == "5" && FirstBlackProbe == "2" && SecondBlackProbe == "8") ||
            (RedProbe == "5" && FirstBlackProbe == "8" && SecondBlackProbe == "2")) &&
            RedProbe != "" && FirstBlackProbe != "" && SecondBlackProbe != "")
        {
            Tablo = "Прямое чередование фаз";
        }else if (RedProbe != "" && FirstBlackProbe != "" && SecondBlackProbe != "")
        {
            Tablo = "Подключите фазу";
        }

        //if (_probesOnPhases[0].IsOnPhase && !_redProbe.activeSelf && !_probes[0].activeSelf && !_probes[1].activeSelf && Probe_3 ||
        //    _probesOnPhases[1].IsOnPhase && !_redProbe.activeSelf && !_probes[0].activeSelf && !_probes[1].activeSelf && Probe_3 ||
        //    _probesOnPhases[2].IsOnPhase && !_redProbe.activeSelf && !_probes[0].activeSelf && !_probes[1].activeSelf && Probe_3)
        //{
        //    Tablo = "Прямое чередование фаз";
        //}

        //if (((RedProbe != "5" && FirstBlackProbe != "2" && SecondBlackProbe != "8" )||
        //    (RedProbe != "5" && FirstBlackProbe != "8" && SecondBlackProbe != "2")) &&
        //    RedProbe != "" && FirstBlackProbe != "" && SecondBlackProbe != "")
        //{
        //    Tablo = "Подключите фазу";
        //}

    //    if ((RedProbe != "2" && RedProbe != "5" && RedProbe != "8") &&
    //(FirstBlackProbe != "2" && FirstBlackProbe != "5" && FirstBlackProbe != "8") &&
    //(SecondBlackProbe != "2" && SecondBlackProbe != "5" && SecondBlackProbe != "8") &&
    //RedProbe != "" && FirstBlackProbe != "" && SecondBlackProbe != "")
    //    {
    //        Tablo = "Подключите фазу";
    //    }

        if (RedProbe == "2" && FirstBlackProbe == "8" && SecondBlackProbe == "5" ||
            RedProbe == "2" && FirstBlackProbe == "5" && SecondBlackProbe == "8" ||
            RedProbe == "8" && FirstBlackProbe == "5" && SecondBlackProbe == "2" ||
            RedProbe == "8" && FirstBlackProbe == "2" && SecondBlackProbe == "5")
        {
            Tablo = "Обратное чередование фаз";
        }


        //if (_Сorrect[0] != "" && _Сorrect[0] != "" && _Сorrect[0] != "" )
        //{
        //    Table = "Прямое чередование фаз";
        //    //if (GloreUpload == false)
        //    //{
        //    //    GloreFail = true;
        //    //}
        //    //return;
        //}
        //else if (_Сorrect[1] != "" && _Сorrect[2] == "" && _Сorrect[3] == "" ||
        //    _Сorrect[1] == "" && _Сorrect[2] != "" && _Сorrect[3] == ""||
        //    _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] != "")
        //{
        //    Table = "Подключите фазу";
        //}
        if (_watch.IsUssedWatch)
            IsUssedStopWatch = true;

        if (MeteringBlack1 != "" || MeteringBlack2 != ""|| MeteringRed != "" || Kleshi != "")
        {
            IsUssed = true;

            if (GloreUpload == false)
            {
                GloreFail = true;
            }
                
            Probe_On = true;
            
        }
        else
        {
            Probe_On = false;
                
        }
        
        
        Clamp_On = false;

        if (_Clamp[0].activeSelf == true)
        {
            IsUssedClamps = true;
            float power = (100 / 5) * Phase_A[1];
            Tablo = "                         " + Mathf.Abs(((Phase_A[0] * power * Mathf.Cos(Phase_A[2] * Mathf.Deg2Rad)) / 1000)).ToString("0.0") + "kWt\n                       " + (100 / 5) * Phase_A[1] + "A";
            Clamp_On = true;
        }
        else if (_Clamp[1].activeSelf == true)
        {
            float power = (100 / 5) * Phase_B[1];
            Tablo = "                         " + Mathf.Abs(((Phase_B[0] * power * Mathf.Cos(Phase_B[2] * Mathf.Deg2Rad)) / 1000)).ToString("0.0") + "kWt\n                       " + (100 / 5) * Phase_B[1] + "A";
            Clamp_On = true;
            IsUssedClamps = true;

        }
        else if (_Clamp[2].activeSelf == true)
        {
            float power = (100 / 5) * Phase_C[1];
            Tablo = "                         " + Mathf.Abs(((Phase_C[0] * power * Mathf.Cos(Phase_C[2] * Mathf.Deg2Rad)) / 1000)).ToString("0.0") + "kWt\n                       " + (100 / 5) * Phase_C[1] + "A";
            Clamp_On = true;
            IsUssedClamps = true;
        }

        //if (_Clamp[0].activeSelf == true)
        //{
        //    Tablo = " 41kWt\n " + (100 / 5) * Phase_A[1] + "A";
        //    Clamp_On = true;
        //}
        //else if (_Clamp[1].activeSelf == true)
        //{
        //    Tablo = " 41kWt\n " + (100 / 5) * Phase_B[1] + "A";
        //    Clamp_On = true;
        //}
        //else if (_Clamp[2].activeSelf == true)
        //{
        //    Tablo = " 41kWt\n " + (100 / 5) * Phase_C[1] + "A";
        //    Clamp_On = true;
        //}

        float A = (100 / 5) * Phase_A[1];
        float B = (100 / 5) * Phase_B[1];
        float C = (100 / 5) * Phase_C[1];
        _act.Instrumental._KleshiA = A.ToString();
        _act.Instrumental._KleshiB = B.ToString();
        _act.Instrumental._KleshiC = C.ToString();

        //var pulOutsCables = _cables.Where(item => item.IsPullOut).ToList();
        //var screws = _screws.Where(item => !item.IsOpen).ToList();

        //if (pulOutsCables.Count >= 1 ||  screws.Count >= 1)
        //{
        //    Debug.Log(3);
        //    Table = "           " + 0 + "Hz     " + "0V\n        " + "                    " + 0 + "A";
        //    return;
        //}



        if (Probe_3 == true)
        {
            if (Probe_3 == true && Obratnoe_Cheredovanie_Faz == true && N == 1)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "2-A" && _Сorrect[2] == "5-B" && _Сorrect[3] == "8-C" || _Сorrect[0] == "" && _Сorrect[1] == "2-C" && _Сorrect[2] == "5-A" && _Сorrect[3] == "8-B" || _Сorrect[0] == "" && _Сorrect[1] == "2-B" && _Сorrect[2] == "5-C" && _Сorrect[3] == "8-A")
                {
                    Tablo = "Обратное чередование фаз";
                    if (GloreUpload == false)
                    {
                        GloreFail = true;
                    }
                }
                else if (_Сorrect[0] == "" && _Сorrect[1] == "2-B" && _Сorrect[2] == "5-A" && _Сorrect[3] == "8-C" || _Сorrect[0] == "" && _Сorrect[1] == "2-A" && _Сorrect[2] == "5-C" && _Сorrect[3] == "8-B" || _Сorrect[0] == "" && _Сorrect[1] == "2-C" && _Сorrect[2] == "5-B" && _Сorrect[3] == "8-A")

                {
                    Debug.Log("Прямое чередование фаз");
                    Tablo = "Прямое чередование фаз";
                    if (GloreUpload == false)
                    {
                        GloreFail = true;
                    }
                }
                _act.Instrumental._Cheredovanie = "Обратное чередование фаз";

            }
            else if (Probe_3 == false && Obratnoe_Cheredovanie_Faz == true && N == 1)
            {
                if (MeteringRed == "1-A" || MeteringRed == "2-A" || MeteringRed == "3-A" || MeteringRed == "1-C" || MeteringRed == "2-C" || MeteringRed == "3-C")
                {
                    MeteringRed = "";
                    if (GloreUpload == false)
                    {
                        GloreFail = true;
                    }
                }
                
            }else if (Obryv_Vnutrennih_Cepej_Napryazheniya == true && N == 4)
            {
                _Info.text = "Обрыв внутренних цепей напряжения электросчетчика.";
                
                for (int i = 0; i < _Сorrect.Count; i++)
                {

                    if (_Сorrect[i] == "2-A" || _Сorrect[i] == "5-A" || _Сorrect[i] == "8-A" || _Сorrect[i] == "5-A" || _Сorrect[i] == "5-B" || _Сorrect[i] == "5-C" || _Сorrect[i] == "8-A" || _Сorrect[i] == "8-B" || _Сorrect[i] == "8-C")
                    {
                        Tablo = "Подключите фазу";
                        Debug.Log("Подключите фазу");
                        if (GloreUpload == false)
                        {
                            GloreFail = true;
                        }
                    }
                }
                
                
                //у нас нет одной из фаз. при проверке чередования фаз дает - Подключите фазу
            }else if (Podklyuchenie_Dvuh_Transformatorov_Toka == true && N == 7)
            {
                
                 for (int i = 0; i < _Сorrect.Count; i++)
                  {
                 
                   if (_Сorrect[i] == "2-A" || _Сorrect[i] == "5-A" || _Сorrect[i] == "8-A" || _Сorrect[i] == "5-A" || _Сorrect[i] == "5-B" || _Сorrect[i] == "5-C" || _Сorrect[i] == "8-A" || _Сorrect[i] == "8-B" || _Сorrect[i] == "8-C")
                    {
                        Tablo = "Подключите фазу";
                        Debug.Log("Подключите фазу");

                        if (GloreUpload == false)
                                         {
                                             GloreFail = true;
                                         }
                                         
                     }
                   }
                
                
                
            }else if (Nesootvetstvie_Toka == true && N == 0)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "2-B" && _Сorrect[2] == "5-A" && _Сorrect[3] == "8-C" || _Сorrect[0] == "" && _Сorrect[1] == "2-C" && _Сorrect[2] == "5-B" && _Сorrect[3] == "8-A" || _Сorrect[0] == "" && _Сorrect[1] == "2-A" && _Сorrect[2] == "5-C" && _Сorrect[3] == "8-B")

                {
                    Debug.Log("Прямое чередование фаз");

                    Tablo = "Прямое чередование фаз";
                    
                }
                else if (_Сorrect[0] == "" && _Сorrect[1] == "2-A" && _Сorrect[2] == "5-B" && _Сorrect[3] == "8-C" || _Сorrect[0] == "" && _Сorrect[1] == "2-C" && _Сorrect[2] == "5-A" && _Сorrect[3] == "8-B" || _Сorrect[0] == "" && _Сorrect[1] == "2-B" && _Сorrect[2] == "5-C" && _Сorrect[3] == "8-A")
                {
                    Tablo = "Обратное чередование фаз";
                    if (GloreUpload == false)
                    {
                        GloreFail = true;
                    }
                }
                else
                {
                    for (int i = 0; i < _Сorrect.Count; i++)
                    {

                        if (_Сorrect[i] == "2-A" || _Сorrect[i] == "5-A" || _Сorrect[i] == "8-A" || _Сorrect[i] == "5-A" || _Сorrect[i] == "5-B" || _Сorrect[i] == "5-C" || _Сorrect[i] == "8-A" || _Сorrect[i] == "8-B" || _Сorrect[i] == "8-C")
                        {
                            Debug.Log("Подключите фазу");
                            Tablo = "Подключите фазу";
                            if (GloreUpload == false)
                            {
                                GloreFail = true;
                            }
                        }
                    }
                }

                _act.Instrumental._Cheredovanie = "Обратное чередование фаз";
            }
            else 
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "2-A" && _Сorrect[2] == "5-B" && _Сorrect[3] == "8-C" ||
                    _Сorrect[0] == "" && _Сorrect[1] == "2-C" && _Сorrect[2] == "5-A" && _Сorrect[3] == "8-B" ||
                    _Сorrect[0] == "" && _Сorrect[1] == "2-B" && _Сorrect[2] == "5-C" && _Сorrect[3] == "8-A")
                {
                    Debug.Log("Прямое чередование фаз");

                    Tablo = "Прямое чередование фаз";
                    if (GloreUpload == false)
                    {
                        GloreFail = true;
                    }
                }
                else if (_Сorrect[0] == "" && _Сorrect[1] == "2-B" && _Сorrect[2] == "5-A" && _Сorrect[3] == "8-C" ||
                         _Сorrect[0] == "" && _Сorrect[1] == "2-A" && _Сorrect[2] == "5-C" && _Сorrect[3] == "8-B" ||
                         _Сorrect[0] == "" && _Сorrect[1] == "2-C" && _Сorrect[2] == "5-B" && _Сorrect[3] == "8-A")

                {
                    Tablo = "Обратное чередование фаз";
                    if (GloreUpload == false)
                    {
                        GloreFail = true;
                    }
                }
                else
                {
                    //for (int i = 0; i < _Сorrect.Count; i++)
                    //{

                    //    if (_Сorrect[i] == "2-A" || _Сorrect[i] == "5-A" || _Сorrect[i] == "8-A" ||
                    //        _Сorrect[i] == "5-A" || _Сorrect[i] == "5-B" || _Сorrect[i] == "5-C" ||
                    //        _Сorrect[i] == "8-A" || _Сorrect[i] == "8-B" || _Сorrect[i] == "8-C")
                    //    {
                    //        Debug.Log("Подключите фазу");
                    //        Table = "Подключите фазу";
                    //        if (GloreUpload == false)
                    //        {
                    //            GloreFail = true;
                    //        }
                    //    }
                    //}
                }

                _act.Instrumental._Cheredovanie = "Прямое чередование фаз";
            }
        }
        else
        {
            
              
            
            

            if (Obryv_Vnutrennih_Cepej_Toka == true && N == 5)
            {
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "1-Down" && _Сorrect[3] == "2-A")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[0].ToString("0.0") + "V\n        " + "                    " + "0A";
                    Debug.Log("фаза А");
                    Debug.Log(1);
                    if (GloreUpload == false)
                    {
                        GloreFail = true;
                    }
                }
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "1-Top" && _Сorrect[3] == "2-A")
                {
                    
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[0].ToString("0.0") + "V\n        " + "                    " + "0A";
                    Debug.Log("фаза А"); Debug.Log(1);

                    if (GloreUpload == false)
                    {
                        GloreFail = true;
                    }
                }
                
            }else if (Obryv_Vnutrennih_Cepej_Napryazheniya == true && N == 4)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "1-Down" && _Сorrect[3] == "2-A")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_A[1] + "A";
                    Debug.Log("фаза А"); Debug.Log(1);

                    if (GloreUpload == false)
                    {
                        GloreFail = true;
                    }
                }
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "1-Top" && _Сorrect[3] == "2-A")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " +  "0V\n        " + "                    " + Phase_A[1] + "A";
                    Debug.Log("фаза А"); Debug.Log(1);

                    if (GloreUpload == false)
                    {
                        GloreFail = true;
                    }
                }
                
            }else if (Obratnoe_Cheredovanie_Faz == true && N == 1)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "1-Down" && _Сorrect[3] == "2-A")
                {
                    
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_A[2] + "L°       " +
                                 Phase_A[1] + "A";

                    Debug.Log("фаза А");
                    
                    if (GloreUpload == false)
                    {
                        GloreFail = true;
                    }
                }
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "1-Top" && _Сorrect[3] == "2-A")
                {
                    float gradus = 180f - Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "C°       " +
                                 Phase_A[1] + "A";

                    if (GloreUpload == false)
                    {
                        GloreFail = true;
                    }
                }
                
            }
            else if(Nesootvetstvie_Toka == true && N == 0)
            {
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "1-Down" && _Сorrect[3] == "2-A")
                {
                    float Grad = 180f - Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_A[2] + "C°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "1-Top" && _Сorrect[3] == "2-A")
                {
                    float Grad = 180f - Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Grad + "L°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }else if(Podklyuchenie_Dvuh_Transformatorov_Toka == true && N == 7)
            {
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "1-Down" && _Сorrect[3] == "2-A")
                {
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_A[2] + "C°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "1-Top" && _Сorrect[3] == "2-A")
                {
                    float Grad = 180f - Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Grad + "L°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }else if(Vstrechnoe_Vklyuchenie_Transformatorov_Toka == true && N == 2)
            {
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "1-Down" && _Сorrect[3] == "2-A")
                {
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_A[2].ToString("0.0") + "C°       " + Phase_A[1].ToString("0.0") + "A";
                    Debug.Log("фаза А");
                }
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "1-Top" && _Сorrect[3] == "2-A")
                {
                    float gradus = 180f - Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "L°       " + Phase_A[1].ToString("0.0") + "A";
                    Debug.Log("фаза А");
                }
            }else if(Nalichie_Zakorotok == true && N == 6)
            {
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "1-Down" && _Сorrect[3] == "2-A")
                {
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_A[2].ToString("0.0") + "L°       " +
                                 Phase_A[1] + "A";

                    Debug.Log("фаза А");
                    if (GloreUpload == false)
                    {
                        GloreFail = true;
                    }
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "1-Top" && _Сorrect[3] == "2-A")
                {
                    
                    
                    float gradus = 180f - Phase_A[2];
                                        Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "C°       " +
                                                     Phase_A[1] + "A";
                                        Debug.Log("фаза А");
                                        if (GloreUpload == false)
                                        {
                                            GloreFail = true;
                                        }
                }
            }else if (Kontakty_Transformatorov_On == true && N == 3) 
            {
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "1-Down" && _Сorrect[3] == "2-A")
                {
                    
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Gradus_Kont + "C°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "1-Top" && _Сorrect[3] == "2-A")
                {
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_A[2] + "L°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }
            else 
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "1-Down" && _Сorrect[3] == "2-A")
                {
                    float gradus = Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "L°       " +
                                 Phase_A[1] + "A";

                    Debug.Log("фаза А");
                    if (GloreUpload == false)
                    {
                        GloreFail = true;
                    }
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "1-Top" && _Сorrect[3] == "2-A")
                {

                    //float gradus = 180f - Phase_A[2];
                    float gradus = 180f - Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "C°       " +
                                 Phase_A[1] + "A";
                    Debug.Log("фаза А");
                    if (GloreUpload == false)
                    {
                        GloreFail = true;
                    }
                }
               
            }
            
           
            
            


            if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "2-A" && _Сorrect[3] == "2-Down")
            {
                Tablo = "Фuu=           ?°          " + Phase_A[0] + "0V\nФui=            ?°       " + "0A";
                Debug.Log("фаза А");
            }            
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "2-A" && _Сorrect[3] == "2-Top")
            {
                Tablo = "Фuu=           ?°          " + Phase_A[0] + "0V\nФui=            ?°       " + "0A";
                Debug.Log("фаза А");
            }


            
            if (Obryv_Vnutrennih_Cepej_Toka == true && N == 5)
            {
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "2-A" && _Сorrect[3] == "3-Down")
                {
                    Debug.Log(1);

                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[0].ToString("0.0") + "V\n        " + "                    " + "0A";
                    Debug.Log("фаза А");
                    if (GloreUpload == false)
                    {
                        GloreFail = true;
                    }
                }
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "2-A" && _Сorrect[3] == "3-Top")
                {
                    Debug.Log(1);

                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[0].ToString("0.0") + "V\n        " + "                    " + "0A";
                    Debug.Log("фаза А");
                    if (GloreUpload == false)
                    {
                        GloreFail = true;
                    }
                }
                
            }else if(Nesootvetstvie_Toka == true && N == 0)
            {
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "2-A" && _Сorrect[3] == "3-Down")
                {
                    float Grad = 180f - Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Grad + "L°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "2-A" && _Сorrect[3] == "3-Top")
                { Debug.Log("Good");
                    float Grad = 180f - Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_A[2] + "C°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }else if(Obryv_Vnutrennih_Cepej_Napryazheniya == true && N == 4)
            {
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "2-A" && _Сorrect[3] == "3-Down")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "2-A" && _Сorrect[3] == "3-Top")
                { Debug.Log("Good");
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }else if(Podklyuchenie_Dvuh_Transformatorov_Toka == true && N == 7)
            {
                
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "2-A" && _Сorrect[3] == "3-Down")
                {
                    float gradus = 180f - Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "L°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "2-A" && _Сorrect[3] == "3-Top")
                {
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_A[2] + "C°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
                
            }else if(Vstrechnoe_Vklyuchenie_Transformatorov_Toka == true && N == 2)
            {
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "2-A" && _Сorrect[3] == "3-Down")
                {
                    float gradus = 180f - Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "L°       " + Phase_A[1].ToString("0.0") + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "2-A" && _Сorrect[3] == "3-Top")
                {
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_A[2] + "C°       " + Phase_A[1].ToString("0.0") + "A";
                    Debug.Log("фаза А");
                }
            }else if(Kontakty_Transformatorov_On == false)
            {
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "2-A" && _Сorrect[3] == "3-Down")
                {
                    float gradus = 180f - Phase_A[2];
                    //float gradus = Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "C°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "2-A" && _Сorrect[3] == "3-Top")
                {
                    float gradus =  Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "L°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }
            else
            {
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "2-A" && _Сorrect[3] == "3-Down")
                {
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_A[2] + "L°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "2-A" && _Сorrect[3] == "3-Top")
                {
                    
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Gradus_Kont + "C°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }
            
            
            
            if(Nesootvetstvie_Toka == true && N == 0)
            {
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "10-Down" && _Сorrect[3] == "2-A")
                {
                    float Grad = 180f - Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_A[2] + "C°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "10-Top" && _Сorrect[3] == "2-A")
                { Debug.Log("Good");
                    float Grad = 180f - Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Grad + "L°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }else if(Obryv_Vnutrennih_Cepej_Toka == true && N == 5)
            {
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "10-Down" && _Сorrect[3] == "2-A")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[0].ToString("0.0") + "V\n        " + "                    " + "0A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "10-Top" && _Сorrect[3] == "2-A")
                {
                    
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[0].ToString("0.0") + "V\n        " + "                    " + "0A";
                    Debug.Log("фаза А");
                }
                
            }else if(Vstrechnoe_Vklyuchenie_Transformatorov_Toka == true && N == 2)
            {
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "10-Down" && _Сorrect[3] == "2-A")
                                {
                                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_A[2].ToString("0.0") + "C°       " + Phase_A[1].ToString("0.0") + "A";
                                    Debug.Log("фаза А");
                                }
                
                                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "10-Top" && _Сorrect[3] == "2-A")
                                {
                                    float gradus = 180f - Phase_A[2];
                                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus.ToString("0.0") + "L°       " + Phase_A[1].ToString("0.0") + "A";
                                    Debug.Log("фаза А");
                                }
                
            }else if(Obryv_Vnutrennih_Cepej_Napryazheniya == true && N == 4)
            {
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "10-Down" && _Сorrect[3] == "2-A")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "10-Top" && _Сorrect[3] == "2-A")
                {
                    
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
                
            }else if(Podklyuchenie_Dvuh_Transformatorov_Toka == true && N == 7)
            {
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "10-Down" && _Сorrect[3] == "2-A")
                {
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_A[2] + "C°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "10-Top" && _Сorrect[3] == "2-A")
                {
                    float Grad = 180f - Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Grad + "L°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

            }else if (Kontakty_Transformatorov_On == false)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "10-Down" && _Сorrect[3] == "2-A")
                {
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_A[2].ToString("0.0") + "L°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "10-Top" && _Сorrect[3] == "2-A")
                {
                    float gradus = 180f - Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus.ToString("0.0") + "C°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }
            else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "10-Down" && _Сorrect[3] == "2-A")
                {

                    
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Gradus_Kont.ToString("0.0") + "C°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "10-Top" && _Сorrect[3] == "2-A")
                {
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_A[2].ToString("0.0") + "L°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }

            if(Nesootvetstvie_Toka == true && N == 0)
            {
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "11-Top" && _Сorrect[3] == "2-A")
                {
                    float Grad = 180f - Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_A[2] + "C°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "11-Down" && _Сorrect[3] == "2-A")
                { 
                    float Grad = 180f - Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Grad + "L°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }else if(Obryv_Vnutrennih_Cepej_Toka == true && N == 5)
            {
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "11-Top" && _Сorrect[3] == "2-A")
                {
                    
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[0].ToString("0.0") + "V\n        " + "                    " + "0A";
                    Debug.Log("фаза А");
                }
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "11-Down" && _Сorrect[3] == "2-A")
                { Debug.Log("Good");
                    
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[0].ToString("0.0") + "V\n        " + "                    " + "0A";
                    Debug.Log("фаза А");
                }
            }else if(Obryv_Vnutrennih_Cepej_Napryazheniya == true && N == 4)
            {
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "11-Top" && _Сorrect[3] == "2-A")
                {
                    
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "11-Down" && _Сorrect[3] == "2-A")
                { Debug.Log("Good");
                    
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }else if(Podklyuchenie_Dvuh_Transformatorov_Toka == true && N == 7)
            {
                
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "11-Down" && _Сorrect[3] == "2-A")
                {
                    float gradus = 180f - Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "L°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "11-Top" && _Сorrect[3] == "2-A")
                {
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_A[2] + "C°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
                
            }else if(Vstrechnoe_Vklyuchenie_Transformatorov_Toka == true && N == 2)
            {
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "11-Down" && _Сorrect[3] == "2-A")
                {
                    float gradus = 180f - Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "L°       " + Phase_A[1].ToString("0.0") + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "11-Top" && _Сorrect[3] == "2-A")
                {
                    Tablo = "Фuu=           ?°м          " + "0V\nФui=            " + Phase_A[2].ToString("0.0") + "C°       " + Phase_A[1].ToString("0.0") + "A";
                    Debug.Log("фаза А");
                }
                
            }else if (Kontakty_Transformatorov_On == false)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "11-Down" && _Сorrect[3] == "2-A")
                {
                    float gradus = 180f - Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "C°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "11-Top" && _Сorrect[3] == "2-A")
                {
                    Tablo = "Фuu=           ?°м          " + "0V\nФui=            " + Phase_A[2] + "L°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }
            else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "11-Down" && _Сorrect[3] == "2-A")
                {
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_A[2] + "L°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "11-Top" && _Сorrect[3] == "2-A")
                {
                    
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Gradus_Kont + "C°       " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }
            

            ///////////

            
            if(Vstrechnoe_Vklyuchenie_Transformatorov_Toka == true && N == 2)
            {
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "4-Down" && _Сorrect[3] == "5-A")
                {
                    float gradus = 180f - Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "L°       " + Phase_A[1].ToString("0.0") + "A";
                    Debug.Log("фаза В");
                }
            
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "4-Top"  && _Сorrect[3] == "5-A")
                {
                    
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_A[2].ToString("0.0") + "С°       " + Phase_B[1].ToString("0.0") + "A";
                    Debug.Log("фаза В");
                }
            }
            else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "4-Down" && _Сorrect[3] == "5-A")
                {
                    float gradus = Phase_B[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "L°       " + Phase_B[1].ToString("0.0") + "A";
                    Debug.Log("фаза В");

                }
            
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "4-Top"  && _Сorrect[3] == "5-A")
                {
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + (180f - Phase_B[2]) + "C°       " + Phase_B[1].ToString("0.0") + "A";
                    Debug.Log("фаза В");
                }
            }
           


            if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "5-A" && _Сorrect[3] == "5-Down")
            {
                Tablo = "Фuu=           ?°          " + Phase_B[0] + "0V\nФui=            ?°       " + "0A";
                Debug.Log("фаза В");
            }
            if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "5-A" && _Сorrect[3] == "5-Top")
            {
                Tablo = "Фuu=           ?°          " + Phase_B[0] + "0V\nФui=            ?°       " + "0A";
                Debug.Log("фаза В");
            }

            if (Vstrechnoe_Vklyuchenie_Transformatorov_Toka == true && N == 2)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "5-A" && _Сorrect[3] == "6-Down")
                {
                    Debug.Log("фаза С");


                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_A[2].ToString("0.0") + "С°       " + Phase_B[1].ToString("0.0")+ "A";
                }
            
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "5-A" && _Сorrect[3] == "6-Top")
                {
                    float gradus = 180f - Phase_A[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus.ToString("0.0") + "L°       " + Phase_B[1].ToString("0.0") + "A";
                    Debug.Log("фаза В");
                }
            }
            else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "5-A" && _Сorrect[3] == "6-Down")
                {
                
                    float gradus = 180f - Phase_B[2];
                    //float gradus = Phase_B[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "C°       " + Phase_B[1].ToString("0.0") + "A";
                    Debug.Log("фаза В");
                }
            
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "5-A" && _Сorrect[3] == "6-Top")
                {
                    //float gradus = 180f - Phase_B[2];
                    float gradus = Phase_B[2];

                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "L°       " + Phase_B[1].ToString("0.0") + "A";
                    Debug.Log("фаза В");
                }
                
            }

            


            if (Vstrechnoe_Vklyuchenie_Transformatorov_Toka == true && N == 2)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "12-Down" && _Сorrect[3] == "5-A")
                {
                    float gradus = 180f - Phase_A[2]; 
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "L°       " + Phase_A[1].ToString("0.0") + "A";
                    Debug.Log("фаза В");
                }
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "12-Top" && _Сorrect[3] == "5-A")
                {
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_A[2].ToString("0.0") + "C°       " + Phase_B[1].ToString("0.0")+ "A";
                    Debug.Log("фаза В");
                }
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "13-Down" && _Сorrect[3] == "5-A")
                {

                    
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_A[2].ToString("0.0") + "C°       " + Phase_B[1].ToString("0.0")+ "A";
                    Debug.Log("фаза В");
                }
            }
            else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "12-Down" && _Сorrect[3] == "5-A")
                {
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_B[2] + "L°       " + Phase_B[1].ToString("0.0") + "A";
                    
                    Debug.Log("фаза В");
                }
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "12-Top" && _Сorrect[3] == "5-A")
                {
                    float gradus = 180f - Phase_B[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus.ToString("0.0") + "C°       " + Phase_B[1].ToString("0.0") + "A";
                    Debug.Log("фаза В");
                }
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "13-Down" && _Сorrect[3] == "5-A")
                {

                    float gradus = 180f - Phase_B[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus.ToString("0.0") + "C°       " + Phase_B[1].ToString("0.0") + "A";
                    Debug.Log("фаза В");
                }
            }
            
            
            
            

            
            
            

            if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "13-Top" && _Сorrect[3] == "5-A")
            {
                Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_B[2].ToString("0.0") + "L°       " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза В");
            }

            ////////////

            if (Nalichie_Zakorotok == true && N == 6)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "7-Down" && _Сorrect[3] == "8-A")
                {
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_C[2] + "L°       "  + "0.3A";
                    Debug.Log("фаза С");
                }
            
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "7-Top" && _Сorrect[3] == "8-A")
                {
                    float gradus = 180f - Phase_C[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "C°       " + "0.3A";
                    Debug.Log("фаза С");


                }
            }
            else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "7-Down" && _Сorrect[3] == "8-A")
                {
                    float gradus =  Phase_C[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "L°       " + Phase_C[1] + "A";
                    Debug.Log("фаза С");
                }
            
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "7-Top" && _Сorrect[3] == "8-A")
                {
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + (180f - Phase_C[2]) + "C°       " + Phase_C[1] + "A";
                    Debug.Log("фаза С");



                }
            }
            
           
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "8-A" && _Сorrect[3] == "8-Down")
            {
                Tablo = "Фuu=           ?°          " + Phase_C[0] + "0V\nФui=            ?°       " + "0A";
                Debug.Log("фаза С");
            }
            if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "8-A" && _Сorrect[3] == "8-Top")
            {
                Tablo = "Фuu=           ?°          " + Phase_C[0] + "0V\nФui=            ?°       " + "0A";
                Debug.Log("фаза С");
            }


            if (Nalichie_Zakorotok == true && N == 6)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "8-A" && _Сorrect[3] == "9-Down")
                {
                    float gradus = 180f - Phase_C[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "C°       "  + "0.3A";
                    Debug.Log("фаза С");
                }
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "8-A" && _Сorrect[3] == "9-Top")
                {
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_C[2] + "L°       "  + "0.3A";
                    Debug.Log("фаза С");
                }
            }else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "8-A" && _Сorrect[3] == "9-Down")
                {

                    float gradus = 180f - Phase_C[2];
                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "C°       " + Phase_C[1] + "A";
                    Debug.Log("фаза С");
                }
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "8-A" && _Сorrect[3] == "9-Top")
                {

                    //float gradus = 180f - Phase_C[2];
                    float gradus = Phase_C[2];

                    Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "L°       " + Phase_C[1] + "A";
                    Debug.Log("фаза С");
                }
            }

            

            if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "14-Down" && _Сorrect[3] == "8-A")
            {
                Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_C[2] + "L°       " + Phase_C[1] + "A";
                Debug.Log("фаза С");
            }
            if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "14-Top" && _Сorrect[3] == "8-A")
            {
                float gradus = 180f - Phase_C[2];
                Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "C°       " + Phase_C[1] + "A";
                Debug.Log("фаза С");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "15-Down" && _Сorrect[3] == "8-A")
            {

                float gradus = 180f - Phase_C[2];
                Tablo = "Фuu=           ?°          " + "0V\nФui=            " + gradus + "C°       " + Phase_C[1] + "A";
                Debug.Log("фаза С");
            }
            if (_Сorrect[0] == "" && _Сorrect[1] == "10-A" && _Сorrect[2] == "15-Top" && _Сorrect[3] == "8-A")
            {
                Tablo = "Фuu=           ?°          " + "0V\nФui=            " + Phase_C[2] + "L°       " + Phase_C[1] + "A";
                Debug.Log("фаза С");
            }


            // измерение тока клещами////////////////////////////////////////
            
            //
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "1-Down")
            {
                Tablo = "                     " + "0V\n       " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[1] + "A";
                Debug.Log("фаза А");
            }
            //
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "1-Top")
            {
                Tablo = "                     " + "0V\n       " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[1] + "A";
                Debug.Log("фаза А");
            }
            
            //
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "3-Top")
            {
                Tablo = "                     " + "0V\n       " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[1] + "A";
                Debug.Log("фаза А");
            }

            //
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "3-Down")
            {
                Tablo = "                     " + "0V\n       " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[1] + "A";
                Debug.Log("фаза А");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "10-Top")
            {
                Tablo = "                     " + "0V\n       " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[1] + "A";
                Debug.Log("фаза А");
            }

            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "10-Down")
            {
                Tablo = "                     " + "0V\n       " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[1] + "A";
                Debug.Log("фаза А");
            } 
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "11-Top")
            {
                Tablo = "                     " + "0V\n       " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[1] + "A";
                Debug.Log("фаза А");
            }

            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "11-Down")
            {
                Tablo = "                     " + "0V\n       " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[1] + "A";
                Debug.Log("фаза А");
            }

            ///////////////////////////////////////
            //
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "4-Down")
            {
                Tablo = "                    " + "0V\n        " + Phase_B[3].ToString("0.0") + "Hz     " + Phase_B[1].ToString("0.0")+ "A";
                Debug.Log("фаза B");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "4-Top")
            {
                Tablo = "                    " + "0V\n        " + Phase_B[3].ToString("0.0") + "Hz     " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза B");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "6-Top")
            {
                Tablo = "                     " + "0V\n        " + Phase_B[3].ToString("0.0") + "Hz     " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза B");
            }
            //
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "6-Down")
            {
                Tablo = "                    " + "0V\n        " + Phase_B[3].ToString("0.0") + "Hz     " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза B");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "12-Top")
            {
                Tablo = "                     " + "0V\n        " + Phase_B[3].ToString("0.0") + "Hz     " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза B");
            }

            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "12-Down")
            {
                Tablo = "                    " + "0V\n        " + Phase_B[3].ToString("0.0") + "Hz     " + Phase_B[1].ToString("0.0")+ "A";
                Debug.Log("фаза B");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "13-Top")
            {
                Tablo = "                     " + "0V\n        " + Phase_B[3].ToString("0.0") + "Hz     " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза B");
            }

            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "13-Down")
            {
                Tablo = "                    " + "0V\n        " + Phase_B[3].ToString("0.0") + "Hz     " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза B");
            }

            //////////////////////////////////

            //
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "7-Down")
            {
                Tablo = "                     " + "0V\n        " + Phase_C[3].ToString("0.0") + "Hz     " + Phase_C[1] + "A";
                Debug.Log("фаза C");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "7-Top")
            {
                Tablo = "                     " + "0V\n        " + Phase_C[3].ToString("0.0") + "Hz     " + Phase_C[1] + "A";
                Debug.Log("фаза C");
            }
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "9-Top")
            {
                Tablo = "                    " + "0V\n        " + Phase_C[3].ToString("0.0") + "Hz     " + Phase_C[1] + "A";
                Debug.Log("фаза C");
            }

            //
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "9-Down")
            {
                Tablo = "                     " + "0V\n        " + Phase_C[3].ToString("0.0") + "Hz     " + Phase_C[1] + "A";
                Debug.Log("фаза C");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "14-Top")
            {
                Tablo = "                    " + "0V\n        " + Phase_C[3].ToString("0.0") + "Hz     " + Phase_C[1] + "A";
                Debug.Log("фаза C");
            }


            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "14-Down")
            {
                Tablo = "                     " + "0V\n        " + Phase_C[3].ToString("0.0") + "Hz     " + Phase_C[1] + "A";
                Debug.Log("фаза C");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "15-Top")
            {
                Tablo = "                    " + "0V\n        " + Phase_C[3].ToString("0.0") + "Hz     " + Phase_C[1] + "A";
                Debug.Log("фаза C");
            }


            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "15-Down")
            {
                Tablo = "                     " + "0V\n        " + Phase_C[3].ToString("0.0") + "Hz     " + Phase_C[1] + "A";
                Debug.Log("фаза C");
            }

            ///// два щупа Лево
            if (Obryv_Vnutrennih_Cepej_Napryazheniya == true && N == 4)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "2-A")
                {
                    Tablo = "                         0,V\n                       0mA";
                    Debug.Log("фаза А");
                }
            }
            else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "2-A")
                {
                    Tablo = "                     " + Phase_A[0] + "V\n        " + Phase_A[3].ToString("0.0") + "Hz     " + "0A";
                    Debug.Log("фаза А");
                }
            }
            
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "5-A")
            {
                Tablo = "                     " + Phase_B[0] + "V\n        " + Phase_A[3].ToString("0.0") + "Hz     " + "0A";
                Debug.Log("фаза В");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "8-A")
            {
                Tablo = "                     " + Phase_C[0] + "V\n        " + Phase_A[3].ToString("0.0") + "Hz     " + "0A";
                Debug.Log("фаза C");
            }


            //Права

            if (Obryv_Vnutrennih_Cepej_Napryazheniya == true && N==4)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "2-C")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " +  "0V\n        " + "       " + "0A";
                    Debug.Log("фаза А");
                } 
            }
            else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "2-C")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[0] + "V\n        " + "       " + "0A";
                    Debug.Log("фаза А");
                }
            }
            

            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "5-C")
            {
                Tablo = "           " + Phase_B[3].ToString("0.0") + "Hz     " + Phase_B[0] + "V\n        " + "       " + "0A";
                Debug.Log("фаза В");
            }

            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "8-C")
            {
                Tablo = "           " + Phase_C[3].ToString("0.0") + "Hz     " + Phase_C[0] + "V\n        " + "       " + "0A";
                Debug.Log("фаза C");

            }


            ///  клещи и 1 щуп
            if (Obryv_Vnutrennih_Cepej_Toka == true && N==5)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "1-Down")
                {
                    Tablo = "                         0,V\n                       0mA";                    Debug.Log("фаза А");
                    Debug.Log("фаза А");
                }
            }
            else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "1-Down")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " +  "0V\n        " + "                    " + Phase_A[1].ToString("0.0") + "A";
                    Debug.Log("фаза А");
                }
            }
            
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "3-Down")
            {
                Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_A[1].ToString("0.0") + "A";
                Debug.Log("фаза А");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "1-Top")
            {
                Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " +  "0V\n        " + "                    " + Phase_A[1].ToString("0.0") + "A";
                Debug.Log("фаза А");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "3-Top")
            {
                Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_A[1].ToString("0.0") + "A";
                Debug.Log("фаза А");
            }


            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "4-Down")
            {
                Tablo = "           " + Phase_B[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза В");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "6-Down")
            {
                Tablo = "           " + Phase_B[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_B[1].ToString("0.0")+ "A";
                Debug.Log("фаза В");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "4-Top")
            {
                Tablo = "           " + Phase_B[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза В");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "6-Top")
            {
                Tablo = "           " + Phase_B[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза В");
            }

            if (Nalichie_Zakorotok == true && N == 6)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "7-Down")
                {
                    Tablo = "           " + Phase_C[3].ToString("0.0") + "Hz     " + "0V\n        " + "                    "  + "0.3A";
                    Debug.Log("фаза С");
                }
            }
            else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "7-Down")
                {
                    Tablo = "           " + Phase_C[3].ToString("0.0") + "Hz     " + "0V\n        " + "                    " + Phase_C[1] + "A";
                    Debug.Log("фаза С");
                }
            }
            
           
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "9-Down")
            {
                Tablo = "           " + Phase_C[3].ToString("0.0") + "Hz     " + "0V\n        " + "                    " + Phase_C[1] + "A";
                Debug.Log("фаза С");
            }

            if (Nalichie_Zakorotok == true && N== 6)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "7-Top")
                {
                    Tablo = "           " + Phase_C[3].ToString("0.0") + "Hz     " + "0V\n        " + "                    " + "0.3A";
                    Debug.Log("фаза С");
                }
            }else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "7-Top")
                {
                    Tablo = "           " + Phase_C[3].ToString("0.0") + "Hz     " + "0V\n        " + "                    " + Phase_C[1] + "A";
                    Debug.Log("фаза С");
                }
            }
            
            
           
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "9-Top")
            {
                Tablo = "           " + Phase_C[3].ToString("0.0") + "Hz     " + "0V\n        " + "                    " + Phase_C[1] + "A";
                Debug.Log("фаза С");
            }


            if (Obryv_Vnutrennih_Cepej_Toka == true && N == 5)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "10-Down")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[0].ToString("0.0") + "V\n        " + "                    " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }
            else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "10-Down")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " +  "0V\n        " + "                    " + Phase_A[1].ToString("0.0") + "A";
                    Debug.Log("фаза А");
                }
            }

            if (Obryv_Vnutrennih_Cepej_Toka == true && N == 5)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "11-Down")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[0].ToString("0.0") + "V\n        " + "                    " + Phase_A[1] + "A";                    Debug.Log("фаза А");
                }
            }
            else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "11-Down")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_A[1].ToString("0.0") + "A";
                    Debug.Log("фаза А");
                }  
            }
            
            
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "10-Top")
            {
                Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " +  "0V\n        " + "                    " + Phase_A[1].ToString("0.0") + "A";
                Debug.Log("фаза А");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "11-Top")
            {
                Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_A[1].ToString("0.0") + "A";
                Debug.Log("фаза А");
            }


            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "12-Down")
            {
                Tablo = "           " + Phase_B[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза В");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "13-Down")
            {
                Tablo = "           " + Phase_B[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_B[1].ToString("0.0")+ "A";
                Debug.Log("фаза В");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "12-Top")
            {
                Tablo = "           " + Phase_B[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_B[1].ToString("0.0")+ "A";
                Debug.Log("фаза В");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "13-Top")
            {
                Tablo = "           " + Phase_B[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза В");
            }

            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "14-Down")
            {
                Tablo = "           " + Phase_C[3].ToString("0.0") + "Hz     " + "0V\n        " + "                    " + Phase_C[1] + "A";
                Debug.Log("фаза С");
            }
           
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "15-Down")
            {
                Tablo = "           " + Phase_C[3].ToString("0.0") + "Hz     " + "0V\n        " + "                    " + Phase_C[1] + "A";
                Debug.Log("фаза С");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "14-Top")
            {
                Tablo = "           " + Phase_C[3].ToString("0.0") + "Hz     " + "0V\n        " + "                    " + Phase_C[1] + "A";
                Debug.Log("фаза С");
            }
           
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-A" && _Сorrect[3] == "15-Top")
            {
                Tablo = "           " + Phase_C[3].ToString("0.0") + "Hz     " + "0V\n        " + "                    " + Phase_C[1] + "A";
                Debug.Log("фаза С");
            }

            //Право вниз частоту
            if (Obryv_Vnutrennih_Cepej_Toka == true && N == 5)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "1-Down")
                {
                    Debug.Log("фаза С");

                    Tablo = "                         0,V\n                       0mA";                    Debug.Log("фаза А");
                }
            }
            else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "1-Down")
                {
                    Debug.Log("фаза С");

                    Tablo = "           " + "                " + "0V\n        " + Phase_A[3].ToString("0.0") + "Hz       " + Phase_A[1] + "A";
                }
            }
            
           
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "3-Down")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_A[3].ToString("0.0") + "Hz       " + Phase_A[1] + "A";
                Debug.Log("фаза А");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "1-Top")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_A[3].ToString("0.0") + "Hz       " + Phase_A[1] + "A";
                Debug.Log("фаза А");
            }
           
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "3-Top")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_A[3].ToString("0.0") + "Hz       " + Phase_A[1] + "A";
                Debug.Log("фаза А");
            }


            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "4-Down")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_B[3].ToString("0.0") + "Hz       " + Phase_B[1].ToString("0.0")+ "A";
                Debug.Log("фаза В");
            }
           
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "6-Down")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_B[3].ToString("0.0") + "Hz       " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза В");
            } 
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "4-Top")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_B[3].ToString("0.0") + "Hz       " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза В");
            }
           
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "6-Top")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_B[3].ToString("0.0") + "Hz       " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза В");
            }

            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "7-Down")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_C[3].ToString("0.0") + "Hz       " + Phase_C[1] + "A";
                Debug.Log("фаза С");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "9-Down")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_C[3].ToString("0.0") + "Hz       " + Phase_C[1] + "A";
                Debug.Log("фаза С");
            }
            
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "7-Top")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_C[3].ToString("0.0") + "Hz       " + Phase_C[1] + "A";
                Debug.Log("фаза С");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "9-Top")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_C[3].ToString("0.0") + "Hz       " + Phase_C[1] + "A";
                Debug.Log("фаза С");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "10-Down")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_A[3].ToString("0.0") + "Hz       " + Phase_A[1] + "A";
                Debug.Log("фаза А");
            }
           
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "11-Down")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_A[3].ToString("0.0") + "Hz       " + Phase_A[1] + "A";
                Debug.Log("фаза А");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "10-Top")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_A[3].ToString("0.0") + "Hz       " + Phase_A[1] + "A";
                Debug.Log("фаза А");
            }
           
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "11-Top")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_A[3].ToString("0.0") + "Hz       " + Phase_A[1] + "A";
                Debug.Log("фаза А");
            }


            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "12-Down")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_B[3].ToString("0.0") + "Hz       " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза В");
            }
           
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "13-Down")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_B[3].ToString("0.0") + "Hz       " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза В");
            } 
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "12-Top")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_B[3].ToString("0.0") + "Hz       " + Phase_B[1].ToString("0.0")+ "A";
                Debug.Log("фаза В");
            }
           
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "13-Top")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_B[3].ToString("0.0") + "Hz       " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза В");
            }

            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "14-Down")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_C[3].ToString("0.0") + "Hz       " + Phase_C[1] + "A";
                Debug.Log("фаза С");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "15-Down")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_C[3].ToString("0.0") + "Hz       " + Phase_C[1] + "A";
                Debug.Log("фаза С");
            }
            
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "14-Top")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_C[3].ToString("0.0") + "Hz       " + Phase_C[1] + "A";
                Debug.Log("фаза С");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "10-C" && _Сorrect[3] == "15-Top")
            {
                Tablo = "           " + "                " + "0V\n        " + Phase_C[3].ToString("0.0") + "Hz       " + Phase_C[1] + "A";
                Debug.Log("фаза С");
            }


            /////////////////////////////

            if(Nesootvetstvie_Toka == true && N == 0)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "1-Down" && _Сorrect[3] == "2-C")
                {
                    Tablo = Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "1-Top" && _Сorrect[3] == "2-C")
                {
                    Tablo = "-" + Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n-" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }else if (Obryv_Vnutrennih_Cepej_Toka && N == 5)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "1-Down" && _Сorrect[3] == "2-C")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[0] + "V\n        " + "       " + "0A";                    
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "1-Top" && _Сorrect[3] == "2-C")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[0] + "V\n        " + "       " + "0A";
                    Debug.Log("фаза В");
                }
            }
            else if (Obryv_Vnutrennih_Cepej_Napryazheniya == true && N == 4)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "1-Down" && _Сorrect[3] == "2-C")
                {
                    Tablo = "           " + Phase_B[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_A[1] + "A";
                    Debug.Log("фаза В");

                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "1-Top" && _Сorrect[3] == "2-C")
                {
                    Tablo = "           " + Phase_B[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_A[1] + "A";
                    Debug.Log("фаза В");

                }


            }
            else if (Obratnoe_Cheredovanie_Faz == true && N==1)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "1-Down" && _Сorrect[3] == "2-C")
                {
                    Tablo =  Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "1-Top" && _Сorrect[3] == "2-C")
                {
                    Tablo = "-" + Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n-" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }else if (Vstrechnoe_Vklyuchenie_Transformatorov_Toka == true && N == 2)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "1-Down" && _Сorrect[3] == "2-C")
                {
                    Tablo =  Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n" + Pr_A.ToString("0.0") + "var              " + Phase_A[1].ToString("0.0") + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "1-Top" && _Сorrect[3] == "2-C")
                {
                    Tablo = "-" + Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n-" + Pr_A.ToString("0.0") + "var              " + Phase_A[1].ToString("0.0")+ "A";
                    Debug.Log("фаза А");
                }
                
            }else if (Nalichie_Zakorotok == true && N == 6)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "1-Down" && _Сorrect[3] == "2-C")
                {
                    Tablo =  Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "1-Top" && _Сorrect[3] == "2-C")
                {
                    Tablo = "-" + Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n-" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
                
            }else if (Podklyuchenie_Dvuh_Transformatorov_Toka == true && N == 7)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "1-Down" && _Сorrect[3] == "2-C")
                {
                    Tablo =  Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "1-Top" && _Сorrect[3] == "2-C")
                {
                    Tablo = "-" + Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n-" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
                
            }
            else if (Kontakty_Transformatorov_On == false)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "1-Down" && _Сorrect[3] == "2-C")
                {
                    Tablo = Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";

                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "1-Top" && _Сorrect[3] == "2-C")
                {
                    Tablo = "-" + Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n-" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";

                    Debug.Log("фаза А");
                }
            }
            else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "1-Down" && _Сorrect[3] == "2-C")
                {
                    Tablo =   Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "1-Top" && _Сorrect[3] == "2-C")
                {
                    Tablo = Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }


            if (Nalichie_Zakorotok == true && N==4)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "2-C" && _Сorrect[3] == "2-Down")
                {
                    Tablo = "                         0,V\n                       0mA";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "2-C" && _Сorrect[3] == "2-Top")
                {
                    Tablo = "                         0,V\n                       0mA";
                    Debug.Log("фаза А");
                }
            }
            else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "2-C" && _Сorrect[3] == "2-Down")
                {
                    Tablo = "1.5Wt                    " + Phase_A[0] + "V\n" + "0.5var              " + "1.5мA";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "2-C" && _Сorrect[3] == "2-Top")
                {
                    Tablo = "-1.5Wt                    " + Phase_A[0] + "V\n-" + "0.5var              " + "1.5мA";
                    Debug.Log("фаза А");
                }
            }
            //Ра =1,5(+- 0,05) ( для каждой фазы должны быть разные, но в этом интервале,Рr=0,5(+-0,02) и I=1,5мА(+-0,5)
             
            
            
            if(Nesootvetstvie_Toka == true && N == 0)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "2-C" && _Сorrect[3] == "3-Down")
                {
                    Tablo = "-" + Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n-" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";

                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "2-C" && _Сorrect[3] == "3-Top")
                {
                    Tablo = Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";

                    Debug.Log("фаза А");
                }
            }else if (Obratnoe_Cheredovanie_Faz == true && N == 1)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "2-C" && _Сorrect[3] == "3-Down")
                {
                    Tablo = "-" + Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n-" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "2-C" && _Сorrect[3] == "3-Top")
                {
                    Tablo = Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }else if (Obryv_Vnutrennih_Cepej_Toka == true && N == 5)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "2-C" && _Сorrect[3] == "3-Down")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[0] + "V\n        " + "       " + "0A";                    
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "2-C" && _Сorrect[3] == "3-Top")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[0] + "V\n        " + "       " + "0A";                    
                    Debug.Log("фаза А");
                }
            }else if (Vstrechnoe_Vklyuchenie_Transformatorov_Toka == true && N==2)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "2-C" && _Сorrect[3] == "3-Down")
                {
                    Tablo = "-" + Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n-" + Pr_A.ToString("0.0") + "var              " + Phase_A[1].ToString("0.0") + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "2-C" && _Сorrect[3] == "3-Top")
                {
                    Tablo = Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n" + Pr_A.ToString("0.0") + "var              " + Phase_A[1].ToString("0.0") + "A";
                    Debug.Log("фаза А");
                }
                
            }else if (Nalichie_Zakorotok == true && N==6)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "2-C" && _Сorrect[3] == "3-Down")
                {
                    Tablo = "-" + Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n-" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "2-C" && _Сorrect[3] == "3-Top")
                {
                    Tablo = Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
                
            }else if (Podklyuchenie_Dvuh_Transformatorov_Toka == true && N==7)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "2-C" && _Сorrect[3] == "3-Down")
                {
                    Tablo = "-" + Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n-" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "2-C" && _Сorrect[3] == "3-Top")
                {
                    Tablo = Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
                
            }else if (Obryv_Vnutrennih_Cepej_Napryazheniya == true && N==4)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "2-C" && _Сorrect[3] == "3-Down")
                {
                    Tablo = "           " + Phase_B[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "2-C" && _Сorrect[3] == "3-Top")
                {
                    Tablo = "           " + Phase_B[3].ToString("0.0") + "Hz     "  + "0V\n        " + "                    " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
                
            }else if (Kontakty_Transformatorov_On == false)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "2-C" && _Сorrect[3] == "3-Down")
                {
                    Tablo = "-" + Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n-" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "2-C" && _Сorrect[3] == "3-Top")
                {
                    Tablo = Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }
            else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "2-C" && _Сorrect[3] == "3-Down")
                {
                    Tablo = Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "2-C" && _Сorrect[3] == "3-Top")
                {
                    Tablo = "-" + Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n-" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }


            if (Nalichie_Zakorotok == true && N==6)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "11-Down" && _Сorrect[3] == "2-C")
                {
                    Tablo = "-" + Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n-" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "11-Top" && _Сorrect[3] == "2-C")
                {
                    Tablo = Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }else if (Obryv_Vnutrennih_Cepej_Toka == true && N==5)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "11-Down" && _Сorrect[3] == "2-C")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[0] + "V\n        " + "       " + "0A";                    
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "11-Top" && _Сorrect[3] == "2-C")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[0] + "V\n        " + "       " + "0A";                    
                    Debug.Log("фаза А");
                }
            }else if (Obryv_Vnutrennih_Cepej_Napryazheniya == true && N==4)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "11-Down" && _Сorrect[3] == "2-C")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + "0V\n        " + "       " + Phase_A[1] + "A";                    
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "11-Top" && _Сorrect[3] == "2-C")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + "0V\n        " + "       " + Phase_A[1] + "A";                    
                    Debug.Log("фаза А");
                }
            }else if (Kontakty_Transformatorov_On == false)
            {
                 
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "11-Down" && _Сorrect[3] == "2-C")
                {
                    Tablo = "-" + Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n-" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "11-Top" && _Сorrect[3] == "2-C")
                {
                    Tablo = Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }
            else
            {
                 
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "11-Down" && _Сorrect[3] == "2-C")
                {
                    Tablo = Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "11-Top" && _Сorrect[3] == "2-C")
                {
                    Tablo ="-" + Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n-" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";
                    Debug.Log("фаза А");
                }
            }
            
            

            ///////////////////////////////////////

            if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "4-Down" && _Сorrect[3] == "5-C")
            {
                Tablo = Pa_B.ToString("0.0") + "Wt              " + Phase_B[0] + "V\n" + Pr_B.ToString("0.0") + "var              " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза А");
            }

            if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "4-Top" && _Сorrect[3] == "5-C")
            {
                Tablo = "-" + Pa_B.ToString("0.0") + "Wt              " + Phase_B[0] + "V\n-" + Pr_B.ToString("0.0") + "var              " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза А");
            }
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "12-Down" && _Сorrect[3] == "5-C")
            {
                Tablo =  Pa_B.ToString("0.0") + "Wt              " + Phase_B[0] + "V\n" + Pr_B.ToString("0.0") + "var              " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза А");
            }

            if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "12-Top" && _Сorrect[3] == "5-C")
            {
                Tablo = "-" + Pa_B.ToString("0.0") + "Wt              " + Phase_B[0] + "V\n-" + Pr_B.ToString("0.0") + "var              " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза А");
            }


            //Ра =1,5(+- 0,05) ( для каждой фазы должны быть разные, но в этом интервале,Рr=0,5(+-0,02) и I=1,5мА(+-0,5)

            if (Obryv_Vnutrennih_Cepej_Napryazheniya == true && N == 4)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "5-C" && _Сorrect[3] == "5-Down")
                {
                    Tablo = "1.45Wt                    " + Phase_B[0] + "V\n" + "0.52var              " + "1.0мA";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "5-C" && _Сorrect[3] == "5-Top")
                {
                    Tablo = "-1.45Wt                    " + Phase_B[0] + "V\n-" + "0.52var              " + "1.0мA";
                    Debug.Log("фаза А");
                }
                
            }
            else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "5-C" && _Сorrect[3] == "5-Down")
                {
                    Tablo = "1.45Wt                    " + Phase_B[0] + "V\n" + "0.52var              " + "1.0мA";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "5-C" && _Сorrect[3] == "5-Top")
                {
                    Tablo = "-1.45Wt                    " + Phase_B[0] + "V\n-" + "0.52var              " + "1.0мA";
                    Debug.Log("фаза А");
                }
            }



            if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "5-C" && _Сorrect[3] == "6-Down")
            {
                Tablo = "-" + Pa_B.ToString("0.0") + "Wt              " + Phase_B[0] + "V\n-" + Pr_B.ToString("0.0") + "var              " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза А");
            }

            if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "5-C" && _Сorrect[3] == "6-Top")
            {
                Tablo = Pa_B.ToString("0.0") + "Wt              " + Phase_B[0] + "V\n" + Pr_B.ToString("0.0") + "var              " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза А");
            }


            //if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "5-C" && _Сorrect[3] == "6-Down")
            //{
            //    Tablo = Pa_B.ToString("0.0") + "Wt              " + Phase_B[0] + "V\n" + Pr_B.ToString("0.0") + "var              " + Phase_B[1].ToString("0.0") + "A";
            //    Debug.Log("фаза А");
            //}

            //if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "5-C" && _Сorrect[3] == "6-Top")
            //{
            //    Tablo =  Pa_B.ToString("0.0") + "Wt              " + Phase_B[0] + "V\n" + Pr_B.ToString("0.0") + "var              " + Phase_B[1].ToString("0.0") + "A";
            //    Debug.Log("фаза А");
            //} 
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "13-Down" && _Сorrect[3] == "5-C")
            {
                Tablo = "-" + Pa_B.ToString("0.0") + "Wt              " + Phase_B[0] + "V\n-" + Pr_B.ToString("0.0") + "var              " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза А");
            }

            if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "13-Top" && _Сorrect[3] == "5-C")
            {
                Tablo =  Pa_B.ToString("0.0") + "Wt              " + Phase_B[0] + "V\n" + Pr_B.ToString("0.0") + "var              " + Phase_B[1].ToString("0.0") + "A";
                Debug.Log("фаза А");
            }

            //////////////////////////////////


            if (Nalichie_Zakorotok == true && N == 6)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "7-Down" && _Сorrect[3] == "8-C")
                {
                    Tablo =  Pa_C.ToString("0.0") + "Wt              " + Phase_C[0] + "V\n" +  "?var              " + "0.3A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "7-Top" && _Сorrect[3] == "8-C")
                {
                    Tablo = "-" + Pa_C.ToString("0.0") + "Wt              " + Phase_C[0] + "V\n-" + "?var              " + "0.3A";
                    Debug.Log("фаза А");
                }
            }
            else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "7-Down" && _Сorrect[3] == "8-C")
                {
                    Tablo = Pa_C.ToString("0.0") + "Wt              " + Phase_C[0] + "V\n" + Pr_C.ToString("0.0") + "var              " + Phase_C[1] + "A";
                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "7-Top" && _Сorrect[3] == "8-C")
                {
                    Tablo = "-" + Pa_C.ToString("0.0") + "Wt              " + Phase_C[0] + "V\n-" + Pr_C.ToString("0.0") + "var              " + Phase_C[1] + "A";

                    Debug.Log("фаза А");
                }
            }
                
            
            
             
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "14-Down" && _Сorrect[3] == "8-C")
            {
                Tablo =  Pa_C.ToString("0.0") + "Wt              " + Phase_C[0] + "V\n" + Pr_C.ToString("0.0") + "var              " + Phase_C[1] + "A";
                Debug.Log("фаза А");
            }

            if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "14-Top" && _Сorrect[3] == "8-C")
            {
                Tablo = "-" + Pa_C.ToString("0.0") + "Wt              " + Phase_C[0] + "V\n-" + Pr_C.ToString("0.0") + "var              " + Phase_C[1] + "A";
                Debug.Log("фаза А");
            }


            //Ра =1,5(+- 0,05) ( для каждой фазы должны быть разные, но в этом интервале,Рr=0,5(+-0,02) и I=1,5мА(+-0,5)

            if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "8-C" && _Сorrect[3] == "8-Down")
            {
                Tablo = "1.55Wt                    " + Phase_C[0] + "V\n" + "0.48var              " + "2мA";
                Debug.Log("фаза А");
            }

            if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "8-C" && _Сorrect[3] == "8-Top")
            {
                Tablo = "-1.55Wt                    " + Phase_C[0] + "V\n-" + "0.48var              " + "2мA";
                Debug.Log("фаза А");
            }


            if (Nalichie_Zakorotok == true && N == 6)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "8-C" && _Сorrect[3] == "9-Down")
                {
                    Tablo = "-" + Pa_C.ToString("0.0") + "Wt              " + Phase_C[0] + "V\n" +  "-?var              " +   "0.3A";
                    Debug.Log("фаза А");
                }
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "8-C" && _Сorrect[3] == "9-Top")
                {
                    Tablo = Pa_C.ToString("0.0") + "Wt              " + Phase_C[0] + "V\n" +  "?var              " +   "0.3A";
                    Debug.Log("фаза А");
                }
            }else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "8-C" && _Сorrect[3] == "9-Down")
                {
                    Tablo = "-" + Pa_C.ToString("0.0") + "Wt              " + Phase_C[0] + "V\n-" + Pr_C.ToString("0.0") + "var              " + Phase_C[1].ToString("0.0") + "A";
                    Debug.Log("фаза А");
                }
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "8-C" && _Сorrect[3] == "9-Top")
                {
                    Tablo = Pa_C.ToString("0.0") + "Wt              " + Phase_C[0] + "V\n" + Pr_C.ToString("0.0") + "var              " + Phase_C[1].ToString("0.0") + "A";
                    Debug.Log("фаза А");
                }
            }
            
            
            

            
            
            if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] ==  "15-Down" && _Сorrect[3] == "8-C")
            {
                Tablo = "-" + Pa_C.ToString("0.0") + "Wt              " + Phase_C[0] + "V\n-" + Pr_C.ToString("0.0") + "var              " + Phase_C[1] + "A";
                Debug.Log("фаза А");
            }

            if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "15-Top"  && _Сorrect[3] == "8-C")
            {
                Tablo =  Pa_C.ToString("0.0") + "Wt              " + Phase_C[0] + "V\n" + Pr_C.ToString("0.0") + "var              " + Phase_C[1] + "A";
                Debug.Log("фаза А");
            }

            if (Nalichie_Zakorotok == true && N == 6)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] ==  "10-Down" && _Сorrect[3] == "2-C")
                {
                    Tablo =  Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";

                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "10-Top"  && _Сorrect[3] == "2-C")
                {
                    Tablo = "-" + Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n-" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";

                    Debug.Log("фаза А");
                }
            }else if (Obryv_Vnutrennih_Cepej_Toka == true && N == 5)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] ==  "10-Down" && _Сorrect[3] == "2-C")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[0] + "V\n        " + "       " + "0A";                    

                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "10-Top"  && _Сorrect[3] == "2-C")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + Phase_A[0] + "V\n        " + "       " + "0A";                    

                    Debug.Log("фаза А");
                }
            }else if (Obryv_Vnutrennih_Cepej_Napryazheniya == true && N == 4)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] ==  "10-Down" && _Сorrect[3] == "2-C")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " +  "0V\n        " + "       " + Phase_A[1] + "A";                    

                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "10-Top"  && _Сorrect[3] == "2-C")
                {
                    Tablo = "           " + Phase_A[3].ToString("0.0") + "Hz     " + "0V\n        " + "       " +Phase_A[1]+ "A";                    

                    Debug.Log("фаза А");
                }
            }else if (Kontakty_Transformatorov_On == false)
            {
                 
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] ==  "10-Down" && _Сorrect[3] == "2-C")
                {
                    Tablo =  Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";

                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "10-Top"  && _Сorrect[3] == "2-C")
                {
                    Tablo = "-" + Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n-" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";

                    Debug.Log("фаза А");
                }
            }
            else
            {
                 
                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] ==  "10-Down" && _Сorrect[3] == "2-C")
                {
                    Tablo =  "-" + Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n-" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";

                    Debug.Log("фаза А");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "10-C" && _Сorrect[2] == "10-Top"  && _Сorrect[3] == "2-C")
                {
                    Tablo = Pa_A.ToString("0.0") + "Wt              " + Phase_A[0] + "V\n" + Pr_A.ToString("0.0") + "var              " + Phase_A[1] + "A";

                    Debug.Log("фаза А");
                }
            }
            
            
            
           
            
            
            if (Obryv_Vnutrennih_Cepej_Napryazheniya == true && N == 4)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "2-C"  && _Сorrect[3] == "5-C")
                {
                    Tablo = "                         0,V\n                       0mA";                }
            
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "5-C"  && _Сorrect[3] == "8-C")
                {
                    Tablo = "           " + "                " + _Ubc + "V\n        " + Phase_B[3].ToString("0.0") + "Hz       "  + "0A";
                    
                }
                
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "2-C"  && _Сorrect[3] == "8-C")
                {
                    Tablo = "                         0,V\n                       0mA";
                }

            }else if (Podklyuchenie_Dvuh_Transformatorov_Toka == true && N == 7)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "2-C"  && _Сorrect[3] == "5-C")
                {
                    Tablo = "           " + "                " +  "0V\n        " + Phase_A[3].ToString("0.0") + "Hz       "  + "0A";
                    Debug.Log("фаза В");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "5-C"  && _Сorrect[3] == "8-C")
                {
                    Tablo = "           " + "                " + _Ubc + "V\n        " + Phase_B[3].ToString("0.0") + "Hz       "  + "0A";
                    Debug.Log("фаза В");
                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "2-C"  && _Сorrect[3] == "8-C")
                {
                    Tablo = "           " + "                " + _Uac + "V\n        " + Phase_C[3].ToString("0.0") + "Hz       "  + "0A";
                    Debug.Log("фаза В");
                }

            }
            else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "2-C"  && _Сorrect[3] == "5-C")
                {
                    Tablo = "           " + "                " + _Uab + "V\n        " + Phase_A[3].ToString("0.0") + "Hz       "  + "0A";
                    Debug.Log("фаза В");
                }


                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "5-C"  && _Сorrect[3] == "8-C")
                {
                    Tablo = "           " + "                " + _Ubc + "V\n        " + Phase_B[3].ToString("0.0") + "Hz       "  + "0A";
                    Debug.Log("фаза В");

                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "2-C"  && _Сorrect[3] == "8-C")
                {
                    Tablo = "           " + "                " + _Uac + "V\n        " + Phase_C[3].ToString("0.0") + "Hz       "  + "0A";
                    Debug.Log("фаза В");

                }

            }
            
            
            
            
            
            

            if (Podklyuchenie_Dvuh_Transformatorov_Toka == true && N == 7 )
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "2-A"  && _Сorrect[3] == "5-A")
                {
                    Tablo = "           " + "                " + "0V\n        " + Phase_A[3].ToString("0.0") + "Hz       "  + "0A";
                    Debug.Log("фаза В");

                }
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "5-A"  && _Сorrect[3] == "8-A")
                {
                    Tablo = "           " + "                " + _Uac + "V\n        " + Phase_B[3].ToString("0.0") + "Hz       "  + "0A";
                    Debug.Log("фаза В");

                }
            }
            else if (Obryv_Vnutrennih_Cepej_Napryazheniya == true && N == 4)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "2-A"  && _Сorrect[3] == "5-A")
                {
                    Tablo = "                         0,V\n                       0mA";
                    Debug.Log("фаза В");

                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "5-A"  && _Сorrect[3] == "8-A")
                {
                    Tablo = "           " + "                " + _Ubc + "V\n        " + Phase_B[3].ToString("0.0") + "Hz       "  + "0A"; Debug.Log("фаза В");

                }
            }
            else if (Podklyuchenie_Dvuh_Transformatorov_Toka == true && N == 7)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "2-A"  && _Сorrect[3] == "5-A")
                {
                    Tablo = "           " + "                " + _Uab + "V\n        " + Phase_A[3].ToString("0.0") + "Hz       "  + "0A"; Debug.Log("фаза В");

                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "5-A"  && _Сorrect[3] == "8-A")
                {
                    Tablo = "           " + "                " + _Ubc + "V\n        " + Phase_C[3].ToString("0.0") + "Hz       "  + "0A"; Debug.Log("фаза В");

                }
            }
            else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "2-A"  && _Сorrect[3] == "5-A")
                {
                    Tablo = "           " + "                " + _Uab + "V\n        " + Phase_A[3].ToString("0.0") + "Hz       "  + "0A"; Debug.Log("фаза В");

                }

                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "5-A"  && _Сorrect[3] == "8-A")
                {
                    Tablo = "           " + "                " + _Ubc + "V\n        " + Phase_B[3].ToString("0.0") + "Hz       "  + "0A"; Debug.Log("фаза В");

                }
            }


            if (Obryv_Vnutrennih_Cepej_Napryazheniya == true && N == 4)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "2-A" && _Сorrect[3] == "8-A")
                {
                    Tablo = "                         0,V\n                       0mA";

                }
            }else if (Podklyuchenie_Dvuh_Transformatorov_Toka == true && N == 7)
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "2-A"  && _Сorrect[3] == "8-A")
                {
                    Tablo = "           " + "                " + _Uac + "V\n        " + Phase_C[3].ToString("0.0") + "Hz       "  + "0A"; Debug.Log("фаза В");

                }
            }
            else
            {
                if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "2-A"  && _Сorrect[3] == "8-A")
                {
                    Tablo = "           " + "                " + _Uac + "V\n        " + Phase_C[3].ToString("0.0") + "Hz       "  + "0A"; Debug.Log("фаза В");

                }
            }
            
            
            
        }






        if (_Сorrect[0] == "" && _Сorrect[1] == "" && _Сorrect[2] == "" && _Сorrect[3] == "")
        {
            GameObject.Find("CanvasParma").transform.GetChild(0).gameObject.GetComponent<Button>().interactable = true;
            GameObject.Find("CanvasParma").transform.GetChild(1).gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            GameObject.Find("CanvasParma").transform.GetChild(0).gameObject.GetComponent<Button>().interactable = false;
            GameObject.Find("CanvasParma").transform.GetChild(1).gameObject.GetComponent<Button>().interactable = false;
        }

    }


    public void Malfunctions()
    {


        Def();       
        if (N == 8)
        {
            _Info.text = "Неисправности отключены";
        }
        if (N > 8)
        {
            N = 0;
           
        }
        if (Nesootvetstvie_Toka == true && N == 0)
        {
            _act.Instrumental._Va = -2;
            _Info.text = "Несоответствие тока и напряжения во вторичных измерительных цепях";
            int U = Random.Range(209, 237);
            float I = Mathf.Round(Random.Range(0.5f, 4.5f));
            int F = Random.Range(5, 40);
            float H = Random.Range(49.8f, 50.3f);

            Phase_A[0] = U;
            Phase_A[1] = I;
            Phase_A[2] = F;
            Phase_A[3] = H;

            Phase_B[0] = U + U / 100 * 2;
            Phase_B[1] = I + I / 100 * 5;
            Phase_B[2] = F + F / 100.0f * 20;
            Phase_B[3] = H;

            Phase_C[0] = U + U / 100 * -2;
            Phase_C[1] = I + I / 100 * -5;
            Phase_C[2] = F + F / 100.0f * -20;
            Phase_C[3] = H;

            float Cos_A = Mathf.Cos(Phase_A[2] * Mathf.Deg2Rad);
            float Cos_B = Mathf.Cos(Phase_B[2] * Mathf.Deg2Rad);
            float Cos_C = Mathf.Cos(Phase_C[2] * Mathf.Deg2Rad);

            float Sin_A = Mathf.Sin(Phase_A[2] * Mathf.Deg2Rad);
            float Sin_B = Mathf.Sin(Phase_B[2] * Mathf.Deg2Rad);
            float Sin_C = Mathf.Sin(Phase_C[2] * Mathf.Deg2Rad);



            Pa_A = Phase_A[1] * Phase_A[0] * Cos_A;
            Pa_B = Phase_B[1] * Phase_B[0] * Cos_B;
            Pa_C = Phase_C[1] * Phase_C[0] * Cos_C;

            Pr_A = Phase_A[1] * Phase_A[0] * Sin_A;
            Pr_B = Phase_B[1] * Phase_B[0] * Sin_B;
            Pr_C = Phase_C[1] * Phase_C[0] * Sin_C;


            Phase_B[0] = U;
            Phase_B[1] = I;
            Phase_B[2] = 180 - F;
            Phase_B[3] = H;

            Phase_A[0] = Phase_B[0];
            Phase_A[1] = I + I / 100 * 5;
            Phase_A[2] = 180 - F;
            Phase_A[3] = Phase_B[3];

            Phase_C[0] = U + U / 100 * -2;
            Phase_C[1] = I + I / 100 * -5;
            Phase_C[2] = F + F / 100.0f * -20;
            Phase_C[3] = H;

            Pa_B = Phase_B[1] * Phase_B[0] * Cos_B;
            Pa_C = Phase_C[1] * Phase_C[0] * Cos_C;
            Pa_A = Pa_B;

            Pr_B = Phase_B[1]* Phase_B[0] * Sin_B;
            Pr_C = Phase_C[1] * Phase_C[0] * Sin_C;
            Pr_A = Pr_B;
            
            _Uab = Random.Range(360 , 390);
            _Ubc = Random.Range(360 , 390);
            _Uac = Random.Range(360 , 390);

        }

        if (Obratnoe_Cheredovanie_Faz == true && N == 1)
        {
            _act.Instrumental._Va = 1;
            _Info.text = "Обратное чередование фаз в измерительных цепях.";
        }


        if (Vstrechnoe_Vklyuchenie_Transformatorov_Toka == true && N == 2)
        {
            _Info.text = "Встречное включение трансформаторов тока";
            _act.Instrumental._Va = -2;
            int U = Random.Range(209, 237);
            float I = Mathf.Round(Random.Range(0.5f, 4.5f));
            int F = Random.Range(5, 40);
            float H = Random.Range(49.8f, 50.3f);

            Phase_A[0] = U;
            Phase_A[1] = I;
            Phase_A[2] = F;
            Phase_A[3] = H;


            Phase_B[0] = U + U / 100 * 2;
            Phase_B[1] = I + I / 100 * 5;
            Phase_B[2] = F + F / 100.0f * 20;
            Phase_B[3] = H;

            Phase_C[0] = U + U / 100 * -2;
            Phase_C[1] = I + I / 100 * -5;
            Phase_C[2] = F + F / 100.0f * -20;
            Phase_C[3] = H;


            float Cos_A = Mathf.Cos(Phase_A[2] * Mathf.Deg2Rad);
            float Cos_B = Mathf.Cos(Phase_B[2] * Mathf.Deg2Rad);
            float Cos_C = Mathf.Cos(Phase_C[2] * Mathf.Deg2Rad);

            float Sin_A = Mathf.Sin(Phase_A[2] * Mathf.Deg2Rad);
            float Sin_B = Mathf.Sin(Phase_B[2] * Mathf.Deg2Rad);
            float Sin_C = Mathf.Sin(Phase_C[2] * Mathf.Deg2Rad);


            Pa_A = Phase_A[1] * Phase_A[0] * Cos_A;
            Pa_B = Phase_B[1] * Phase_B[0] * Cos_B;
            Pa_C = Phase_C[1] * Phase_C[0] * Cos_C;

            Pr_A = Phase_A[1] * Phase_A[0] * Sin_A;
            Pr_B = Phase_B[1] * Phase_B[0] * Sin_B;
            Pr_C = Phase_C[1] * Phase_C[0] * Sin_C;
            
            _Uab = Random.Range(360 , 390);
            _Ubc = Random.Range(360 , 390);
            _Uac = Random.Range(360 , 390);


            double Iab = ((Mathf.Pow(Phase_A[1], 2) + Mathf.Pow(Phase_B[1], 2)) - (2 * Phase_A[1] * Phase_B[1] * Mathf.Cos((Phase_A[2] - Phase_B[2] + 60) * Mathf.Deg2Rad))) * 0.5 * 0.56;
            float Fab = (Phase_A[2] + Phase_B[2]) / 2 + 110;
            Phase_A[1] = (float)Iab;
            Phase_B[1] = (float)Iab;

            Phase_A[2] = Mathf.Round(180 - Fab);
            Phase_B[2] = Mathf.Round(Fab);
        } 
        
        if (Kontakty_Transformatorov == true && N == 3)
        {
            _act.Instrumental._Va = -2;
            _Info.text = "Один или несколько трансформаторов тока подключены к электросчётчику с нарушением полярности.";
            Kontakty_Transformatorov_On = true;
            _Info.text = _Info.text + " Фаза А";
            float gradus = 180f - Phase_A[2];
            Gradus_Kont = gradus;
            Phase_A[2] = Gradus_Kont;
        }
        
        if (Podklyuchenie_Dvuh_Transformatorov_Toka == true && N == 7)
        {
            _act.Instrumental._Va = -2;
            _Info.text = "Подача напряжения одной фазы на клеммы напряжения двух фаз электросчётчика.";
            Kontakty_Transformatorov_On = false;
            
            int U = Random.Range(209, 237);
            float I = Mathf.Round(Random.Range(0.5f, 4.5f));
            int F = Random.Range(5, 40);
            float H = Random.Range(49.8f, 50.3f);

            Phase_A[0] = U;
            Phase_A[1] = I;
            Phase_A[2] = F;
            Phase_A[3] = H;

            Phase_B[0] = U + U / 100 * 2;
            Phase_B[1] = I + I / 100 * 5;
            Phase_B[2] = F + F / 100.0f * 20;
            Phase_B[3] = H;

            Phase_C[0] = U + U / 100 * -2;
            Phase_C[1] = I + I / 100 * -5;
            Phase_C[2] = F + F / 100.0f * -20;
            Phase_C[3] = H;

            float Cos_A = Mathf.Cos(Phase_A[2] * Mathf.Deg2Rad);
            float Cos_B = Mathf.Cos(Phase_B[2] * Mathf.Deg2Rad);
            float Cos_C = Mathf.Cos(Phase_C[2] * Mathf.Deg2Rad);

            float Sin_A = Mathf.Sin(Phase_A[2] * Mathf.Deg2Rad);
            float Sin_B = Mathf.Sin(Phase_B[2] * Mathf.Deg2Rad);
            float Sin_C = Mathf.Sin(Phase_C[2] * Mathf.Deg2Rad);



            Pa_A = Phase_A[1] * Phase_A[0] * Cos_A;
            Pa_B = Phase_B[1] * Phase_B[0] * Cos_B;
            Pa_C = Phase_C[1] * Phase_C[0] * Cos_C;

            Pr_A = Phase_A[1] * Phase_A[0] * Sin_A;
            Pr_B = Phase_B[1] * Phase_B[0] * Sin_B;
            Pr_C = Phase_C[1] * Phase_C[0] * Sin_C;


            Phase_B[0] = U;
            Phase_B[1] = I;
            Phase_B[2] = F;
            Phase_B[3] = H;

            Phase_A[0] = Phase_B[0];
            Phase_A[1] = I + I / 100 * 5;
            Phase_A[2] = 180 - F;
            Phase_A[3] = H;

            Phase_C[0] = U + U / 100 * -2;
            Phase_C[1] = I + I / 100 * -5;
            Phase_C[2] = F;
            Phase_C[3] = H;

            Pa_B = Phase_B[1] * Phase_B[0] * Cos_B;
            Pa_C = Phase_C[1] * Phase_C[0] * Cos_C;
            Pa_A = Phase_A[1] * Phase_A[0] * Cos_A;

            Pr_B = Phase_B[1] * Phase_B[0] * Sin_B;
            Pr_C = Phase_C[1] * Phase_C[0] * Sin_C;
            Pr_A = Phase_A[1] * Phase_A[0] * Sin_A;
            
            _Uab = Random.Range(360 , 390);
            _Ubc = Random.Range(360 , 390);
            _Uac = Random.Range(360 , 390);
        }
        
        if (Nalichie_Zakorotok == true && N == 6)
        {
            _Info.text = "Наличие закороток в испытательной клеммной коробке.";

           
            
            _Info.text = _Info.text + " Фаза C";
        }


        if (Obryv_Vnutrennih_Cepej_Napryazheniya == true && N == 4)
        {_act.Instrumental._Va = -2;
            Kontakty_Transformatorov_On = false;
            _Info.text = "Обрыв цепей напряжения в измерительной цепи.";
            Phase_A[2] = 0;
        }


        if (Obryv_Vnutrennih_Cepej_Toka == true && N == 5)
        {_act.Instrumental._Va = -2;
            _Info.text = "Обрыв цепей тока в измерительной  цепи.";
            Phase_A[2] = 0;
        }

        _amperageA = Phase_A[1];
        _amperageB = Phase_B[1];
        _amperageC = Phase_C[1];

        Save_Act();
    }

    void Def()
    {
        int U = Random.Range(209, 237);
        float I = Mathf.Round(Random.Range(1.5f, 4.5f));
        int F = Random.Range(5, 40);
        float H = Random.Range(49.8f, 50.3f);

        Phase_A[0] = U;
        Phase_A[1] = I;
        Phase_A[2] = F;
        Phase_A[3] = H;

        Phase_B[0] = U + U / 100 * 2;
        Phase_B[1] = I + I / 100 * 5;
        Phase_B[2] = F + F / 100.0f * 20;
        Phase_B[3] = H;

        Phase_C[0] = U + U / 100 * -2;
        Phase_C[1] = I + I / 100 * -5;
        Phase_C[2] = F + F / 100.0f * -20;
        Phase_C[3] = H;

        float Cos_A = Mathf.Cos(Phase_A[2] * Mathf.Deg2Rad);
        float Cos_B = Mathf.Cos(Phase_B[2] * Mathf.Deg2Rad);
        float Cos_C = Mathf.Cos(Phase_C[2] * Mathf.Deg2Rad);

        float Sin_A = Mathf.Sin(Phase_A[2] * Mathf.Deg2Rad);
        float Sin_B = Mathf.Sin(Phase_B[2] * Mathf.Deg2Rad);
        float Sin_C = Mathf.Sin(Phase_C[2] * Mathf.Deg2Rad);



        Pa_A = Phase_A[1] * Phase_A[0] * Cos_A;
        Pa_B = Phase_B[1] * Phase_B[0] * Cos_B;
        Pa_C = Phase_C[1] * Phase_C[0] * Cos_C;

        Pr_A = Phase_A[1] * Phase_A[0] * Sin_A;
        Pr_B = Phase_B[1] * Phase_B[0] * Sin_B;
        Pr_C = Phase_C[1] * Phase_C[0] * Sin_C;
        
        _Uab = Random.Range(360 , 390);
        _Ubc = Random.Range(360 , 390);
        _Uac = Random.Range(360 , 390);

        float error = (float)System.Math.Round(UnityEngine.Random.Range(0.86f, 0.96f),2);
        _act.Instrumental._Error = System.Math.Round(error, 2).ToString();

        X = error;
        float aPhase = (Phase_A[1] * Phase_A[0] * Mathf.Cos(Phase_A[2] * Mathf.Deg2Rad)) / 1000;
        float bPhase = (Phase_B[1] * Phase_B[0] * Mathf.Cos(Phase_B[2] * Mathf.Deg2Rad)) / 1000;
        float cPhase = (Phase_C[1] * Phase_C[0] * Mathf.Cos(Phase_C[2] * Mathf.Deg2Rad)) / 1000;
        //P = (float)System.Math.Round(aPhase + bPhase + cPhase, 1);
        P = (float)System.Math.Round((Pa_A + Pa_B + Pa_C), 1);
        W = (float)System.Math.Round(((X / 100f) * P) + P, 1);

    }
    public void On_Off(bool status)
    {
        Probe_3 = status;
        
        if (GloreUpload == false)
        {
            GloreFail = true;
        }
    }

    public void Save_Act()
    {
        Debug.Log("Saved parma");
        _act.Instrumental._PaA = Pa_A.ToString();
        _act.Instrumental._PaB = Pa_B.ToString();
        _act.Instrumental._PaC = Pa_C.ToString();
        _act.Instrumental._PrA = Pr_A .ToString();
        _act.Instrumental._PrB = Pr_B .ToString();
        _act.Instrumental._PrC = Pr_C .ToString();
        _act.Instrumental._TokA = Phase_A[1].ToString("0.0");
        _act.Instrumental._TokB = Phase_B[1].ToString("0.0");
        _act.Instrumental._TokC = Phase_C[1].ToString("0.0");
        _act.Instrumental._Ua = Phase_A[0].ToString();
        _act.Instrumental._Ub = Phase_B[0].ToString();
        _act.Instrumental._Uc = Phase_C[0].ToString();
        _act.Instrumental._Uab = _Uab.ToString();
        _act.Instrumental._Ubc = _Ubc.ToString();
        _act.Instrumental._Uac = _Uac.ToString();
        _act.Instrumental._Uab = _Uab.ToString();
        _act.Instrumental._Ubc = _Ubc.ToString();
        _act.Instrumental._Uac = _Uac.ToString();
        _act.Instrumental._Fa = Phase_A[2].ToString();
        _act.Instrumental.W = W;
        _act.Instrumental.P = P;
        _act.Instrumental.X = X;
        if (Vstrechnoe_Vklyuchenie_Transformatorov_Toka == true && N== 2)
        {
            _act.Instrumental._Fb = Phase_B[2].ToString();
        }
        else
        {
            _act.Instrumental._Fb = Phase_B[2].ToString();
        }
        
        _act.Instrumental._Fc = Phase_C[2].ToString();
        _act.Instrumental._CosA = Mathf.Cos(Phase_A[2] * Mathf.Deg2Rad).ToString();
        _act.Instrumental._CosB = Mathf.Cos(Phase_B[2] * Mathf.Deg2Rad).ToString();
        _act.Instrumental._CosC = Mathf.Cos(Phase_C[2] * Mathf.Deg2Rad).ToString();
        //float PA = Mathf.Pow(Pa_A, 2) + Mathf.Pow(Pr_A, 2);
        //float PB = Mathf.Pow(Pa_B, 2) + Mathf.Pow(Pr_B, 2);
        //float PC = Mathf.Pow(Pa_C, 2) + Mathf.Pow(Pr_C, 2);
        _act.Instrumental._PA = Pr_A.ToString();
        _act.Instrumental._PB = Pr_B.ToString();
        _act.Instrumental._PC = Pr_C.ToString();



    }

    public void Restart()
    {
        
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex ) ;
    }

}