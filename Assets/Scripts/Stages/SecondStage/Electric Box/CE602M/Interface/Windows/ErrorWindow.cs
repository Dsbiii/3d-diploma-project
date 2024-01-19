using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M.Interface.Windows
{
    public class ErrorWindow : OptionsWindow
    {
        private AutomaticModeWindow _automaticModeWindow;

        public void Init(AutomaticModeWindow automaticModeWindow)
        {
            _automaticModeWindow = automaticModeWindow;
        }

        public override void SelectHandler()
        {
            if (CurrentCounterOption.OptionType == OptionType.Automatic_Mode)
            {
                _automaticModeWindow.Open();
            }

        }

    }
}