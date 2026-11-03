using System;
using Blog.Models;
using Blog.Services;
using Blog.Views;

namespace Blog.Controllers;

    public class PublicacaoController
    {
        private readonly PublicacaoView _view;
        private readonly PublicacaoService _publicacaoService;
        private readonly SessaoUsuario _sessaoUsuario;


        public PublicacaoController(PublicacaoView view, PublicacaoService publicacaoService, SessaoUsuario sessaoUsuario)
        {
            _view = view;
            _publicacaoService = publicacaoService;
            _sessaoUsuario = sessaoUsuario;
        }

        // Criar uma nova publicação
        public void Criar()
        {
            // Valida se o utilizador está autenticado antes de continuar
            if (!_sessaoUsuario.EstaAutenticado || !_sessaoUsuario.UsuarioId.HasValue)
            {
                _view.ExibirMensagem("É necessário estar autenticado para criar uma publicação.");
                return;
            }

            var (titulo,conteudo)= _view.CriarPublicacao();

            if (string.IsNullOrWhiteSpace(conteudo))
            {
                _view.ExibirMensagem("O conteúdo da publicação não pode estar vazio.");
                return;
            }

            bool sucesso = _publicacaoService.Criar(conteudo, _sessaoUsuario.UsuarioId.Value);

            if (sucesso)
                _view.ExibirMensagem("Publicação criada com sucesso!");
            else
                _view.ExibirMensagem("Erro ao criar publicação.");
        }

        public void Excluir()
        {
        
            if (!_sessaoUsuario.EstaAutenticado || !_sessaoUsuario.UsuarioId.HasValue)
            {
                _view.ExibirMensagem("É necessário estar autenticado para excluir uma publicação.");
                return;
            }

            int idPublicacao = _view.SolicitarIdPublicacaoExclusao();

            if (idPublicacao <= 0)
            {
                _view.ExibirMensagem("ID de publicação inválido.");
                return;
            }

            bool sucesso = _publicacaoService.Excluir(idPublicacao, _sessaoUsuario.UsuarioId.Value);

            if (sucesso)
                _view.ExibirMensagem("Publicação excluída com sucesso!");
            else
                _view.ExibirMensagem("Não foi possível excluir a publicação. Verifique se o ID está correto e se é o autor do post.");
        }

        public void Listar()
        {
            var publicacoes = _publicacaoService.ListarPublicacoes();
            _view.ExibirPublicacoes(publicacoes);
        }

    }
