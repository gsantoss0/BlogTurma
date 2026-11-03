namespace Blog.Views;
public class AutenticacaoView
{
    public(string nome, string email, string senha) SolicitarDadosCadastro()
    {
        Console.WriteLine("Insira seu nome: ");
        string nome = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Informe seu email: ");
        string email = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Crie uma senha: ");
        string senha = Console.ReadLine() ?? string.Empty;

        return (nome,email,senha);
    }

    public(string email, string senha) SolicitarCredenciais()
    {
        Console.WriteLine("Informe o email cadastrado: ");
        string email = Console.ReadLine();
        Console.WriteLine("Digite sua senha: ");
        string senha = Console.ReadLine();

        return (email, senha);
    }

    public void ExibirMensagem(string mensagem)
    {
        Console.WriteLine(mensagem);
    }
}