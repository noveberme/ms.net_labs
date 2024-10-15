# ms.net_labs
# Требования к проекту "Билеты в кино"
Создание сервиса по продаже билетов в кино. Система подразумевает работу только с определенной сетью кинотеатров, с закрепленными за ними адресами.
Пользователь (клиент) может выбирать кинотеатр, фильм, место, покупать и сдавать билет.
Администратор может подтвержать заказ, изменять информацию о фильмах, о времени, о выбранных залах

# Требования к функциональности:

# 1) Регистрация.
   В начале пользователю необходимо зарегестрироваться, предоставив такие данные как:
   а) имя*
   б) фамилия*
   в) номер телефона
   г) адрес электронной почты*
   д) пароль*
   * - обязательные поля
# 2) Логин & Логаут
   Пользователь имеет возможность войти и выйти из аккаунта, используя логин и пароль (логином является адрес электронной почты).
   Тоже самое может сделатьи администратор, и работник кинотеатра.

# 3) (INDEX) Просмотр списка кинотеатров
   Пользователь может выбрать наиболее удобный адрес кинотеатра из данной сети и узнать список сеансов там

# 5) (INDEX) Просмотр списка фильмов.
   Пользователь имеет возможность просмотреть список текущих фильмов, идущих в данном кинотеатре

# 6) (INDEX) Просмотр возможного времени сеанса
   Пользователь может просмотреть по определенному фильму наиболее удобное время сеанса.

# 7) (CREATE) Создание записи
   Администратор может добавлять новые записи для сеансов одного кинотеатра, содержащих такую информацию, как:
   а) название фильма
   б) точное время сеанса (дата и время)
   в) продолжительность сеанса
   г) стоимость
   
# 8) (READ) Просмотр деталей записи
   Пользователь имеет возможность просмотреть детальную информацию о фильме: название, год выхода, страна, актерский состав, режиссер, продюссер, студия, описание сюжета, оценки

# 9) (UPDATE) Редактирование записи
    Администратор имеет возможность изменить запись о конкретном сеансе, изменив любое поле (перенос времени сеанса или изменение фильма, как пример)

# 10) (DELETE) Изменение записи
    Администратор имеет право удалить сеанс полностью, как и записи о нем
