#### Merhabalar, bu proje Patika.dev ve Papara işbirliği ile gerçekleştirilen .NET Bootcamp Cohorts ödevleri için oluşturulmuştur.


## 1. HAFTA İÇERİĞİ
**Restful Api Geliştirilmesi**

 - REST standartlarna uygun olmalıdır. 
>  Proje REST standartlarına uygun geliştirilmiştir.
 
 - GET,POST,PUT,DELETE,PATCH methodları kullanılmalıdır. 
>  Projede Controllerlar içerisinde GET,POST,PUT,DELETE ve PATCH metotları kullanılmıştır.
 
 - HTTP status code standartlarına uyulmalıdır. Error Handler ile 500, 400, 404, 200, 201 hatalarının standart format ile verilmesi 
>  Projede HTPP status kod standartlarına uyulmuştur. ApiResponse isimli bir standart response tipi oluşturulmuş ve response'lar bu tipte dönülerek tüm hata kodları uygun şekilde verilmiştir.
 
 - Modellerde zorunlu alanların kontrolü yapılmalıdır. 
>  Modellerde zorunlu alan kontrolü data annotations ile yapılmaktadır.
 
 - Routing kullanılmalıdır. 
>  Projede routing kullanılmıştır.
 
 - Model binding işlemleri hem body den hemde query den yapılacak şekilde örneklendirilmelidir. 
>  Model binding işlemleri hem body den hemde query den yapılacak şekilde yapılandırılmıştır.
 
** Bonus: ** 
- Standart crud işlemlerine ek olarak, listeleme ve sıralama işlevleride eklenmelidir. Örn: /api/products/list?name=abc 

>Projede listeleme işlemi /list? şeklinde bir query ile yapılmaktadır. Controllerda bulunan List metodu bu amaçla kullanılmakta ve controller içerisinde sıralama işlevi de kullanılmaktadır.
