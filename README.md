
# Cafe App

Cafe App, bir restoran veya kafe yönetim sistemi olarak geliştirdiğim kapsamlı bir uygulamadır. Bu uygulama, işletme sahiplerinin, yöneticilerin ve personelin günlük operasyonları etkin bir şekilde yönetmesine yardımcı olur. Uygulama beş ana panel ve sayısız özellikten oluşur ve tüm paneller birbiriyle entegre çalışır.

### QR Kod Sistemi
Uygulamamın can alıcı özellerinden birisi de içerisinde benzersiz bir QR sistemi bulunmasıdır. Bu QR sistemi ile masalarda kendilerine ait QR bulunmaktadır. Bu QR'ın amacı sadece menü ve ürünleri görüntülemek değil, garsonu çağırmadan ve giriş yapmadan sadece QR'ı okutarak sipariş vermek ve ödeme yapmaktır.

## Özellikler

### Yönetilebilir Bölümler
Cafe App, aşağıdaki kısımların yönetilmesini sağlar:
- **İndirim**: İndirim kampanyalarını ekleme, çıkarma ve düzenleme.
- **Kategori**: Ürün ve menü kategorilerini yönetme.
- **Malzeme**: Kullanılan malzemelerin takibi ve yönetimi.
- **Masa**: Masa düzenini, durumlarını, QR kodlarını ve rezervasyonları yönetme.
- **Menü**: Menü öğelerini ekleme, çıkarma ve düzenleme.
- **Müşteri**: Müşteri bilgilerini yönetme.
- **Personel**: Personel bilgilerini ve görev dağılımını yönetme.
- **Rezervasyon**: Rezervasyon sistemini yönetme.
- **Rol**: Personel rolleri ve yetkilendirmelerini yönetme.
- **Sipariş**: Sipariş takibi ve yönetimi.
- **Stok**: Stok yönetimi, girdi - çıktı ekleme ve malzeme takibi.
- **Tedarikçi**: Tedarikçi bilgileri yönetimi.
- **Ürün**: Ürün ekleme, çıkarma ve düzenleme.

### Paneller
- **Admin Paneli**: Yönetici kontrolleri ve sistem ayarları.
- **Garson Paneli**: Garsonların sipariş alımı ve masalarla ilgili işlemleri.
- **Mutfak Paneli**: Mutfağa gelen ve hazırlanan siparişlerin takibi.
- **Kasa Paneli**: Ödeme işlemleri ve finansal yönetim.
- **Müşteri Paneli**: Müşteri etkileşimleri ve sipariş durumu.



## Teknik Özellikler

- **Geliştirme Ortamı**: ASP.NET Core MVC
- **Veritabanı**: MySQL
- **Frontend**: Bootstrap 5, HTML, CSS ve JS

### Kullanılan Teknolojiler ve Frameworkler

- **Session Memory Cache**: Oturum ve bellek yönetimi.
- **Fluent Validation / Remote Validation**: Veri doğrulama.
- **SweetAlert2**: Alert ve modal dialog kutuları. 
- **imask.js**: Input mask'leri oluşturma.
- **QRCoder**: QR kod oluşturma.
- **X.PagedList**: Sayfalama işlemleri.
- **Ajax**: Dinamik içerik güncellemeleri.
- **Identity**: Kullanıcı kimlik doğrulama ve yetkilendirme.
