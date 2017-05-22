# Задания к модулю LINQ
## Общая информация 
### Для выполнения заданий к данному модулю вам потребуется доработать демонстрационное приложение, исходный код которого лежит в файле Task.zip.
#### Доработка происходит по следующим правилам:
•	Для каждого пункта задания вы создаете в классе LinqSamples новый метод с именем LinqXXX где XXX номер по порядку
•	Для метода указываете атрибуты Category, Title, Description (язык заполнения RUS/ENG/… согласуйте с ментором)
•	Созданный метод должен содержать необходимую инициализацию, сам требуемый LINQ-запрос и выдачу результатов
•	Проверять результат можно запуская приложение (F5) и просматривая результат визуально
Внимание!!! Данный вариант реализации не является обязательным и по согласованию с ментором вы можете заменить его, например, на обычные модульные тесты (по тесту на задание)
#### Задания. 
1.	Выдайте список всех клиентов, чей суммарный оборот (сумма всех заказов) превосходит некоторую величину X. Продемонстрируйте выполнение запроса с различными X (подумайте, можно ли обойтись без копирования запроса несколько раз)
2.	Для каждого клиента составьте список поставщиков, находящихся в той же стране и том же городе. Сделайте задания с использованием группировки и без.
3.	Найдите всех клиентов, у которых были заказы, превосходящие по сумме величину X
4.	Выдайте список клиентов с указанием, начиная с какого месяца какого года они стали клиентами (принять за таковые месяц и год самого первого заказа)
5.	Сделайте предыдущее задание, но выдайте список отсортированным по году, месяцу, оборотам клиента (от максимального к минимальному) и имени клиента
6.	Укажите всех клиентов, у которых указан нецифровой почтовый код или не заполнен регион или в телефоне не указан код оператора (для простоты считаем, что это равнозначно «нет круглых скобочек в начале»).
7.	Сгруппируйте все продукты по категориям, внутри – по наличию на складе, внутри последней группы отсортируйте по стоимости
8.	Сгруппируйте товары по группам «дешевые», «средняя цена», «дорогие». Границы каждой группы задайте сами
9.	Рассчитайте среднюю прибыльность каждого города (среднюю сумму заказа по всем клиентам из данного города) и среднюю интенсивность (среднее количество заказов, приходящееся на клиента из каждого города)
10.	Сделайте среднегодовую статистику активности клиентов по месяцам (без учета года), статистику по годам, по годам и месяцам (т.е. когда один месяц в разные годы имеет своё значение).
