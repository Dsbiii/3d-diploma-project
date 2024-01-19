using Assets.Scripts.Stages.FifthStage.Panels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Stages.FifthStage
{
    public class FifthStageExam : MonoBehaviour
    {
        [SerializeField] private PortListPanel _portListPanel;
        [SerializeField] private SMPanel _sMPanel;
        [SerializeField] private SATPanel _sATPanel;

        public bool ConnectedUspdToPC { get; set; }
        public bool ConnectedCounterToPC { get; set; }
        public bool EnteredIP { get; set; }
        public bool ConfiguredPort
        {
            get
            {
                foreach(var port in _portListPanel.Ports)
                {
                    if(port.NamePortText == "Последовательный порт" &&
                        port.PortType == "Последовательный порт 1" &&
                        _sMPanel.IsReaded && _sMPanel.IsWrited)
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


        public void AddFourthStageExam(string name, string action, string idealAction, int right, int wrong)
        {
            _fourthStageExams.Add(new Exam(wrong, right, idealAction, action, name));
        }

        public void RegisterFifthStageExam()
        {
            ExamSystem.Instance.AddExam(new Exam("Этап 2. Подключение и настройка"));

            CheckUSPDConnection();
            CheckCounterConnection();
            CheckIPEnter();
            CheckPortSettings();
            CheckAddedDeviceInConfigure();
            CheckConfigureSettings();
            CheckHotReload();
            AddFourthStageExam("Настройка даты и времени", "Неправильно", "Получить данные о настройках даты и времени в конфигураторе SM", 0, 0);
            
            foreach (var exam in _fourthStageExams)
                ExamSystem.Instance.AddExam(exam);

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

            _fourthStageExams.Clear();
            ExamSystem.Instance.AddExam(new Exam("Этап 4. Создание личного кабинета физического лица"));

            AddFourthStageExam("Проверка отображения показаний", "Неправильно", "Войти в личный кабинет абонента и проверить отображение показаний", 0, 0);
            //AddFourthStageExam("Задание данных для входа физлица в личный кабинет «Пирамиды»", "Неправильно", "Выбрать абонента, указать тип профиля и аутентификации, задать пароль для пользователя", 0, 0);
            //AddFourthStageExam("Проверка отображения показаний", "Неправильно", "Войти в личный кабинет абонента и проверить отображение показаний", 0, 0);

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
            if (ConfiguredPort)
            {
                AddFourthStageExam("Настройка порта", "Правильно", "Ввести имя порта в настройках конфигуратора, задать тип порта", 2, 0);
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
                AddFourthStageExam("Добавление устройства в конфигуратор", "Правильно", "Указать пароль и порт, указать данные для измерения", 4, 0);
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

    }
}