using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M.Interface.Windows
{
    public class MeasurementsWindow : OptionsWindow
    {
        private NetworkSettingsWindow _networkSettingsWindow;
        private ErrorWindow _errorWindow;
        private VectorDiogram _vectorDiogram;

        public void Init(NetworkSettingsWindow networkSettingsWindow , ErrorWindow errorWindow,VectorDiogram vectorDiogram)
        {
            _vectorDiogram = vectorDiogram;
            _networkSettingsWindow = networkSettingsWindow;
            _errorWindow = errorWindow;
        }

        public override void SelectHandler()
        {
            if (CurrentCounterOption.OptionType == OptionType.Error)
            {
                _errorWindow.Open();
            }
            else if (CurrentCounterOption.OptionType == OptionType.Network_Settings)
            {
                _networkSettingsWindow.Open();
            }
            else if (CurrentCounterOption.OptionType == OptionType.Vector_Diogram)
            {
                _vectorDiogram.Open();
            }
        }
    }
}