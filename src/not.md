# Redis
---
## Caching nedir?

Yazılım süreçlerinde verilere daha hızlı erişebilmek adına bu verilerin bellekte tutulmasıdır.

### Caching'in Sağladığı Faydalar

- Caching, verilere hızlı erişimi sağlar
- Bu hızlı erişim dolayısıyla performansı önemli ölçüde artırır. Veritabanı sorguları gibi maliyetli işlemlerde, verilerin önceden cache'e alınıp bu cache'ten getirilmesi büyük bir performans farkı yaratır.
- Caching, verileri önceden cache'e sakladığı için ihtiyaç dahilinde aynı verilerin tekrarlı şekilde elde edilme maliyetlerini sunucudan soyutlar ve böylece sunucunun iş yükünü azaltır.
- Özellikle çevrimiçi uygulamalarda caching yöntemi, hızlı erişimi mümkün kılar.

### Caching'in Zararları

- Veriler bellekte saklandığı için bellek yükü artar. Bu da performans sorunlarına yol açabilir.
- Yasa dışı kullanım açısından kritik olan verilerin cache'lenmesi, hukuki problemlere sebebiyet verebilir.

### Ne Tarz Veriler Cache'lenir?

- Çoğu veri cache'lenebilir ve verinin hacmi önem taşır.
- Cache'lenecek veriler özenle seçilmelidir.
- Cache'lenecek veriler sıklıkla ve hızlı bir şekilde erişilecek veriler olmalıdır. Örn: sıkça ve sürekli kullanılan db sorguları neticesindeki veriler, konfigürasyon verileri, menü bilgileri, yetkiler vs. gibi sürekli ihtiyaç duyulacak verilere birincil öncelik tanınmalıdır.
- Resim ve videolar gibi statik bileşenler

### Ne Tarz Veriler Cache'lenemez?

- Sürekli güncellenen veya kişisel veriler cache'lenmemelidir. Aksi takdirde yanlış veya eksik veri getirme durumları ortaya çıkarabilir. Geçici veriler için de aynı durum geçerlidir.
- Güvenlik açısından risk teşkil eden veriler de mümkün mertebe cache'lenmemelidir!

### Cache Mekanizmasının Ana Bileşenleri

- Cache Belleği: Verilerin saklandığı bellek.
- Cache Bellek Yönetimi: Saklanan verilerin yönetildiği alan. Saklanma süresi, silinme sıklığı, güncellik durumları vb.
- Cache Algoritması: Verilerin belleğe nasıl eklenip silineceğini belirleyen algoritmadır.
---
## Caching Türleri

### In-Memory Caching

- Verilerin uygulamanın çalıştığı bilgisayarın RAM'inde tutulduğu yaklaşımdır.

### Distributed Caching

- Verilerin ayrı bir mekanizmada(sanal, fiziksel vb.) cache'lenmesidir.
- Veriler farklı noktalarda tutularak güvenlik düzeyi artırılır.
- Büyük veri setleri için çok uygundur.
- Redis, Memcached, Hazelcast, Apache Ignite, EHCache gibi yazılımlar tarafından sağlanabilir.
---
## Redis Veri Türleri
- **String:** Metinsel değerlerle birlikte her tür veriyi saklamakta kullanılır. Hatta binary olarak resim, dosya vb. de saklanabilir
  - SET | Ekleme | SET NAME berkay
  - GET | Okuma | GET NAME -> "berkay"
  - GETRANGE | Karakter aralığı okuma | GETRANGE NAME 1 2 -> "er"
  - INCR/INCRBY | Artırma | INCR SAYI
  - DECR/DECRBY | Azaltma | DECR SAYI
  - APPEND | Üzerine ekleme | APPEND NAME  zaim
	</br></br></br>
	
- **List:** Değerleri koleksiyonel olarak saklama
  - LPUSH | Başa Veri Ekleme | LPUSH NAMES ahmet mehmet -> (integer) 2
  - LRANGE | Verileri Listeleme | LRANGE NAMES 0-1 -> 1) "ahmet" 2) "mehmet"
  - RPUSH | Başa Veri Ekleme | RPUSH NAMES mustafa -> (integer) 3
  - LPOP | Soldan(baştan) Eleman Çıkarma | LPOP NAMES -> "ahmet" (çıkarılan eleman)
  - RPOP| Sağdan(sondan) Eleman Çıkarma | RPOP NAMES -> "mustafa" (çıkarılan eleman)
  - LINDEX | Indexe göre datayı getirme | LINDEX NAMES 0 -> "mehmet"
	</br></br></br>
	
- **Set:** unique şekilde veri saklama
  - SADD | Ekleme | SADD COLOR red green blue pink -> (integer) 4
  - SREM | Silme | SREM COLOR green -> (integer) 1
  - SISMEMBER | Arama | SISMEMBER COLOR blue -> (integer) 1
  - SINTER | ıki Set'teki Kesişimi Getirir | SINTER MAMMALS AQUATICS -> 1) "whale" 2) "dolphin"
  - SCARD | Eleman Sayısını Getirir | SCARD COLOR -> (integer) 3
	</br></br></br>

- **Sorted Set:** sıralanmış set. Her veriye score adı verilen bir değer atanır. Veriler bu değer kullanılarak sıralanıp saklanır
  - ZADD | Ekleme | ZADD TEAMS 1 A -> (integer) 1
  - ZRANGE | Getirme | ZRANGE TEAMS 0-1 WITHSCORES -> 1) "A" 2) "1" 3) "B" 4) "2" 5) "C" 6) "3" 
  - ZREM | Silme | ZREM TEAMS A -> (integer) 1
  - ZREVRANK | ıki Set'teki Kesişimi Getirir | ZREVRANK TEAMS B -> (integer) 2
	</br></br></br>

- **Hash:** key-value formatında veri tutan tür
  - HMSET/HSET | Ekleme | HMSET EMPLOYEES username berkay -> OK
  - HMGET/HGET | Getirme | HMGET EMPLOYEES username -> 1) "berkay"
  - HDEL | Silme | HDEL EMPLOYEES username -> 1
  - HGETALL | Tümünü Getirme | HGETALL EMPLOYEES -> username berkay age 23 (satır satır key-value getirir)
	</br></br></br>
	
- **Streams:** Log gibi hareket eden bir veri türüdür. Streams, event'ların oluşturuldukları sırayla kaydedilmelerini ve daha sonra işletilmelerini sağlar

- **Geospatial Indexes:** Coğrafi koordinatların saklanmasını sağlayan veri türüdür
---
## Redis Pub/Sub Özelliği

### Redis CLI

1. Powershell'den iki pencere açılır.
1. İki pencerede de Redis CLI'a bağlanılır. ``` docker exec -il 8c60 redis-cli -raw ``` Bu pencerelerden biri Publisher, diğeri Consumer işlevi görecektir. 
1. Öncelikle Consumer görevi verilecek olanda ```subscribe ...channel``` talimatını vererek gelecek olan mesajlara abone oluruz.
1. Daha sonra Publisher görevi verilecek olanda ```publish ...channel message``` talimatını vererek ilgili kanala mesaj göndeririz.

### Redis Insight

1. Redis Insight'te sol menüdeki Pub/Sub sekmesi açılır.
1. Buradan tüm kanallardaki mesaj akışını takip edebilir ve yönetebiliriz.
---
## Redis Replication

### Replication nedir?

- Sunucudaki verilerin güvencesini sağlayabilmek ve bir kopyasını saklamak için kullanılır. 
- Veri kaybı gibi durumlara karşı direnç gösteren bir özelliktir.
- Sunucudaki verilerin güvencesini sağlayabilmek ve bir kopyasını saklamak için kullanılır.
- Performans ve maliyette zaafiyet oluşturabilir fakat karşılığında yüksek güvenlik sağlar.

### Replication'da Master-Slave bağlantısı
- master: replike edilen ana sunucu, slave: replike eden yedek sunucu
- master ve slave arasında mikro düzeyde kopmalar olabilir. Bunu önlemek için Redis sürekli bu bağlantıyı yeniden kurmak için çalışacaktır.
- Böyle bir kesinti halinde slave sunucu sorumluluğu devralıp master'ın yerine geçecektir.
- Bir master'ın birden fazla slave'i olabilir. Slave sayısı ve veri güvenliği doğru orantılıdır.
- Slave'in de slave'i olabilir. Derecelendirmeli bir şekilde veri replikasyonu sağlayabiliriz. Lakin bu yöntem pek tercih edilmez.
- Slave master'den etkilenirken, master slave'den etkilenmez. Slave sunuculara readonly diyebiliriz, bu sayede bu sunucuları test ortamlarında etkin bir şekilde değerlendirilebiliriz.

### Replication'ın Uygulanması

1. master ve slave sunucuları belirlenir. Bu sunucular farklı portlara sahip olmalıdır ve isimleri de özelliklerine göre seçilmelidir.
1. Bu sunucular Redis CLI üzerinden ``` docker run -p 1453:6379 --name redis-master -d redis ``` ve ``` docker run -p 1461:6379 --name redis-slave -d redis ``` komutlarıyla ayağa kaldırılır.
1. Bu iki sunucu arasındaki bağlantıyı kurmak için master sunucunun IP'sinden faydalanılır. Bu IP, ``` docker inspect -f ''{{.NetworkSettings.IPAddress}}'' redis-master ``` komutuyla elde edilebilir.
1. Elde ettiğimiz master IP'yi kullanarak ``` docker -it redis-slave redis-cli slaveof 172.17.0.2 6379 ``` komutu ile replikasyonu başlatırız.

---
## Redis Sentinel

### Redis Sentinel nedir?

- Redis sunucusu, kapsamlı ve büyük projelerde gerçekleşen yoğun işlemler sonucu kesinti yaşayabilir. Ölçeklendirme yapsak dahi bu duruma büyük oranda direnç gösterilemeyebilir.
- Redis Sentinel, Redis'in sürdürülebilirliğini ve kesintisiz hizmeti sağlayarak bu soruna çözüm getirir. **high availability** sağlar.
- Redis Sentinel servisi ile farklı bir sunucu üzerinden Redis çalışmalarına devam edilebilir ve kesintisiz hizmet sağlanabilir.
- Örneğin;
  - Redis sunucusunun arızalanması durumunda 
  - Bakım ve güncelleme süreçlerinde 
  - Yedekleme ve geri yükleme durumlarında,
  - Yüksek trafik durumlarında, Redis Sentinel efektif bir şekilde kullanılır.
	
- Redis Sentinel, replikasyon davranışını barındırır ve otomatik olarak failover işlemlerini gerçekleştirerek verileri farklı bir sunucuya geçirir ve hizmeti devam ettirir.
- Redis Sentinel; master, slave, sentinel ve failover yapılarını içerir.

- **Master**
  - Redis veritabanının ana sunucusudur. Aktif olan sunucudur ve slave'lerden daha öncelikli bir konumdadır.
  - Sentinel, master sunucuyu sürekli takip eder ve sağlıklı olup olmadığını kontrol eder.
  - Sunucuda bir problem saptanması halinde Sentinel, başka bir yedek Redis sunucusunu otomatik olarak master olarak seçecektir.
	
- **Slave**
  - master sunucunun yedeklerini ifade eder. Verilerini master'dan replike eder ve readonly bir yapıya sahiptir.
  - Redis Sentinel yapılanmasının uygulandığı bir mimaride herhangi bir t zamanda "master" sunucu yalnızca bir tane olurken slave ise birden fazla olabilir.
	
- **Sentinel**
  - Redis veritabanının sağlığını izleyen ve otomatik failover işlemlerini gerçekleştiren bir yönetim sistemidir.
  - Sentinel sunucusu, Redis Sentinel yapılanmasının merkezi bileşenleridir.
  - Sentinel, master'ın hangi sunucu olduğunu bilecektir. Biz Redis'i kullandığımız durumda önce Sentinel'a en güncel yani master olan sunucuyu soracak, ardından elde ettiğimiz sunucu bilgisine göre bağlantımızı kurup çalışmamızı gerçekleştireceğiz.
  - Sentinel ayrıca tüm slave sunucular hakkında bilgi toplamakta ve aralarından bir master seçmektedir.
	
- **Failover**
  - master sunucunun arızalanması halinde Sentinel'ın yeni master atamasının terminolojik ifadesidir.
  - Sentinel sunucusu, failover işlemi gerçekleştirirken yeni master'ın IP adresini diğer sunuculara ileterek tüm sunucularının senkronize olmasını sağlar.
  
### Redis Sentinel nasıl kullanılır?

1. ``` docker network create redis-network ``` komutu ile docker network oluşturulur.
1. ``` docker run -p 1453:6379 --name redis-master -p 6379:6379 --network redis-network redis redis-server ``` komutu ile master sunucu oluşturulur.
1. ``` docker run -p 1453:6379 --name redis-slave1 -p 6380:6379 --network redis-network redis redis-server --slaveof redis-master 6379 ``` </br>
 ``` docker run -p 1453:6379 --name redis-slave2 -p 6381:6379 --network redis-network redis redis-server --slaveof redis-master 6379 ``` </br>
 ``` docker run -p 1453:6379 --name redis-slave3 -p 6382:6379 --network redis-network redis redis-server --slaveof redis-master 6379 ``` komutları ile slave sunucular oluşturulur.
1. ``` docker run -d --name redis-sentinel --network redis-network -v <conf-path>:<conf-path2> redis redis-sentinel <conf-path2> ``` komutu ile sentinel.conf yapılandırması yüklenerek container ayağa kaldırılır

### sentinel.conf dosyası

``` sentinel monitor mymaster <master-address> <master-port> <sentinel-count> ``` sentinel tarafından izlenecek master sunucusunu ve sentinel sayısı belirlenir </br>
``` sentinel down-after-milliseconds mymaster 5000 ``` master'a 5 saniye boyunca erişilemezse failover işlemini başlatılır </br>
``` sentinel failover-timeout mymaster 1000 ``` sentinel'ın failover sürecini gerçekleştirirkenki timeout süresi </br>
``` sentinel parallel-syncs mymaster <slave-count> ``` sentinel'ın paralel olarak yedekleme yapacağı slave sayısı belirlenir </br>