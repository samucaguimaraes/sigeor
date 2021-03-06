/* Generated by VB & C#.NET Class Generator */

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t24_agenda
{
	#region Declarations
    pageBase pb = new pageBase();
	private int _t24_cd_agenda;
    private int _t03_cd_projeto;
	private string _nm_agenda;
	private string _ds_agenda;
	private DateTime _dt_data;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	private bool _fl_ativa;
    private string _order;
	private bool _found;

	#endregion

	#region Properties

	public int t24_cd_agenda
	{
		get { return _t24_cd_agenda; }
		set { _t24_cd_agenda = value; }
	}

    public int t03_cd_projeto
    {
        get { return _t03_cd_projeto; }
        set { _t03_cd_projeto = value; }
    }

	public string nm_agenda
	{
		get { return _nm_agenda; }
		set { _nm_agenda = value; }
	}

	public string ds_agenda
	{
		get { return _ds_agenda; }
		set { _ds_agenda = value; }
	}

	public DateTime dt_data
	{
		get { return _dt_data; }
		set { _dt_data = value; }
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
        SqlDataAdapter adp = new SqlDataAdapter("select * from t24_agenda where t03_cd_projeto=@t03_cd_projeto and fl_ativa=1 "+ _order, sqlConn);
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
    public DataSet ListProjetos()
    {
        SqlConnection sqlConn = new SqlConnection(pb.strConn());
        SqlDataAdapter adp = new SqlDataAdapter("select * from t24_agenda where fl_ativa=1 " + _order, sqlConn);
        DataSet ds = new DataSet();

        try
        {
            sqlConn.Open();
            adp.SelectCommand.CommandType = CommandType.Text;
            adp.Fill(ds);
        }

        catch (Exception ex) { System.Web.HttpContext.Current.Response.Write(ex.Message); }

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
        SqlCommand cmd = new SqlCommand("select * from t24_agenda where t24_cd_agenda=@t24_cd_agenda", sqlConn);
        cmd.Parameters.Add("@t24_cd_agenda", SqlDbType.Int).Value = _t24_cd_agenda;
		SqlDataReader data;

		try
		{
			sqlConn.Open();
			cmd.CommandType = CommandType.Text;
			data = cmd.ExecuteReader();

			if (data.Read())
			{
				_found = true;
				if (!Convert.IsDBNull(data["t24_cd_agenda"])) _t24_cd_agenda = (int) data["t24_cd_agenda"];
                if (!Convert.IsDBNull(data["t03_cd_projeto"])) _t03_cd_projeto = (int)data["t03_cd_projeto"];
				if (!Convert.IsDBNull(data["nm_agenda"])) _nm_agenda = (string) data["nm_agenda"];
				if (!Convert.IsDBNull(data["ds_agenda"])) _ds_agenda = (string) data["ds_agenda"];
				if (!Convert.IsDBNull(data["dt_data"])) _dt_data = (DateTime) data["dt_data"];
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
        SqlCommand cmd = new SqlCommand("insert into t24_agenda values(@t03_cd_projeto, @nm_agenda, @ds_agenda, @dt_data, @dt_cadastro, @dt_alterado, 1)", sqlConn);
		bool result;

        cmd.Parameters.Add("@t03_cd_projeto", SqlDbType.Int).Value = _t03_cd_projeto;
		cmd.Parameters.Add("@nm_agenda", SqlDbType.VarChar, 500).Value = _nm_agenda;
		cmd.Parameters.Add("@ds_agenda", SqlDbType.Text).Value = _ds_agenda;
		cmd.Parameters.Add("@dt_data", SqlDbType.DateTime).Value = _dt_data;
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
        SqlCommand cmd = new SqlCommand("update t24_agenda set t03_cd_projeto=@t03_cd_projeto, nm_agenda=@nm_agenda, ds_agenda=@ds_agenda, dt_data=@dt_data, dt_alterado=@dt_alterado "+
            "where t24_cd_agenda=@t24_cd_agenda", sqlConn);
		bool result;

		cmd.Parameters.Add("@t24_cd_agenda", SqlDbType.Int).Value = _t24_cd_agenda;
        cmd.Parameters.Add("@t03_cd_projeto", SqlDbType.Int).Value = _t03_cd_projeto;
		cmd.Parameters.Add("@nm_agenda", SqlDbType.VarChar, 500).Value = _nm_agenda;
		cmd.Parameters.Add("@ds_agenda", SqlDbType.Text).Value = _ds_agenda;
		cmd.Parameters.Add("@dt_data", SqlDbType.DateTime).Value = _dt_data;
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
        SqlCommand cmd = new SqlCommand("update t24_agenda set fl_ativa=0 where t24_cd_agenda=@t24_cd_agenda", sqlConn);
        cmd.Parameters.Add("@t24_cd_agenda", SqlDbType.Int).Value = _t24_cd_agenda;
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
