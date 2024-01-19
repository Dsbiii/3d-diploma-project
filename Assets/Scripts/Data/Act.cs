using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(menuName = "Act", fileName = "Act")]
public class Act : ScriptableObject
{

    [SerializeField] public ActData Instrumental;


    [System.Serializable]
    public class ActData
    {
        public string _FIO;
        public string _Name_DZO;
        public string _Branch;
        public string _JobTittle;
        public string _Start_Time;
        public string _Stop_Time;
        public string _PaA;
        public string _PaB;
        public string _PaC;
        public string _PrA;
        public string _PrB;
        public string _PrC;
        public string _TokA;
        public string _TokB;
        public string _TokC;
        public string _Ua;
        public string _Ub;
        public string _Uc;
        public string _Uab;
        public string _Ubc;
        public string _Uac;
        public string _Fa;
        public string _Fb;
        public string _Fc;
        public string _CosA;
        public string _CosB;
        public string _CosC;
        public string _PA;
        public string _PB;
        public string _PC;
        public string _Cheredovanie;
        public string _Treshesna;
        public string _Indecator;
        public int _Va;
        public string _Sec_Time;
        public string _Cout_Inp;
        public string _Problem;
        public string _ProblemReferi;
        public string _OtsutstviePlomb;
        public string _OtsutstviePlomb2;
        public string _CepiProblem;
        public string _KleshiA;
        public string _KleshiB;
        public string _KleshiC;
        public string _Error;

        public string _CE602MDate;
        public string _ParmaDate;
        public string _ClampsDate;
        public string _TimerDate;

        public string[] TT1;
        public string[] TT2;
        public string[] TT3;


        public string Kleshi;
        public string Parma;
        public string Nomer_Inst;
        public float W;
        public float X;
        public float P;


    }

    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.dataPath, "Akt.Save"));
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(string.Concat(Application.dataPath, "Akt.Save")))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.dataPath, "Akt.Save"), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }

    //private void Awake()
    //{
    //    Load();
    //}

    public void Reset()
    {
        Instrumental._ProblemReferi = "";
    }
}
