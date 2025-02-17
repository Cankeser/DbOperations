```markdown
# DynamicAdoNet - Dinamik ADO.NET Veri Tabanı İşlemleri

DynamicAdoNet, ADO.NET kullanarak dinamik ve tekrar kullanılabilir şekilde veritabanı işlemleri yapmanıza olanak tanıyan bir yardımcı sınıf yapısıdır. Bu yapı, özellikle **SQL Server** ile çalışırken veri çekme, ekleme, güncelleme ve silme işlemlerini kolaylaştırmak için tasarlanmıştır.

---

## 🎯 Özellikler
- **Dinamik Veri Çekme:** Tablodan tüm sütunları veya sadece belirli sütunları çekebilme.
- **Kolay Veri Ekleme:** Dictionary kullanarak parametrelerle yeni kayıt ekleyebilme.
- **Güncelleme İşlemleri:** ID bazlı güncellemeler yapabilme.
- **Silme İşlemleri:** ID'ye göre kayıt silme.
- **Tekrar Kullanılabilirlik:** Tek bir sınıfla birçok tablo ve işlem için kullanılabilir yapı.
- **Kolay Entegrasyon:** Mevcut Windows Forms projelerine kolayca entegre edilebilir.

---

## 🚀 Kurulum ve Kullanım

### 1. Projeye Ekleme
DynamicAdoNet sınıfını kullanmak için, `DatabaseOperations.cs` dosyasını projenize dahil edin.

### 2. Bağlantı Ayarları
`DatabaseOperations` sınıfındaki `connectionString` değişkenini kendi veritabanınıza göre güncelleyin:

```csharp
string connectionString = "server=localhost;database=KutuphaneDB;Trusted_Connection=true";
```

---

## 📚 Kullanım Detayları

### 1. Tüm Veriyi Çekme
Belirli bir tablodan tüm sütunları ve satırları çekmek için:

```csharp
DatabaseOperations dbOps = new DatabaseOperations();
DataTable allData = dbOps.GetAll("TabloAdi");
```

---

### 2. Belirli Sütunları Çekme
Sadece belirli sütunları çekmek isterseniz:

```csharp
List<string> columns = new List<string> { "Sutun1", "Sutun2" };
DataTable selectedData = dbOps.GetAll("TabloAdi", columns);
```

---

### 3. Veri Ekleme
Yeni bir kayıt eklemek için Dictionary yapısını kullanabilirsiniz:

```csharp
Dictionary<string, object> newRecord = new Dictionary<string, object>()
{
    { "Sutun1", "Deger1" },
    { "Sutun2", 123 }
};
dbOps.Insert("TabloAdi", newRecord);
```

**Örnek Kullanım:**
```csharp
dbOps.Insert("Musteriler", new Dictionary<string, object>()
{
    { "Adi", "Ali" },
    { "Soyadi", "Veli" },
    { "Yas", 30 }
});
```

---

### 4. Veri Güncelleme
Belirli bir ID'ye ait kaydı güncellemek için:

```csharp
Dictionary<string, object> updatedRecord = new Dictionary<string, object>()
{
    { "Sutun1", "YeniDeger" },
    { "Sutun2", 456 }
};
dbOps.Update("TabloAdi", 1, updatedRecord);
```

**Örnek Kullanım:**
```csharp
dbOps.Update("Musteriler", 1, new Dictionary<string, object>()
{
    { "Adi", "Mehmet" },
    { "Yas", 35 }
});
```

---

### 5. Veri Silme
Belirli bir ID'ye ait kaydı silmek için:

```csharp
dbOps.Delete("TabloAdi", 1);
```

**Örnek Kullanım:**
```csharp
dbOps.Delete("Musteriler", 1);
```

---

## 📦 Bağımlılıklar
- **System.Data.SqlClient:** ADO.NET ile SQL Server bağlantısı için gereklidir.
- **System.Windows.Forms:** Mesaj kutuları için kullanılmıştır (isteğe bağlı olarak çıkarılabilir).

## 📌 Sınıf Yapısı
`DatabaseOperations` sınıfı, ADO.NET'in temel bileşenlerini kullanarak şu işlemleri gerçekleştirir:
- **SqlConnection:** Veritabanı bağlantısı için kullanılır.
- **SqlDataAdapter:** Veri çekme işlemlerinde kullanılır.
- **SqlCommand:** Ekleme, güncelleme ve silme işlemleri için kullanılır.
- **MessageBox:** İşlemler sonrası kullanıcıya bilgi vermek amacıyla kullanılmıştır.


## 📌 Notlar
- Bu sınıf yapısı **.NET Framework** projelerinde kullanılmak üzere tasarlanmıştır.
- **.NET Core** projelerinde kullanmak için gerekli `using` kütüphanelerini güncellemeniz gerekebilir.
```
