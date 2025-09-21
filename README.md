# 🚗 CQRS Rent A Car - AI Destekli Araç Kiralama Sistemi 🏎️

## 📋 Proje Tanıtımı

Bu proje, .NET Core 9.0 teknolojisi kullanılarak geliştirilmiş, yapay zeka destekli modern bir araç kiralama platformudur. Sistem, CQRS mimari deseni ile tasarlanmış ve Entity Framework Core ile güçlendirilmiştir.

## 🛠️ Teknoloji Altyapısı

**Sunucu Tarafı:**
- .NET Core 9.0 Framework
- ASP.NET Core MVC
- Entity Framework Core 9.0.8

**Mimari Yaklaşımlar:**
- CQRS (Command Query Responsibility Segregation)
- Repository Design Pattern
- Dependency Injection Container

**E-posta Sistemi:**
- Gmail SMTP Protokolü
- HTML E-posta Şablonları

**Yapay Zeka Entegrasyonu:**
- OpenAI ChatGPT API (RapidAPI üzerinden)

**Dış Servis Entegrasyonları:**
- RapidAPI platformu üzerinden
- Yakıt Fiyat API'si
- Havalimanı Bilgi API'si
- Mesafe Hesaplama API'si

**Kullanıcı Arayüzü:**
- Razor Pages ve ViewComponents
- Bootstrap 5.0.0
- AdminLTE 3.1.0
- jQuery, Font Awesome, WOW.js

## 🤖 Yapay Zeka Özellikleri

### 📧 Akıllı E-posta Sistemi
- Kullanıcılar anasayfa üzerinden iletişim formu doldurur
- Sistem, gelen mesajı veritabanına kaydeder
- ChatGPT API'si devreye girerek mesajı analiz eder
- Kullanıcının dilini otomatik tespit eder
- Aynı dilde profesyonel bir yanıt oluşturur
- Yanıt otomatik olarak kullanıcıya e-posta ile gönderilir

### 🎯 Araç Öneri Sistemi
- Kullanıcılar chatbot üzerinden araç kiralama isteklerini belirtir
- AI sistemi kullanıcı ihtiyaçlarını değerlendirir
- Bütçe, konfor ve güvenlik kriterlerine göre araç önerileri sunar
- Gerçek zamanlı müşteri desteği sağlar
- Çok dilli destek ile uluslararası kullanıcılara hizmet verir

## 🚙 Araç Rezervasyon Süreci

Kullanıcılar aşağıdaki adımları takip ederek araç kiralayabilir:

**Araç Seçimi:**
- Marka bazlı filtreleme yapabilir
- Havalimanı konumuna göre araç listesini görüntüleyebilir

**Maliyet Hesaplama:**
- Başlangıç ve varış noktalarını belirler
- Yakıt türünü seçer
- Sistem otomatik mesafe hesaplaması yapar
- Güncel yakıt fiyatları API'den çekilir
- Toplam maliyet, yakıt tüketimi ve varış süresi hesaplanır

**Rezervasyon:**
- Alış ve teslim tarihlerini belirler
- Rezervasyon sistemine kayıt yapar

> **Not:** Yakıt tüketimi ve maliyet hesaplamaları yaklaşık değerlerdir.

## 👨‍💼 Yönetim Paneli

Admin paneli üzerinden aşağıdaki işlemler gerçekleştirilebilir:

**Ana Kontrol Paneli:**
- Sistem istatistikleri
- Araç ve rezervasyon özetleri
- Performans metrikleri

**İçerik Yönetimi:**
- Araç kataloğu yönetimi
- AI destekli araç öneri sistemi
- Rezervasyon takibi
- Referans ve personel bilgileri
- Slider ve hizmet içerikleri

**Teknik Özellikler:**
- Tam CRUD işlemleri
- Görsel önizleme sistemi
- Gelişmiş filtreleme seçenekleri

## 🌍 API Entegrasyonları

**Yakıt Fiyat Servisi:**
- Endpoint: `https://gas-price.p.rapidapi.com/europeanCountries`
- Avrupa ülkeleri için güncel yakıt fiyatları

**Havalimanı Bilgi Servisi:**
- Endpoint: `https://airports15.p.rapidapi.com/airports`
- Dünya genelinde havalimanı bilgileri

**Mesafe Hesaplama Servisi:**
- Endpoint: `https://airport-distance-api-apiverve.p.rapidapi.com/v1/iata`
- Havalimanılar arası mesafe hesaplaması

**ChatGPT AI Servisi:**
- Endpoint: `https://chatgpt-42.p.rapidapi.com/chatgpt`
- Yapay zeka destekli müşteri hizmetleri

## 🏛️ Sistem Mimarisi

### CQRS (Command Query Responsibility Segregation) Uygulaması

**Komut İşlemleri (Veri Değiştirme):**
- Yeni kayıt oluşturma komutları
- Mevcut veri güncelleme komutları
- Veri silme komutları

**Sorgu İşlemleri (Veri Okuma):**
- Liste getirme sorguları
- Tekil veri getirme sorguları
- Özel durum sorguları (kiralık araç kontrolü gibi)

**İşleyici Yapısı:**
- Komut işleyicileri: Veri değiştirme operasyonlarını yönetir
- Sorgu işleyicileri: Veri okuma operasyonlarını yönetir
- Sonuç nesneleri: Tip güvenli veri aktarımı sağlar

**Mimari Avantajları:**
- Kod modülerliği ve test edilebilirlik
- Komut ve sorgu işlemlerinin ayrıştırılması
- Okuma ve yazma operasyonlarının bağımsızlaştırılması

### ViewComponent Yapısı
- Dinamik içerik ve önizleme gerektiren alanlarda kullanılır
- Kod tekrarını azaltır
- Yeniden kullanılabilir bileşenler oluşturur

## 📨 E-posta Otomasyonu
- Gmail SMTP protokolü üzerinden gönderim
- AI tabanlı otomatik yanıt sistemi
- Çoklu dil desteği
- HTML formatında e-posta şablonları

## ⚡ Temel Sistem Özellikleri

**Ana Sayfa Bileşenleri:**
- Öne çıkan araçlar
- Şirket hakkında bilgiler
- Sunulan hizmetler
- Popüler araç modelleri
- Ekip tanıtımı
- Müşteri değerlendirmeleri
- İletişim formu

**Araç Listeleme:**
- Marka bazlı filtreleme
- Havalimanı konumu filtreleme
- Yakıt tüketimi ve maliyet tahmini

**Yönetim Paneli:**
- Dashboard ve istatistikler
- Araç ve rezervasyon yönetimi
- Referans ve personel yönetimi
- Slider ve hizmet içerik yönetimi
- AI destekli araç öneri sistemi

**Görsel Özellikler:**
- Resim URL'si girildiğinde anlık önizleme
- Responsive tasarım

### Ana Özellikler

#### 1. **Araç Kiralama Süreci**
- Araç filtreleme: tip, fiyat, şanzıman, yakıt türü
- Tarih seçimi: alış ve teslim tarihleri
- Konum bazlı: havalimanı bazlı kiralama
- Rezervasyon kontrolü: çakışan rezervasyonları önleme
- Mesafe hesaplama: havalimanılar arası mesafe

#### 2. **AI Destekli Chatbot**
- Otomatik dil algılama: Türkçe/İngilizce
- Araç kiralama danışmanlığı: AI destekli öneriler
- Gerçek zamanlı destek: 7/24 müşteri hizmetleri
- E-posta entegrasyonu: otomatik yanıt gönderimi
- Çok dilli yanıtlar: kullanıcı diline göre yanıt

#### 3. **Yönetim Paneli**
- Araç yönetimi: CRUD işlemleri
- Rezervasyon yönetimi: kiralama takibi
- İletişim mesajları: mesaj yönetimi ve yanıt
- İçerik yönetimi: slider, özellik, hizmet yönetimi

  ## 🖼️ Proje Görselleri
  ------------------------
 - UI
<img width="1895" height="949" alt="UISlider" src="https://github.com/user-attachments/assets/bda7d720-8389-45ec-bcd6-d8b8ced8f917" />

<img width="1890" height="949" alt="UIFeatures" src="https://github.com/user-attachments/assets/4ce8d604-f39d-47ee-8430-bb79951ac4a0" />

<img width="1891" height="944" alt="UIServices" src="https://github.com/user-attachments/assets/771125e4-2047-4f11-8c93-aafca9d7babe" />

<img width="1892" height="946" alt="UIEmployees" src="https://github.com/user-attachments/assets/48b2f921-7e26-49b1-b9aa-dbb9829e78c2" />

<img width="1889" height="947" alt="UIStatsPoplarCars" src="https://github.com/user-attachments/assets/66a16983-0b2b-4ef6-a4da-cd02f35eec1d" />

<img width="1889" height="951" alt="UITestimonial" src="https://github.com/user-attachments/assets/9c371939-744b-4067-8b2e-ea0726a6075b" />

<img width="1887" height="942" alt="UIContact" src="https://github.com/user-attachments/assets/61b853b7-b27b-43bf-ac4e-dce634d22a1c" />

<img width="1886" height="951" alt="UICarRental" src="https://github.com/user-attachments/assets/db5f5858-3733-4b05-a0eb-502805ecd966" />

<img width="1906" height="953" alt="UICarRental2" src="https://github.com/user-attachments/assets/513bfe9a-838b-4a35-bc43-700fd06e1ed8" />

<img width="1898" height="949" alt="UIRentalPopUp" src="https://github.com/user-attachments/assets/f48d0e52-e1fa-4311-9a44-1133222a5031" />

- ADMIN
<img width="1906" height="948" alt="GasPrice" src="https://github.com/user-attachments/assets/adf58384-08dc-46dc-ad42-e09dc4167e2e" />

<img width="1903" height="942" alt="Employees" src="https://github.com/user-attachments/assets/4c2c8871-64ff-495a-846d-7645dd064d9d" />

<img width="1910" height="944" alt="Cars" src="https://github.com/user-attachments/assets/212a9912-34f1-4a30-9aa4-9ae04e8c814d" />

<img width="1913" height="948" alt="CarsUpdate" src="https://github.com/user-attachments/assets/cf3dda76-c99c-4a97-b62a-c71f89c9c886" />

<img width="1912" height="955" alt="RCars" src="https://github.com/user-attachments/assets/84170392-1f73-4f41-9ec3-0c2194ef9ada" />

<img width="1909" height="956" alt="Services" src="https://github.com/user-attachments/assets/ad893100-0ed3-42c4-b573-08649169e5a7" />

<img width="1902" height="953" alt="ContactMessage" src="https://github.com/user-attachments/assets/ac0dde68-0a2f-4c39-af20-ee092da9fa79" />

<img width="1905" height="951" alt="ContactDetail" src="https://github.com/user-attachments/assets/b6eac87f-a76d-4895-ab6f-ac88bba64237" />

<img width="1579" height="870" alt="gmail" src="https://github.com/user-attachments/assets/be895d12-e7f9-4d1b-9938-c8117af593b6" />

<img width="1905" height="948" alt="Slider" src="https://github.com/user-attachments/assets/c62f4490-c112-4276-a7b3-18a8dd37521f" />
