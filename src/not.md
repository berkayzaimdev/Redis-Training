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
1. İki pencerede de Redis CLI'a bağlanılır. ``` (docker exec -il 8c60 redis-cli -raw) ``` Bu pencerelerden biri Publisher, diğeri Consumer işlevi görecektir. 
1. Öncelikle Consumer görevi verilecek olanda ```subscribe ...channel``` talimatını vererek gelecek olan mesajlara abone oluruz.
1. Daha sonra Publisher görevi verilecek olanda ```publish ...channel message``` talimatını vererek ilgili kanala mesaj göndeririz.

### Redis Insight

1. Redis Insight'te sol menüdeki Pub/Sub sekmesi açılır.
1. Buradan tüm kanallardaki mesaj akışını takip edebilir ve yönetebiliriz.