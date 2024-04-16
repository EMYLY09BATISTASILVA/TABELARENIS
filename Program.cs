using System;
using MySqlConnector;

namespace CARROAPP
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString =
             "server=localhost;user=root;password=meuscachorros;database=projeto_teste;";

            AutomovelDAO automovelDAO = new AutomovelDAO(connectionString);

            while (true)
            {
                Console.WriteLine("\n---------------");
                Console.WriteLine("Menu de Opções:");
                Console.WriteLine("---------------");
                Console.WriteLine("1.Inserir Carro");
                Console.WriteLine("2. Listar Carros");
                Console.WriteLine("3. Editar Carro");
                Console.WriteLine("4. Excluir Carro");
                Console.WriteLine("5. Sair");
                Console.WriteLine("Escolha uma opção (1/2/3/4/5):");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        InserirAutomovel(automovelDAO);
                        break;
                    case "2":
                        ListarAutomoveis(automovelDAO);
                        break;
                    case "3":
                        EditarAutomovel(automovelDAO);
                        break;
                    case "4":
                        ExcluirAutomovel(automovelDAO); 
                        break;
                    case "5":
                        Console.WriteLine("Encerrando o programa... Até logo!");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente");
                        break;
                }
            }
        }

        static void InserirAutomovel(AutomovelDAO automovelDAO)
        {
            try
            {
                Console.Write("Digite a Marca do Carro: ");
                string marcaCarro = Console.ReadLine();
                Console.Write("Informe o modelo do carro: ");
                string modeloCarro = Console.ReadLine();
                Console.Write("Informe a placa do carro: ");
                string placaCarro = Console.ReadLine();

                Automovel novoAutomovel = new Automovel
                {
                    MarcaCarro = marcaCarro,
                    ModeloCarro = modeloCarro,
                    PlacaCarro = placaCarro
                };

                automovelDAO.InserirAutomovel(novoAutomovel);
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        static void ListarAutomoveis(AutomovelDAO automovelDAO)
        {
            try
            {
                var automoveis = automovelDAO.ListarAutomoveis();
                Console.WriteLine("Lista de Carros:");
                foreach (var automovel in automoveis)
                {
                    Console.WriteLine($"ID: {automovel.Id}, Marca: {automovel.MarcaCarro}, Modelo: {automovel.ModeloCarro}, Placa: {automovel.PlacaCarro}");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        static void ExcluirAutomovel(AutomovelDAO automovelDAO)
        {
            try
            {
                Console.Write("Digite o ID do carro que deseja excluir: ");
                int id = int.Parse(Console.ReadLine());

                automovelDAO.ExcluirAutomovel(id);

                Console.WriteLine("Carro excluído com sucesso!");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        static void EditarAutomovel(AutomovelDAO automovelDAO)
        {
            try
            {
                Console.Write("Digite o ID do carro que deseja editar: ");
                int id = int.Parse(Console.ReadLine());
                
                Console.Write("Digite a nova marca do carro: ");
                string marca = Console.ReadLine();
                
                Console.Write("Digite o novo modelo do carro: ");
                string modelo = Console.ReadLine();
                
                Console.Write("Digite a nova placa do carro: ");
                string placa = Console.ReadLine();
                
                Automovel automovelAtualizado = new Automovel
                {
                    Id = id,
                    MarcaCarro = marca,
                    ModeloCarro = modelo,
                    PlacaCarro = placa
                };
                
                automovelDAO.EditarAutomovel(automovelAtualizado);
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }
}
