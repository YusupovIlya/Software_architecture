# Тестирование API

## Ресурс stream-data/schemas

### 1. Получение схемы данных потока
- **URI**: /api/cl/stream-data/schemas/{streamId}
- **Тип запроса**: GET
- **Код тестов для для данного метода**
```
pm.test("Status code is 200", function () {
    pm.response.to.have.status(200);
});

pm.test("Response structure is correct", function () {
    let jsonData = pm.response.json();
    pm.expect(jsonData).to.have.property('streamId');
    pm.expect(jsonData).to.have.property('columns');
    pm.expect(Array.isArray(jsonData.columns)).to.be.true; // Проверка, что columns это массив
});


pm.test("StreamId is a number", function () {
    let jsonData = pm.response.json();
    pm.expect(typeof jsonData.streamId).to.eql('number');
});

pm.test("Columns array is not empty", function () {
    let jsonData = pm.response.json();
    pm.expect(jsonData.columns.length).to.be.above(0);
});

pm.test("Each column has name and type", function () {
    let jsonData = pm.response.json();
    jsonData.columns.forEach(function (column) {
        pm.expect(column).to.have.property('name');
        pm.expect(column).to.have.property('type');
    });
});
```
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork4/Lab%20Work%20%E2%84%964/docs/images/schema1%20input%20and%20results.jpg)
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork4/Lab%20Work%20%E2%84%964/docs/images/schema1%20test.jpg)

### 2. Создание схемы данных потока
- **URI**: /api/cl/stream-data/schemas
- **Тип запроса**: POST
- **Код тестов для для данного метода**
```
pm.test("Status code is 204, schema created successfully", function () {
    pm.response.to.have.status(204);
});

pm.test("Response body is empty", function () {
    pm.expect(pm.response.text()).to.be.empty;
});
```
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork4/Lab%20Work%20%E2%84%964/docs/images/schema2%20input%20and%20result.jpg)
![img]()

### 3. Обновление схемы данных потока
- **URI**: /api/cl/stream-data/schemas
- **Тип запроса**: PUT
- **Код тестов для для данного метода**
```
pm.test("Status code is 204, schema updated successfully", function () {
    pm.response.to.have.status(204);
});

pm.test("Response body is empty", function () {
    pm.expect(pm.response.text()).to.be.empty;
});
```
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork4/Lab%20Work%20%E2%84%964/docs/images/schema2%20input%20and%20result.jpg)
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork4/Lab%20Work%20%E2%84%964/docs/images/schema3%20tests.jpg)

### 4. Удаление схемы данных потока
- **URI**: /api/cl/stream-data/schemas/{streamId}
- **Тип запроса**: DELETE
- **Код тестов для для данного метода**
```
pm.test("Status code is 204, schema deleted successfully", function () {
    pm.response.to.have.status(204);
});

pm.test("Response body is empty", function () {
    pm.expect(pm.response.text()).to.be.empty;
});
```
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork4/Lab%20Work%20%E2%84%964/docs/images/schema5%20res.jpg)
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork4/Lab%20Work%20%E2%84%964/docs/images/schema5%20tests.jpg)

## Ресурс stream-data

### 1. Получение отфильтрованных данных нескольких потоков и агрегации для гистограммы частотости событий
- **URI**: /api/cl/stream-data/filter
- **Тип запроса**: POST
- **Код тестов для для данного метода**
```
pm.test("Status code is 200", function () {
    pm.response.to.have.status(200);
});

pm.test("Response structure is correct", function () {
    let jsonData = pm.response.json();
    pm.expect(jsonData).to.have.property('aggregations');
    pm.expect(jsonData).to.have.property('documents');
});

pm.test("Aggregations and documents are arrays", function () {
    let jsonData = pm.response.json();
    pm.expect(Array.isArray(jsonData.aggregations)).to.be.true;
    pm.expect(Array.isArray(jsonData.documents)).to.be.true;
});
```
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork4/Lab%20Work%20%E2%84%964/docs/images/data1%20input%20and%20results.jpg)
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork4/Lab%20Work%20%E2%84%964/docs/images/data1%20tests.jpg)

### 2. Экспорт отфильтрованных логов в файл
- **URI**: /api/cl/stream-data/filter/file
- **Тип запроса**: POST
- **Код тестов для для данного метода**
```
pm.test("Status code is 200", function () {
    pm.response.to.have.status(200);
});

pm.test("Content-Type is text/csv or application/csv", function () {
    pm.expect(pm.response.headers.get('Content-Type')).to.be.oneOf(['text/csv', 'application/csv']);
});

pm.test("Body is not empty", function () {
    pm.expect(pm.response.text()).to.not.be.empty;
});
```
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork4/Lab%20Work%20%E2%84%964/docs/images/data2%20input%20and%20results.jpg)
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork4/Lab%20Work%20%E2%84%964/docs/images/data2%20tests.jpg)

### 3. Агрегация данных потока
- **URI**: /api/cl/stream-data/aggregate
- **Тип запроса**: POST
- **Код тестов для для данного метода**
```
pm.test("Status code is 200", function () {
    pm.response.to.have.status(200);
});

pm.test("Response structure is correct", function () {
    let jsonData = pm.response.json();
    pm.expect(jsonData).to.have.property('function');
    pm.expect(jsonData).to.have.property('column');
    pm.expect(jsonData).to.have.property('result');
    pm.expect(Array.isArray(jsonData.result)).to.be.true; // Проверка, что result это массив
});
```
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork4/Lab%20Work%20%E2%84%964/docs/images/data3%20input%20and%20results.jpg)
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork4/Lab%20Work%20%E2%84%964/docs/images/data3%20tests.jpg)

### 4. Получение количества событий - логов в потоке
- **URI**: /api/cl/stream-data/{streamId}/count
- **Тип запроса**: GET
- **Код тестов для для данного метода**
```
pm.test("Status code is 200", function () {
    pm.response.to.have.status(200);
});

pm.test("Response structure is correct", function () {
    let jsonData = pm.response.json();
    pm.expect(jsonData).to.have.property('count');
    pm.expect(typeof jsonData.count).to.eql('number'); // Проверка, что count это число
});

pm.test("Count is a non-negative number", function () {
    let jsonData = pm.response.json();
    pm.expect(jsonData.count).to.be.at.least(0);
});
```
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork4/Lab%20Work%20%E2%84%964/docs/images/data4.jpg)