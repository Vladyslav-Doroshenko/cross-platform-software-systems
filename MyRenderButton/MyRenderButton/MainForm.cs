using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyRenderButton
{
    public class MainForm : Form
    {
        private MyButton myButton;

        public MainForm()
        {
            // Налаштування основної форми
            Text = "MyRenderButton";
            Width = 400;
            Height = 300;

            // Ініціалізація кнопки
            myButton = new MyButton("Click Me", new Rectangle(100, 100, 200, 50));
            myButton.Click += MyButton_Click;

            // Підписка на події форми
            Paint += MainForm_Paint;
            MouseMove += MainForm_MouseMove;
            MouseDown += MainForm_MouseDown;
            MouseUp += MainForm_MouseUp;
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            myButton.Draw(e.Graphics); // Малює кнопку
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            myButton.HandleMouseMove(e.Location); // Відстежує наведення курсора
            Invalidate(); // Оновлює форму
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            myButton.HandleMouseDown(e.Location); // Обробляє натискання кнопки миші
            Invalidate(); // Оновлює форму
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            myButton.HandleMouseUp(e.Location); // Обробляє відпускання кнопки миші
            Invalidate(); // Оновлює форму
        }

        private void MyButton_Click(object sender, ButtonEventArgs e)
        {
            MessageBox.Show($"Button clicked! Text: {e.ButtonText}"); // Відображає повідомлення при натисканні кнопки
        }
    }
}
