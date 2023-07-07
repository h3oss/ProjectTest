using System.Data;
using System.Data.SqlClient;

namespace LibraryGamesCRUD.Models
{
    public class ContexDb
    {
        public class ContextDb
        {
            string conn = "Data Source=DESKTOP-KSERBB9\\MSSQLSER; Database=LibraryGames;Trusted_Connection=True;MultipleActiveResultSets=True";
            public List<Games> GetGames()
            {
                List<Games> list = new List<Games>();
                string query = "Select * from Games";
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        foreach (DataRow dr in dt.Rows)
                        {
                            list.Add(new Games { Id = Convert.ToInt32(dr[0]), Name = Convert.ToString(dr[1]) });
                        }
                    }
                }
                return list;
            }
            public bool CreateGame(Games game)
            {
                string query = "INSERT INTO Games (Name, Id_Studio) VALUES (@Name, @Id_Studio)";
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Name", game.Name);
                        cmd.Parameters.AddWithValue("@Id_Studio", game.Id_Studio);

                        try
                        {
                            con.Open();
                            int i = cmd.ExecuteNonQuery();
                            if (i >= 1)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ошибка при создании игры: " + ex.Message);
                            return false;
                        }
                    }
                }
            }

            public List<Games> GetGamesByGenre(string genre)
            {
                string query = "SELECT g.* FROM Games g INNER JOIN GamesToGenres gg ON g.Id = gg.Id_Game INNER JOIN Genres ge ON gg.Id_Genre = ge.Id WHERE ge.Name = @Genre";
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Genre", genre);

                        try
                        {
                            con.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                List<Games> games = new List<Games>();
                                while (reader.Read())
                                {
                                    Games game = new Games
                                    {
                                        Id = Convert.ToInt32(reader["Id"]),
                                        Name = reader["Name"].ToString(),
                                        Id_Studio = Convert.ToInt32(reader["Id_Studio"])
                                    };
                                    games.Add(game);
                                }
                                return games;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ошибка при получении списка игр по жанру: " + ex.Message);
                            return null;
                        }
                    }
                }
            }

            public bool UpdateGame(Games game)
            {
                string query = "UPDATE Games SET Name = @Name, Id_Studio = @Id_Studio WHERE Id = @Id";
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Id", game.Id);
                        cmd.Parameters.AddWithValue("@Name", game.Name);
                        cmd.Parameters.AddWithValue("@Id_Studio", game.Id_Studio);

                        try
                        {
                            con.Open();
                            int i = cmd.ExecuteNonQuery();
                            if (i >= 1)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ошибка при обновлении игры: " + ex.Message);
                            return false;
                        }
                    }
                }
            }

            public bool DeleteGame(int gameId)
            {
                string query = "DELETE FROM Games WHERE Id = @Id";
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Id", gameId);

                        try
                        {
                            con.Open();
                            int i = cmd.ExecuteNonQuery();
                            if (i >= 1)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ошибка при удалении игры: " + ex.Message);
                            return false;
                        }
                    }
                }
            }
        }
    }

           
}
