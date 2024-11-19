using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CustomCheckbox
{
    public class CustomCheckbox
    {
        private readonly CheckBox _mainCheckBox;
        private readonly List<CheckBox> _dependentCheckBoxes;

        public CustomCheckbox(CheckBox mainCheckBox, List<CheckBox> dependentCheckBoxes)
        {
            _mainCheckBox = mainCheckBox;
            _dependentCheckBoxes = dependentCheckBoxes;

            // Підписка на події
            _mainCheckBox.CheckedChanged += MainCheckBox_CheckedChanged;
            foreach (var checkBox in _dependentCheckBoxes)
            {
                checkBox.CheckedChanged += DependentCheckBox_CheckedChanged;
            }

            UpdateMainCheckBoxState(); // Ініціалізація стану головного CheckBox
        }

        private void MainCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_mainCheckBox.CheckState == CheckState.Indeterminate)
                return;

            var isChecked = _mainCheckBox.Checked;

            foreach (var checkBox in _dependentCheckBoxes)
            {
                checkBox.CheckedChanged -= DependentCheckBox_CheckedChanged;
                checkBox.Checked = isChecked;
                checkBox.CheckedChanged += DependentCheckBox_CheckedChanged;
            }
        }

        private void DependentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateMainCheckBoxState();
        }

        private void UpdateMainCheckBoxState()
        {
            var allChecked = _dependentCheckBoxes.All(cb => cb.Checked);
            var noneChecked = _dependentCheckBoxes.All(cb => !cb.Checked);

            _mainCheckBox.CheckedChanged -= MainCheckBox_CheckedChanged;

            _mainCheckBox.CheckState = allChecked
                ? CheckState.Checked
                : noneChecked
                    ? CheckState.Unchecked
                    : CheckState.Indeterminate;

            _mainCheckBox.CheckedChanged += MainCheckBox_CheckedChanged;
        }
    }
}
