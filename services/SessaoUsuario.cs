public class SessaoUsuario
{
    public int? UsuarioId{get; set;}
    public string NomeUsuario {get;set;} = string.Empty;

    public bool EstaAutenticado => UsuarioId.HasValue;

    public void Iniciar(int usuarioId, string nomeUsuario)
    {
        UsuarioId = usuarioId;
        NomeUsuario = nomeUsuario;
    }

    public void Encerrar()
    {
        UsuarioId = null;
        NomeUsuario = string.Empty;
    }
}