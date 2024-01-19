using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.SceneManagement;


[Serializable]
public class GuardDTO
{
    public string OldLaunch;
    public string ActivationEndDate;
    public string ActivDate;
    public List<string> ActivOldKey = new List<string>();
    public int ActivDay;
    public int ControlDay;
    public bool Indefinitely;
    public bool T;
    public GuardDTO()
    {

    }

    public GuardDTO(string oldLaunch,
        string activationEndDate,
        string activDate,
        List<string> activOldKey,
        int activDay,
        int controlDay,
        bool indefinetely,
        bool t)
    {
        T = t;
        OldLaunch = oldLaunch;
        ActivationEndDate = activationEndDate;
        ActivDate = activDate;
        ActivOldKey = activOldKey;
        ActivDay = activDay;
        ControlDay = controlDay;
        Indefinitely = indefinetely;
    }
}
public class Guard : MonoBehaviour
{
    [SerializeField] string OldLaunch;
    [SerializeField] string ActivationEndDate;
    [SerializeField] string ActivDate;
    [SerializeField] List<string> ActivOldKey = new List<string>();

    [SerializeField] int ActivDay;
    [SerializeField] int ControlDay;
    [SerializeField] private bool Indefinitely;

    string[] TempSecretKey = new string[] { "df46de3", "dfg545", "sdf2w235", "3453fg", "dfeg424", "bcdfg4", "dfg3", "dfg66", "gd43636", "dfg336", "dfg5673" };
    public bool T = true; 
    //UI Панели активации
    public TMP_Text ID_Text;
    public TMP_InputField Activation_Pole;
    //UI Панели запуска
    public TMP_Text ActivDayText;

    
    public GameObject Loading_Panel;
    public GameObject ActivProgramm_Panel;
    public GameObject Launch_Panel;


    void Awake()
    {
        Debug.Log(Application.persistentDataPath);

        Screen.SetResolution(650, 650, false);
        OldLaunch = DateTime.Today.ToString();
        Load();

        string id = SystemInfo.deviceUniqueIdentifier;
        ID_Text.text = id;

        Launch_Panel.SetActive(false);

        CheckingDate();

    }


    public void Copy()
    {
        GUIUtility.systemCopyBuffer = Random.Range(100, 999) + ID_Text.text + Random.Range(0, 999); // копирование в буфер обмена
    }


    public void Activation()
    {
        Debug.Log(1);
        for (int j = 0; j < ActivOldKey.Count; j++)
        {
            Debug.Log(2);

            if (Activation_Pole.text == ActivOldKey[j])
            {
                Debug.Log(3);

                T = false;
            }
        }
        Debug.Log(4);

        if (T == true)
        {
            Debug.Log(5);

            Activ();
        }
    }


    public void Activ()
    {

        string id = SystemInfo.deviceUniqueIdentifier.Substring(0, 4);
        if (Activation_Pole.text.Contains(id))
        {
            print(id);
            string[] SecretKey = new string[]
                {"s89f", "h8df", "df0f", "47kf", "43hf", "ew2f", "4vbf", "dsff", "1jkf", "se5f", "y6tf"};

            string id7 = id.Substring(0, 4); //Первые 4 символа


            var isWork = false;
            for (int i = 0; i < SecretKey.Length && isWork == false; i++)
            {
                isWork = Activation_Pole.text.Contains(id7 + SecretKey[i]);
            }

            bool Day_30;
            bool Day_10;
            bool Day_360;

            if (Day_30 = Activation_Pole.text.Contains("tu3j"))
            {
                ActivDay = 30;
                ActivationEndDate = DateTime.Today.AddDays(ActivDay).ToString();
                ActivDate = DateTime.Today.ToString();
            }
            else if (Day_10 = Activation_Pole.text.Contains("i18r"))
            {
                ActivDay = 5;
                ActivationEndDate = DateTime.Today.AddDays(ActivDay).ToString();
                ActivDate = DateTime.Today.ToString();
            }
            else if (Day_360 = Activation_Pole.text.Contains("r3k6"))
            {
                Indefinitely = true;

                Launch_Program();
            }

            ControlDay = ActivDay;
            if (ActivDay >= 1)
            {
                ActivProgramm_Panel.SetActive(false);
                Launch_Panel.SetActive(true);
            }
            else
            {
                Loading_Panel.SetActive(false);
            }

            ActivOldKey.Add(Activation_Pole.text);
            Save();
        }
    }



    public void Save()
    {
        GuardDTO guardDTO = new GuardDTO(OldLaunch, ActivationEndDate, ActivDate, ActivOldKey, ActivDay, ControlDay, Indefinitely, T);
        string potion = JsonUtility.ToJson(guardDTO, true);
        File.WriteAllText(Application.persistentDataPath + "/LV2.json", potion);
        Debug.Log("Save");
    }

    public void Load()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath + "/LV2.json")))
        {
            var guardDTO = JsonUtility.FromJson<GuardDTO>(File.ReadAllText(Path.Combine(Application.persistentDataPath + "/LV2.json")));
            OldLaunch = guardDTO.OldLaunch;
            ActivationEndDate = guardDTO.ActivationEndDate;
            ActivDate = guardDTO.ActivDate;
            ActivOldKey = guardDTO.ActivOldKey;
            ActivDay = guardDTO.ActivDay;
            ControlDay = guardDTO.ControlDay;
            Indefinitely = guardDTO.Indefinitely;
            T = guardDTO.T;
            CheckingDate();
        }
        else
        {
            Debug.Log("save");
            CheckingDate();

        }
    }


    private void Update()
    {
        ActivDayText.text = ActivDay.ToString();
    }

    void CheckingDate()
    {


        if (Indefinitely == true)
        {
            Launch_Program();
        }


        if (ActivDay >= 1 && DateTime.Parse(ActivDate)! <= DateTime.Today && DateTime.Parse(OldLaunch)! <= DateTime.Today)
        {
            var Time = DateTime.Today.Subtract(DateTime.Parse(ActivationEndDate));
            ActivDay = Time.Days * -1;
            Loading_Panel.SetActive(false);
            Launch_Panel.SetActive(true);
            OldLaunch = DateTime.Today.ToString();
            Save();
        }
        else
        {
            Loading_Panel.SetActive(false);
            ActivProgramm_Panel.SetActive(true);

        }



    }

    public void Launch_Program()
    {
        Screen.SetResolution(1920, 1080, true);
        SceneManager.LoadScene(1);
    }
}
