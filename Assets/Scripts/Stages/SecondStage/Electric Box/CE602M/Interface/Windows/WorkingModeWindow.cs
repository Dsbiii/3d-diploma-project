using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M.Interface.Windows
{
    public class WorkingModeWindow : OptionsWindow
    {
        private MeasurementsWindow _measurementsWindow;

        public void Init(MeasurementsWindow measurementsWindow)
        {
            _measurementsWindow = measurementsWindow;
        }

        public override void SelectHandler()
        {
            if (CurrentCounterOption.OptionType == OptionType.Measurements)
            {
                _measurementsWindow.Open();
            }
        }
    }
}