/* Generated by VB & C#.NET Class Generator */

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t19_fase
{
	#region Declarations
    pageBase pb = new pageBase();
	private int _t19_cd_fase;
	private string _nm_fase;
	private string _ds_fase;
	private string _fl_fase;
	private string _nm_arquivo;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	private bool _fl_ativa;
    private string _order;
	private bool _found;

	#endregion

	#region Properties

	public int t19_cd_fase
	{
		get { return _t19_cd_fase; }
		set { _t19_cd_fase = value; }
	}

	public string nm_fase
	{
		get { return _nm_fase; }
		set { _nm_fase = value; }
	}

	public string ds_fase
	{
		get { return _ds_fase; }
		set { _ds_fase = value; }
	}

	public string fl_fase
	{
		get { return _fl_fase; }
		set { _fl_fase = value; }
	}

	public string nm_arquivo
	{
		get { return _nm_arquivo; }
		set { _nm_arquivo = value; }
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
		SqlDataAdapter adp = new SqlDataAdapter("select * from t19_fase "+ _order, sqlConn);
		DataSet ds = new DataSet();

		try
		{
			sqlConn.Open();
			adp.SelectCommand.CommandType = CommandType.Text;
			adp.Fill(ds);
		}

        catch { System.Web.HttpContext.Current.Response.Write(adp.SelectCommand); }

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
        SqlCommand cmd = new SqlCommand("select * from t19_fase where fl_fase=@fl_fase", sqlConn);
        cmd.Parameters.Add("@fl_fase", SqlDbType.Char, 2).Value = _fl_fase;
		SqlDataReader data;

		try
		{
			sqlConn.Open();
			cmd.CommandType = CommandType.Text;
			data = cmd.ExecuteReader();

			if (data.Read())
			{
				_found = true;
				if (!Convert.IsDBNull(data["t19_cd_fase"])) _t19_cd_fase = (int) data["t19_cd_fase"];
				if (!Convert.IsDBNull(data["nm_fase"])) _nm_fase = (string) data["nm_fase"];
				if (!Convert.IsDBNull(data["ds_fase"])) _ds_fase = (string) data["ds_fase"];
				if (!Convert.IsDBNull(data["fl_fase"])) _fl_fase = (string) data["fl_fase"];
				if (!Convert.IsDBNull(data["nm_arquivo"])) _nm_arquivo = (string) data["nm_arquivo"];
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

    public void RetrieveFaseProjeto()
    {
        SqlConnection sqlConn = new SqlConnection(pb.strConn());
        SqlCommand cmd = new SqlCommand("select * from t19_fase where t19_cd_fase in (select t19_cd_fase from t20_faseprojeto where t03_cd_projeto=@t19_cd_fase and fl_ativa=1)", sqlConn);
        cmd.Parameters.Add("@t19_cd_fase", SqlDbType.Int).Value = _t19_cd_fase; // usando o t03_cd_projeto
        SqlDataReader data;

        try
        {
            sqlConn.Open();
            cmd.CommandType = CommandType.Text;
            data = cmd.ExecuteReader();

            if (data.Read())
            {
                _found = true;
                if (!Convert.IsDBNull(data["t19_cd_fase"])) _t19_cd_fase = (int)data["t19_cd_fase"];
                if (!Convert.IsDBNull(data["nm_fase"])) _nm_fase = (string)data["nm_fase"];
                if (!Convert.IsDBNull(data["ds_fase"])) _ds_fase = (string)data["ds_fase"];
                if (!Convert.IsDBNull(data["fl_fase"])) _fl_fase = (string)data["fl_fase"];
                if (!Convert.IsDBNull(data["nm_arquivo"])) _nm_arquivo = (string)data["nm_arquivo"];
                if (!Convert.IsDBNull(data["dt_cadastro"])) _dt_cadastro = (DateTime)data["dt_cadastro"];
                if (!Convert.IsDBNull(data["dt_alterado"])) _dt_alterado = (DateTime)data["dt_alterado"];
                if (!Convert.IsDBNull(data["fl_ativa"])) _fl_ativa = (bool)data["fl_ativa"];
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
    public void RetrieveFase()
    {
        SqlConnection sqlConn = new SqlConnection(pb.strConn());
        SqlCommand cmd = new SqlCommand("select * from t19_fase where t19_cd_fase=@t19_cd_fase", sqlConn);
        cmd.Parameters.Add("@t19_cd_fase", SqlDbType.Int).Value = _t19_cd_fase;
        SqlDataReader data;

        try
        {
            sqlConn.Open();
            cmd.CommandType = CommandType.Text;
            data = cmd.ExecuteReader();

            if (data.Read())
            {
                _found = true;
                if (!Convert.IsDBNull(data["t19_cd_fase"])) _t19_cd_fase = (int)data["t19_cd_fase"];
                if (!Convert.IsDBNull(data["nm_fase"])) _nm_fase = (string)data["nm_fase"];
                if (!Convert.IsDBNull(data["ds_fase"])) _ds_fase = (string)data["ds_fase"];
                if (!Convert.IsDBNull(data["fl_fase"])) _fl_fase = (string)data["fl_fase"];
                if (!Convert.IsDBNull(data["nm_arquivo"])) _nm_arquivo = (string)data["nm_arquivo"];
                if (!Convert.IsDBNull(data["dt_cadastro"])) _dt_cadastro = (DateTime)data["dt_cadastro"];
                if (!Convert.IsDBNull(data["dt_alterado"])) _dt_alterado = (DateTime)data["dt_alterado"];
                if (!Convert.IsDBNull(data["fl_ativa"])) _fl_ativa = (bool)data["fl_ativa"];
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
		SqlCommand cmd = new SqlCommand("usp_t19_faseInsert", sqlConn);
		bool result;

		cmd.Parameters.Add("@nm_fase", SqlDbType.VarChar, 200).Value = _nm_fase;
		cmd.Parameters.Add("@ds_fase", SqlDbType.Text).Value = _ds_fase;
		cmd.Parameters.Add("@fl_fase", SqlDbType.Char, 2).Value = _fl_fase;
		cmd.Parameters.Add("@nm_arquivo", SqlDbType.VarChar, 200).Value = _nm_arquivo;
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
		SqlCommand cmd = new SqlCommand("usp_t19_faseUpdate", sqlConn);
		bool result;

		cmd.Parameters.Add("@t19_cd_fase", SqlDbType.Int).Value = _t19_cd_fase;
		cmd.Parameters.Add("@nm_fase", SqlDbType.VarChar, 200).Value = _nm_fase;
		cmd.Parameters.Add("@ds_fase", SqlDbType.Text).Value = _ds_fase;
		cmd.Parameters.Add("@fl_fase", SqlDbType.Char, 2).Value = _fl_fase;
		cmd.Parameters.Add("@nm_arquivo", SqlDbType.VarChar, 200).Value = _nm_arquivo;
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

	#region Delete

	public bool Delete()
	{
		SqlConnection sqlConn = new SqlConnection(pb.strConn());
		SqlCommand cmd = new SqlCommand("usp_t19_faseDelete", sqlConn);
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
