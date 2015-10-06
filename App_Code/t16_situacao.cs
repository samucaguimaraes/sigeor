/* Generated by VB & C#.NET Class Generator */

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t16_situacao
{
	#region Declarations
    pageBase pb = new pageBase();
	private int _t16_cd_situacao;
	private int _t03_cd_projeto;
	private string _ds_situacao;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	private bool _found;

	#endregion

	#region Properties

	public int t16_cd_situacao
	{
		get { return _t16_cd_situacao; }
		set { _t16_cd_situacao = value; }
	}

	public int t03_cd_projeto
	{
		get { return _t03_cd_projeto; }
		set { _t03_cd_projeto = value; }
	}

	public string ds_situacao
	{
		get { return _ds_situacao; }
		set { _ds_situacao = value; }
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
		SqlDataAdapter adp = new SqlDataAdapter("usp_t16_situacaoList", sqlConn);
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
        SqlCommand cmd = new SqlCommand("select * from t16_situacao where t03_cd_projeto=@t03_cd_projeto order by dt_cadastro desc", sqlConn);
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
				if (!Convert.IsDBNull(data["t16_cd_situacao"])) _t16_cd_situacao = (int) data["t16_cd_situacao"];
				if (!Convert.IsDBNull(data["t03_cd_projeto"])) _t03_cd_projeto = (int) data["t03_cd_projeto"];
				if (!Convert.IsDBNull(data["ds_situacao"])) _ds_situacao = (string) data["ds_situacao"];
				if (!Convert.IsDBNull(data["dt_cadastro"])) _dt_cadastro = (DateTime) data["dt_cadastro"];
				if (!Convert.IsDBNull(data["dt_alterado"])) _dt_alterado = (DateTime) data["dt_alterado"];
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
        SqlCommand cmd = new SqlCommand("insert into t16_situacao values(@t03_cd_projeto, @ds_situacao, @dt_cadastro, @dt_alterado)", sqlConn);
		bool result;

		cmd.Parameters.Add("@t03_cd_projeto", SqlDbType.Int).Value = _t03_cd_projeto;
		cmd.Parameters.Add("@ds_situacao", SqlDbType.Text).Value = _ds_situacao;
		cmd.Parameters.Add("@dt_cadastro", SqlDbType.DateTime).Value = _dt_cadastro;
		cmd.Parameters.Add("@dt_alterado", SqlDbType.DateTime).Value = _dt_alterado;

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
		SqlCommand cmd = new SqlCommand("usp_t16_situacaoUpdate", sqlConn);
		bool result;

		cmd.Parameters.Add("@t16_cd_situacao", SqlDbType.Int).Value = _t16_cd_situacao;
		cmd.Parameters.Add("@t03_cd_projeto", SqlDbType.Int).Value = _t03_cd_projeto;
		cmd.Parameters.Add("@ds_situacao", SqlDbType.Text).Value = _ds_situacao;
		cmd.Parameters.Add("@dt_cadastro", SqlDbType.DateTime).Value = _dt_cadastro;
		cmd.Parameters.Add("@dt_alterado", SqlDbType.DateTime).Value = _dt_alterado;

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
		SqlCommand cmd = new SqlCommand("usp_t16_situacaoDelete", sqlConn);
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
