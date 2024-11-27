from PyQt5.QtWidgets import QApplication, QCheckBox, QVBoxLayout, QWidget
from PyQt5.QtCore import Qt


class CustomCheckBox(QCheckBox):
    def __init__(self, text, children=None):
        super().__init__(text)
        self.setTristate(True)  # Увімкнення тристатного режиму
        self.children = children if children else []
        self.is_updating = False  # Прапор для уникнення конфліктів сигналів

        # Підключаємо зміну стану головного чекбокса
        self.stateChanged.connect(self.on_parent_state_changed)

    def on_parent_state_changed(self, state):
        """
        Обробник стану головного чекбокса.
        Змінює стан усіх дочірніх чекбоксів залежно від нового стану.
        """
        if self.is_updating:
            return  # Уникаємо циклічних оновлень
        self.is_updating = True
        if state == Qt.Checked:
            self.set_children_state(Qt.Checked)
        elif state == Qt.Unchecked:
            self.set_children_state(Qt.Unchecked)
        self.is_updating = False

    def set_children_state(self, state):
        """
        Встановлює однаковий стан для всіх дочірніх чекбоксів.
        """
        for child in self.children:
            child.blockSignals(True)  # Уникаємо рекурсії
            child.setCheckState(state)
            child.blockSignals(False)  # Відновлюємо сигнали

    def update_state_from_children(self):
        """
        Оновлює стан головного чекбокса залежно від станів дочірніх чекбоксів.
        """
        if self.is_updating:
            return  # Уникаємо циклічних оновлень
        self.is_updating = True
        states = [child.checkState() for child in self.children]
        if all(s == Qt.Checked for s in states):  # Усі дочірні позначені
            self.setCheckState(Qt.Checked)
        elif all(s == Qt.Unchecked for s in states):  # Усі дочірні зняті
            self.setCheckState(Qt.Unchecked)
        else:  # Змішані стани дочірніх
            self.setCheckState(Qt.PartiallyChecked)
        self.is_updating = False


class MainWindow(QWidget):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("TriState CheckBox Example")
        layout = QVBoxLayout()

        # Дочірні чекбокси
        child1 = QCheckBox("Child 1")
        child2 = QCheckBox("Child 2")
        child3 = QCheckBox("Child 3")

        # Головний чекбокс
        parent = CustomCheckBox("Parent", children=[child1, child2, child3])

        # Підключаємо дочірні чекбокси до головного
        for child in [child1, child2, child3]:
            child.stateChanged.connect(parent.update_state_from_children)

        # Додаємо всі чекбокси до макета
        layout.addWidget(parent)
        layout.addWidget(child1)
        layout.addWidget(child2)
        layout.addWidget(child3)
        self.setLayout(layout)


if __name__ == "__main__":
    import sys

    app = QApplication(sys.argv)
    window = MainWindow()
    window.show()
    sys.exit(app.exec_())


