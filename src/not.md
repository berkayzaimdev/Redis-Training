# Redis
---
## Caching nedir?

Yaz�l�m s�re�lerinde verilere daha h�zl� eri�ebilmek ad�na bu verilerin bellekte tutulmas�d�r.

### Caching'in Sa�lad��� Faydalar

- Caching, verilere h�zl� eri�imi sa�lar
- Bu h�zl� eri�im dolay�s�yla performans� �nemli �l��de art�r�r. Veritaban� sorgular� gibi maliyetli i�lemlerde, verilerin �nceden cache'e al�n�p bu cache'ten getirilmesi b�y�k bir performans fark� yarat�r.
- Caching, verileri �nceden cache'e saklad��� i�in ihtiya� dahilinde ayn� verilerin tekrarl� �ekilde elde edilme maliyetlerini sunucudan soyutlar ve b�ylece sunucunun i� y�k�n� azalt�r.
- �zellikle �evrimi�i uygulamalarda caching y�ntemi, h�zl� eri�imi m�mk�n k�lar.

### Caching'in Zararlar�

- Veriler bellekte sakland��� i�in bellek y�k� artar. Bu da performans sorunlar�na yol a�abilir.
- Yasa d��� kullan�m a��s�ndan kritik olan verilerin cache'lenmesi, hukuki problemlere sebebiyet verebilir.

### Ne Tarz Veriler Cache'lenir?

- �o�u veri cache'lenebilir ve verinin hacmi �nem ta��r.
- Cache'lenecek veriler �zenle se�ilmelidir.
- Cache'lenecek veriler s�kl�kla ve h�zl� bir �ekilde eri�ilecek veriler olmal�d�r. �rn: s�k�a ve s�rekli kullan�lan db sorgular� neticesindeki veriler, konfig�rasyon verileri, men� bilgileri, yetkiler vs. gibi s�rekli ihtiya� duyulacak verilere birincil �ncelik tan�nmal�d�r.
- Resim ve videolar gibi statik bile�enler

### Ne Tarz Veriler Cache'lenemez?

- S�rekli g�ncellenen veya ki�isel veriler cache'lenmemelidir. Aksi takdirde yanl�� veya eksik veri getirme durumlar� ortaya ��karabilir. Ge�ici veriler i�in de ayn� durum ge�erlidir.
- G�venlik a��s�ndan risk te�kil eden veriler de m�mk�n mertebe cache'lenmemelidir!

### Cache Mekanizmas�n�n Ana Bile�enleri

- Cache Belle�i: Verilerin sakland��� bellek.
- Cache Bellek Y�netimi: Saklanan verilerin y�netildi�i alan. Saklanma s�resi, silinme s�kl���, g�ncellik durumlar� vb.
- Cache Algoritmas�: Verilerin belle�e nas�l eklenip silinece�ini belirleyen algoritmad�r.
---
## Caching T�rleri

### In-Memory Caching

- Verilerin uygulaman�n �al��t��� bilgisayar�n RAM'inde tutuldu�u yakla��md�r.

### Distributed Caching

- Verilerin ayr� bir mekanizmada(sanal, fiziksel vb.) cache'lenmesidir.
- Veriler farkl� noktalarda tutularak g�venlik d�zeyi art�r�l�r.
- B�y�k veri setleri i�in �ok uygundur.
- Redis, Memcached, Hazelcast, Apache Ignite, EHCache gibi yaz�l�mlar taraf�ndan sa�lanabilir.
---
## Redis Veri T�rleri
- **String:** Metinsel de�erlerle birlikte her t�r veriyi saklamakta kullan�l�r. Hatta binary olarak resim, dosya vb. de saklanabilir
- ��levleri;
  - SET | Ekleme | SET NAME berkay
  - GET | Okuma | GET NAME -> "berkay"
  - GETRANGE | Karakter aral��� okuma | GETRANGE NAME 1 2 -> "er"
  - INCR/INCRBY | Art�rma | INCR SAYI
  - DECR/DECRBY | Azaltma | DECR SAYI
  - APPEND | �zerine ekleme | APPEND NAME  zaim
	</br></br></br>
	
- **List:** De�erleri koleksiyonel olarak saklama
- ��levleri;
  - LPUSH | Ba�a Veri Ekleme | LPUSH NAMES ahmet mehmet -> (integer) 2
  - LRANGE | Verileri Listeleme | LRANGE NAMES 0-1 -> 1) "ahmet" 2) "mehmet"
  - RPUSH | Ba�a Veri Ekleme | RPUSH NAMES mustafa -> (integer) 3
  - LPOP | Soldan(ba�tan) Eleman ��karma | LPOP NAMES -> "ahmet" (��kar�lan eleman)
  - RPOP| Sa�dan(sondan) Eleman ��karma | RPOP NAMES -> "mustafa" (��kar�lan eleman)
  - LINDEX | Indexe g�re datay� getirme | LINDEX NAMES 0 -> "mehmet"
	</br></br></br>
	
- **Set:** unique �ekilde veri saklama
- ��levleri;
  - SADD | Ekleme | SADD COLOR red green blue pink -> (integer) 4
  - SREM | Silme | SREM COLOR green -> (integer) 1
  - SISMEMBER | Arama | SISMEMBER COLOR blue -> (integer) 1
  - SINTER | �ki Set'teki Kesi�imi Getirir | SINTER MAMMALS AQUATICS -> 1) "whale" 2) "dolphin"
  - SCARD | Eleman Say�s�n� Getirir | SCARD COLOR -> (integer) 3
	</br></br></br>

- **Sorted Set:** s�ralanm�� set. Her veriye score ad� verilen bir de�er atan�r. Veriler bu de�er kullan�larak s�ralan�p saklan�r
- ��levleri;
  - ZADD | Ekleme | ZADD TEAMS 1 A -> (integer) 1
  - ZRANGE | Getirme | ZRANGE TEAMS 0-1 WITHSCORES -> 1) "A" 2) "1" 3) "B" 4) "2" 5) "C" 6) "3" 
  - ZREM | Silme | ZREM TEAMS A -> (integer) 1
  - ZREVRANK | �ki Set'teki Kesi�imi Getirir | ZREVRANK TEAMS B -> (integer) 2
	</br></br></br>

- **Hash:** key-value format�nda veri tutan t�r
  - HMSET/HSET | Ekleme | HMSET EMPLOYEES username berkay -> OK
  - HMGET/HGET | Getirme | HMGET EMPLOYEES username -> 1) "berkay"
  - HDEL | Silme | HDEL EMPLOYEES username -> 1
  - HGETALL | T�m�n� Getirme | HGETALL EMPLOYEES -> username berkay age 23 (sat�r sat�r key-value getirir)
	</br></br></br>
	
- **Streams:** Log gibi hareket eden bir veri t�r�d�r. Streams, event'lar�n olu�turulduklar� s�rayla kaydedilmelerini ve daha sonra i�letilmelerini sa�lar

- **Geospatial Indexes:** Co�rafi koordinatlar�n saklanmas�n� sa�layan veri t�r�d�r

