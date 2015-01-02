#Инструкции за стартиране нa Web API проекта от master branch-a
1. Необходимо е да се свалят и инсталират следните неща (с тези е тествано):
    - Visual studio 2013 (Ultimate)
    - SQL Server 2014 (Express)
2. Взима се последна версия на master branch-a
    - най сигурният начин за това е локално да се изтрие всичко свързано с проекта в  master branch-a и да се изтегли наново
3. Проектът се отваря във Visual Studio 2013 и се build-ва.
4. От горната лента с менюта във Visual Studio се избира View->Other Windows->Package Manager Console
5. Въвежда се команда за създаването на локална база и ициализирането й с тестови данни

>update-database

6. Проектът се стартира с Ctrl + F5
7. От линка API Documentation може да се види как да се достъпят заявките, които са написани до момента. Засега всички се достъпват с [host]/api/име-на-контролер.
Например:

>http://localhost:1871/api/courses

#Често срещани проблеми
1. Ако проектът не може да се build-не след взимане на последна версия, много вероятно е да има неща, които не са merge-нати.
2. При проблеми с базата данни или SQL Server-a е най-добре да се търси информация за проблемите в Google, stackoverflow и подобни. Много често трябва да се дадат права за четене/писане, да се стартира нещо с администраторски права и подобни.
