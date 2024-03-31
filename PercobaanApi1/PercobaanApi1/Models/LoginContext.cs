using Microsoft.IdentityModel.Tokens;
using Npgsql;
using Percobaan1.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PercobaanApi1.Models
{
    public class LoginContext
    {
        private string __constr;
        private string __errormsg;

        public LoginContext(string pConstr)
        {
            __constr = pConstr;
        }

        public List<Login> Authentifikasi(string p_username, string p_password, IConfiguration p_config)
        {
            List<Login> list1 = new List<Login>();

            string query = string.Format(@"SELECT ps.id_murid, ps.nama, ps.kelas, ps.alamat, ps.email, pm.id_peran, p.nama_peran
                FROM users.murid ps
                INNER JOIN users.peran_murid pm ON ps.id_murid=pm.id_murid
                INNER JOIN users.peran p ON pm.id_peran=p.id_peran
                where ps.username='{0}' and ps.password='{1}'; ", p_username, p_password);
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    list1.Add(new Login()
                    {
                        id_murid = int.Parse(reader["id_murid"].ToString()),
                        nama = reader["nama"].ToString(),
                        kelas = reader["kelas"].ToString(),
                        alamat = reader["alamat"].ToString(),
                        email = reader["email"].ToString(),
                        id_peran = int.Parse(reader["id_peran"].ToString()),
                        nama_peran = reader["nama_peran"].ToString(),
                        token = GenerateJwtToken(p_username, p_password, p_config)


                    });
                }

                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __errormsg = ex.Message;
            }
            return list1;
        }

        private string GenerateJwtToken(string namaUser, string peran, IConfiguration pConfig)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(pConfig["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, namaUser),
        new Claim(ClaimTypes.Role, peran),
      };
            var token = new JwtSecurityToken(pConfig["Jwt:Issuer"],
              pConfig["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}