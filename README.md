### PhoneBookDemo
**Code demonstration**

Тестовое задание

API «Телефонный справочник» 
Технологии: ASP.NET WEB API, MS SQL(можно LocalDB), EntityFramework

1. Консольное приложение 
* проект PhoneBookConsole (ado.net)
* проект PhoneBookConsole EF (entity framework)

*завершено*

2. Web API
проект PhoneBookWebApi
*	сортировка: orderBy
*	постраничный вывод: PageNumber и PageSize
* URL example "https://localhost:44382/api/users?PageSize=20&PageNumber=1&OrderBy=firstName desc"

*завершено*

Дополнительные задания
не реализованы

***********
по 5-му допу
>Web интерфейс тестирования запросов к WEB API (ввод параметров и вывод результата на страницу)
можно реализовать уже в рамках 2 задания поднять проект до NET Core 5, задействовать встроенный SwaggUI - автоматом получаем интерфейс тестирования для Web API

по 4-му допу
>OAuth2
аутентификация через сторонние службы может быть реализована в коде конфигурации сервисов
Startup()
	services.AddAuthentication().AddProviderName =>
но требует регистрации приложения на сторонних сервисах и хранение полученных идентификаторов/секретов 


