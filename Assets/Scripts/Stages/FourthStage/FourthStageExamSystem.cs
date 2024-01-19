using Assets.Scripts.Stages.FourthStage.CablesSystem;
using Assets.Scripts.Stages.FourthStage.SelectingCablesPanel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Stages.FourthStage
{
    public class FourthStageExamSystem : MonoBehaviour
    {
        [SerializeField] private AktReport _aktReport;
        [SerializeField] private FourthStagePlomb[] _plombs;
        [SerializeField] private AntenaPoint _antenaPoint;
        [SerializeField] private SIMPoint[] _simPoints;
        [SerializeField] private MovableObject _uspd;
        [SerializeField] private MovableObject _powerBlock;
        [SerializeField] private PlakatService _pakatService;

        [Inject] private FourthStageCableConnector _fourthStageCableConnector;
        [Inject] private FourthStageCablesSelectingPanel _fourthStageCablesSelectingPanel;


        private List<Exam> _fourthStageExams = new List<Exam>();
        private bool _isRightPickedCottonGlovesInspectionStage;
        private bool _isRightPickedHelmetInspectionStage;
        private bool _isRightPickedGlassesInspectionStage;

        private bool _isRightExitFromTP = true;

        public void SetRightExitFromTP(bool mode)
        {
            Debug.Log("Mode " + mode);
            _isRightExitFromTP = mode;
        }

        public void AddFourthStageExam(string name, string action, string idealAction, int right, int wrong)
        {
            _fourthStageExams.Add(new Exam(wrong, right, idealAction, action, name));
        }
        public void AddFourthStageExam(string name, string action, string idealAction, string result)
        {
            _fourthStageExams.Add(new Exam(result, idealAction, action, name));
        }
        public void TakedRightPickedCottonGlovesInspectionStage()
        {
            _isRightPickedCottonGlovesInspectionStage = true;
        }
        public void TakedRightPickedGlassesInspectionStage()
        {
            _isRightPickedGlassesInspectionStage = true;
        }
        public void TakedRightPickedHelmetInspectionStage()
        {
            _isRightPickedHelmetInspectionStage = true;
        }

        public void RegisterExamSystem()
        {
            ExamSystem.Instance.AddExam(new Exam("Этап 1. Монтаж"));
            CheckSIZ();
            InstallUSPDPowerBlock();
            CheckSelectCables();
            CheckRightConnection();
            CheckSIMPoint();
            CheckAntenaPoint();
            CheckPlombs();
            ExitFromTP();
            ReportAKT();
            ReportPlakats();
            foreach (var exam in _fourthStageExams)
                ExamSystem.Instance.AddExam(exam);
        }

        private void ReportPlakats()
        {
            if (_pakatService.CheckForRightFieldPlakats())
            {
                AddFourthStageExam("Вывешивание плакатов", "Правильно", "«Работать здесь» – над счетчиком и под автоматом, «Стой! Напряжение» – на рубильник", 3, 0);
            }
            else
            {
                AddFourthStageExam("Вывешивание плакатов", "Неправильно", "«Работать здесь» – над счетчиком и под автоматом, «Стой! Напряжение» – на рубильник", 0, 0);
            }
        }

        private void ReportAKT()
        {
            if (_aktReport.CheckForRightFillAkt())
            {
                AddFourthStageExam("Заполнение акта", "Правильно", "Заполнить акт, указать сведения о пломбах и поставить подписи", 6, 0);
            }
            else
            {
                AddFourthStageExam("Заполнение акта", "Неправильно", "Заполнить акт, указать сведения о пломбах и поставить подписи", 0, 0);
            }
        }

        private void ExitFromTP()
        {
            if (_isRightExitFromTP)
            {
                AddFourthStageExam("Вывод ТП из ремонта", "Правильно", "Вывести ТП из ремонта после всех требуемых действий", 1, 0);
            }
            else
            {
                AddFourthStageExam("Вывод ТП из ремонта", "Неправильно", "Вывести ТП из ремонта после всех требуемых действий", 0, 0);
            }
        }

        private void CheckPlombs()
        {
            int count = 0;
            foreach (var item in _plombs)
            {
                if (item.IsSetupedPlomb)
                    count++;
            }
            if (count == 3)
            {
                AddFourthStageExam("Опломбировка", "Правильно", "Установить пломбы на клеммную крышку счетчика, ИКК, трансформаторы тока", count, 0);
            }
            else
            {
                AddFourthStageExam("Опломбировка", "Неправильно", "Установить пломбы на клеммную крышку счетчика, ИКК, трансформаторы тока", count, 0);
            }
        }


        private void CheckAntenaPoint()
        {
            if (_antenaPoint.IsSetupedAntena)
            {
                AddFourthStageExam("Подключение антенны к УСПД", "Правильно", "Подключить антенну к УСПД", 1, 0);
            }
            else
            {
                AddFourthStageExam("Подключение антенны к УСПД", "Неправильно", "Подключить антенну к УСПД", 0, 0);
            }
        }

        private void CheckSIMPoint()
        {
            bool isConnected = false;

            foreach(var item in _simPoints)
            {
                if(item.IsIndicated)
                    isConnected = true;
            }
            if (isConnected)
            {
                AddFourthStageExam("Установка SIM-карты", "Правильно", "Вставить SIM-карту в слот SIM1 или SIM2", 1, 0);
            }
            else
            {
                AddFourthStageExam("Установка SIM-карты", "Неправильно", "Вставить SIM-карту в слот SIM1 или SIM2", 0, 0);
            }
        }

        private void CheckRightConnection()
        {
            if (_fourthStageCableConnector.CheckToRightConnection())
            {
                AddFourthStageExam("Выполнение подключений", "Правильно", "Блок питания – автомат\r\nБлок питания – УСПД\r\nСчетчик – УСПД", 6, 0);
            }
            else
            {
                AddFourthStageExam("Выполнение подключений", "Неправильно", "Блок питания – автомат\r\nБлок питания – УСПД\r\nСчетчик – УСПД", 0, 0);
            }
        }

        private void CheckSelectCables()
        {
            if (_fourthStageCablesSelectingPanel.CheckRightConnection)
            {
                AddFourthStageExam($"Выбор провода для подключения", "Правильно", "Блок питания – автомат: ПуВ 1 × 0,75 мм\u00B2\r\nБлок питания – УСПД: ПуВ 1 × 0,75 мм\u00B2)\r\nСчетчик – УСПД: UTP 0,5 мм\u00B2 Cu", 6, 0);
            }
            else
            {
                AddFourthStageExam($"Выбор провода для подключения", "Неправильно", "Блок питания – автомат: ПуВ 1 × 0,75 мм\u00B2\r\nБлок питания – УСПД: ПуВ 1 × 0,75 мм\u00B2)\r\nСчетчик – УСПД: UTP 0,5 мм\u00B2 Cu", 0, 0);
            }
        }

        private void InstallUSPDPowerBlock()
        {
            int rightCount = 0;
            if (_uspd.IsPlanted)
            {
                rightCount++;
            }
            if (_powerBlock.IsPlanted)
            {
                rightCount++;
            }
            if(rightCount == 2)
            {
                AddFourthStageExam("Установка УСПД и блока питания на DIN-рейке", "Правильно", "Установить УСПД на DIN-рейке. Установить блок питания на DIN-рейке", 2, 0);
            }
            else
            {
                AddFourthStageExam("Установка УСПД и блока питания на DIN-рейке", "Неправильно", "Установить УСПД на DIN-рейке. Установить блок питания на DIN-рейке", rightCount, 0);
            }
        }

        private void CheckSIZ()
        {
            int rightCount = 0;

            if (_isRightPickedCottonGlovesInspectionStage)
            {
                rightCount++;
            }
            if (_isRightPickedGlassesInspectionStage)
            {
                rightCount++;
            }
            if (_isRightPickedHelmetInspectionStage)
            {
                rightCount++;
            }

            if (rightCount == 3)
            {
                AddFourthStageExam("Проверка СИЗ", "Правильно", "Проверить и надеть х/б перчатки, каску, защитные очки", 3, 0);
            }
            else
            {
                AddFourthStageExam("Проверка СИЗ", "Неправильно", "Проверить и надеть х/б перчатки, каску, защитные очки", rightCount, 0);
            }
        }
    }
}