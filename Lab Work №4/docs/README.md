# Logging.Service.WebApi

Версия API: 1.0

## Ресурс stream-data/schemas
Данный ресурс предоставляет методы для создания потоков данных - таблиц в базе, которые могут иметь разный набор свойств для хранения логов.
### `ССЫЛКА НА РЕАЛИЗАЦИЮ`

## Методы


### 1. Получение схемы данных потока
- **URI**: /api/cl/stream-data/schemas/{streamId}
- **Тип запроса**: GET
- **Параметры**:
  - `streamId` (path, integer, required): Идентификатор потока.
- **Ответы**:
  - **200 Success**: Возвращает `StreamDataSchemaViewModel` с идентификатором потока и колонками.

### Модель StreamDataSchemaViewModel

| Свойство | Тип                                            | Описание                           |
|----------|------------------------------------------------|------------------------------------|
| streamId | integer (format: int64)                        | Идентификатор потока               |
| columns  | Array of StreamDataSchemaColumnViewModel (nullable) | Массив моделей колонок схемы потока |

### Модель StreamDataSchemaColumnViewModel

| Свойство    | Тип                                    | Описание                             |
|-------------|----------------------------------------|--------------------------------------|
| name        | string (nullable)                      | Имя колонки                          |
| type        | StreamDataSchemaColumnType (enum)      | Тип колонки                          |
| elementType | StreamDataSchemaColumnType (enum, nullable) | Тип элемента для коллекционных типов |
| defaultValue| string (nullable)                      | Значение по умолчанию для колонки    |


### 2. Создание схемы данных потока
- **URI**: /api/cl/stream-data/schemas
- **Тип запроса**: POST
- **Тело запроса**: `StreamDataSchemaPostViewModel`

### Модель StreamDataSchemaPostViewModel

| Свойство | Тип                                            | Описание                           |
|----------|------------------------------------------------|------------------------------------|
| streamId | integer (format: int64, minimum: 1)            | Идентификатор потока               |
| columns  | Array of StreamDataSchemaColumnViewModel (nullable) (описание в п.1)| Массив моделей колонок для схемы потока |
- **Ответы**:
  - **200 Success**: Успешное создание схемы.

### 3. Обновление схемы данных потока
- **URI**: /api/cl/stream-data/schemas
- **Тип запроса**: PUT
- **Тело запроса**: `StreamDataSchemaPutViewModel`

### Модель StreamDataSchemaPutViewModel

| Свойство   | Тип                                            | Описание                           |
|------------|------------------------------------------------|------------------------------------|
| streamId   | integer (format: int64, minimum: 1)            | Идентификатор потока               |
| columnsToAdd | Array of StreamDataSchemaColumnViewModel (nullable) (описание в п.1) | Массив колонок, которые будут добавлены к схеме потока |

- **Ответы**:
  - **200 Success**: Успешное обновление схемы.



## Ресурс stream-data
Данный ресурс предоставляет методы для работы с логами - событиями, их просмотр с фильтрацией и агрегацией, экспортов в файл.
### `ССЫЛКА НА РЕАЛИЗАЦИЮ`

## Методы

### 1. Получение отфильтрованных данных нескольких потоков и агрегации для гистограммы частотости событий
- **URI**: /api/cl/stream-data/filter
- **Тип запроса**: POST
- **Параметры**:
  - `timestampSortDir` (query, string, default: "desc"): Направление сортировки логов в ответе (desc, asc).
- **Тело запроса**: `StreamDataFilterViewModel`
### Модель StreamDataFilterViewModel

| Свойство       | Тип                            | Описание                           |
|----------------|--------------------------------|------------------------------------|
| query          | string (nullable)              | Запрос для фильтрации логов             |
| filteredCount  | integer (format: int32)        | Количество логов в ответе от сервера (min: 0, max: 2147483647) |
| interval       | integer (format: int32)        | Интервал  разбивки для гистограммы частотности событий                         |
| timestamp      | DateRangePostViewModel         | Временной диапазон запроса         |
| streamIds      | Array of integer (format: int64, nullable) | Массив идентификаторов потоков данных |

### Модель DateRangePostViewModel

| Свойство | Тип                  | Описание                     |
|----------|----------------------|------------------------------|
| start    | date-time | Начальная дата и время     |
| end      | date-time | Конечная дата и время      |


- **Ответ**:
  - **200 Success**: Возвращает `StreamDataFilterResultViewModel` с агрегацией для гистограммы и логами.
### Модель StreamDataFilterResultViewModel

| Свойство    | Тип                                       | Описание                             |
|-------------|-------------------------------------------|--------------------------------------|
| aggregations| Array of StreamDataAggregateViewModel (nullable) | Массив агрегаций для гистограммы частотности                 |
| documents   | Array of StreamDataEventViewModel (nullable)    | Массив логов                   | 
### Модель StreamDataAggregateViewModel

| Свойство  | Тип                        | Описание                       |
|-----------|----------------------------|--------------------------------|
| timestamp | date-time | Временная метка      |
| count     | integer (format: int64)    | Количество логов в данном интервале |

### Модель StreamDataEventViewModel

| Свойство | Тип                      | Описание                        |
|----------|--------------------------|---------------------------------|
| id       | uuid  | Уникальный идентификатор события |
| timestamp| date-time | Временная метка события          |
| source   | (string)          | Событие в json |



### 2. Экспорт отфильтрованных логов в файл

- **URI**: /api/cl/stream-data/filter/file
- **Тип запроса**: POST
- **Параметры**:
  - `type` (query, string, required): Тип файла (xlsx, csv)
  - `timestampSortDir` (query, string, default: "desc"): Направление сортировки логов в ответе (desc, asc).
- **Тело запроса**: `StreamDataFilterFileViewModel`
### Модель StreamDataFilterFileViewModel

| Свойство     | Тип                                          | Описание                           |
|--------------|----------------------------------------------|------------------------------------|
| query        | string (nullable)                            | Запрос для фильтрации              |
| checkedFields| Array of string (nullable)                   | Массив проверяемых полей           |
| timestamp    | DateRangePostViewModel                       | Временной диапазон запроса         |
| streamIds    | Array of integer (format: int64, nullable)   | Массив идентификаторов потоков     |

- **Ответы**:
  - **200 Success**: Возвращает файл в формате binary.

### 3. Агрегация данных потока
- **URI**: /api/cl/stream-data/aggregate
- **Тип запроса**: POST
- **Тело запроса**: `AggregationPostViewModel`
### Модель AggregationPostViewModel

| Свойство         | Тип                                          | Описание                               |
|------------------|----------------------------------------------|----------------------------------------|
| query            | string (nullable)                            | Запрос для фильтрации                   |
| aggregationQuery | string (nullable)                            | Запрос агрегации                       |
| streamIds        | Array of integer (format: int64, nullable)   | Массив идентификаторов потоков         |
| timestamp        | DateRangePostViewModel                       | Временной диапазон для агрегации       |
- **Ответы**:
  - **200 Success**: Возвращает `AggregationViewModel` с функцией, колонкой и результатами агрегации.

### Модель AggregationViewModel

| Свойство | Тип                        | Описание                             |
|----------|----------------------------|--------------------------------------|
| function | string (nullable)          | Функция агрегации                    |
| column   | string (nullable)          | Колонка для агрегации                |
| result   | Array of AggregationResultViewModel (nullable) | Массив с результатами агрегации |


### 4. Получение количества событий - логов в потоке

- **URI**: /api/cl/stream-data/{streamId}/count
- **Тип запроса**: GET
- **Параметры**:
  - `streamId` (path, integer, required): Идентификатор потока.
- **Ответы**:
  - **200 Success**: Возвращает `StreamDataEventsCountViewModel` с количеством событий.
### Модель StreamDataEventsCountViewModel

| Свойство | Тип                    | Описание                        |
|----------|------------------------|---------------------------------|
| count    | integer (format: int64)| Количество событий              |
