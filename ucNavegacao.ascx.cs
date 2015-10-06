using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class ucNavegacao : System.Web.UI.UserControl
{
    bool _projetos;
    bool _arvore;
    bool _resultado;
    bool _foco;
    bool _premissas;
    bool _colaboradores;
    bool _agenda;
    bool _agendaProjetos;
    bool _noticias;
    bool _noticiasProjetos;
    bool _parceiros;
    bool _documentos;
    bool _gerenciamento;
    bool _acao;
    bool _financeiro;
    bool _produto;
    bool _acaoGraf;
    bool _restricao;
    
    public bool arvore
    {
        get { return _arvore; }
        set { _arvore = value; }
    }
    public bool projetos
    {
        get { return _projetos; }
        set { _projetos = value; }
    }
    public bool resultado
    {
        get { return _resultado; }
        set { _resultado = value; }
    }

    public bool foco
    {
        get { return _foco; }
        set { _foco = value; }
    }
    public bool premissas
    {
        get { return _premissas; }
        set { _premissas = value; }
    }
    public bool colaboradores
    {
        get { return _colaboradores; }
        set { _colaboradores = value; }
    }
    public bool agenda
    {
        get { return _agenda; }
        set { _agenda = value; }
    }
    public bool agendaProjetos
    {
        get { return _agendaProjetos; }
        set { _agendaProjetos = value; }
    }
    public bool noticias
    {
        get { return _noticias; }
        set { _noticias = value; }
    }
    public bool noticiasProjetos
    {
        get { return _noticiasProjetos; }
        set { _noticiasProjetos = value; }
    }
    public bool parceiros
    {
        get { return _parceiros; }
        set { _parceiros = value; }
    }
    public bool documentos
    {
        get { return _documentos; }
        set { _documentos = value; }
    }

    public bool gerenciamento
    {
        get { return _gerenciamento; }
        set { _gerenciamento = value; }
    }
    public bool acao
    {
        get { return _acao; }
        set { _acao = value; }
    }
    public bool financeiro
    {
        get { return _financeiro; }
        set { _financeiro = value; }
    }
    public bool produto
    {
        get { return _produto; }
        set { _produto = value; }
    }
    public bool acaoGraf
    {
        get { return _acaoGraf; }
        set { _acaoGraf = value; }
    }
    public bool restricao
    {
        get { return _restricao; }
        set { _restricao = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        pageBase pb = new pageBase();
        if (pb.gestaoInterna())
        {
            linkFoco.Text = "> Demanda";
        }
        else
        {
            linkFoco.Text = "> Foco estratégico";
        }

        if (pb.fl_admin())
        {
            linkFiltro.Text = "Filtro de Parceiros";
        }
        else
        {
            linkFiltro.Text = "Filtro de Eixos";
        }

        linkArvore.Visible = false;
        linkProjetos.Visible = false;
        linkResultado.Visible = false;
        linkGerenciamento.Visible = false;
        linkRestricao.Visible = false;
        linkAcao.Visible = false;
        linkFoco.Visible = false;
        linkPremissas.Visible = false;
        linkColaboradores.Visible = false;
        linkAgenda.Visible = false;
        linkNoticias.Visible = false;
        linkParceiros.Visible = false;
        linkDocumentos.Visible = false;
        linkAgendaProjetos.Visible = false;
        linkNoticiasProjetos.Visible = false;
        linkAcaoGraf.Visible = false;
        linkProduto.Visible = false;
        linkFinanceiro.Visible = false;
        if (_projetos)
        {
            linkProjetos.Visible = true;
            linkProjetos.NavigateUrl = "";
            linkProjetos.Font.Bold = true;
        }
        else if (_noticiasProjetos)
        {
            linkNoticiasProjetos.Visible = true;
            linkNoticiasProjetos.NavigateUrl = "";
            linkNoticiasProjetos.Font.Bold = true;
        }
        else if (_agendaProjetos)
        {
            linkAgendaProjetos.Visible = true;
            linkAgendaProjetos.NavigateUrl = "";
            linkAgendaProjetos.Font.Bold = true;
        }
        else if (_arvore)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkArvore.NavigateUrl ="";
            linkArvore.Font.Bold = true;
        }
        else if (_resultado)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkResultado.Visible = true;
            linkResultado.NavigateUrl ="";
            linkResultado.Font.Bold = true;
        }
        else if (_foco)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkFoco.Visible = true;
            linkFoco.NavigateUrl = "";
            linkFoco.Font.Bold = true;
        }
        else if (_premissas)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkPremissas.Visible = true;
            linkPremissas.NavigateUrl = "";
            linkPremissas.Font.Bold = true;
        }
        else if (_colaboradores)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkColaboradores.Visible = true;
            linkColaboradores.NavigateUrl = "";
            linkColaboradores.Font.Bold = true;
        }
        else if (_agenda)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkAgenda.Visible = true;
            linkAgenda.NavigateUrl = "";
            linkAgenda.Font.Bold = true;
        }
        else if (_noticias)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkNoticias.Visible = true;
            linkNoticias.NavigateUrl = "";
            linkNoticias.Font.Bold = true;
        }
        else if (_parceiros)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkParceiros.Visible = true;
            linkParceiros.NavigateUrl = "";
            linkParceiros.Font.Bold = true;
        }
        else if (_documentos)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkDocumentos.Visible = true;
            linkDocumentos.NavigateUrl = "";
            linkDocumentos.Font.Bold = true;
        }
        else if (_gerenciamento)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkGerenciamento.Visible = true;
            linkGerenciamento.NavigateUrl ="";
            linkGerenciamento.Font.Bold = true;
        }
        else if (_acao)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkGerenciamento.Visible = true;
            linkAcao.Visible = true;
            linkAcao.NavigateUrl ="";
            linkAcao.Font.Bold = true;
        }
        else if (_acaoGraf)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkGerenciamento.Visible = true;
            linkAcaoGraf.Visible = true;
            linkAcaoGraf.NavigateUrl = "";
            linkAcaoGraf.Font.Bold = true;

        }
        else if (_financeiro)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkGerenciamento.Visible = true;
            linkAcao.Visible = true;
            linkFinanceiro.Visible = true;
            linkFinanceiro.NavigateUrl = "";
            linkFinanceiro.Font.Bold = true;

        }
        else if (_produto)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkGerenciamento.Visible = true;
            linkAcao.Visible = true;
            linkProduto.Visible = true;
            linkProduto.NavigateUrl = "";
            linkProduto.Font.Bold = true;
        }
        else if (_restricao)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkGerenciamento.Visible = true;
            linkRestricao.Visible = true;
            linkRestricao.NavigateUrl = "";
            linkRestricao.Font.Bold = true;
        }
        else
        {
            linkFiltro.NavigateUrl = "";
            linkFiltro.Font.Bold = true;
        }

    }
}
