using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CustomCheckbox
{
    public class MainForm : Form
    {
        public MainForm()
        {
            // Створення головного CheckBox
            var mainCheckBox = new CheckBox
            {
                Text = "",
                AutoCheck = true,
                Top = 10,
                Left = 10
            };

            // Створення залежних CheckBox
            var checkBoxes = new List<CheckBox>
            {
                new CheckBox { Text = "Чекбокс 1", Top = 40, Left = 10 },
                new CheckBox { Text = "Чекбокс 2", Top = 70, Left = 10 },
                new CheckBox { Text = "Чекбокс 3", Top = 100, Left = 10 }
            };

            // Додавання елементів на форму
            this.Controls.Add(mainCheckBox);
            checkBoxes.ForEach(checkBox => this.Controls.Add(checkBox));

            // Ініціалізація менеджера
            _ = new CustomCheckbox(mainCheckBox, checkBoxes);
        }
    }
}
