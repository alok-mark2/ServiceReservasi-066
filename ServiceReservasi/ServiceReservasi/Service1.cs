using ServiceReservasi;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceReservasi
{
       
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        string constring = "Data Source=E3-SENTRY;Initial Catalog=WCFReservas;Persist Security Info=True;User ID=sa;Password=bariskartalii737aewc";
        SqlConnection connection;
        SqlCommand com;

        public List<DetailLokasi> DetailLokasi()
        {
            List<DetailLokasi> lokasiFull = new List<DetailLokasi>();
            try
            {
                string sql = "SELECT ID_lokasi, Nama_lokasi, Deskripsi_singkat, Deskripsi_full, Kuota FROM dbo.Lokaasi";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    DetailLokasi data = new DetailLokasi();
                    data.IDLokasi = reader.GetString(0);
                    data.NamaLokasi = reader.GetString(1);
                    data.DeskripsiFull = reader.GetString(2);
                    data.Kuota = reader.GetInt32(3);
                    lokasiFull.Add(data);
                }
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return lokasiFull;
        }



        public string pemesanan(string IDPemesanan, string NamaCustomer, string NoTelpon, int JumlahPemesanan, string IDLokasi)
        {
            string a = "kok gagal?";
            try
            {
                string sql = "INSERT INTO dbo.Pemesanan VALUES ('" + IDPemesanan + "', '" + NamaCustomer + "', '" + NoTelpon + "', '" + JumlahPemesanan + "', '" + IDLokasi + "')";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                string sql2 = "update dbo.Lokasi set kuota = kuota - " + JumlahPemesanan + " where ID_lokasi = '" + IDLokasi + "' ";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql2, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }




        /*public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }*/


        public string editPemesanan(string IDPemesanan, string NamaCustomer, string No_telpon)
        {

            string a = "gagal";
            try
            {
                string sql = "update dbo.Pemesanan set Nama_customer = '" + NamaCustomer + "', No_telpon = '" + No_telpon + "'" +
                    " where ID_reservasi = '" + IDPemesanan + "' ";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;

            // throw new NotImplementedException();
        }

        public string deletePemesanan(string IDPemesanan)
        {
            string a = "gagal";
            try
            {
                string sql = "delete from dbo.Pemesanan where ID_reservasi = '" + IDPemesanan + "'";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();
                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }


        public List<CekLokasi> ReviewLokasi()
        {
            throw new NotImplementedException();
        }

        public List<Pemesanan> Pemesanan()
        {
            //  throw new NotImplementedException();
            List<Pemesanan> pemesanans = new List<Pemesanan>();
            try
            {
                string sql = "select ID_reservasi, Nama_customer, No_telpon, " +
                    "Jumlah_pemesanan, Nama_lokasi from dbo.Pemesanan p join dbo.Lokaasi l on p.ID_lokasi = l.ID_lokasi";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Pemesanan data = new Pemesanan();
                    data.IDPemesanan = reader.GetString(0);
                    data.NamaCustomer = reader.GetString(1);
                    data.NoTelpon = reader.GetString(2);
                    data.JumlahPemesanan = reader.GetInt32(3);
                    data.Lokasi = reader.GetString(4);
                    pemesanans.Add(data);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return pemesanans;
        }

        public string editPemesanan(string IDPemesanan, string NamaCustomer)
        {
            throw new NotImplementedException();
        }

        public string GetData(int value)
        {
            throw new NotImplementedException();
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            throw new NotImplementedException();
        }

        public string Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public string Register(string username, string password, string kategori)
        {
            throw new NotImplementedException();
        }

        public string UpdateRegister(string username, string password, string ketegori, int id)
        {
            throw new NotImplementedException();
        }

        public string DeleteRegister(string username)
        {
            throw new NotImplementedException();
        }

        public List<DataRegister> DataRegist()
        {
            throw new NotImplementedException();
        }
    }


    public string Register(string username, string password, string kategori, SqlConnection connection, string constring, object com)
    {
        try
        {
            string sql = "insert  into Login values('" + username + "','" + password + "','" + kategori + "')";
            connection = new SqlConnection(constring);
            com = new SqlCommand(sql, connection);
            connection.Open();
            com.ExecuteNonQuery();
            connection.Close();

            return "sukses";
        }
        catch (Exception e)
        {
            return e.ToString();
        }
    }

    public string DeleteRegister(string username, SqlCommand com, SqlConnection connection, string constring)
    {
        try
        {
            int id = 0;
            string sql = "select ID_Login from dbo.Login where Username = '" + username + "'";
            connection = new SqlConnection(constring);
            com = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                id = reader.GetInt32(0);
            }
            connection.Close();
            string sql2 = "delete from Login where ID_Login = '" + id + "'";
            connection = new SqlConnection(constring);
            com = new SqlCommand(sql2, connection);
            connection.Open();
            com.ExecuteNonQuery();
            connection.Close();

            return "Sukses";
        }

        catch (Exception e)
        {
            return e.ToString();
        }
    }

    public List<DataRegister> DataRegist(string constring, SqlConnection connection, SqlCommand com)
    {
        List<DataRegister> list = new List<DataRegister>();
        try
        {
            string sql = "select ID_Login, Username, Password, Kategori from Login";
            connection = new SqlConnection(constring);
            com = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                DataRegister data = new DataRegister();
                data.id = reader.GetInt32(0);
                data.username = reader.GetString(1);
                data.password = reader.GetString(2);
                data.kategori = reader.GetString(3);
                list.Add(data);
            }
            connection.Close();
        }
        catch (Exception e)
        {
            e.ToString();
        }
        return list;
    }
}