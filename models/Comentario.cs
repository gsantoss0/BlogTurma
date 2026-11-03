namespace Blog.Models;

public class Comentario
{
    public int IdComentario {get;set;}
    public string Texto {get;set;}
    public DateTime DataComentario {get;set;}
    
    public int IdUsuario {get;set;}
    public Usuario? Usuario {get;set;} // fk
    
    public int IdPublicacao {get;set;}
    public Publicacao? Publicacao {get;set;} // fk
}