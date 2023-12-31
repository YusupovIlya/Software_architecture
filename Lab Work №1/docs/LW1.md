# Разработка универсального сервиса логирования для компании (ВКР)

## Описание
Необходимо разработать сервис для логирования действий пользователей, который можно при необходимости использовать на любом проекте в компании.

## Перечень заинтересованных лиц (стейкхолдеров)
1. IT-компания, т.к. сможет использовать при необходимости готовое решение, следовательно сократятся время и затраты на реализацию сервиса логирования на новом проекте.
2. Администратор системы заказчика - пользователь системы, которому будет доступен раздел с логами пользователей.

## Перечень функциональных требований
- Сервис должен хранить следующую информацию о действии пользователя в системе (Система, Раздел системы, Роль пользователя, IP-адрес компьютера, Время совершения действия, Наименование операции, Успешность завершения операции, Параметры запроса).
- Должна быть возможность фильтрации логов по следующим параметрам: Система, Раздел системы, Роль пользователя, IP-адрес компьютера, Время совершения действия.
- Должна быть возможность фильтрации логов с использованием языка запросов с поддержкой агрегатных функций.
- Возможность выгрузки логов за выбранный период в различных форматах (xlsx, csv, json).
- В сервисе должна быть возможность построить гистограмму по выбранному событию (операции) за определенный период.
- Должна быть функция построения маршрутов пользователей в системе.
- Должен быть авторизованный доступ для возможности просмотра логов.

## Диаграмма вариантов использования
<p align="center"><img src="https://github.com/YusupovIlya/Software_architecture/blob/LabWork1/Lab%20Work%20%E2%84%961/docs/LR1%20Diagram.jpg"></p>

## Перечень сделанных предположений
- У заказчика сильно ограничены ресурсы, которые он готов выделить под сервис логирования.
- Доступ к сервису логирования должен осуществляться через раздел "Администрирование" основной информационной системы. Следовательно, доступ к этому разделу должны иметь только пользователи с ролью "Администратор".

## Перечень нефункциональных требований
- Сервис логирования должен поддерживать интеграцию с любым проектом в компании.
- Сервис должен хранить логи пользователей за все время пользования системой.
- Сервис логирования должен быть производительным, т.е. поддерживать сохранение большого количества логов за ед. времени.
- Сервис логирования должен быть масштабируемым, т.к. система может быть с большим количеством пользователей.
