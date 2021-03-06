/* Generated by VB & C#.NET Class Generator */

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t13_foco
{
	#region Declarations
    pageBase pb = new pageBase();
	private int _t13_cd_foco;
	private int _t03_cd_projeto;
	private string _nm_foco;
	private int _nu_ordem;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	private bool _fl_ativa;
    private string _order;
	private bool _found;

	#endregion

	#region Properties

	public int t13_cd_foco
	{
		get { return _t13_cd_foco; }
		set { _t13_cd_foco = value; }
	}

	public int t03_cd_projeto
	{
		get { return _t03_cd_projeto; }
		set { _t03_cd_projeto = value; }
	}

	public string nm_foco
	{
		get { return _nm_foco; }
		set { _nm_foco = value; }
	}

	public int nu_ordem
	{
		get { return _nu_ordem; }
		set { _nu_ordem = value; }
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

	#endregion

	#region Functions/Routines

	#region List

	public DataSet List()
	{
		SqlConnection sqlConn = new SqlConnection(pb.strConn());
        SqlDataAdapter adp = new SqlDataAdapter("select * from t13_foco where t03_cd_projeto=@t03_cd_projeto AND fl_ativa=1" + _order, sqlConn);
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

	#endregion

	#region Retrieve

	public void Retrieve()
	{
		SqlConnection sqlConn = new SqlConnection(pb.strConn());
        SqlCommand cmd = new SqlCommand("select * from t13_foco where t13_cd_foco=@t13_cd_foco AND fl_ativa=1", sqlConn);
        cmd.Parameters.Add("@t13_cd_foco", SqlDbType.Int).Value = _t13_cd_foco;
		SqlDataReader data;

		try
		{
			sqlConn.Open();
			cmd.CommandType = CommandType.Text;
			data = cmd.ExecuteReader();

			if (data.Read())
			{
				_found = true;
				if (!Convert.IsDBNull(data["t13_cd_foco"])) _t13_cd_foco = (int) data["t13_cd_foco"];
				if (!Convert.IsDBNull(data["t03_cd_projeto"])) _t03_cd_projeto = (int) data["t03_cd_projeto"];
				if (!Convert.IsDBNull(data["nm_foco"])) _nm_foco = (string) data["nm_foco"];
				if (!Convert.IsDBNull(data["nu_ordem"])) _nu_ordem = (int) data["nu_ordem"];
				if (!Convert.IsDBNull(data["dt_cadastro"])) _dt_cadastro = (DateTime) data["dt_cadastro"];
				if (!Convert.IsDBNull(data["dt_alterado"])) _dt_alterado = (DateTime) data["dt_alterado"];
				if (!Convert.IsDBNull(data["fl_ativa"])) _fl_ativa = (bool) data["fl_ativa"];
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

	#endregion

	#region Save

	public bool Save()
	{
		SqlConnection sqlConn = new SqlConnection(pb.strConn());
        SqlCommand cmd = new SqlCommand("insert into t13_foco values(@t03_cd_projeto, @nm_foco, null, @dt_cadastro, @dt_alterado, 1)", sqlConn);
		bool result;

		cmd.Parameters.Add("@t03_cd_projeto", SqlDbType.Int).Value = _t03_cd_projeto;
		cmd.Parameters.Add("@nm_foco", SqlDbType.VarChar, 500).Value = _nm_foco;
		cmd.Parameters.Add("@nu_ordem", SqlDbType.Int).Value = _nu_ordem;
		cmd.Parameters.Add("@dt_cadastro", SqlDbType.DateTime).Value = _dt_cadastro;
		cmd.Parameters.Add("@dt_alterado", SqlDbType.DateTime).Value = _dt_alterado;
		cmd.Parameters.Add("@fl_ativa", SqlDbType.Bit).Value = _fl_ativa;

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
        SqlCommand cmd = new SqlCommand("update t13_foco set nm_foco=@nm_foco, dt_alterado=@dt_alterado where t13_cd_foco=@t13_cd_foco", sqlConn);
		bool result;

		cmd.Parameters.Add("@t13_cd_foco", SqlDbType.Int).Value = _t13_cd_foco;
		//cmd.Parameters.Add("@t03_cd_projeto", SqlDbType.Int).Value = _t03_cd_projeto;
		cmd.Parameters.Add("@nm_foco", SqlDbType.VarChar, 500).Value = _nm_foco;
		//cmd.Parameters.Add("@nu_ordem", SqlDbType.Int).Value = _nu_ordem;
		//cmd.Parameters.Add("@dt_cadastro", SqlDbType.DateTime).Value = _dt_cadastro;
		cmd.Parameters.Add("@dt_alterado", SqlDbType.DateTime).Value = _dt_alterado;
		//cmd.Parameters.Add("@fl_ativa", SqlDbType.Bit).Value = _fl_ativa;

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
        SqlCommand cmd = new SqlCommand("update t13_foco set fl_ativa=0 where t13_cd_foco=@t13_cd_foco", sqlConn);
        cmd.Parameters.Add("@t13_cd_foco", SqlDbType.Int).Value = _t13_cd_foco;
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
