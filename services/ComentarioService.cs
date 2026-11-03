using Blog.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.IO.Compression;
using System.Runtime.CompilerServices;

namespace Blog.Services;
public class ComentarioService
{
    private readonly Conexao _conexao;
    private readonly SessaoUsuario _sessaoUsuario;

public ComentarioService(Conexao conexao, SessaoUsuario sessaoUsuario)
    {
        _conexao = conexao;
        _sessaoUsuario = sessaoUsuario;
    }

public bool Adicionar(int IdPublicacao, string texto)
    {
        if(!_sessaoUsuario.EstaAutenticado)
        {
            return false;
        }

        using (var connection = _conexao.CriarConexao())
        {
            connection.Open();
            string query = "INSERT INTO Comentario (texto, data_criacao, id_publicacao, id_usuario) VALUES (@texto, @dataCriacao, @IdPublicacao, @idUsuario)";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@texto", texto);
                command.Parameters.AddWithValue("@dataCriacao", DateTime.Now);
                command.Parameters.AddWithValue("@IdPublicacao", IdPublicacao);
                command.Parameters.AddWithValue("idUsuario", _sessaoUsuario.UsuarioId.Value);

                int linhasAfetadas = command.ExecuteNonQuery();
                return linhasAfetadas > 0;
            }

        }
    }

    public List<Comentario> ListarPorPublicacao(int IdPublicacao)
    {
        List<Comentario> comentarios = new List<Comentario>();
        using (var connection = _conexao.CriarConexao())
        {
            connection.Open();

            string query = "SELECT id_comentario, texto, data_criacao, id_publicacao, id_usuario " +
                            "FROM Comentario WHERE id_publicacao = @idPublicacao " +
                            "ORDER BY data_criacao ASC";

            using (MySqlCommand command = new MySqlCommand (query, connection))
            {
                command.Parameters.AddWithValue("@IdPublicacao", IdPublicacao);

                using (MySqlDataReader leitor = command.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        Comentario com = new Comentario
                        {
                            IdComentario = Convert.ToInt32(leitor["id_comentario"]),
                            Texto = leitor["texto"].ToString() ?? string.Empty,
                            DataComentario = Convert.ToDateTime(leitor["data_criacao"]),
                            IdPublicacao = Convert.ToInt32(leitor["id_publicacao"]),
                            IdUsuario = Convert.ToInt32(leitor["id_usuario"])
                        };
                        comentarios.Add(com);
                    }
                }
            }
            return comentarios;
        }
    }

}