from PyQt5.QtWidgets import QApplication, QWidget
from PyQt5.QtGui import QPainter, QFont, QColor
from PyQt5.QtCore import QRect, Qt


class RenderButton(QWidget):
    def __init__(self, text, width=150, height=50, bg_color="#CCCCCC", hover_color="#87CEEB", text_color="#000000", parent=None):
        super().__init__(parent)
        self.text = text
        self.width = width
        self.height = height
        self.bg_color = bg_color
        self.hover_color = hover_color
        self.text_color = text_color
        self.is_hovered = False
        self.clicked_callback = None

        self.setFixedSize(self.width, self.height)

    def set_on_click(self, callback):
        """Прив'язує функцію для обробки кліків."""
        self.clicked_callback = callback

    def mousePressEvent(self, event):
        if event.button() == Qt.LeftButton and self.clicked_callback:
            self.clicked_callback()

    def enterEvent(self, event):
        """Подія наведення курсору."""
        self.is_hovered = True
        self.update()

    def leaveEvent(self, event):
        """Подія виходу курсору."""
        self.is_hovered = False
        self.update()

    def paintEvent(self, event):
        painter = QPainter(self)
        painter.setRenderHint(QPainter.Antialiasing)

        # Фон кнопки
        color = QColor(self.hover_color if self.is_hovered else self.bg_color)
        painter.setBrush(color)
        painter.setPen(Qt.NoPen)
        painter.drawRect(QRect(0, 0, self.width, self.height))

        # Текст кнопки
        painter.setPen(QColor(self.text_color))
        painter.setFont(QFont("Arial", 14))
        painter.drawText(self.rect(), Qt.AlignCenter, self.text)


class MainWindow(QWidget):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("Render Button")
        self.setGeometry(100, 100, 400, 300)

        # Створення кнопки
        self.button = RenderButton("Click Me", parent=self)
        self.button.set_on_click(self.on_button_click)
        self.button.move(125, 125)

    def on_button_click(self):
        print("Button clicked!")


if __name__ == "__main__":
    import sys

    app = QApplication(sys.argv)
    window = MainWindow()
    window.show()
    sys.exit(app.exec_())
