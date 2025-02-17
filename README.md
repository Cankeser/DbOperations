```markdown
# DynamicAdoNet - Dinamik ADO.NET Veri Tabanı İşlemleri / Dynamic ADO.NET Database Operations

DynamicAdoNet, ADO.NET kullanarak dinamik ve tekrar kullanılabilir şekilde veritabanı işlemleri yapmanıza olanak tanıyan bir yardımcı sınıf yapısıdır. Bu yapı, özellikle **SQL Server** ile çalışırken veri çekme, ekleme, güncelleme ve silme işlemlerini kolaylaştırmak için tasarlanmıştır.

DynamicAdoNet is a helper class structure that allows you to perform database operations dynamically and reusable using ADO.NET. This structure is designed to facilitate data retrieval, insertion, updating, and deletion, especially when working with **SQL Server**.

---

## 🎯 Özellikler / Features
- **Dinamik Veri Çekme:** Tablodan tüm sütunları veya sadece belirli sütunları çekebilme.  
- **Dynamic Data Retrieval:** Fetch all columns or only specific columns from a table.  

- **Kolay Veri Ekleme:** Dictionary kullanarak parametrelerle yeni kayıt ekleyebilme.  
- **Easy Data Insertion:** Add new records using parameters with Dictionary.

- **Güncelleme İşlemleri:** ID bazlı güncellemeler yapabilme.  
- **Update Operations:** Perform updates based on ID.

- **Silme İşlemleri:** ID'ye göre kayıt silme.  
- **Delete Operations:** Delete records by ID.

- **Tekrar Kullanılabilirlik:** Tek bir sınıfla birçok tablo ve işlem için kullanılabilir yapı.  
- **Reusability:** Usable structure for multiple tables and operations with a single class.

- **Kolay Entegrasyon:** Mevcut Windows Forms projelerine kolayca entegre edilebilir.  
- **Easy Integration:** Easily integrable into existing Windows Forms projects.

---

## 🚀 Kurulum ve Kullanım / Installation and Usage

### 1. Projeye Ekleme / Adding to Project
DynamicAdoNet sınıfını kullanmak için, `DatabaseOperations.cs` dosyasını projenize dahil edin.  
To use the DynamicAdoNet class, include the `DatabaseOperations.cs` file in your project.

### 2. Bağlantı Ayarları / Connection Settings
`DatabaseOperations` sınıfındaki `connectionString` değişkenini kendi veritabanınıza göre güncelleyin:  
Update the `connectionString` variable in the `DatabaseOperations` class according to your database:

```csharp
string connectionString = "server=localhost;database=KutuphaneDB;Trusted_Connection=true";
```

---

## 📚 Kullanım Detayları / Usage Details

### 1. Tüm Veriyi Çekme / Get All Data
Belirli bir tablodan tüm sütunları ve satırları çekmek için:  
To fetch all columns and rows from a specific table:

```csharp
DatabaseOperations dbOps = new DatabaseOperations();
DataTable allData = dbOps.GetAll("TabloAdi");
// DataTable allData = dbOps.GetAll("TableName");
```

---

### 2. Belirli Sütunları Çekme / Get Specific Columns
Sadece belirli sütunları çekmek isterseniz:  
If you want to fetch only specific columns:

```csharp
List<string> columns = new List<string> { "Sutun1", "Sutun2" };
// List<string> columns = new List<string> { "Column1", "Column2" };
DataTable selectedData = dbOps.GetAll("TabloAdi", columns);
// DataTable selectedData = dbOps.GetAll("TableName", columns);
```

---

### 3. Veri Ekleme / Insert Data
Yeni bir kayıt eklemek için Dictionary yapısını kullanabilirsiniz:  
You can use Dictionary structure to add a new record:

```csharp
Dictionary<string, object> newRecord = new Dictionary<string, object>()
{
    { "Sutun1", "Deger1" },
    { "Sutun2", 123 }
};
// Dictionary<string, object> newRecord = new Dictionary<string, object>()
// {
//     { "Column1", "Value1" },
//     { "Column2", 123 }
// };
dbOps.Insert("TabloAdi", newRecord);
// dbOps.Insert("TableName", newRecord);
```

**Örnek Kullanım / Example Usage:**
```csharp
dbOps.Insert("Musteriler", new Dictionary<string, object>()
{
    { "Adi", "Ali" },
    { "Soyadi", "Veli" },
    { "Yas", 30 }
});
```

---

### 4. Veri Güncelleme / Update Data
Belirli bir ID'ye ait kaydı güncellemek için:  
To update a record with a specific ID:

```csharp
Dictionary<string, object> updatedRecord = new Dictionary<string, object>()
{
    { "Sutun1", "YeniDeger" },
    { "Sutun2", 456 }
};
// Dictionary<string, object> updatedRecord = new Dictionary<string, object>()
// {
//     { "Column1", "NewValue" },
//     { "Column2", 456 }
// };
dbOps.Update("TabloAdi", 1, updatedRecord);
// dbOps.Update("TableName", 1, updatedRecord);
```

**Örnek Kullanım / Example Usage:**
```csharp
dbOps.Update("Musteriler", 1, new Dictionary<string, object>()
{
    { "Adi", "Mehmet" },
    { "Yas", 35 }
});
```

---

### 5. Veri Silme / Delete Data
Belirli bir ID'ye ait kaydı silmek için:  
To delete a record with a specific ID:

```csharp
dbOps.Delete("TabloAdi", 1);
// dbOps.Delete("TableName", 1);
```

**Örnek Kullanım / Example Usage:**
```csharp
dbOps.Delete("Musteriler", 1);
```

---

## 📦 Bağımlılıklar / Dependencies
- **System.Data.SqlClient:** ADO.NET ile SQL Server bağlantısı için gereklidir.  
- **System.Data.SqlClient:** Required for SQL Server connection with ADO.NET.

- **System.Windows.Forms:** Mesaj kutuları için kullanılmıştır (isteğe bağlı olarak çıkarılabilir).  
- **System.Windows.Forms:** Used for message boxes (can be removed if desired).

---

## 📌 Sınıf Yapısı / Class Structure
`DatabaseOperations` sınıfı, ADO.NET'in temel bileşenlerini kullanarak şu işlemleri gerçekleştirir:  
The `DatabaseOperations` class performs the following operations using the basic components of ADO.NET:
- **SqlConnection:** Veritabanı bağlantısı için kullanılır.  
  **SqlConnection:** Used for database connection.
- **SqlDataAdapter:** Veri çekme işlemlerinde kullanılır.  
  **SqlDataAdapter:** Used for data retrieval.
- **SqlCommand:** Ekleme, güncelleme ve silme işlemleri için kullanılır.  
  **SqlCommand:** Used for insert, update, and delete operations.
- **MessageBox:** İşlemler sonrası kullanıcıya bilgi vermek amacıyla kullanılmıştır.  
  **MessageBox:** Used to inform the user after operations.

---

## 📌 Notlar / Notes
- Bu sınıf yapısı **.NET Framework** projelerinde kullanılmak üzere tasarlanmıştır.  
  This class structure is designed for **.NET Framework** projects.
- **.NET Core** projelerinde kullanmak için gerekli `using` kütüphanelerini güncellemeniz gerekebilir.  
  You may need to update the necessary `using` libraries to use in **.NET Core** projects.
```
