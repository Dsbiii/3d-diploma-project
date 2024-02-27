using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

[CreateAssetMenu(menuName = "Setting", fileName = "Setting")]
public class Setting : ScriptableObject
{
    public int _TimeRun = 30;
    public bool _SIZError;
    public bool _TreshchinaKorpuseSchetchika;
    public bool _PereobzhataPlomba;
    public bool _PovrezhdenieVtorichnyhIzmeritel;
    public bool _BoltsPodKrysh;
    public bool _PeremichkaSuntirovanay;
    public bool _PodklyuchenieSilovyhCepej;
    public bool _BoltNaBlokeProverk;
    public bool _NeRabotaetZHKDisplej;
    public bool _NalichieMagnita;
    public bool _PlombEnergosnabzhayushchejs;
    public bool _OtsutstviePlomb;
    public bool _TransformatoryToka;
    public bool _Indicator;
    public bool _Pemichka;
    public bool Nesootvetstvie_Toka;
    public bool Vstrechnoe_Vklyuchenie_Transformatorov_Toka;
    public bool Kontakty_Transformatorov;
    public bool Podklyuchenie_Dvuh_Transformatorov_Toka;
    public bool Nalichie_Zakorotok;
    public bool Obryv_Vnutrennih_Cepej_Toka;
    public bool Obryv_Vnutrennih_Cepej_Napryazheniya;
    public bool Obratnoe_Cheredovanie_Faz;
    public bool Gercon;


    public void SaveTime()
    {
        Save();
        //PlayerPrefs.SetInt("Time", _TimeRun);
    }

    public void LoadTime()
    {
        //if (PlayerPrefs.HasKey("Time"))
        //{
        //    _TimeRun = PlayerPrefs.GetInt("Time");
        //}
        //else
        //{
        //    _TimeRun = 120;
        //}
    }

    public void SetDirtySave()
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }

    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.dataPath, "Setting.Save"));
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(string.Concat(Application.dataPath, "Setting.Save")))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.dataPath, "Setting.Save"), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }

    //private void Awake()
    //{
    //    Load();
    //}
}
