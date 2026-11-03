using System.Data;
using Blog.Models;
namespace Blog.Services;
public class UsuarioService
{
    private readonly Conexao _conexao;
    private readonly SenhaService _senhaService;

    public UsuarioService(Conexao conexao)
    {
        _conexao = conexao;
        _senhaService = new SenhaService();
    }


    public bool Cadastrar(string nome, string email, string senha)
    {
        string senhaHash = _senhaService.GerarHash(senha);

        var con = _conexao.CriarConexao();
        using (var cmd = con.CreateCommand())
        {
            cmd.CommandText = "INSERT INTO Usuario (nome,email,senha) VALUES (@nome, @email, @senha)";

            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@senha", senha);

            int linhasAfetadas = cmd.ExecuteNonQuery();

            return linhasAfetadas > 0;
        }
    }

    public Usuario? Autenticar(string email, string senha)
    {
        var con = _conexao.CriarConexao();

        using var cmd = con.CreateCommand();
        cmd.CommandText = "SELECT id_usuario, nome, email, senha FROM Usuario WHERE email = @email"; 
        cmd.Parameters.AddWithValue("@email", email);
   
        using (var leitor = cmd.ExecuteReader())
        {
            if(leitor.Read())
            {
                string hashArmazenado = leitor["senha"].ToString() ?? string.Empty;

                if(_senhaService.Verificar(senha, hashArmazenado))
                {
                    return new Usuario
                    {
                        IdUsuario = Convert.ToInt32(leitor["id_usuario"]),
                        Nome = leitor["nome"].ToString() ?? string.Empty,
                        Email = leitor["email"].ToString() ?? string.Empty
                    };
                }
            }
                return null;   
        }
    }
   
}