# Redis
---
## Caching nedir?

Yazýlým süreçlerinde verilere daha hýzlý eriþebilmek adýna bu verilerin bellekte tutulmasýdýr.

### Caching'in Saðladýðý Faydalar

- Caching, verilere hýzlý eriþimi saðlar
- Bu hýzlý eriþim dolayýsýyla performansý önemli ölçüde artýrýr. Veritabaný sorgularý gibi maliyetli iþlemlerde, verilerin önceden cache'e alýnýp bu cache'ten getirilmesi büyük bir performans farký yaratýr.
- Caching, verileri önceden cache'e sakladýðý için ihtiyaç dahilinde ayný verilerin tekrarlý þekilde elde edilme maliyetlerini sunucudan soyutlar ve böylece sunucunun iþ yükünü azaltýr.
- Özellikle çevrimiçi uygulamalarda caching yöntemi, hýzlý eriþimi mümkün kýlar.

### Caching'in Zararlarý

- Veriler bellekte saklandýðý için bellek yükü artar. Bu da performans sorunlarýna yol açabilir.
- Yasa dýþý kullaným açýsýndan kritik olan verilerin cache'lenmesi, hukuki problemlere sebebiyet verebilir.

### Ne Tarz Veriler Cache'lenir?

- Çoðu veri cache'lenebilir ve verinin hacmi önem taþýr.
- Cache'lenecek veriler özenle seçilmelidir.
- Cache'lenecek veriler sýklýkla ve hýzlý bir þekilde eriþilecek veriler olmalýdýr. Örn: sýkça ve sürekli kullanýlan db sorgularý neticesindeki veriler, konfigürasyon verileri, menü bilgileri, yetkiler vs. gibi sürekli ihtiyaç duyulacak verilere birincil öncelik tanýnmalýdýr.
- Resim ve videolar gibi statik bileþenler

### Ne Tarz Veriler Cache'lenemez?

- Sürekli güncellenen veya kiþisel veriler cache'lenmemelidir. Aksi takdirde yanlýþ veya eksik veri getirme durumlarý ortaya çýkarabilir. Geçici veriler için de ayný durum geçerlidir.
- Güvenlik açýsýndan risk teþkil eden veriler de mümkün mertebe cache'lenmemelidir!

### Cache Mekanizmasýnýn Ana Bileþenleri

- Cache Belleði: Verilerin saklandýðý bellek.
- Cache Bellek Yönetimi: Saklanan verilerin yönetildiði alan. Saklanma süresi, silinme sýklýðý, güncellik durumlarý vb.
- Cache Algoritmasý: Verilerin belleðe nasýl eklenip silineceðini belirleyen algoritmadýr.
---
## Caching Türleri

### In-Memory Caching

- Verilerin uygulamanýn çalýþtýðý bilgisayarýn RAM'inde tutulduðu yaklaþýmdýr.

### Distributed Caching

- Verilerin ayrý bir mekanizmada(sanal, fiziksel vb.) cache'lenmesidir.
- Veriler farklý noktalarda tutularak güvenlik düzeyi artýrýlýr.
- Büyük veri setleri için çok uygundur.
- Redis, Memcached, Hazelcast, Apache Ignite, EHCache gibi yazýlýmlar tarafýndan saðlanabilir.
---
## Redis Veri Türleri
- **String:** Metinsel deðerlerle birlikte her tür veriyi saklamakta kullanýlýr. Hatta binary olarak resim, dosya vb. de saklanabilir
- Ýþlevleri;
  - SET | Ekleme | SET NAME berkay
  - GET | Okuma | GET NAME -> "berkay"
  - GETRANGE | Karakter aralýðý okuma | GETRANGE NAME 1 2 -> "er"
  - INCR/INCRBY | Artýrma | INCR SAYI
  - DECR/DECRBY | Azaltma | DECR SAYI
  - APPEND | Üzerine ekleme | APPEND NAME  zaim
	</br></br></br>
	
- **List:** Deðerleri koleksiyonel olarak saklama
- Ýþlevleri;
  - LPUSH | Baþa Veri Ekleme | LPUSH NAMES ahmet mehmet -> (integer) 2
  - LRANGE | Verileri Listeleme | LRANGE NAMES 0-1 -> 1) "ahmet" 2) "mehmet"
  - RPUSH | Baþa Veri Ekleme | RPUSH NAMES mustafa -> (integer) 3
  - LPOP | Soldan(baþtan) Eleman Çýkarma | LPOP NAMES -> "ahmet" (çýkarýlan eleman)
  - RPOP| Saðdan(sondan) Eleman Çýkarma | RPOP NAMES -> "mustafa" (çýkarýlan eleman)
  - LINDEX | Indexe göre datayý getirme | LINDEX NAMES 0 -> "mehmet"
	</br></br></br>
	
- **Set:** unique þekilde veri saklama
- Ýþlevleri;
  - SADD | Ekleme | SADD COLOR red green blue pink -> (integer) 4
  - SREM | Silme | SREM COLOR green -> (integer) 1
  - SISMEMBER | Arama | SISMEMBER COLOR blue -> (integer) 1
  - SINTER | Ýki Set'teki Kesiþimi Getirir | SINTER MAMMALS AQUATICS -> 1) "whale" 2) "dolphin"
  - SCARD | Eleman Sayýsýný Getirir | SCARD COLOR -> (integer) 3
	</br></br></br>

- **Sorted Set:** sýralanmýþ set. Her veriye score adý verilen bir deðer atanýr. Veriler bu deðer kullanýlarak sýralanýp saklanýr
- Ýþlevleri;
  - ZADD | Ekleme | ZADD TEAMS 1 A -> (integer) 1
  - ZRANGE | Getirme | ZRANGE TEAMS 0-1 WITHSCORES -> 1) "A" 2) "1" 3) "B" 4) "2" 5) "C" 6) "3" 
  - ZREM | Silme | ZREM TEAMS A -> (integer) 1
  - ZREVRANK | Ýki Set'teki Kesiþimi Getirir | ZREVRANK TEAMS B -> (integer) 2
	</br></br></br>

- **Hash:** key-value formatýnda veri tutan tür
  - HMSET/HSET | Ekleme | HMSET EMPLOYEES username berkay -> OK
  - HMGET/HGET | Getirme | HMGET EMPLOYEES username -> 1) "berkay"
  - HDEL | Silme | HDEL EMPLOYEES username -> 1
  - HGETALL | Tümünü Getirme | HGETALL EMPLOYEES -> username berkay age 23 (satýr satýr key-value getirir)
	</br></br></br>
	
- **Streams:** Log gibi hareket eden bir veri türüdür. Streams, event'larýn oluþturulduklarý sýrayla kaydedilmelerini ve daha sonra iþletilmelerini saðlar

- **Geospatial Indexes:** Coðrafi koordinatlarýn saklanmasýný saðlayan veri türüdür

