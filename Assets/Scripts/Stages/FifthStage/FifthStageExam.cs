using Assets.Scripts.Stages.FifthStage.Panels;
using Assets.Scripts.Stages.SixthStage.Report;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Stages.FifthStage
{
    public class FifthStageExam : MonoBehaviour
    {
        [SerializeField] private PortListPanel _portListPanel;
        [SerializeField] private DeviceDataPanel _deviceDataPanel;
        [SerializeField] private SMPanel _sMPanel;
        [SerializeField] private SATPanel _sATPanel;
        [SerializeField] private SumPoints _sumPoints;
        [SerializeField] private DateClockPanel _dateClockPanelFirst;
        [SerializeField] private DateClockPanel _dateClockPanelSecond;
        [SerializeField] private PortSettingsPanel _portSettingsPanel;

        public bool ConnectedUspdToPC { get; set; }
        public bool ConnectedCounterToPC { get; set; }
        public bool EnteredIP { get; set; }
        public bool IsScrolledDataPanel { get; set; }
        public bool ConfiguredPort
        {
            get
            {
                foreach(var port in _portListPanel.Ports)
                {
                    if(port.NamePortText.Contains("Последовательный порт 1"))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public bool ConfiguredDevice { get; set; }
        public bool ConfiguredSAT => _sATPanel.IsRight();
        public bool HotReloaded { get; set; }

        private List<Exam> _fourthStageExams = new List<Exam>();

        private bool _isReported;

        public void AddFourthStageExam(string name, string action, string idealAction, int right, int wrong)
        {
            _fourthStageExams.Add(new Exam(wrong, right, idealAction, action, name));
        }

        public void RegisterFifthStageExam()
        {
            if (_isReported)
                return;
            ExamSystem.Instance.AddExam(new Exam("Этап 2. Подключение и настройка"));

            CheckUSPDConnection();
            CheckCounterConnection();
            CheckIPEnter();
            CheckPortSettings();
            CheckAddedDeviceInConfigure();
            CheckConfigureSettings();
            CheckHotReload();
            CheckDateTime();

            foreach (var exam in _fourthStageExams)
                ExamSystem.Instance.AddExam(exam);

            _fourthStageExams.Clear();
            ExamSystem.Instance.AddExam(new Exam("Этап 3. Настройка «Пирамиды»"));
            
            CreateUSPDPoint();
            SettingPointAbonent();
            SettingDataCollection();
            CreateAbonent();
            DataForSingIn();
            GetInfoCounter();

            
            foreach (var exam in _fourthStageExams)
                ExamSystem.Instance.AddExam(exam);

            _fourthStageExams.Clear();
            ExamSystem.Instance.AddExam(new Exam("Этап 4. Создание личного кабинета физического лица"));

            AddFourthStageExam("Проверка отображения показаний", "Неправильно", "Войти в личный кабинет абонента и проверить отображение показаний", 0, 0);
            //AddFourthStageExam("Задание данных для входа физлица в личный кабинет «Пирамиды»", "Неправильно", "Выбрать абонента, указать тип профиля и аутентификации, задать пароль для пользователя", 0, 0);
            //AddFourthStageExam("Проверка отображения показаний", "Неправильно", "Войти в личный кабинет абонента и проверить отображение показаний", 0, 0);

            foreach (var exam in _fourthStageExams)
                ExamSystem.Instance.AddExam(exam);
            _isReported = true;
        }
        private void SixStageExam()
        {
            _fourthStageExams.Clear();
            ExamSystem.Instance.AddExam(new Exam("Этап 3. Настройка «Пирамиды»"));
            
            AddFourthStageExam("Заведение точки УСПД в «Пирамиде»", "Неправильно", "Ввести серийный номер УСПД, данные в полях «Пользователь» и «Пароль», выбрать часовой пояс, указать место установки. Настроить маршрут, ввести адрес и порт", 0, 0);
            AddFourthStageExam("Настройка точки учета абонента", "Неправильно", "Выбрать счетчик в программе, указать его серийный номер, дату выпуска, установки, последней и следующей поверки, часовой пояс и связной номер. Настроить маршрут. Привязать счетчик к точке учета", 0, 0);
            AddFourthStageExam("Настройка сценария сбора данных", "Неправильно", "Создать сценарий сбора данных: указать наименование, глубину сбора, параметры, время принудительного завершения работы, включить проверку наличия данных в БД перед сбором, чтений событий, запуск сценария сразу после старта сервиса.\r\nНастроить расписание для сценария сбора данных: тип, периодичность выполнения, начало работы", 0, 0);
            AddFourthStageExam("Создание абонента в базе", "Неправильно", "Ввести данные абонента, выбрать тип абонента, указать счетчик", 0, 0);
            AddFourthStageExam("Задание данных для входа физлица в личный кабинет «Пирамиды»", "Неправильно", "Выбрать абонента, указать тип профиля и аутентификации, задать пароль для пользователя", 0, 0);
            AddFourthStageExam("Получение показаний со счетчика", "Неправильно", "Выбрать счетчик, задать интервал, выбрать параметр измерения", 0, 0);

            foreach (var exam in _fourthStageExams)
                ExamSystem.Instance.AddExam(exam);
        }
        private void CheckUSPDConnection()
        {
            if (ConnectedUspdToPC)
            {
                AddFourthStageExam("Подключение УСПД к ПК", "Правильно", "Использовать кабель для подключения УСПД", 1, 0);
            }
            else
            {
                AddFourthStageExam("Подключение УСПД к ПК", "Неправильно", "Использовать кабель для подключения УСПД", 0, 0);
            }
        }

        private void CheckCounterConnection()
        {
            if (ConnectedCounterToPC)
            {
                AddFourthStageExam("Подключение счетчика к ПК", "Правильно", "Использовать кабель для подключения счетчика", 1, 0);
            }
            else
            {
                AddFourthStageExam("Подключение счетчика к ПК", "Неправильно", "Использовать кабель для подключения счетчика", 0, 0);
            }
        }


        private void CheckIPEnter()
        {
            if (EnteredIP)
            {
                AddFourthStageExam("Ввод IP-адреса компьютера", "Правильно", "Ввести адрес компьютера", 1, 0);
            }
            else
            {
                AddFourthStageExam("Ввод IP-адреса компьютера", "Неправильно", "Ввести адрес компьютера", 0, 0);
            }
        }

        private void CheckPortSettings()
        {
            //if (ConfiguredPort && _portSettingsPanel.WritedCountErrors)
            if (ConfiguredPort && _portSettingsPanel.WritedCountErrors)
            {
                    AddFourthStageExam("Настройка порта", "Правильно", "Ввести имя порта в настройках конфигуратора, задать тип порта", 3, 0);
            }
            else
            {
                AddFourthStageExam("Настройка порта", "Неправильно", "Ввести имя порта в настройках конфигуратора, задать тип порта", 0, 0);
            }
        }

        private void CheckAddedDeviceInConfigure()
        {
            if (ConfiguredDevice)
            {
                if(IsScrolledDataPanel)
                {
                    AddFourthStageExam("Добавление устройства в конфигуратор", "Правильно", "Указать пароль и порт, указать данные для измерения", 5, 0);
                }
                else
                {
                    AddFourthStageExam("Добавление устройства в конфигуратор", "Правильно", "Указать пароль и порт, указать данные для измерения", 4, 0);
                }
            }
            else
            {
                AddFourthStageExam("Добавление устройства в конфигуратор", "Неправильно", "Указать пароль и порт, указать данные для измерения", 0, 0);
            }
        }

        private void CheckConfigureSettings()
        {
            if (ConfiguredSAT)
            {
                AddFourthStageExam("Настройка соединения в конфигураторе счетчика", "Правильно", "Задать параметры соединения (нечет), выбрать порт, указать пароль", 4, 0);
            }
            else
            {
                AddFourthStageExam("Настройка соединения в конфигураторе счетчика", "Неправильно", "Задать параметры соединения (нечет), выбрать порт, указать пароль", 0, 0);
            }
        }

        private void CheckHotReload()
        {
            if (HotReloaded)
            {
                AddFourthStageExam("Горячий перезапуск контроллера", "Правильно", "Выполнить горячий перезапуск контроллера", 1, 0);
            }
            else
            {
                AddFourthStageExam("Горячий перезапуск контроллера", "Неправильно", "Выполнить горячий перезапуск контроллера", 0, 0);
            }
        }

        private void CheckDateTime()
        {
            if (_dateClockPanelFirst.IsRightDatedTime && _dateClockPanelSecond.IsRightDatedTime)
            {
                AddFourthStageExam("Настройка даты и времени", "Правильно", "Получить данные о настройках даты и времени в конфигураторе SM", 1, 0);
            }
            else
            {
                AddFourthStageExam("Настройка даты и времени", "Неправильно", "Получить данные о настройках даты и времени в конфигураторе SM", 0, 0);
            }
        }

        private void CreateUSPDPoint()
        {
            if(_sumPoints.SumPoint1() > 0)
            {
                AddFourthStageExam("Заведение точки УСПД в «Пирамиде»", "Правильно", "Ввести серийный номер УСПД, данные в полях «Пользователь» и «Пароль», выбрать часовой пояс, указать место установки. Настроить маршрут, ввести адрес и порт", _sumPoints.SumPoint1(), 0);
            }
            else
            {
                AddFourthStageExam("Заведение точки УСПД в «Пирамиде»", "Неправильно", "Ввести серийный номер УСПД, данные в полях «Пользователь» и «Пароль», выбрать часовой пояс, указать место установки. Настроить маршрут, ввести адрес и порт", 0, 0);

            }
            CreateUSPDPointAddition();
        }
        private void CreateUSPDPointAddition()
        {
            string[] Action = new string[8] { "Добавить каналообразующее оборудование", "Указать серийный номер оборудования", "Заполнить поля «Пользователь» и «Пароль»", "Выставить часовой пояс", "Указать место установки", "Указать тип соединения", "Указать IP-адрес", "Указать порт"};
            for(int i = 0; i < Action.Length; i++)
            {
                AddFourthStageExam(Action[i], _sumPoints.ReportPoint1[i], "", 0, 0);
            }
        }
        private void SettingPointAbonent()
        {
            if(_sumPoints.SumPoint2() > 0)
            {
                //AddFourthStageExam("Настройка точки учета абонента", "Правильно", "Выбрать счетчик в программе, указать его серийный номер, дату выпуска, установки, последней и следующей поверки, часовой пояс и связной номер. Настроить маршрут. Привязать счетчик к точке учета", 0, 0);

                AddFourthStageExam("Настройка точки учета абонента", "Правильно", "Выбрать счетчик в программе, указать его серийный номер, дату выпуска, установки, последней и следующей поверки, часовой пояс и связной номер. Настроить маршрут. Привязать счетчик к точке учета", _sumPoints.SumPoint2(), 0);
            }
            else
            {
                AddFourthStageExam("Настройка точки учета абонента", "Неправильно", "Выбрать счетчик в программе, указать его серийный номер, дату выпуска, установки, последней и следующей поверки, часовой пояс и связной номер. Настроить маршрут. Привязать счетчик к точке учета", 0, 0);
            }
            SettingPointAbonentAddition();
        }
        private void SettingPointAbonentAddition()
        {
            string[] Action = new string[12] { "Добавить счетчик",
                "Указать модель счетчика",
                "Указать серийный номер", "Выставить дату выпуска",
                "Выставить дату установки",
                "Выставить дату последней поверки",
                "Выставить дату следующей поверки", "Указать связной номер",
                "Настроить каналообразующее оборудование",
                "Выбрать каналообразующее оборудование", 
                "Указать счетчик в истории замен", "Выбрать дату установки в истории замен"};
            for (int i = 0; i < Action.Length; i++)
            {
                AddFourthStageExam(Action[i], _sumPoints.ReportPoint2[i], "", 0, 0);
            }
        }
        private void SettingDataCollection()
        {
            if (_sumPoints.SumPoint3() > 0)
            {
                //AddFourthStageExam("Настройка сценария сбора данных", "Правильно", "Создать сценарий сбора данных: указать наименование, глубину сбора, параметры, время принудительного завершения работы, включить проверку наличия данных в БД перед сбором, чтений событий, запуск сценария сразу после старта сервиса.\r\nНастроить расписание для сценария сбора данных: тип, периодичность выполнения, начало работы", 0, 0);

                AddFourthStageExam("Настройка сценария сбора данных", "Правильно", "Создать сценарий сбора данных: указать наименование, глубину сбора, параметры, время принудительного завершения работы, включить проверку наличия данных в БД перед сбором, чтений событий, запуск сценария сразу после старта сервиса.\r\nНастроить расписание для сценария сбора данных: тип, периодичность выполнения, начало работы", _sumPoints.SumPoint3(), 0);
            }
            else
            {
                AddFourthStageExam("Настройка сценария сбора данных", "Неправильно", "Создать сценарий сбора данных: указать наименование, глубину сбора, параметры, время принудительного завершения работы, включить проверку наличия данных в БД перед сбором, чтений событий, запуск сценария сразу после старта сервиса.\r\nНастроить расписание для сценария сбора данных: тип, периодичность выполнения, начало работы", 0, 0);
            }
        }
        private void SettingDataCollectionAddition() 
        {
            string[] Action = new string[9] {
                "Указать название сценария сбора данных",
                "Указать глубину сбора",
                "Указать параметры",
                "Поставить галочки в настройках сценария сбора данных",
                "Указать время принудительного завершения работы",
                "Создать расписание",
                "Указать тип расписания",
                "Указать периодичность выполнения расписания",
                "Указать дату начала работы расписания",};
            for (int i = 0; i < Action.Length; i++)
            {
                AddFourthStageExam(Action[i], _sumPoints.ReportPoint3[i], "", 0, 0);
            }
        }
        private void CreateAbonent()
        {
            if (_sumPoints.SumPoint4() > 0)
            {
                AddFourthStageExam("Создание абонента в базе", "Правильно", "Ввести данные абонента, выбрать тип абонента, указать счетчик", _sumPoints.SumPoint4(), 0);
            }
            else
            {
                AddFourthStageExam("Создание абонента в базе", "Неправильно", "Ввести данные абонента, выбрать тип абонента, указать счетчик", 0, 0);
            }
            CreateAbonentAddition();
        }
        private void CreateAbonentAddition()
        {
            string[] Action = new string[6] {
                "Создать абонента",
                "Указать номер лицевого счета",
                "Указать Ф.И.О. абонента",
                "Указать тип абонента",
                "Во вкладке «Паспорт» выбрать созданного ранее абонента",
                "Указать тип абонента во вкладке «Паспорт»"};
            for (int i = 0; i < Action.Length; i++)
            {
                AddFourthStageExam(Action[i], _sumPoints.ReportPoint4[i], "", 0, 0);
            }
        }
        private void DataForSingIn()
        {
            if(_sumPoints.SumPoint5() > 0)
            {
                //AddFourthStageExam("Задание данных для входа физлица в личный кабинет «Пирамиды»", "Правильно", "Выбрать абонента, указать тип профиля и аутентификации, задать пароль для пользователя", 0, 0);

                AddFourthStageExam("Задание данных для входа физлица в личный кабинет «Пирамиды»", "Правильно", "Выбрать абонента, указать тип профиля и аутентификации, задать пароль для пользователя", _sumPoints.SumPoint5(), 0);
            }
            else
            {
                AddFourthStageExam("Задание данных для входа физлица в личный кабинет «Пирамиды»", "Неправильно", "Выбрать абонента, указать тип профиля и аутентификации, задать пароль для пользователя", 0, 0);
            }
        }
        private void DataForSingInAddition()
        {
            string[] Action = new string[3] {
                "Создать абонента и указать его логин",
                "Выбрать из списка созданного ранее абонента, указать профиль абонента и тип аутентификации",
                "Установить пароль"};
            for (int i = 0; i < Action.Length; i++)
            {
                AddFourthStageExam(Action[i], _sumPoints.ReportPoint5[i], "", 0, 0);
            }
        }
        private void GetInfoCounter()
        {
            if(_sumPoints.SumPoint6() > 0)
            {
                //AddFourthStageExam("Получение показаний со счетчика", "Правильно", "Выбрать счетчик, задать интервал, выбрать параметр измерения", 0, 0);

                AddFourthStageExam("Получение показаний со счетчика", "Правильно", "Выбрать счетчик, задать интервал, выбрать параметр измерения", _sumPoints.SumPoint6(), 0);
            }
            else
            {
                AddFourthStageExam("Получение показаний со счетчика", "Неправильно", "Выбрать счетчик, задать интервал, выбрать параметр измерения", 0, 0); ;
            }
        }
        private void GetInfoCounterAddition()
        {
            string[] Action = new string[3] {
                "Выбрать счетчик из списка",
                "Указать интервал показаний",
                "Указать собираемые показания"};
            for (int i = 0; i < Action.Length; i++)
            {
                AddFourthStageExam(Action[i], _sumPoints.ReportPoint6[i], "", 0, 0);
            }
        }
    }
}