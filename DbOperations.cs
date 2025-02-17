using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace DynamicAdoNet
{
    public class DatabaseOperations
    {
        // Veritabanı bağlantısı için connection string. Bu bağlantı, veritabanınızla iletişimi sağlar.
        // Bu alanda veritabanınızın adı, sunucu bilgileri ve bağlantı türü yer alır.
        string connectionString = "server=localhost;database=KutuphaneDB;Trusted_Connection=true";



        /// Verilen tablo adından tüm sütunları ve satırları döndüren bir metot.
        public DataTable GetAll(string tableName)
        {
            // Yeni bir DataTable nesnesi oluşturuluyor. Bu, sorgudan dönecek verilerin tutulacağı yapıdır.
            DataTable table = new DataTable();

            // Veritabanı bağlantısı oluşturuluyor.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // SQL sorgusu "SELECT * FROM tableName" şeklinde oluşturuluyor.
                // "*" tüm sütunları ifade eder, yani tablodaki tüm veriler alınır.
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM {tableName}", connection);
                // Veritabanından veriler çekiliyor ve DataTable'a dolduruluyor.
                adapter.Fill(table);
            }
            return table;  // Sonuç olarak tüm sütunları içeren DataTable döndürülüyor.
        }


        // Verilen tablo adından yalnızca belirli sütunları çeken metot.
        public DataTable GetAll(string tableName, List<string> columns)
        {
            DataTable table = new DataTable();

            // Sütunları virgülle ayırarak SQL sorgusuna yerleştiriyoruz.
            string columnList = string.Join(",", columns);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // SQL sorgusu "SELECT column1, column2, ..." şeklinde oluşturuluyor.
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT {columnList} FROM {tableName}", connection);
                // Veritabanından veriler çekilip DataTable'a aktarılıyor.
                adapter.Fill(table);
            }
            return table;  // Yalnızca belirtilen sütunlarla doldurulmuş DataTable döndürülüyor.
        }


        // Bu metot, Belirtilen tablonun belirtilen sütunlarına yeni bir kayıt eklemek için kullanılır.
        public void Insert(string tableName, Dictionary<string, object> parameter)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Bağlantıyı açıyoruz.
                    connection.Open();

                    // Sütun adlarını birleştirerek bir liste oluşturuyoruz.
                    string columns = string.Join(",", parameter.Keys);
                    // Değerleri sorguya eklerken, parametre isimlerini hazırlıyoruz (örneğin @column1, @column2, ...).
                    string values = string.Join(",", parameter.Keys.Select(keys => "@" + keys));

                    // INSERT INTO sorgusunu dinamik olarak oluşturuyoruz.
                    string insertQuery = $"INSERT INTO {tableName}({columns}) VALUES({values})";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        // Parametreleri değerleriyle eşleştirip sorguya ekliyoruz.
                        foreach (var item in parameter)
                        {
                            command.Parameters.AddWithValue("@" + item.Key, item.Value);
                        }

                        // Sorguyu çalıştırıyoruz ve etkilenen satır sayısını alıyoruz.
                        int affectedRows = command.ExecuteNonQuery();

                        //Etkilenen satır sayısı 0'dan büyükse Kayıt başarıyla eklenmiş demektir ve bu durumda kullanıcıya mesaj gösteriyoruz.
                        if (affectedRows > 0)
                            MessageBox.Show("Kayıt Başarıyla Eklendi");
                        else
                            MessageBox.Show("Kayıt Eklenirken bir sorun oluştu");
                    }
                }
                catch (Exception ex)
                {
                    // Hata oluşursa kullanıcıya hata mesajı gösteriliyor.
                    MessageBox.Show("Bir hata oluştu : " + ex.Message);
                }
            }
        }


        //Bu metot, Belirtilen tablo içerisindeki belirtilen ID'ye ait kaydı günceller
        public void Update(string tableName, int id, Dictionary<string, object> parameter)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Bağlantıyı açıyoruz.
                    connection.Open();

                    // Güncellenmesi gereken sütunları hazırlıyoruz. Örneğin: column1=@column1, column2=@column2, ...
                    string set = string.Join(",", parameter.Keys.Select(x => x + "=@" + x));

                    // UPDATE sorgusunu oluşturuyoruz. ID ile hangi kaydın güncelleneceğini belirtiyoruz.
                    string updateQuery = $"UPDATE {tableName} SET {set} WHERE Id=@Id";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        // ID  parametresini değeriyle eşleştirip sorguya ekliyoruz.
                        command.Parameters.AddWithValue("@Id", id);

                        // Diğer parametreleri değerleriyle eşleştirip  sorguya ekliyoruz.
                        foreach (var item in parameter)
                            command.Parameters.AddWithValue("@" + item.Key, item.Value);

                        // Sorguyu çalıştırıyoruz ve etkilenen satır sayısını kontrol ediyoruz.
                        int affectedRows = command.ExecuteNonQuery();

                        //Etkilenen satır sayısı 0'dan büyükse Kayıt başarıyla güncellenmiş demektir, bu durumda kullanıcıya bilgi veriyoruz.
                        if (affectedRows > 0)
                            MessageBox.Show("Kayıt Başarıyla Güncellendi");
                        else
                            MessageBox.Show("Kayıt Güncellenirken bir sorun oluştu");
                    }
                }
                catch (Exception ex)
                {
                    // Hata oluşursa kullanıcıya hata mesajı gösteriliyor.
                    MessageBox.Show("Bir hata oluştu : " + ex.Message);
                }
            }
        }

        //Bu metot,verilen ID'ye ait Kaydın silinmesini sağlar
        public void Delete(string tableName, int id) 
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Bağlantıyı açıyoruz.
                    connection.Open();

                    // DELETE sorgusunu oluşturuyoruz. ID'ye göre hangi kaydın silineceği belirtiliyor.
                    string deleteQuery = $"DELETE FROM {tableName} WHERE Id=@Id";

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        // ID parametresini değeriyle eşleştirip sorguya ekliyoruz.
                        command.Parameters.AddWithValue("@Id", id);

                        // Sorguyu çalıştırıyoruz ve etkilenen satır sayısını kontrol ediyoruz.
                        int affectedRows = command.ExecuteNonQuery();

                        //Etkilenen satır sayısı 0'dan büyükse Kayıt başarıyla silinmiş demektir ve bu durumda kullanıcıya mesaj gösterilir.
                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Kayıt Başarıyla Silindi");
                        }
                        else
                            MessageBox.Show("Kayıt Silinirken bir sorun oluştu");
                    }
                }
                catch (Exception ex)
                {
                    // Hata oluşursa kullanıcıya hata mesajı gösteriliyor.
                    MessageBox.Show("Bir Hata Oluştu: " + ex.Message);
                }
            }
        }
    }
}
