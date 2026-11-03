using Blog.Services;
using Blog.Views;

namespace Blog.Controllers;
public class AutenticacaoController
{
    private readonly AutenticacaoView _view;
    private readonly UsuarioService _usuarioService;
    private readonly SessaoUsuario _sessaoUsuario;

    public AutenticacaoController(AutenticacaoView view, UsuarioService usuarioService, SessaoUsuario sessaoUsuario)
    {
        _view = view;
        _usuarioService = usuarioService;
        _sessaoUsuario = sessaoUsuario;
    }

    public void Cadastrar()
    {

    var (nome, email, senha) = _view.SolicitarDadosCadastro();

    bool sucesso = _usuarioService.Cadastrar(nome, email, senha);

    if (sucesso)
    {
        _view.ExibirMensagem("Utilizador cadastrado com sucesso!");
    }
    else
    {
        _view.ExibirMensagem("Falha ao cadastrar o utilizador. Verifique os dados.");
    }
    }

    public void Entrar()
    {

        var (email, senha) = _view.SolicitarCredenciais();


        var usuario = _usuarioService.Autenticar(email, senha);


        if (usuario != null)
        {

            _sessaoUsuario.Iniciar(usuario.IdUsuario, usuario.Nome);
            _view.ExibirMensagem($"Bem-vindo, {usuario.Nome}!");
        }
        else
        {
            _view.ExibirMensagem("E-mail ou senha incorretos.");
        }
    }

    public void Sair()
    {
    _sessaoUsuario.Encerrar();

    _view.ExibirMensagem("Sessão encerrada com sucesso.");
    }
}
