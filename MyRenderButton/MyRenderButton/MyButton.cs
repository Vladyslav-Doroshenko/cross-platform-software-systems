using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyRenderButton
{
    public class MyButton
    {
        public string Text { get; set; }
        public Rectangle Bounds { get; set; }
        public Color BackgroundColor { get; set; } = Color.LightGray;
        public Color ForegroundColor { get; set; } = Color.Black;
        public Color HoverColor { get; set; } = Color.Gray;
        public Color ClickedColor { get; set; } = Color.DarkGray;

        private bool isHovered = false;
        private bool isClicked = false;

        public event EventHandler<ButtonEventArgs> Click;

        public MyButton(string text, Rectangle bounds)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Bounds = bounds;
        }

        public void Draw(Graphics graphics)
        {
            // Визначаємо поточний колір кнопки
            Color currentColor = isClicked ? ClickedColor : isHovered ? HoverColor : BackgroundColor;

            // Малюємо фон кнопки
            using (Brush brush = new SolidBrush(currentColor))
            {
                graphics.FillRectangle(brush, Bounds);
            }

            // Малюємо текст кнопки
            using (Brush textBrush = new SolidBrush(ForegroundColor))
            {
                var stringFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                graphics.DrawString(Text, SystemFonts.DefaultFont, textBrush, Bounds, stringFormat);
            }

            // Малюємо рамку кнопки
            using (Pen borderPen = new Pen(Color.Black))
            {
                graphics.DrawRectangle(borderPen, Bounds);
            }
        }

        public void HandleMouseMove(Point location)
        {
            isHovered = Bounds.Contains(location);
        }

        public void HandleMouseDown(Point location)
        {
            if (Bounds.Contains(location))
            {
                isClicked = true;
            }
        }

        public void HandleMouseUp(Point location)
        {
            if (isClicked && Bounds.Contains(location))
            {
                Click?.Invoke(this, new ButtonEventArgs(Text));
            }
            isClicked = false;
        }
    }

    public class ButtonEventArgs : EventArgs
    {
        public string ButtonText { get; }

        public ButtonEventArgs(string buttonText)
        {
            ButtonText = buttonText ?? throw new ArgumentNullException(nameof(buttonText));
        }
    }
}
