using System;
using Blog.Models;
using MySql.Data.MySqlClient;

namespace Blog.Services;
public class PublicacaoService
{
    readonly Conexao _conexao;

    public PublicacaoService(Conexao connection)
    {
        _conexao = connection;
    }

    public bool Criar(string conteudo, int idUsuario)
    {
        using (var connection = _conexao.CriarConexao())
        {
            connection.Open();

            string query = "INSERT INTO Publicacao (conteudo, id_usuario, data_criacao) VALUES (@conteudo, @idUsuario, @dataCriacao)";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@conteudo", conteudo);
                command.Parameters.AddWithValue("@idUsuario", idUsuario);
                command.Parameters.AddWithValue("@dataCriacao", DateTime.Now);

                int linhasAfetadas = command.ExecuteNonQuery();
                return linhasAfetadas > 0;
            }
        }
    }

    public List<Publicacao> ListarPublicacoes()
    {
        List<Publicacao> publicacoes = new List<Publicacao>();

        using (var connection = _conexao.CriarConexao())
        {
            connection.Open();  

            string query = "SELECT id_publicacao,titulo,conteudo,data_criacao,id_usuario FROM Publicacao";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                using(MySqlDataReader leitor = command.ExecuteReader())
                {
                    while(leitor.Read())
                    {
                        Publicacao pub = new Publicacao
                        {
                            IdPublicacao = Convert.ToInt32(leitor["id_publicacao"]),
                            Titulo = leitor["titulo"].ToString() ?? string.Empty,
                            Conteudo = leitor["conteudo"].ToString() ?? string.Empty,
                            DataCriacao = Convert.ToDateTime(leitor["data_criacao"]),
                            IdUsuario = Convert.ToInt32(leitor["id_usuario"])
                        };

                        publicacoes.Add(pub);
                    }
                }
            }
        }

        return publicacoes;
    }

    public bool Excluir(int idPublicacao, int idUsuario)
{
    using (var connection = _conexao.CriarConexao())
    {
        connection.Open();

        string query = "DELETE FROM Publicacao WHERE id_publicacao = @idPublicacao AND id_usuario = @idUsuario";

        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@idPublicacao", idPublicacao);
            command.Parameters.AddWithValue("@idUsuario", idUsuario);

            int linhasAfetadas = command.ExecuteNonQuery();
            return linhasAfetadas > 0;
        }
    }
}

    //Funcao publica ListarPublicacoes que
    //abrir a conexao
    //Criar o statement para rodar no banco
    //executar a query
    //retornar a lista de Publicacao
}