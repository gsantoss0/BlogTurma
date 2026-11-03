namespace Blog.Models;
public class Usuario
{
    public int IdUsuario {get; set;}
    public string Nome {get; set;} = string.Empty;
    public string Email {get;set;} = string.Empty;
    public string SenhaHash {get;set;} = string.Empty;

    public ICollection<Publicacao> Publicacoes {get;set;} = new List<Publicacao>();
    public ICollection<Comentario> Comentarios {get;set;} = new List<Comentario>();
}