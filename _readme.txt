Тестовое задание
API «Телефонный справочник» 
Технологии: ASP.NET WEB API, MS SQL(можно LocalDB), EntityFramework

1 
консольное приложение 
проект PhoneBookConsole (ado.net)
проект PhoneBookConsole EF (entity framework)
завершено

2
Web API
проект PhoneBookWebApi
	сортировка: orderBy
	постраничный вывод: PageNumber и PageSize
URL example "https://localhost:44382/api/users?PageSize=20&PageNumber=1&OrderBy=firstName desc"
завершено	 

Дополнительные задания
не реализованы

PS
***********
по 5-му допу
>>Web интерфейс тестирования запросов к WEB API (ввод параметров и вывод результата на страницу)
можно реализовать как
 уже в рамках 2 задания можно поднять проект до NET Core 5, задействовать встроенный SwaggUI - автоматом получаем интерфейс тестирования для Web API

по 4-му допу
>Аутентификацию OAuth2
аутентификация через сторонние службы реализуется уже в коде конфигурации сервисов
Startup()
	services.AddAuthentication().AddProviderName =>
но требуется регистрация приложения на сторонних сервисах и хранение полученных идентификаторов/секретов 

