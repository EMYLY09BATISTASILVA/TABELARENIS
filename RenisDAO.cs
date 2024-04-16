using System;
using System.Collections.Generic;
using MySqlConnector;

namespace CARROAPP
{
    class AutomovelDAO
    {
        private readonly string _connectionString;
        
        public AutomovelDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InserirAutomovel(Automovel novoAutomovel)
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(_connectionString))
                {
                    conexao.Open();
                    string inserirDados = "INSERT INTO automovel (MarcaCarro, ModeloCarro, PlacaCarro) VALUES (@MarcaCarro, @ModeloCarro, @PlacaCarro)";
                    using (MySqlCommand comando = new MySqlCommand(inserirDados, conexao))
                    {
                        comando.Parameters.AddWithValue("@MarcaCarro", novoAutomovel.MarcaCarro);
                        comando.Parameters.AddWithValue("@ModeloCarro", novoAutomovel.ModeloCarro);
                        comando.Parameters.AddWithValue("@PlacaCarro", novoAutomovel.PlacaCarro);
                        comando.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Automóvel inserido com sucesso!");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao inserir automóvel: {ex.Message}");
            }
        }

        public void EditarAutomovel(Automovel automovelAtualizado)
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(_connectionString))
                {
                    conexao.Open();
                    string query = "UPDATE automovel SET MarcaCarro = @MarcaCarro, ModeloCarro = @ModeloCarro, PlacaCarro = @PlacaCarro WHERE idCarro = @idCarro";
                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@idCarro", automovelAtualizado.Id);
                        comando.Parameters.AddWithValue("@MarcaCarro", automovelAtualizado.MarcaCarro);
                        comando.Parameters.AddWithValue("@ModeloCarro", automovelAtualizado.ModeloCarro);
                        comando.Parameters.AddWithValue("@PlacaCarro", automovelAtualizado.PlacaCarro);
                        comando.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Automóvel atualizado com sucesso!");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao atualizar automóvel: {ex.Message}");
            }
        }

  public List<Automovel> ListarAutomoveis()
{
    List<Automovel> automoveis = new List<Automovel>();

    try
    {
        using (MySqlConnection conexao = new MySqlConnection(_connectionString))
        {
            conexao.Open();
            string query = "SELECT idCarro, MarcaCarro, ModeloCarro, PlacaCarro FROM automovel";
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Automovel automovel = new Automovel
                        {
                            Id = Convert.ToInt32(reader["idCarro"]),
                            MarcaCarro = reader["MarcaCarro"].ToString(),
                            ModeloCarro = reader["ModeloCarro"].ToString(),
                            PlacaCarro = reader["PlacaCarro"].ToString()
                        };
                        automoveis.Add(automovel);
                    }
                }
            }
        }
    }
    catch (MySqlException ex)
    {
        Console.WriteLine($"Erro ao listar automóveis: {ex.Message}");
    }

    return automoveis;
}


        public void ExcluirAutomovel(int id)
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(_connectionString))
                {
                    conexao.Open();
                    string query = "DELETE FROM automovel WHERE idCarro = @idCarro";
                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@idCarro", id);
                        comando.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Automóvel excluído com sucesso!");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao excluir automóvel: {ex.Message}");
            }
        }
    }
}
