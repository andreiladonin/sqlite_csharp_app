using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
namespace myProojectSQL.DataBaseConnection
{
    internal static class MySQLcontext
    {
        public static SqliteConnection SqlConnection ()
        {

            string Connect = "Data Source=sqllite.db";

            SqliteConnection mysql_connection = new SqliteConnection(Connect);

            return mysql_connection;
        }

        public static bool Insert(Person p)
        {
            string sqlExpression = $"INSERT INTO personal (name, last_name, tell, profession) VALUES ('{p.Name}', '{p.LastName}', {p.Telephone}, '{p.Profession}')";

            try
            {
                using (var db = SqlConnection())
                {
                    db.Open();

                    var command = new SqliteCommand();
                    command.Connection = db;
                    command.CommandText = sqlExpression;

                    int number = command.ExecuteNonQuery();

                    return Convert.ToBoolean(number);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
        }

        public static object GetCountItems ()
        {
            try
            {
                using (var db = SqlConnection())
                {
                    db.Open();
                    var sql = db.CreateCommand();
                    sql.CommandText = "Select COUNT('name') From personal";
                    sql.Connection = db;
                    object count = sql.ExecuteScalar();
                    return count;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public static List<Person> GetPeople ()
        {
            string sql = "Select name, last_name, tell, profession From personal";
            try
            {
                using (var db = SqlConnection())
                {
                    db.Open();
                    var cmd = db.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.Connection = db;
                    var res = cmd.ExecuteReader();

                    var list = new List<Person>();

                    while (res.Read())
                    {
                        list.Add(
                            new Person()
                            {
                                Name = res.GetString(0),
                                LastName = res.GetString(1),
                                Telephone = res.GetInt32(2),
                                Profession = res.GetString(3)
                            }
                            );
                    }

                    return list;
                }
            }
            catch (Exception ex)
            {

                return null;
            }

        }
    }
}
