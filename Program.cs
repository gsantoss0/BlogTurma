using Blog.Views;
using Blog.Services;
using Blog.Controllers;
class Program
{
    static void Main(string[] args)
    {
       Conexao conexao = new Conexao();
            SessaoUsuario sessao = new SessaoUsuario();

            UsuarioService usuarioService = new UsuarioService(conexao);
            PublicacaoService publicacaoService = new PublicacaoService(conexao);

            MenuView menuView = new MenuView();
            AutenticacaoView autenticacaoView = new AutenticacaoView();
            PublicacaoView publicacaoView = new PublicacaoView();
            
            ComentarioService comentarioService = new ComentarioService(conexao, sessao);
            
            AutenticacaoController autController = new AutenticacaoController(autenticacaoView, usuarioService, sessao);
            PublicacaoController pubController = new PublicacaoController(publicacaoView, publicacaoService, sessao);
            


            while (true)
            {
                Console.WriteLine("\n==================================");
                if (sessao.EstaAutenticado)
                {
                    Console.WriteLine($"[ Logado como: {sessao.NomeUsuario} ]");
                }
                else
                {
                    Console.WriteLine("[ Não Autenticado ]");
                }
                Console.WriteLine("==================================");

                menuView.ExibirMenu();
                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine() ?? "";
                Console.WriteLine();

                switch (opcao)
                {
                    case "1":
                        autController.Cadastrar();
                        break;
                    case "2":
                        autController.Entrar();
                        break;
                    case "3":
                        pubController.Listar();
                        break;
                    case "4":
                        //por fazer
                        break;
                    case "5":
                        pubController.Criar();
                        break;
                    case "6":
                        //por fazer
                        break;
                    case "7":
                        pubController.Excluir();
                        break;
                    case "8":
                        
                        break;
                    case "9":
                        autController.Sair();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Tente novamente.");
                        break;
                }
            }
    }
    
}