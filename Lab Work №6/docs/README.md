# Лабораторная работа №6
**Тема:** Использование шаблонов проектирования

**Цель работы:** Получить опыт применения шаблонов проектирования при написании кода программной системы.

## Шаблоны проектирования GoF
## Порождающие шаблоны
### 1. Фабричный метод, используется для создания сервисов выгрузки логов в разлчиные типы файлов
#### [Абстрактный класс объектов, которые создает фабрика](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/FileService.cs)
#### [Абстрактный класс фабрика объектов](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/FileFactories/FileFactory.cs)
#### [Конкретный класс-создатель xlsx файлов](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/FileFactories/XlsxFactory.cs)
#### [Конкретный класс-создатель csv файлов](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/FileFactories/CsvFactory.cs)
#### [Сервис xlsx файлов](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/XlsxFileService.cs)
#### [Сервис csv файлов](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/CsvFileService.cs)
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/docs/images/factory.jpg)

### 2. Строитель, используется для создания sql-запроса с условием для ClickHouse
#### [Абстрактный класс - строитель](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/Builders/QueryBuilder.cs)
#### [Конкретный класс-строитель запросов ClickHouse](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/Builders/ClickHouseQueryBuilder.cs)
#### [Распорядитель - создает объект, используя объект Builder](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/Builders/ClickHouseQueryClient.cs)
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/docs/images/builder.jpg)

### 3. Одиночка, используется для создания единственного экземпляра подключения к БД
#### [Код класса](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/ClickHouseDatabaseConnection.cs)
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/docs/images/singleton.jpg)

## Структурные шаблоны
### 1. Декоратор, используется для расширения функционала метода логирования событий
#### [Декоратор, реализуется в виде абстрактного класса](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Interfaces/LoggerDecorator.cs)
#### [Интерфейс для наследуемых объектов](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Interfaces/ILogger.cs)
#### [Конкретная реализация компонента, в которую с помощью декоратора добавляется новая функциональность](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/Logger.cs)
#### [Конкретная реализация декоратора, который представляет дополнительную функциональность для логирования по условию и уровню лога](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/Decorator/ConditionalLoggerDecorator.cs)
#### [Конкретная реализация декоратора, который представляет дополнительную функциональность для логирования по определенному уровню лога](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/Decorator/LogLevelFilterDecorator.cs)

### 2. Адаптер, используется для преобразования интерфейса класса PostgresDatabaseRepository в интерфейс IStreamDataSchemasRepository
#### [Адаптируемый класс](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/Adapter/PostgresDatabaseRepository.cs)
#### [Класс - адаптер](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/Adapter/PostgresStreamDataSchemasRepositoryAdapter.cs)
#### [Интерфейс, который используется клиентом](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Interfaces/IStreamDataSchemasRepository.cs)
#### [Клиент, который использует объекты IStreamDataSchemasRepository](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Controllers/StreamDataSchemasController.cs)
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/docs/images/adapter.jpg)

### 3. Фасад, предоставляет интерфейс для работы с различными типами логгеров
#### [Класс-фасад, который предоставляет интерфейс клиентскому коду для работы с компонентами](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/Loggers/LoggingFacade.cs)
#### [Компонент фасада, логирует в Postgres](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/Loggers/PostgresLogger.cs)
#### [Компонент фасада, логирует в файл](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/Loggers/FileLogger.cs)
#### [Компонент фасада, логирует в консоль](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/Loggers/ConsoleLogger.cs)
#### [Клиентский код, взаимодействует с логгером-фасадом](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Program.cs)
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/docs/images/facade.jpg)

### 4. Заместитель (прокси), предоставляет объект-заместитель, который управляет доступом к сервису выгрузки в xlsx, нужен для логирования самого события выгрузки в xlsx.
#### [Класс-заместитель](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/XlsxFileServiceProxy.cs)
#### [Сервис выгрузки в xlsx](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/XlsxFileService.cs)
#### [Абстрактный класс для прокси и сервиса выгрузки в xlsx](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/FileService.cs)
#### [Использует объект прокси для доступа к сервису выгрузки в xlsx](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Controllers/StreamDataController.cs)
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/docs/images/proxy.jpg)
