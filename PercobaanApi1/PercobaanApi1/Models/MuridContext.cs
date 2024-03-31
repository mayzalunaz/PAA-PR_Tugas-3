using Npgsql;
using Percobaan1.Helpers;

namespace PercobaanApi1.Models
{
    public class MuridContext
    {
        private string __constr;
        private string __ErrorMsg;

        public MuridContext(string pConstr)
        {
            __constr = pConstr;
        }

        public List<Murid> ListMurid()
        {
            List<Murid> list1 = new List<Murid>();
            string query = @"SELECT id_murid, nama, kelas, alamat, email, username, password FROM users.murid;";
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            {
                try
                {
                    NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list1.Add(new Murid()
                        {
                            id_murid = int.Parse(reader["id_murid"].ToString()),
                            nama = reader["nama"].ToString(),
                            kelas = reader["kelas"].ToString(),
                            alamat = reader["alamat"].ToString(),
                            email = reader["email"].ToString(),
                            username = reader["username"].ToString(),
                            password = reader["password"].ToString(),
                        });
                    }
                }
                catch (Exception ex)
                {
                    __ErrorMsg = ex.Message;
                }
            }
            return list1;
        }

        public Murid GetMuridById(int id_murid)
        {
            Murid person = null;
            string query = $"SELECT id_murid, nama, kelas, alamat, email, username, password FROM users.murid WHERE id_murid = {id_murid};";
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            {
                try
                {
                    NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        person = new Murid()
                        {
                            id_murid = int.Parse(reader["id_murid"].ToString()),
                            nama = reader["nama"].ToString(),
                            kelas = reader["kelas"].ToString(),
                            alamat = reader["alamat"].ToString(),
                            email = reader["email"].ToString(),
                            username = reader["username"].ToString(),
                            password = reader["password"].ToString(),
                        };
                    }
                }
                catch (Exception ex)
                {
                    __ErrorMsg = ex.Message;
                }
            }
            return person;
        }

        public void AddMurid(Murid person)
        {
            string query = $@"INSERT INTO users.murid (id_murid, nama, kelas, alamat, email, username, password) 
                              VALUES ({person.id_murid}, '{person.nama}', '{person.kelas}', '{person.alamat}', '{person.email}',
                                     '{person.username}', '{person.password}');";
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            {
                try
                {
                    NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    __ErrorMsg = ex.Message;
                }
            }
        }

        public void UpdateMurid(Murid person)
        {
            string query = $@"UPDATE users.murid 
                              SET nama = '{person.nama}', kelas = '{person.kelas}', alamat = '{person.alamat}', 
                                  email = '{person.email}', username = '{person.username}', password = '{person.password}'
                              WHERE id_murid = {person.id_murid};";
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            {
                try
                {
                    NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    __ErrorMsg = ex.Message;
                }
            }
        }

        public void DeleteMurid(int id_murid)
        {
            string query = $"DELETE FROM users.murid WHERE id_murid = {id_murid};";
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            {
                try
                {
                    NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    __ErrorMsg = ex.Message;
                }
            }
        }
    }
}
