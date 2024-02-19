using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Assets.Scripts.Stages.FifthStage.Panels
{
    public class HotReloadPanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _passwordDevice;
        [SerializeField] private TMP_Dropdown _port;
        [SerializeField] private GameObject _panel;
        [SerializeField] private GameObject _reloadPanel;
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private DeviceDataPanel _deviceDataPanel;
        private float _totalTime = 300.0f; // Время в секундах (5 минут)
        private Coroutine _coroutine;
        [Inject] private FifthStageExam _fifthStageExam;

        private IEnumerator StartTimer()
        {
            while (_totalTime > 0)
            {
                yield return new WaitForSeconds(1.0f);
                _totalTime -= 1.0f;
                UpdateTimerText();
            }
            _totalTime = 0;
            // Таймер достиг нуля, выполните необходимые действия
            Debug.Log("Таймер завершен!");
        }

        public void Stop()
        {
            StopCoroutine(_coroutine);
        }

        public void End()
        {
            _fifthStageExam.HotReloaded = true;

            if (_totalTime <= 0)
            {
                _reloadPanel.SetActive(false);
                if((_passwordDevice.text == "0000" || _passwordDevice.text == "000000"))
                {
                    _deviceDataPanel.SetDevicesStatus();
                }
            }
        }

        private void UpdateTimerText()
        {
            // Форматирование времени в минуты и секунды
            int minutes = Mathf.FloorToInt(_totalTime / 60F);
            int seconds = Mathf.FloorToInt(_totalTime - minutes * 60);
            string timerString = string.Format("{0:0}:{1:00}", minutes, seconds);

            // Обновление текста таймера
            _timerText.text = timerString;
        }

        public void Open()
        {
            _panel.SetActive(true);
        }

        public void Continue()
        {
            _panel.SetActive(false);
            _reloadPanel.SetActive(true);
            UpdateTimerText();
            _coroutine = StartCoroutine(StartTimer());
        }
    }
}