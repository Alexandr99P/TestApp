# TestApp

## Описание
Приложение для выполнения тестов с возможностью отмены операции. Позволяет пользователю ввести идентификатор изделия, выбрать тест из списка и запустить его выполнение. Результаты теста сохраняются в текстовый файл.

---

## Функционал
1. Ввод данных:
   - Поле для ввода идентификатора изделия.
   - Список доступных тестов (Test 1, Test 2, Test 3).

2. Контролы:
   - Кнопка "Start" для начала выполнения теста.
   - Кнопка "Stop" для отмены текущего теста.

3. Статус:
   - Отображение текущего состояния приложения (ожидание, выполнение, завершение или отмена).

4. Результаты:
   - Результаты теста сохраняются в файл с именем {идентификатор_изделия}.txt.

---

## Требования
- .NET Framework 4.7.2 или выше.
- Windows OS.

---

## Как использовать
1. Запустите приложение.
2. Введите идентификатор изделия в соответствующее поле.
3. Выберите тест из выпадающего списка.
4. Нажмите кнопку "Start" для запуска теста.
5. Во время выполнения теста можно нажать "Stop" для его отмены.
6. После завершения теста результаты будут сохранены в файл.

---

## Логика работы
1. При выборе теста и нажатии на "Start":
   - Проверяется наличие идентификатора изделия.
   - Запускается асинхронное выполнение теста.
   - Статус обновляется до "Выполнение теста...".

2. Во время выполнения:
   - Тест имитирует длительную операцию (от 10 до 30 секунд).
   - Результат определяется случайным образом (успех или ошибка).

3. После завершения:
   - Если тест успешен, данные записываются в файл.
   - Если тест отменен, статус обновляется до "Тест отменен".
   - Если произошла ошибка, она сохраняется в файле.

---

## Примечания
- Имя файла результата формируется как {идентификатор_изделия}.txt.
- Данные в файле включают информацию об успехе/ошибке и сгенерированные значения.
- При отмене теста файл не создается.
