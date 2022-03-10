## Простейший IoC контейнер

Для инициализации в кокретной сборки необходимо выполнить:
```csharp
SimpleFactory.Initialize(typeof(Program).Assembly);
```
в качестве аргумента метода передается ссылка на текущую сборку, все классы помеченные атрибутом [SimpleBinder] будут автоматически зарегистрированы.

Регистрация классов:
```csharp
SimpleFactory.Register<T>();
SimpleFactoty.Bind<TService, TImplement>();
SimpleFactoty.Singleton<T>();
SimpleFactoty.Singleton<TService, TImplement>();
```
Есть возможность регистрировать классы через аттрибут
```csharp
[SimpleBinder(typeof(TService), Name = "")]
```
А также переопределять добавленые в контейнер классы с помощью аттрибута:
```csharp
[SimpleOverrided]
```
или из кода
```csharp
SimpleFactory.ReBind<TOldImpl, TImpl>(bool isSingleton = false);
```
Для поучение инстанса класса можно использовать:
```csharp
// Возвращает конкретную реализацию
var instance = SimpleFactory.Get<AfterRuleAge>(25); // 25 аргумент передаваемый в конструктор
// Возвращает все реализации для сервиса IRuleOrderConvert
IRuleOrderConvert[] rules = SimpleFactory.GetAll<IRuleOrderConvert>(); 
```

Пример в проекте ConsoleApp