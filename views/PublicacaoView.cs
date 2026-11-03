using System.Collections.Generic;
using Blog.Models;
namespace Blog.Views;
public class PublicacaoView
{
    public (string titulo, string conteudo) CriarPublicacao()
    {
        Console.Write("Digite o título da publicação: ");
        string titulo = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o conteúdo da publicação: ");
        string conteudo = Console.ReadLine() ?? string.Empty;

        return (titulo, conteudo);
    }
    public void ExibirPublicacoes(List<Publicacao> publicacoes)
    {
        Console.WriteLine("\n-----------------------------------");
        Console.WriteLine("       LISTA DE PUBLICAÇÕES         ");
        Console.WriteLine("-----------------------------------");

        if (publicacoes == null || publicacoes.Count == 0)
        {
            ExibirMensagem("Nenhuma publicação encontrada.");
            return;
        }

        foreach (var pub in publicacoes)
        {
            Console.WriteLine($"Titulo: {pub.Titulo}");
            Console.WriteLine($"Texto: {pub.Conteudo}");
            Console.WriteLine($"Data de postagem: {pub.DataCriacao:dd/MM/yyyy HH:mm}");
            Console.WriteLine(new string('-', 40));
        }
    }

    public int SolicitarIdPublicacaoExclusao()
    {
        Console.Write("Digite o ID da publicação que deseja excluir: ");
        string entrada = Console.ReadLine() ?? "";
        
        int.TryParse(entrada, out int id);
        return id;
    }

    public void ExibirMensagem (string mensagem)
    {
        Console.WriteLine("\n[AVISO]: {mensagem}");
    }
}