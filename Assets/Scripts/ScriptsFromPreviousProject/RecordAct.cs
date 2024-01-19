using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RecordAct : MonoBehaviour
{
    public TMP_Text Nomer_Act;
    public TMP_Text Date_Act;
    public InputField FIO;
    public InputField DZO;
    public InputField IP;
    public InputField FIO_IP;
    public InputField Adress;
    public InputField Tochka;
    public InputField Old_Check;
    public InputField Tip;
    public InputField Nomer_Shtka;
    public InputField God_Izgotov;
    public InputField Max_Tok;
    public InputField Max_Nap;
    public Text Start_Time;
    public Text End_Time;
    public InputField Prinadle;
    public InputField Start_Chek;
    public InputField End_Chek;
    public InputField[] PaA;
    public InputField[] PaB;
    public InputField[] PaC;
    public InputField[] PrA;
    public InputField Nal_Otmet;
    public InputField Povrejdeniy;
    public InputField Indicator;
    public InputField[] TT1;
    public InputField[] TT2;
    public InputField[] TT3;
    public InputField KleshiA;
    public InputField KleshiB;
    public InputField KleshiC;
    public InputField TokA;
    public InputField TokB;
    public InputField TokC;
    public InputField Ua;
    public InputField Ub;
    public InputField Uc;
    public InputField Uab;
    public InputField Ubc;
    public InputField Uac;
    public InputField Fa;
    public InputField Fb;
    public InputField Fc;
    public InputField Imp;
    public InputField Pa;
    public InputField PaInp;
    public InputField CosA;
    public InputField CosB;
    public InputField CosC;
    public InputField Cheredovanie;
    public InputField PA;
    public InputField PB;
    public InputField PC;
    public InputField P;
    public InputField[] Date;
    public InputField Ustr_Date;
    public InputField Data_Podp;
    public InputField[] Fio_Podp;
    public InputField[] Summa;
    public InputField[] Tok;
    public InputField Sec_Time;
    public InputField Problem;
    public InputField[] OtsutstviePlomb;
    public InputField[] OtsutstviePlomb2;




    public Act _Act;


    private void Start()
    {         
        Nomer_Act.text = Random.Range(12, 48).ToString() + "/" + Random.Range(111111, 555555).ToString();
        _Act.Instrumental.Nomer_Inst = Nomer_Act.text;
        Date_Act.text = DateTime.Today.ToString("dd MM y");
        FIO.text = _Act.Instrumental._FIO;
        DZO.text = _Act.Instrumental._Name_DZO;
        IP.text = "Индивидуальный Предприниматель";
        FIO_IP.text = "Петров Андрей Александрович";
        Adress.text = "ул.Строителей";
        Tochka.text = "ТП-10кВ №10, ВЛ-10кВ филер Новая ПС-110/10кВ Южная";
        Old_Check.text = "26.01.2019";
        Tip.text = "4TM.03M";
        Nomer_Shtka.text = "0112055629";
        God_Izgotov.text = "2020";
        Max_Tok.text = "5(10)A";
        Max_Nap.text = "3x(120-230)";
        Start_Time.text = _Act.Instrumental._Start_Time;
        if (_Act.Instrumental._Sec_Time != "")
        {
            End_Time.text = _Act.Instrumental._Sec_Time;
        }
        Prinadle.text = "Потребитель";
        Start_Chek.text = "1-19";
        End_Chek.text = "1-29";
        
        
        PaA[0].text = _Act.Instrumental._PaA;
        PaA[1].text = _Act.Instrumental._PaA;
        PaB[0].text = _Act.Instrumental._PaB;
        PaB[1].text = _Act.Instrumental._PaB;
        PaC[0].text = _Act.Instrumental._PaC;
        PaC[1].text = _Act.Instrumental._PaC;
        PrA[0].text = _Act.Instrumental._PrA;
        PrA[1].text = _Act.Instrumental._PrA;
        
        
        
        Date[0].text = _Act.Instrumental.Kleshi;
        Date[1].text = DateTime.Today.AddDays(UnityEngine.Random.Range(20, 64)).ToString("dd MMMM yyyy");
        Date[2].text = _Act.Instrumental.Parma;
        Date[3].text = DateTime.Today.AddDays(UnityEngine.Random.Range(20, 64)).ToString("dd MMMM yyyy");
        Ustr_Date.text = DateTime.Today.AddDays(10).ToString("dd MMMM yyyy");
        Data_Podp.text = DateTime.Today.ToString("dd MMMM yyyy");

        
        Fio_Podp[0].text = FIO.text = _Act.Instrumental._FIO;
        Fio_Podp[1].text = FIO.text = _Act.Instrumental._FIO;

        float sum = float.Parse(_Act.Instrumental._PaA) + float.Parse(_Act.Instrumental._PaB) + float.Parse(_Act.Instrumental._PaC);

        Summa[0].text = sum.ToString();
        Summa[1].text = sum.ToString();

        TT3[1].text = _Act.Instrumental.TT3[1];
        TT3[2].text = _Act.Instrumental.TT3[2];
        TT3[3].text = _Act.Instrumental.TT3[3];

        TT2[1].text = _Act.Instrumental.TT2[1];
        TT2[2].text = _Act.Instrumental.TT2[2];
        TT2[3].text = _Act.Instrumental.TT2[3];

        TT1[1].text = _Act.Instrumental.TT1[1];
        TT1[2].text = _Act.Instrumental.TT1[2];
        TT1[3].text = _Act.Instrumental.TT1[3];
        if (SceneManager.GetActiveScene().name == "Akt_1Referi")
        {
            Problem.text = _Act.Instrumental._ProblemReferi;
            
            Imp.text = _Act.Instrumental._Cout_Inp;
            Sec_Time.text = _Act.Instrumental._Sec_Time;
            Cheredovanie.text = _Act.Instrumental._Cheredovanie;
            Povrejdeniy.text = _Act.Instrumental._Treshesna;
            Indicator.text = _Act.Instrumental._Indecator;
            TokA.text = _Act.Instrumental._TokA;
            TokB.text = _Act.Instrumental._TokB;
            TokC.text = _Act.Instrumental._TokC;
            Ua.text = _Act.Instrumental._Ua;
            Ub.text = _Act.Instrumental._Ub;
            Uc.text = _Act.Instrumental._Uc;
            //P.text = "410,6";
            Problem.text = _Act.Instrumental._ProblemReferi;
            Uab.text = _Act.Instrumental._Uab;
            Ubc.text = _Act.Instrumental._Ubc;
            Uac.text = _Act.Instrumental._Uac;
            OtsutstviePlomb[0].text = _Act.Instrumental._OtsutstviePlomb;
            OtsutstviePlomb[1].text = _Act.Instrumental._OtsutstviePlomb;
            OtsutstviePlomb[2].text = _Act.Instrumental._OtsutstviePlomb;
            OtsutstviePlomb[3].text = _Act.Instrumental._OtsutstviePlomb;

            OtsutstviePlomb2[0].text = _Act.Instrumental._OtsutstviePlomb2;
            OtsutstviePlomb2[1].text = _Act.Instrumental._OtsutstviePlomb2;

            Fb.text = _Act.Instrumental._Fb + "L°";
            Fc.text = _Act.Instrumental._Fc + "L°";
            
            if (_Act.Instrumental._Va == 1)
            {
                Fa.text = _Act.Instrumental._Fa + "L°";
            }
            else if(_Act.Instrumental._Va == -1)
            {
                Fa.text = _Act.Instrumental._Fa + "C°";
                Fb.text = _Act.Instrumental._Fb + "C°";
            }else if(_Act.Instrumental._Va == -2)
            {
                Fa.text = _Act.Instrumental._Fa + "C°";
                Fb.text = _Act.Instrumental._Fb + "L°";
            }

            
            if (_Act.Instrumental._Fa == "0")
            {
                Fa.text = "";
            }


            if (_Act.Instrumental._CosA == "0")
            {
                CosA.text = "";
            }
            else
            {
                CosA.text = _Act.Instrumental._CosA;
            }
            
            CosB.text = _Act.Instrumental._CosB;
            CosC.text = _Act.Instrumental._CosC;
            PA.text = _Act.Instrumental._PA;
            PB.text = _Act.Instrumental._PB;
            PC.text = _Act.Instrumental._PC;

            

            KleshiA.text = _Act.Instrumental._KleshiA;
            KleshiB.text = _Act.Instrumental._KleshiB;
            KleshiC.text = _Act.Instrumental._KleshiC;
            TT1[0].text = _Act.Instrumental.TT1[0];

            TT1[4].text = _Act.Instrumental.TT1[4];

            TT2[0].text = _Act.Instrumental.TT2[0];

            TT2[4].text = _Act.Instrumental.TT2[4];

            TT3[0].text = _Act.Instrumental.TT3[0];

            TT3[4].text = _Act.Instrumental.TT3[4];
            Tok[0].text = "1.5мA";
            Tok[1].text = "1.0мA";
            Tok[2].text = "2.0мA";
            Pa.text = _Act.Instrumental._PaA;
            float r = float.Parse(_Act.Instrumental._Cout_Inp);
            float t = float.Parse(_Act.Instrumental._Sec_Time);
            float Pinp = (3600 * r) / (1250 * t);
            PaInp.text = Pinp.ToString();
        }
        

        
    }
}
