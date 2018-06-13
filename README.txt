Проект требует:
- установленного NET 4.7.1
- MSSQL 2014 (возможно и на 2012 пойдет)

Запускаемый проект - WebApiDemo2. Можно запустить из студии или развернуть его на IIS. БД создается в фолдере App_Data при первом запуске.
Первичное наполнение БД происходит автоматически через исполнение скрипта Assets.sql

ПРоект можно переключить на работу как с Entity Framework, так и с NHibernate. Переключение - через корневой web.config:
      <register type="WebApiTest.Interfaces.IAppDbContext, WebApiTest.Interfaces" mapTo="WebApiTest.MSSQL.WebApiTestDB, WebApiTest.MSSQL" />
      <!--<register type="WebApiTest.Interfaces.IAppDbContext, WebApiTest.Interfaces" mapTo="WebApiTest.NHibernate.WebApiTestDBNHibernate, WebApiTest.NHibernate"/>-->



