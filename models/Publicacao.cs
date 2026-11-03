namespace Blog.Models;
public class Publicacao
{
    public int IdPublicacao {get; set;}
    public string Titulo {get;set;} = string.Empty;
    public string Conteudo {get;set;} = string.Empty;
    public DateTime DataCriacao {get;set;}
    public int IdUsuario {get;set;} //fk
    public Usuario? Usuario {get;set;} //fk

    public ICollection<Comentario> Comentarios {get;set;} = new List<Comentario>();
}