# Лабораторная работа №3
**Тема:** Использование принципов проектирования на уровне методов и классов.

**Цель работы:** Получить опыт проектирования и реализации модулей с использованием принципов KISS, YAGNI, DRY и SOLID.

## Диаграмма компонентов
На диаграмме представлен **Web-Api Application**, который предоставляет API для работы с логами (просмотр, выгрузка, агрегационные операции и т.д.). 
Назначение каждого компонента отрожено на диаграмме в описании компонента.
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork3/Lab%20Work%20%E2%84%963/docs/components_diagram.png)

## Диаграмма последовательностей
Диаграмма составлена для варианта использования - **Отфильтровать логи с использованием языка запросов**. 
На ней отображено взаимодействие компонентов для реализации выбранного варианта использования.
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork3/Lab%20Work%20%E2%84%963/docs/sequence_diagram.png)

## Модель БД
На диаграмме представлена схема БД, которая состоит из 5 таблиц: Лог, Пользователь, Роль, Раздел системы, Система.
- Сущность "Пользователь" имеет связь один ко многим с сущностью "Лог" и один к одному с сущностью "Роль".
- Сущность "РазделСистемы" имеет связь один ко многим с сущностью "Лог".
- Сущность "Система" имеет связь один ко многим с сущностью "РазделСистемы".
![img](https://github.com/YusupovIlya/Software_architecture/blob/LabWork3/Lab%20Work%20%E2%84%963/docs/db_diagram.png)

## Применение основных принципов разработки
# SOLID
**S – Single Responsibility**
Данный метод WriteBatch выполняет только одну функцию - записывает переданный список событий в базу данных.
```
public async Task WriteBatch(IEnumerable<EventItem> events, string tableName)
        {
            await using var connection = GetConnection();

            var columns = events.Select(x => x.Columns).FirstOrDefault();
            var values = events.Select(x => x.Values.ToArray());

            using var command = new ClickHouseBulkCopy(connection)
            {
                MaxDegreeOfParallelism = Options.MaxDegreeOfParallelism,
                BatchSize = Options.EventsFlushCount,
                DestinationTableName = connection.Database + "." + tableName
            };
            await command.WriteToServerAsync(values, columns);
        }
```
**O — Open-Closed**
Добавляем новый функционал классу EventsBufferEngine путем использования метода расширения, а не явного редактирования кода этого класса.
```
public static class EventsBufferEngineExtensions
    {
        /// <summary>
        /// Add the event to the buffer.
        /// </summary>
        public static Task AddEvent<T>(this EventsBufferEngine engine, [NotNull] T @event, string tableName, bool useCamelCase = true)
            where T : class
        {
            return engine.AddEvent(@event, tableName, useCamelCase);
        }
    }
```
**L — Liskov Substitution**
Не переопределяем поведение в конструкторе класса наследника. Тем самым в коде можно неявно заменять BaseRepository на DefaultRepository и тем самым код останется также работоспособным.
```
    public class DefaultRepository
        : BaseRepository, IPersistRepository
    {
        /// <summary>
        /// Initializes a new instance of the class <see cref="DefaultRepository" />.
        /// </summary>
        /// <param name="engineOptions">ClickHouse configuration.</param>
        public DefaultRepository(
            IOptions<EngineOptions> engineOptions) : base(engineOptions)
        { }
```
**I — Interface Segregation**
Разделяем интерфейсы на IEventsWriter и IErrorEventsHandler, тем самым не нужно реализовывать лишние методы, если например нужен только метод Write. Каждый интерфейс решает одну задачу.
```
    public interface IEventsWriter
    {
        Task Write(IEnumerable<EventItem> events, string tableName);
    }
    public interface IErrorEventsHandler
    {
        Task Handle(IEnumerable<EventItem> events);
    }
```
**D — Dependency Inversion**
Инвертируем зависимости persistRepository и logger, тем самым класс EventsWriter зависит не от конретных их реализаций, а от интерфейсов. Следовательно, при необходимости можно подменять реализации persistRepository и logger без редактирования кода класса EventsWriter.
```
        readonly ILogger<EventsWriter> _logger;
        readonly IPersistRepository _persistRepository;

        public EventsWriter(
            IPersistRepository persistRepository,
            ILogger<EventsWriter> logger)
        {
            _persistRepository = persistRepository;
            _logger = logger;
        }
```
## KISS
Из кода метода ToCamelCase сразу стает понятно, что он делает, так как он короткий и простой.
```
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the string <paramref name="value"/> to camelcase.
        /// </summary>
        public static string ToCamelCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            return char.ToLower(value[0]) + value.Substring(1, value.Length - 1);
        }
    }
```
## DRY и YAGNI
Реализуем метод получения соединения с бд в родительском классе, чтобы в классах наследниках переиспользовать этот код, а не дублировать.
Также не реализуем лишний код в классе BaseRepository, так как при необходимости его всегда можно реализовать в классах наследниках.
```
   public abstract class BaseRepository
    {
/// other code
        protected ClickHouseConnection GetConnection() =>
            new ClickHouseConnection(_connectionString);
```
