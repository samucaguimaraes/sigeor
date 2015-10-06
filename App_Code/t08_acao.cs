/* Generated by VB & C#.NET Class Generator */

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t08_acao
{
	#region Declarations
    pageBase pb = new pageBase();
	private int _t08_cd_acao;
	private int _t03_cd_projeto;
	private string _t02_cd_usuario;
	private string _nm_acao;
	private string _ds_acao;
	private DateTime _dt_inicio;
	private DateTime _dt_fim;
	private DateTime _dt_original;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	private bool _fl_ativa;
    private string _order;
	private bool _found;
    private string _ds_palvo;
    private string _ds_andamento;
	private string _ds_latuacao;
	private string _ds_parceiro;
	private DateTime _dt_aviso;		
	private string _ds_ano;
	private string _cd_programa;
	private string _ds_setor;

	private string _nu_acao;
	private string _ds_compromisso;
	private string _ds_subacao;
	private string _vl_orcado;
	private string _ds_fonte;
	private string _ds_meta;

	#endregion

	#region Properties

	public int t08_cd_acao
	{
		get { return _t08_cd_acao; }
		set { _t08_cd_acao = value; }
	}

	public int t03_cd_projeto
	{
		get { return _t03_cd_projeto; }
		set { _t03_cd_projeto = value; }
	}

	public string t02_cd_usuario
	{
		get { return _t02_cd_usuario; }
		set { _t02_cd_usuario = value; }
	}

	public string nm_acao
	{
		get { return _nm_acao; }
		set { _nm_acao = value; }
	}

	public string ds_acao
	{
		get { return _ds_acao; }
		set { _ds_acao = value; }
	}

	public DateTime dt_inicio
	{
		get { return _dt_inicio; }
		set { _dt_inicio = value; }
	}

	public DateTime dt_fim
	{
		get { return _dt_fim; }
		set { _dt_fim = value; }
	}
	
	
	public DateTime dt_original
	{
		get { return _dt_original; }
		set { _dt_original = value; }
	}

	public DateTime dt_cadastro
	{
		get { return _dt_cadastro; }
		set { _dt_cadastro = value; }
	}

	public DateTime dt_alterado
	{
		get { return _dt_alterado; }
		set { _dt_alterado = value; }
	}

	public bool fl_ativa
	{
		get { return _fl_ativa; }
		set { _fl_ativa = value; }
	}
    public string order
    {
        get { return _order; }
        set { _order = value; }
    }
	public bool Found
	{
		get { return _found; }
	}

    public string ds_palvo
    {
        get { return _ds_palvo; }
        set { _ds_palvo = value; }
    }
    public string ds_andamento
    {
        get { return _ds_andamento; }
        set { _ds_andamento = value; }
    }
	 public string ds_latuacao
    {
        get { return _ds_latuacao; }
        set { _ds_latuacao = value; }
    }
	public string ds_parceiro
    {
        get { return _ds_parceiro; }
        set { _ds_parceiro = value; }
    }

	public DateTime dt_aviso
	{
		get { return _dt_aviso; }
		set { _dt_aviso = value; }
	}	
	
	public string ds_ano
    {
        get { return _ds_ano; }
        set { _ds_ano = value; }
    }
	public string ds_setor
    {
        get { return _ds_setor; }
        set { _ds_setor = value; }
    }
    public string cd_programa
	{
		get { return _cd_programa; }
		set { _cd_programa = value; }
	}

	public string nu_acao
	{
		get { return _nu_acao; }
		set { _nu_acao = value; }
	}
	public string ds_compromisso
	{
		get { return _ds_compromisso; }
		set { _ds_compromisso = value; }
	}
	public string ds_subacao
	{
		get { return _ds_subacao; }
		set { _ds_subacao = value; }
	}
	public string vl_orcado
	{
		get { return _vl_orcado; }
		set { _vl_orcado = value; }
	}
	public string ds_fonte
	{
		get { return _ds_fonte; }
		set { _ds_fonte = value; }
	}
	public string ds_meta
	{
		get { return _ds_meta; }
		set { _ds_meta = value; }
	}
	#endregion

	#region Functions/Routines

	#region List
	//dataset list2 levi criou para arquivo de consulta 2014
	public DataSet List2()
	{
		SqlConnection sqlConn = new SqlConnection(pb.strConn());
        SqlDataAdapter adp = new SqlDataAdapter("select * from t08_acao where ds_ano like '2014' and t08.t03_cd_projeto=@t03_cd_projeto " + _order, sqlConn);
        adp.SelectCommand.Parameters.Add("@t03_cd_projeto", SqlDbType.Int).Value = _t03_cd_projeto;
		DataSet ds = new DataSet();

		try
		{
			sqlConn.Open();
			adp.SelectCommand.CommandType = CommandType.Text;
			adp.Fill(ds);
		}

		catch (Exception ex) { System.Web.HttpContext.Current.Response.Write(ex.Message);  }

		finally
		{
			sqlConn.Close();
			adp.Dispose();
			sqlConn.Dispose();
		}

		return ds;
	}	
	
	


	public DataSet List()
	{
		SqlConnection sqlConn = new SqlConnection(pb.strConn());
        SqlDataAdapter adp = new SqlDataAdapter("select * from t08_acao where fl_ativa=1 and t03_cd_projeto=@t03_cd_projeto " + _order, sqlConn);
        adp.SelectCommand.Parameters.Add("@t03_cd_projeto", SqlDbType.Int).Value = _t03_cd_projeto;
		DataSet ds = new DataSet();

		try
		{
			sqlConn.Open();
			adp.SelectCommand.CommandType = CommandType.Text;
			adp.Fill(ds);
		}

		catch (Exception ex) { System.Web.HttpContext.Current.Response.Write(ex.Message);  }

		finally
		{
			sqlConn.Close();
			adp.Dispose();
			sqlConn.Dispose();
		}

		return ds;
	}

    public DataSet ListAlt()
    {
        SqlConnection sqlConn = new SqlConnection(pb.strConn());
        SqlDataAdapter adp = new SqlDataAdapter("select min(nu_ano) as menor, max(nu_ano) as maior " +
            "from t28_vlfinanceiro where t11_cd_financeiro in " +
            "(select t11_cd_financeiro from t11_financeiro where t08_cd_acao in " +
            "(select t08_cd_acao from t08_acao where t08_cd_acao = " + _t08_cd_acao + "))", sqlConn);
        //adp.SelectCommand.Parameters.Add("@t11_cd_financeiro", SqlDbType.Int).Value = _t11_cd_financeiro;
        DataSet ds = new DataSet();

        try
        {
            sqlConn.Open();
            adp.SelectCommand.CommandType = CommandType.Text;
            adp.Fill(ds);
        }

        catch { throw; }

        finally
        {
            sqlConn.Close();
            adp.Dispose();
            sqlConn.Dispose();
        }

        return ds;
    }


	#endregion

	#region Retrieve

	public void Retrieve()
	{
		SqlConnection sqlConn = new SqlConnection(pb.strConn());
        SqlCommand cmd = new SqlCommand("select * from t08_acao where t08_cd_acao=@t08_cd_acao", sqlConn);
        cmd.Parameters.Add("@t08_cd_acao", SqlDbType.Int).Value = _t08_cd_acao;
		SqlDataReader data;

		try
		{
			sqlConn.Open();
			cmd.CommandType = CommandType.Text;
			data = cmd.ExecuteReader();

			if (data.Read())
			{
				_found = true;
				if (!Convert.IsDBNull(data["t08_cd_acao"])) _t08_cd_acao = (int) data["t08_cd_acao"];
				if (!Convert.IsDBNull(data["t03_cd_projeto"])) _t03_cd_projeto = (int) data["t03_cd_projeto"];
				if (!Convert.IsDBNull(data["t02_cd_usuario"])) _t02_cd_usuario = (string) data["t02_cd_usuario"];
				if (!Convert.IsDBNull(data["nm_acao"])) _nm_acao = (string) data["nm_acao"];
				if (!Convert.IsDBNull(data["ds_acao"])) _ds_acao = (string) data["ds_acao"];
				if (!Convert.IsDBNull(data["dt_inicio"])) _dt_inicio = (DateTime) data["dt_inicio"];
				if (!Convert.IsDBNull(data["dt_fim"])) _dt_fim = (DateTime) data["dt_fim"];				
				if (!Convert.IsDBNull(data["dt_original"])) _dt_original = (DateTime) data["dt_original"];
				if (!Convert.IsDBNull(data["dt_cadastro"])) _dt_cadastro = (DateTime) data["dt_cadastro"];
				if (!Convert.IsDBNull(data["dt_alterado"])) _dt_alterado = (DateTime) data["dt_alterado"];
				if (!Convert.IsDBNull(data["fl_ativa"])) _fl_ativa = (bool) data["fl_ativa"];
                if (!Convert.IsDBNull(data["ds_palvo"])) _ds_palvo = (string) data["ds_palvo"];
				if (!Convert.IsDBNull(data["ds_andamento"])) _ds_andamento = (string) data["ds_andamento"];
				if (!Convert.IsDBNull(data["ds_latuacao"])) _ds_latuacao = (string) data["ds_latuacao"];
				if (!Convert.IsDBNull(data["ds_parceiro"])) _ds_parceiro = (string) data["ds_parceiro"];
				if (!Convert.IsDBNull(data["dt_aviso"])) _dt_aviso = (DateTime) data["dt_aviso"];
                if (!Convert.IsDBNull(data["ds_ano"])) _ds_ano = (string)data["ds_ano"];
                if (!Convert.IsDBNull(data["ds_setor"])) _ds_setor = (string)data["ds_setor"];
                if (!Convert.IsDBNull(data["cd_programa"])) _cd_programa = (string) data["cd_programa"];

                if (!Convert.IsDBNull(data["nu_acao"])) _nu_acao = (string) data["nu_acao"];
                if (!Convert.IsDBNull(data["ds_compromisso"])) _ds_compromisso = (string) data["ds_compromisso"];
                if (!Convert.IsDBNull(data["ds_subacao"])) _ds_subacao = (string) data["ds_subacao"];
                if (!Convert.IsDBNull(data["vl_orcado"])) _vl_orcado = (string) data["vl_orcado"];
                if (!Convert.IsDBNull(data["ds_fonte"])) _ds_fonte = (string) data["ds_fonte"];
                if (!Convert.IsDBNull(data["ds_meta"])) _ds_meta = (string) data["ds_meta"];				
			}
		}

		catch (Exception ex) { System.Web.HttpContext.Current.Response.Write(ex.Message);  }

		finally
		{
			sqlConn.Close();
			cmd.Dispose();
			sqlConn.Dispose();
		}
	}
    public void RetrieveCod()
    {
        SqlConnection sqlConn = new SqlConnection(pb.strConn());
        SqlCommand cmd = new SqlCommand("select max(t08_cd_acao) as t08_cd_acao from t08_acao where t03_cd_projeto=@t03_cd_projeto", sqlConn);
        cmd.Parameters.Add("@t03_cd_projeto", SqlDbType.Int).Value = _t03_cd_projeto;
        SqlDataReader data;

        try
        {
            sqlConn.Open();
            cmd.CommandType = CommandType.Text;
            data = cmd.ExecuteReader();

            if (data.Read())
            {
                _found = true;
                if (!Convert.IsDBNull(data["t08_cd_acao"])) _t08_cd_acao = (int)data["t08_cd_acao"];
            }
        }

        catch (Exception ex) { System.Web.HttpContext.Current.Response.Write(ex.Message); }

        finally
        {
            sqlConn.Close();
            cmd.Dispose();
            sqlConn.Dispose();
        }
    }
	#endregion

	#region Save

	public bool Save()
	{
		SqlConnection sqlConn = new SqlConnection(pb.strConn());
        SqlCommand cmd = new SqlCommand("insert into t08_acao values(@t03_cd_projeto,"+
            "'plima', @nm_acao, @ds_acao, @dt_inicio, @dt_fim, @dt_original, @dt_cadastro, @dt_alterado, 1, @ds_palvo, @ds_andamento, @ds_latuacao, @ds_parceiro, @dt_aviso, @ds_ano, @ds_setor, @cd_programa, @nu_acao, @ds_compromisso, @ds_subacao, @vl_orcado, @ds_fonte, @ds_meta)", sqlConn);
		bool result;

		cmd.Parameters.Add("@t03_cd_projeto", SqlDbType.Int).Value = _t03_cd_projeto;
		cmd.Parameters.Add("@t02_cd_usuario", SqlDbType.VarChar, 20).Value = _t02_cd_usuario;
		cmd.Parameters.Add("@nm_acao", SqlDbType.VarChar, 500).Value = _nm_acao;
		cmd.Parameters.Add("@ds_acao", SqlDbType.Text).Value = _ds_acao;
		cmd.Parameters.Add("@dt_inicio", SqlDbType.DateTime).Value = _dt_inicio;
		cmd.Parameters.Add("@dt_fim", SqlDbType.DateTime).Value = _dt_fim;	
		cmd.Parameters.Add("@dt_original", SqlDbType.DateTime).Value = _dt_original;
		cmd.Parameters.Add("@dt_cadastro", SqlDbType.DateTime).Value = _dt_cadastro;
		cmd.Parameters.Add("@dt_alterado", SqlDbType.DateTime).Value = _dt_alterado;
		//cmd.Parameters.Add("@fl_ativa", SqlDbType.Bit).Value = _fl_ativa;
        cmd.Parameters.Add("@ds_palvo", SqlDbType.Text).Value = _ds_palvo;
		cmd.Parameters.Add("@ds_andamento", SqlDbType.Text).Value = _ds_andamento;
		cmd.Parameters.Add("@ds_latuacao", SqlDbType.Text).Value = _ds_latuacao;
		cmd.Parameters.Add("@ds_parceiro", SqlDbType.Text).Value = _ds_parceiro;
		cmd.Parameters.Add("@dt_aviso", SqlDbType.DateTime).Value = _dt_aviso;	
        cmd.Parameters.Add("@ds_ano", SqlDbType.Text).Value = _ds_ano;		
        cmd.Parameters.Add("@ds_setor", SqlDbType.Text).Value = _ds_setor;
        cmd.Parameters.Add("@cd_programa", SqlDbType.Text).Value = _cd_programa;

        cmd.Parameters.Add("@nu_acao", SqlDbType.Text).Value = _nu_acao;
        cmd.Parameters.Add("@ds_compromisso", SqlDbType.Text).Value = _ds_compromisso;
        cmd.Parameters.Add("@ds_subacao", SqlDbType.Text).Value = _ds_subacao;
        cmd.Parameters.Add("@vl_orcado", SqlDbType.Text).Value = _vl_orcado;
        cmd.Parameters.Add("@ds_fonte", SqlDbType.Text).Value = _ds_fonte;
        cmd.Parameters.Add("@ds_meta", SqlDbType.Text).Value = _ds_meta;		

		
		try
		{
			sqlConn.Open();
			cmd.CommandType = CommandType.Text;
			result = Convert.ToBoolean(cmd.ExecuteNonQuery());
		}

		catch (Exception ex) { System.Web.HttpContext.Current.Response.Write(ex.Message); result = false; }

		finally
		{
			sqlConn.Close();
			cmd.Dispose();
			sqlConn.Dispose();
		}

		return result;
	}

	#endregion

	#region Update

	public bool Update()
	{
		SqlConnection sqlConn = new SqlConnection(pb.strConn());
        SqlCommand cmd = new SqlCommand("update t08_acao set t02_cd_usuario=@t02_cd_usuario, "+
            "nm_acao=@nm_acao, ds_acao=@ds_acao, dt_inicio=@dt_inicio, dt_fim=@dt_fim, dt_original=@dt_original, "+
             "dt_cadastro=@dt_cadastro, dt_alterado=@dt_alterado, ds_palvo=@ds_palvo, ds_andamento=@ds_andamento, ds_latuacao=@ds_latuacao, ds_parceiro=@ds_parceiro,dt_aviso=@dt_aviso, ds_ano=@ds_ano, ds_setor=@ds_setor, cd_programa=@cd_programa, nu_acao=@nu_acao, ds_compromisso=@ds_compromisso, ds_subacao=@ds_subacao, vl_orcado=@vl_orcado, ds_fonte=@ds_fonte, ds_meta=@ds_meta where t08_cd_acao=@t08_cd_acao", sqlConn);
		bool result;

		cmd.Parameters.Add("@t08_cd_acao", SqlDbType.Int).Value = _t08_cd_acao;
		//cmd.Parameters.Add("@t03_cd_projeto", SqlDbType.Int).Value = _t03_cd_projeto;
		cmd.Parameters.Add("@t02_cd_usuario", SqlDbType.VarChar, 20).Value = _t02_cd_usuario;
		cmd.Parameters.Add("@nm_acao", SqlDbType.VarChar, 500).Value = _nm_acao;
		cmd.Parameters.Add("@ds_acao", SqlDbType.Text).Value = _ds_acao;
		cmd.Parameters.Add("@dt_inicio", SqlDbType.DateTime).Value = _dt_inicio;
		cmd.Parameters.Add("@dt_fim", SqlDbType.DateTime).Value = _dt_fim;
		
		cmd.Parameters.Add("@dt_original", SqlDbType.DateTime).Value = _dt_original;
		cmd.Parameters.Add("@dt_cadastro", SqlDbType.DateTime).Value = _dt_cadastro;
		cmd.Parameters.Add("@dt_alterado", SqlDbType.DateTime).Value = _dt_alterado;
		//cmd.Parameters.Add("@fl_ativa", SqlDbType.Bit).Value = _fl_ativa;
        cmd.Parameters.Add("@ds_palvo", SqlDbType.Text).Value = _ds_palvo;
		cmd.Parameters.Add("@ds_andamento", SqlDbType.Text).Value = _ds_andamento;
		cmd.Parameters.Add("@ds_latuacao", SqlDbType.Text).Value = _ds_latuacao;
		cmd.Parameters.Add("@ds_parceiro", SqlDbType.Text).Value = _ds_parceiro;
		cmd.Parameters.Add("@dt_aviso", SqlDbType.DateTime).Value = _dt_aviso;
        cmd.Parameters.Add("@ds_ano", SqlDbType.Text).Value = _ds_ano;	
        cmd.Parameters.Add("@ds_setor", SqlDbType.Text).Value = _ds_setor;	
        cmd.Parameters.Add("@cd_programa", SqlDbType.Text).Value = _cd_programa;

        cmd.Parameters.Add("@nu_acao", SqlDbType.Text).Value = _nu_acao;
        cmd.Parameters.Add("@ds_compromisso", SqlDbType.Text).Value = _ds_compromisso;
        cmd.Parameters.Add("@ds_subacao", SqlDbType.Text).Value = _ds_subacao;
        cmd.Parameters.Add("@vl_orcado", SqlDbType.Text).Value = _vl_orcado;
        cmd.Parameters.Add("@ds_fonte", SqlDbType.Text).Value = _ds_fonte;
        cmd.Parameters.Add("@ds_meta", SqlDbType.Text).Value = _ds_meta;
	
		try
		{
			sqlConn.Open();
			cmd.CommandType = CommandType.Text;
			result = Convert.ToBoolean(cmd.ExecuteNonQuery());
		}

		catch (Exception ex) { System.Web.HttpContext.Current.Response.Write(ex.Message); result = false; }

		finally
		{
			sqlConn.Close();
			cmd.Dispose();
			sqlConn.Dispose();
		}

		return result;
	}

	#endregion

	#region Delete

	public bool Delete()
	{
		SqlConnection sqlConn = new SqlConnection(pb.strConn());
        SqlCommand cmd = new SqlCommand("update t08_acao set fl_ativa=0, dt_alterado=getdate()  where t08_cd_acao=@t08_cd_acao", sqlConn);
        cmd.Parameters.Add("@t08_cd_acao", SqlDbType.Int).Value = _t08_cd_acao;
		bool result;

		try
		{
			sqlConn.Open();
			cmd.CommandType = CommandType.Text;
			result = Convert.ToBoolean(cmd.ExecuteNonQuery());
		}

		catch (Exception ex) { System.Web.HttpContext.Current.Response.Write(ex.Message); result = false; }

		finally
		{
			sqlConn.Close();
			cmd.Dispose();
			sqlConn.Dispose();
		}

		return result;
	}


    public bool DeleteAlt()
    {
        SqlConnection sqlConn = new SqlConnection(pb.strConn());
        SqlCommand cmd = new SqlCommand("delete t28_vlfinanceiro where  t11_cd_financeiro in " +
            "(select t11_cd_financeiro from t11_financeiro where t08_cd_acao in " +
            "(select t08_cd_acao from t08_acao where t08_cd_acao = @t08_cd_acao )) and "+ _order +"", sqlConn);
        cmd.Parameters.Add("@t08_cd_acao", SqlDbType.Int).Value = _t08_cd_acao;
        bool result;

        try
        {
            sqlConn.Open();
            cmd.CommandType = CommandType.Text;
            result = Convert.ToBoolean(cmd.ExecuteNonQuery());
        }

        catch (Exception ex) { System.Web.HttpContext.Current.Response.Write(ex.Message); result = false; }

        finally
        {
            sqlConn.Close();
            cmd.Dispose();
            sqlConn.Dispose();
        }

        return result;
    }

	#endregion

	#endregion
}
