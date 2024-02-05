using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



 public class MenuSetting : MonoBehaviour
{

     public InputField _Time;

     public Toggle _SIZerror;

     public Toggle _TreshchinaKorpuseSchetchika;

     public Toggle _PereobzhataPlomba;

     public Toggle _PovrezhdenieVtorichnyhIzmeritel;

     public Toggle _BoltsPodKrysh;

     public Toggle _PeremichkaSuntirovanay;

     public Toggle _PodklyuchenieSilovyhCepej;

     public Toggle _BoltNaBlokeProverk;

     public Toggle _NeRabotaetZHKDisplej;

     public Toggle _NalichieMagnita;

     public Toggle _PlombEnergosnabzhayushchejs;

     public Toggle _OtsutstviePlomb;

     public Toggle _TransformatoryToka;

     public Toggle _Indicator;

     public Toggle _Pemichka;

    
     public Toggle Nesootvetstvie_Toka;
    
     public Toggle Vstrechnoe_Vklyuchenie_Transformatorov_Toka;
    
     public Toggle Kontakty_Transformatorov;
    
     public Toggle Podklyuchenie_Dvuh_Transformatorov_Toka;
    
     public Toggle Nalichie_Zakorotok;
    
     public Toggle Obryv_Vnutrennih_Cepej_Toka;
    
     public Toggle Obryv_Vnutrennih_Cepej_Napryazheniya;
    
     public Toggle Obratnoe_Cheredovanie_Faz;
    
     public Toggle Gercon;

        public Setting _Setting; 
    
    void Awake()
    {
        _Setting.LoadTime();
        _Time.text = _Setting._TimeRun.ToString();
        _TreshchinaKorpuseSchetchika.isOn = _Setting._TreshchinaKorpuseSchetchika;
        _PereobzhataPlomba.isOn = _Setting._PereobzhataPlomba;
        _PovrezhdenieVtorichnyhIzmeritel.isOn = _Setting._PovrezhdenieVtorichnyhIzmeritel;
        _BoltsPodKrysh.isOn = _Setting._BoltsPodKrysh;
        _PeremichkaSuntirovanay.isOn = _Setting._PeremichkaSuntirovanay;
        _PodklyuchenieSilovyhCepej.isOn = _Setting._PodklyuchenieSilovyhCepej;
        _BoltNaBlokeProverk.isOn = _Setting._BoltNaBlokeProverk;
        _NeRabotaetZHKDisplej.isOn = _Setting._NeRabotaetZHKDisplej;
        _NalichieMagnita.isOn = _Setting._NalichieMagnita;
        _PlombEnergosnabzhayushchejs.isOn = _Setting._PlombEnergosnabzhayushchejs;
        _OtsutstviePlomb.isOn = _Setting._OtsutstviePlomb;
        _TransformatoryToka.isOn = _Setting._TransformatoryToka;
        _Indicator.isOn = _Setting._Indicator;
        _Pemichka.isOn = _Setting._Pemichka;

        Nesootvetstvie_Toka.isOn = _Setting.Nesootvetstvie_Toka;
        Vstrechnoe_Vklyuchenie_Transformatorov_Toka.isOn = _Setting.Vstrechnoe_Vklyuchenie_Transformatorov_Toka;
        Kontakty_Transformatorov.isOn = _Setting.Kontakty_Transformatorov;
        Podklyuchenie_Dvuh_Transformatorov_Toka.isOn = _Setting.Podklyuchenie_Dvuh_Transformatorov_Toka;
        Nalichie_Zakorotok.isOn = _Setting.Nalichie_Zakorotok;
        Obryv_Vnutrennih_Cepej_Toka.isOn = _Setting.Obryv_Vnutrennih_Cepej_Toka;
        Obryv_Vnutrennih_Cepej_Napryazheniya.isOn = _Setting.Obryv_Vnutrennih_Cepej_Napryazheniya;
        Obratnoe_Cheredovanie_Faz.isOn = _Setting.Obratnoe_Cheredovanie_Faz;
        
        Gercon.isOn = _Setting.Gercon;
        


    }

    public void Accept()
    {
        
        _Setting._TimeRun = int.Parse(_Time.text);
        //_Setting.SaveTime();
        //_Setting._SIZError = _SIZerror.isOn;
        //_Setting._TreshchinaKorpuseSchetchika =  _TreshchinaKorpuseSchetchika.isOn;
        // _Setting._PereobzhataPlomba = _PereobzhataPlomba.isOn;
        // _Setting._PovrezhdenieVtorichnyhIzmeritel = _PovrezhdenieVtorichnyhIzmeritel.isOn;
        // _Setting._BoltsPodKrysh = _BoltsPodKrysh.isOn;
        // _Setting._PeremichkaSuntirovanay = _PeremichkaSuntirovanay.isOn;
        // _Setting._PodklyuchenieSilovyhCepej = _PodklyuchenieSilovyhCepej.isOn;
        // _Setting._BoltNaBlokeProverk = _BoltNaBlokeProverk.isOn;
        // _Setting._NeRabotaetZHKDisplej = _NeRabotaetZHKDisplej.isOn;
        // _Setting._NalichieMagnita = _NalichieMagnita.isOn;
        // _Setting._PlombEnergosnabzhayushchejs = _PlombEnergosnabzhayushchejs.isOn;
        // _Setting._OtsutstviePlomb = _OtsutstviePlomb.isOn;
        // _Setting._TransformatoryToka = _TransformatoryToka.isOn;
        // _Setting._Indicator = _Indicator.isOn;
        // _Setting._Pemichka = _Pemichka.isOn;


        // _Setting.Nesootvetstvie_Toka = Nesootvetstvie_Toka.isOn;
        // _Setting.Vstrechnoe_Vklyuchenie_Transformatorov_Toka = Vstrechnoe_Vklyuchenie_Transformatorov_Toka.isOn;
        // _Setting.Podklyuchenie_Dvuh_Transformatorov_Toka = Podklyuchenie_Dvuh_Transformatorov_Toka.isOn;
        // _Setting.Nalichie_Zakorotok=Nalichie_Zakorotok.isOn;
        // _Setting.Obryv_Vnutrennih_Cepej_Toka=Obryv_Vnutrennih_Cepej_Toka.isOn;
        // _Setting.Obryv_Vnutrennih_Cepej_Napryazheniya=Obryv_Vnutrennih_Cepej_Napryazheniya.isOn;
        // _Setting.Obratnoe_Cheredovanie_Faz=Obratnoe_Cheredovanie_Faz.isOn;

        // _Setting.Gercon = Gercon.isOn;
         _Setting.Save();
        _Setting.SetDirtySave();
        //_Setting.SaveTime();
    }
}
