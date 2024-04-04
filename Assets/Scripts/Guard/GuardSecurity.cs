using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Zenject;
using Assets.Scripts;

[System.Serializable]
public class GuardDataDTO
{
    public string Cofficient;
    public int TermsOfUssage;
    public DateTime LastLaunch;
    public bool IsWrong;
    public int ActivationDay;
    public int ActivationMonth;
    public int ActivationYear;

    public int LastLaunchDay;
    public int LastLaunchMounth;
    public int LastLaunchYear;
    public int LastLaunchHour;
    public int LastLaunchMinute;
    public int LastLaunchSecond;
    public int DaysLeft;
    public string ActivationKey;
    public bool IsDemoVersion;

    public GuardDataDTO(string cofficient, int activationDay, int activationMonth, int activationYear, int termsOfUssage,
        DateTime lastLaunch, string activationKey, int daysLeft, bool isWorng)
    {
        IsWrong = isWorng;
        Cofficient = cofficient;
        ActivationDay = activationDay;
        ActivationMonth = activationMonth;
        ActivationYear = activationYear;
        TermsOfUssage = termsOfUssage;
        ActivationKey = activationKey;
        LastLaunchDay = lastLaunch.Day;
        LastLaunchMounth = lastLaunch.Month;
        LastLaunchYear = lastLaunch.Year;
        LastLaunchHour = lastLaunch.Hour;
        LastLaunchMinute = lastLaunch.Minute;
        LastLaunchSecond = lastLaunch.Second;
        DaysLeft = daysLeft;
    }

}

public class GuardSecurity : MonoBehaviour
{
    [SerializeField] private int _simulatorCode;
    [SerializeField] private TMP_InputField _keyField;
    [SerializeField] private TMP_Text _requestKeyField;
    [SerializeField] private TMP_Text _dayToDeativeField;
    [SerializeField] private GameObject _activitionPanel;
    [SerializeField] private GameObject _lauchPanel;
    [Inject] private GameMode _gameMode;


    private string _keyGenerated;
    private int[] _key = new int[5] { 1, 2, 3, 4, 5 };
    private string _cofficient;
    private string _controllSumm;
    private DateTime _lastLaunch;
    private string _path;

    private void Awake()
    {
        Screen.SetResolution(650, 650, false);
        _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                $"SC{_simulatorCode}Keyv2.json");
        _lastLaunch = DateTime.Now;
        Debug.Log("_path " + _path);
        CheckDate();

        //LaunchProgram();
    }

    private DateTime GetDate(string requestChar)
    {
        int year;
        int mounth;
        int day;
        Debug.Log(requestChar);
        if (requestChar.Length == 20)
        {
            if (requestChar.Length > 20)
            {

            }
            _cofficient = requestChar[2].ToString() + requestChar[3].ToString();
            year = int.Parse(requestChar[10].ToString() + requestChar[11].ToString());
            mounth = int.Parse(requestChar[12].ToString() + requestChar[13].ToString());
            day = int.Parse(requestChar[14].ToString() + requestChar[15].ToString());
            _controllSumm = requestChar[4].ToString() + requestChar[5].ToString() + requestChar[6].ToString() + requestChar[7].ToString();
        }
        else
        {
            _cofficient = requestChar[1].ToString() + requestChar[2].ToString();
            year = int.Parse(requestChar[9].ToString() + requestChar[10].ToString());
            mounth = int.Parse(requestChar[11].ToString() + requestChar[12].ToString());
            day = int.Parse(requestChar[13].ToString() + requestChar[14].ToString());



            _controllSumm = requestChar[3].ToString() + requestChar[4].ToString() + requestChar[5].ToString() + requestChar[6].ToString();
        }

        return new DateTime(2000 + year, mounth, day);

    }

    public void GenerateKey()
    {
        _keyGenerated = PlayerPrefs.GetString("GeneratedKeyv2" + _simulatorCode);
        if (PlayerPrefs.HasKey("GeneratedKeyv2" + _simulatorCode))
        {
            DateTime dateTime = GetDate(_keyGenerated);
            Debug.Log(dateTime.Year + " " + dateTime.Month + " " + dateTime.Day);
            Debug.Log((new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) - dateTime).Days);
            if ((new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) - dateTime).Days >= 7 ||
                (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) - dateTime).Days < 0)
            {
                CreateKey();
            }
            else
            {
                _requestKeyField.text = _keyGenerated;
            }
        }
        else
        {
            CreateKey();
        }
    }

    private void CreateKey()
    {
        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        _cofficient = alphabet[UnityEngine.Random.Range(0, alphabet.Length)].ToString() +
            alphabet[UnityEngine.Random.Range(0, alphabet.Length)].ToString();

        string twoSymbols = _simulatorCode.ToString() + _cofficient.ToString();
        string id = SystemInfo.deviceUniqueIdentifier;
        _controllSumm = GetControllSum(id).Substring(0, 4);
        string controllSumWithCofficient = _controllSumm + _cofficient;
        string[] dateArray = DateTime.Now.ToString("yy-MM-dd").Split('-');

        string date = dateArray[0] + dateArray[1] + dateArray[2] + _cofficient;

        Debug.Log("twoSymbols " + twoSymbols.ToString() + " controlSumm " + controllSumWithCofficient +
            " date " + date + " generatedCofficient " + _cofficient.ToString());

        string request = twoSymbols + controllSumWithCofficient + date + _cofficient;

        _keyGenerated = request;
        _requestKeyField.text = _keyGenerated;
        PlayerPrefs.SetString("GeneratedKeyv2" + _simulatorCode, _keyGenerated);
        Debug.Log(request + " " + request.Length);

    }

    public bool TryLoadData(out GuardDataDTO guardDTO)
    {
        if (File.Exists(Path.Combine(_path)))
        {
            guardDTO = JsonUtility.FromJson<GuardDataDTO>(File.ReadAllText(_path));
            return true;
        }
        else
        {
            guardDTO = null;
            return false;
        }
    }

    public void LaunchProgram()
    {
        Screen.SetResolution(1920, 1080, true);
        SceneManager.LoadScene(1);
    }

    public void CheckDate()
    {
        if (TryLoadData(out GuardDataDTO guardDTO))
        {
            DateTime dateTime = new DateTime(2000 + guardDTO.ActivationYear, guardDTO.ActivationMonth, guardDTO.ActivationDay);
            DateTime lastLaunch = new DateTime(guardDTO.LastLaunchYear, guardDTO.LastLaunchMounth, guardDTO.LastLaunchDay, guardDTO.LastLaunchHour, guardDTO.LastLaunchMinute, guardDTO.LastLaunchSecond);

            Debug.Log(guardDTO.TermsOfUssage);
            Debug.Log((new DateTime(guardDTO.LastLaunchYear, guardDTO.LastLaunchMounth, guardDTO.LastLaunchDay).Day - DateTime.Now.Day));

            int days = (dateTime.AddDays(guardDTO.TermsOfUssage) - DateTime.Now).Days + 1;
            Debug.Log("Days " + days + " guardDTO.DaysLeft " + guardDTO.DaysLeft);

            if (days > guardDTO.DaysLeft)
            {
                Save(guardDTO.Cofficient, guardDTO.ActivationDay, guardDTO.ActivationMonth, guardDTO.ActivationYear, guardDTO.TermsOfUssage,
                    new DateTime(), guardDTO.ActivationKey, 1000000000, true);
                CreateKey();
                OpenActivationPanel();
                return;
            }

            if (!guardDTO.IsWrong && (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) - dateTime).Days <= guardDTO.TermsOfUssage || guardDTO.TermsOfUssage == 999)
            {
                Save(guardDTO.Cofficient, guardDTO.ActivationDay, guardDTO.ActivationMonth, guardDTO.ActivationYear, guardDTO.TermsOfUssage,
                    DateTime.Now, guardDTO.ActivationKey, days, false);
                Debug.Log("guardDTO.TermsOfUssage " + guardDTO.TermsOfUssage);
                if (guardDTO.TermsOfUssage == 5 || guardDTO.TermsOfUssage == 15)
                {
                    _gameMode.SetupDemo(true);
                }
                else
                {
                    _gameMode.SetupDemo(false);
                }
                if (guardDTO.TermsOfUssage != 999)
                {
                    _dayToDeativeField.text = ((dateTime.AddDays(guardDTO.TermsOfUssage) - DateTime.Now).Days + 1).ToString();
                }
                else
                {
                    _dayToDeativeField.text = "Бессрочная";
                }
                OpenLaunchPanel();
            }
            else
            {
                GenerateKey();
                OpenActivationPanel();
            }
        }
        else
        {
            GenerateKey();
            OpenActivationPanel();
        }
    }
    public void OpenLaunchPanel()
    {
        _activitionPanel.SetActive(false);
        _lauchPanel.SetActive(true);
    }

    public void OpenActivationPanel()
    {
        _activitionPanel.SetActive(true);
        _lauchPanel.SetActive(false);
    }

    public void DecompileRequest()
    {
        try
        {

            char[] requestChar = _keyField.text.ToCharArray();


            string twoSymbols;
            int simulatorCode;
            string criptedGeneratedCofficient;
            string decriptedGeneratedCofficient;
            char[] decriptedGeneratedCofficientCharArray;
            string cofficient;
            string controlSumm;
            string date;
            string termsOfUssage;
            int year;
            int mounth;
            int day;
            bool isDemoVersion;

            if (requestChar.Length == 20)
            {
                if (requestChar.Length > 20)
                {
                    Debug.Log(1);

                    _keyField.text = "Не верный ключ";
                    return;
                }
                twoSymbols = requestChar[0].ToString() + requestChar[1].ToString() + requestChar[2].ToString() + requestChar[3].ToString();
                simulatorCode = int.Parse(requestChar[0].ToString() + requestChar[1].ToString());
                criptedGeneratedCofficient = requestChar[2].ToString() + requestChar[3].ToString() + requestChar[4].ToString() + requestChar[5].ToString() + requestChar[6].ToString();
                decriptedGeneratedCofficient = Decrypt(criptedGeneratedCofficient);
                decriptedGeneratedCofficientCharArray = decriptedGeneratedCofficient.ToCharArray();
                cofficient = decriptedGeneratedCofficientCharArray[2].ToString() + decriptedGeneratedCofficientCharArray[3].ToString();

                controlSumm = requestChar[7].ToString() + requestChar[8].ToString() + requestChar[9].ToString() + requestChar[10].ToString();
                date = requestChar[11].ToString() + requestChar[12].ToString() + requestChar[13].ToString()
                    + requestChar[14].ToString() + requestChar[15].ToString() + requestChar[16].ToString();
                termsOfUssage = (requestChar[17].ToString() + requestChar[18].ToString() + requestChar[19].ToString()).ToString().Replace("F", string.Empty);

                year = int.Parse(requestChar[11].ToString() + requestChar[12].ToString());
                mounth = int.Parse(requestChar[13].ToString() + requestChar[14].ToString());
                day = int.Parse(requestChar[15].ToString() + requestChar[16].ToString());
            }
            else
            {
                if (requestChar.Length > 19)
                {
                    Debug.Log(1);

                    _keyField.text = "Не верный ключ";
                    return;
                }
                twoSymbols = requestChar[0].ToString() + requestChar[1].ToString() + requestChar[2].ToString();
                simulatorCode = int.Parse(requestChar[0].ToString());
                criptedGeneratedCofficient = requestChar[1].ToString() + requestChar[2].ToString() + requestChar[3].ToString() + requestChar[4].ToString() + requestChar[5].ToString();
                decriptedGeneratedCofficient = Decrypt(criptedGeneratedCofficient);
                decriptedGeneratedCofficientCharArray = decriptedGeneratedCofficient.ToCharArray();
                cofficient = decriptedGeneratedCofficientCharArray[2].ToString() + decriptedGeneratedCofficientCharArray[3].ToString();

                controlSumm = requestChar[6].ToString() + requestChar[7].ToString() + requestChar[8].ToString() + requestChar[9].ToString();
                date = requestChar[10].ToString() + requestChar[11].ToString() + requestChar[12].ToString()
                    + requestChar[13].ToString() + requestChar[14].ToString() + requestChar[15].ToString();
                termsOfUssage = (requestChar[16].ToString() + requestChar[17].ToString() + requestChar[18].ToString()).ToString().Replace("F", string.Empty);

                year = int.Parse(requestChar[10].ToString() + requestChar[11].ToString());
                mounth = int.Parse(requestChar[12].ToString() + requestChar[13].ToString());
                day = int.Parse(requestChar[14].ToString() + requestChar[15].ToString());
            }

            Debug.Log("simulatorCode " + simulatorCode + " cofficient " + cofficient + " controlSum " + controlSumm + " date " + date + " termsOfUssage " + termsOfUssage);
            //DateTime dateGeneration = DateTime.Parse(date);
            Debug.Log(requestChar[10].ToString() + requestChar[11].ToString());
            Debug.Log(requestChar[12].ToString() + requestChar[13].ToString());
            Debug.Log(requestChar[14].ToString() + requestChar[15].ToString());
            Debug.Log("controlSumm " + controlSumm + " _controllSumm " + _controllSumm);

            if (controlSumm != _controllSumm)
            {
                _keyField.text = "Ключ не соответствует";
                return;
            }

            if (cofficient != _cofficient)
            {
                Debug.Log(1);
                _keyField.text = "Не верный ключ";
                return;
            }


            if (simulatorCode != _simulatorCode)
            {
                _keyField.text = "Ключ активации не подходит к этой установки тренажера";
                return;
            }
            DateTime dateTime = new DateTime(2000 + year, mounth, day);


            if ((dateTime - DateTime.Now).Days >= 7)
            {
                _keyField.text = "Ключ активации просрочен";
                return;
            }
            Debug.Log("guardDTO.TermsOfUssage " + termsOfUssage);
            int ussage = int.Parse(termsOfUssage);
            if (ussage == 5 || ussage == 15)
            {
                _gameMode.SetupDemo(true);
            }
            else
            {
                _gameMode.SetupDemo(false);
            }

            Save(cofficient, day, mounth, year, int.Parse(termsOfUssage), DateTime.Now, _keyField.text, int.Parse(termsOfUssage), false);
            LaunchProgram();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            _keyField.text = "Не верный ключ";
        }
    }
    public void Save(string cofficent, int activitionDay, int activationMonth, int activationYear, int termsOfUssage, DateTime lastLaunch, string key, int daysLeft, bool isWrong)
    {
        GuardDataDTO guardDTO = new GuardDataDTO(cofficent, activitionDay, activationMonth, activationYear, termsOfUssage, lastLaunch, key, daysLeft, isWrong);
        string potion = JsonUtility.ToJson(guardDTO, true);
        File.WriteAllText(_path, potion);
    }

    public string Decrypt(string input)
    {
        string result = "";

        for (int i = 0; i < input.Length; i += _key.Length)
        {
            char[] transposition = new char[_key.Length];

            for (int j = 0; j < _key.Length; j++)
                transposition[j] = input[i + _key[j] - 1];

            for (int j = 0; j < _key.Length; j++)
                result += transposition[j];
        }

        return result;
    }

    private string GetControllSum(string value)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(value);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();

        }
    }
}
