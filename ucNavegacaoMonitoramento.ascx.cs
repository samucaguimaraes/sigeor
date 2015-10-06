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

public partial class ucNavegacaoMonitoramento : System.Web.UI.UserControl
{
    bool _painel;
    bool _restricao;
    bool _alavancagem;
    bool _fingraf;
    bool _fisgraf;
    bool _finind;
    bool _fisind;
    bool _acoes;
    bool _media;
    bool _parceiros;
    bool _projetos;

    public bool restricao
    {
        get { return _restricao; }
        set { _restricao = value; }
    }
    public bool painel
    {
        get { return _painel; }
        set { _painel = value; }
    }
    public bool alavancagem
    {
        get { return _alavancagem; }
        set { _alavancagem = value; }
    }
    public bool fingraf
    {
        get { return _fingraf; }
        set { _fingraf = value; }
    }
    public bool fisgraf
    {
        get { return _fisgraf; }
        set { _fisgraf = value; }
    }
    public bool finind
    {
        get { return _finind; }
        set { _finind = value; }
    }
    public bool fisind
    {
        get { return _fisind; }
        set { _fisind = value; }
    }
    public bool acoes
    {
        get { return _acoes; }
        set { _acoes = value; }
    }
    public bool media
    {
        get { return _media; }
        set { _media = value; }
    }
    public bool parceiros
    {
        get { return _parceiros; }
        set { _parceiros = value; }
    }
    public bool projetos
    {
        get { return _projetos; }
        set { _projetos = value; }
    }
  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (_painel)
        {
            linkPainel.Visible = true;
            linkPainel.NavigateUrl = "";
            linkPainel.Font.Bold = true;
        }
        else if (_restricao)
        {
            linkPainel.Visible = true;
            linkMonRestricoes.Visible = true;
            linkMonRestricoes.NavigateUrl = "";
            linkMonRestricoes.Font.Bold = true;
        }
        else if (_alavancagem)
        {
            linkPainel.Visible = true;
            linkMonAlavancagem.Visible = true;
            linkMonAlavancagem.NavigateUrl = "";
            linkMonAlavancagem.Font.Bold = true;
        }
        else if (_fingraf)
        {
            linkPainel.Visible = true;
            linkMonFinanceiroGraf.Visible = true;
            linkMonFinanceiroGraf.NavigateUrl = "";
            linkMonFinanceiroGraf.Font.Bold = true;
        }
        else if (_fisgraf)
        {
            linkPainel.Visible = true;
            linkMonFisicoGraf.Visible = true;
            linkMonFisicoGraf.NavigateUrl = "";
            linkMonFisicoGraf.Font.Bold = true;
        }
        else if (_finind)
        {
            linkPainel.Visible = true;
            linkMonFinanceiroInd.Visible = true;
            linkMonFinanceiroInd.NavigateUrl = "";
            linkMonFinanceiroInd.Font.Bold = true;
        }
        else if (_fisind)
        {
            linkPainel.Visible = true;
            linkMonFisicoInd.Visible = true;
            linkMonFisicoInd.NavigateUrl = "";
            linkMonFisicoInd.Font.Bold = true;
        }
        else if (_acoes)
        {
            linkPainel.Visible = true;
            linkMonMarcos.Visible = true;
            linkMonMarcos.NavigateUrl = "";
            linkMonMarcos.Font.Bold = true;
        }
        else if (_media)
        {
            linkPainel.Visible = true;
            linkMonMediaDias.Visible = true;
            linkMonMediaDias.NavigateUrl = "";
            linkMonMediaDias.Font.Bold = true;
        }
        else if (_parceiros)
        {
            linkPainel.Visible = true;
            linkMonParceiros.Visible = true;
            linkMonParceiros.NavigateUrl = "";
            linkMonParceiros.Font.Bold = true;
        }
        else if (_projetos)
        {
            linkPainel.Visible = true;
            linkMonProjetos.Visible = true;
            linkMonProjetos.NavigateUrl = "";
            linkMonProjetos.Font.Bold = true;
        }
        else
        {
            linkFiltro.NavigateUrl = "";
            linkFiltro.Font.Bold = true;
        }
    }
}
