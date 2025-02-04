# SeverstalTestTask
Решение содержит 4 проекта
# Common
Содержит логику для взаимодействия с бд (репозитории, модели), используется sqlite, чтобы можно было запустить с любого компа.
# OrderService
WEB API сервиса управления заказами (по умолчанию открывается страница со swagger), использует Common для работы с БД
# ProductService
WEB API сервиса управления заказами (по умолчанию открывается страница со swagger), использует Common для работы с БД
# WpfOrderManagementSystem
WPF-приложение, использующее оба сервиса
# Как запустить
Для запуска с помощью Visual Studio необходимо кликнуть по решению правой кнопкой мыши и выбрать пунст "Настройка начальных проектов", настроить как показана на рисунке ниже

![изображение](https://github.com/user-attachments/assets/5e538959-224b-4529-b69b-5d252b9a806d)

Затем выбрать созданный профиль и запустить 

# Скрины

Swagger сервиса хранения
![изображение](https://github.com/user-attachments/assets/65569241-c08d-403e-9083-f32a52d3490e)

Swagger сервиса управления заказами
![изображение](https://github.com/user-attachments/assets/0149130f-2fa3-44b6-9884-43ecab6c1f44)

Получение информации о товарах
![изображение](https://github.com/user-attachments/assets/94cdb471-8d96-4fdc-b6aa-cc3fbe70193e)

Полчение информации о заказах
![изображение](https://github.com/user-attachments/assets/5f3cfeb9-adaf-45ed-b84d-376f8287b94f)

Изменения заказа
![изображение](https://github.com/user-attachments/assets/bf646a89-b343-4f0d-b0db-98e4fe87542d)

Создание нового заказа
![изображение](https://github.com/user-attachments/assets/042b3223-b470-4722-92b4-06196996795d)





