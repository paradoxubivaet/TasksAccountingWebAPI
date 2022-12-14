# TasksAccountingWebAPI

Тестовое задание для соискателя на должность Fullstack Developer.

Цель задания: разработка информационной системы (ИС) для учета выполнения заданий соискателями на различные должности компании Лимончелло. ИС должна включать:
- базу данных (T-SQL) для хранения сущностей бизнес-процесса
- Web Api приложение Asp.Net Core предоставляющее REST программный интерфейс для получения отчетов и управления сущностями бизнес-процесса

Описание бизнес-процесса: сотрудник отдела кадров вносит в БД сведенья о соискателе: ФИО, номер телефона, должность, на которую претендует, дату первичного собеседования (выдачи задания), фамилию и должность сотрудника, проводившего собеседование и срок, который предоставляется соискателю на выполнение задания. Руководитель структурного подразделения на должность в котором претендуют соискатели, принимает от соискателя выполненное задание, отмечает в базе данных дату и время получения задания, при этом, в базу данных записывается фамилия руководителя, поставившего отметку. После проверки полученного задания, руководитель указывает оценку в баллах от 1 до 5, где 5 – отлично выполненное задание, записывает эту оценку в базу данных. В любой момент времени сотрудниками компании может быть запрошен отчет об общем списке соискателей на все должности компании за определенный срок, где указывается текущий статус выполнения задания и, если задание выполнено, оценка, сформированная системой по правилу:
прямо пропорциональна оценке сотрудника, проверившего задание и обратно пропорциональна времени в днях выполнения задания.
Сотрудник отдела кадров должен быть уведомлен незамедлительно об изменении статуса выполнения задания соискателем:
задание получено
задание сдано на проверку
заданию выставлена оценка сотрудником
истекло время выполнения задания, в том случае, если задание не было сдано на проверку ранее.
При истечении времени выполнения задания, если задание не было сдано на проверку, автоматически выставляется результирующая оценка 0.

Пункты для реализации:
разработать сущности базы данных в соответствии с указанным бизнес-процессом
в базе данных реализовать функцию расчета результирующей оценки
в базе данных реализовать хранимые процедуры для CRUD операций с сущностями
в базе данных реализовать хранимую процедуру получения данных для отчета о ходе выполнения соискателями заданий за указанный период
в Web API приложении реализовать методы взаимодействий с базой данных для обеспечения выполнения указанных в бизнес-процессе задач (выполнение CRUD операций по запросу внешних служб, формирование и выдача отчета)
в Web Api приложении реализовать следящую подсистему, которая будет контролировать этапы выполнения заданий соискателями, в случае изменения их статуса отправлять Http запросы на внешнюю систему с уведомлением об изменении и, в случае истечения срока выполнения работы, выставлять соответствующему абоненту результирующую оценку 0.

Дополнительные условия и ограничения:
- база данных mssql, язык T -SQL
- приложение ASP.NET CORE, язык C#

Форма предоставления результата выполнения задания:
- репозиторий Git, содержащий проект Visual Studio. Код должен быть отлажен и компилироваться без ошибок
- сущности, хранимые процедуры и функцию предоставить в виде листинга примитивов (операции CREATE)

Критерии оценки результата выполнения задания:
Время. Испытуемый изучает и уточняет задание, оценивает свои силы и указывает время, за которое задание будет выполнено. Оцениваться будет факт, уложился ли испытуемый в указанный ми срок.
Полнота. В оценку включается соответствие функций разработанной ИС потребностям бизнес-процесса.
Отсутствие логических ошибок.
Чистота кода, соответствие стиля написания кода общепринятым методикам (например SOLID)
Читабельность кода, наличие понятного описания к методам и объектам ИС
Целесообразность применения сторонних библиотек для решения задач при реализации ИС.
