# Лабораторная работа №6
**Тема:** Использование шаблонов проектирования

**Цель работы:** Получить опыт применения шаблонов проектирования при написании кода программной системы.

## Шаблоны проектирования GoF
### Порождающие шаблоны
1. Фабричный метод
### [Абстрактный класс объектов, которые создает фабрика](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/FileService.cs)
### [Абстрактный класс фабрика объектов](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/FileFactories/FileFactory.cs)
### [Конкретный класс-создатель xlsx файлов](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/FileFactories/XlsxFactory.cs)
### [Конкретный класс-создатель csv файлов](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/FileFactories/CsvFactory.cs)
### [Сервис xlsx файлов](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/XlsxFileService.cs)
### [Сервис csv файлов](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/src/src/Logging.Service.WebApi/Services/Implementation/CsvFileService.cs)
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork6/Lab%20Work%20%E2%84%966/docs/images/factory_method.jpg)
