/* Generated by VB & C#.NET Class Generator */

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t01_entidade
{
	#region Declarations
    pageBase pb = new pageBase();
	private int _t01_cd_entidade;
	private string _nm_entidade;
    private string _nm_uf;
    private string _nm_arquivo;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	private bool _fl_ativa;
    private string _nm_cnpj;
    private string _order;
	private bool _found;

	#endregion

	#region Properties

	public int t01_cd_entidade
	{
		get { return _t01_cd_entidade; }
		set { _t01_cd_entidade = value; }
	}

	public string nm_entidade
	{
		get { return _nm_entidade; }
		set { _nm_entidade = value; }
	}
    
    public string nm_uf
    {
        get { return _nm_uf; }
        set { _nm_uf = value; }
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
    
    public string nm_cnpj
    {
        get { return _nm_cnpj; }
        set { _nm_cnpj = value; }
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
        SqlDataAdapter adp = new SqlDataAdapter("select * from t01_entidade where fl_ativa=@fl_ativa " + _order, sqlConn);
        adp.SelectCommand.Parameters.Add("@fl_ativa", SqlDbType.Bit).Value = _fl_ativa;
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
		SqlCommand cmd = new SqlCommand("select * from t01_entidade where t01_cd_entidade=@t01_cd_entidade", sqlConn);
        cmd.Parameters.Add("@t01_cd_entidade", SqlDbType.Int).Value = _t01_cd_entidade;
		SqlDataReader data;

		try
		{
			sqlConn.Open();
			cmd.CommandType = CommandType.Text;
			data = cmd.ExecuteReader();

			if (data.Read())
			{
				_found = true;
				if (!Convert.IsDBNull(data["t01_cd_entidade"])) _t01_cd_entidade = (int) data["t01_cd_entidade"];
				if (!Convert.IsDBNull(data["nm_entidade"])) _nm_entidade = (string) data["nm_entidade"];
                if (!Convert.IsDBNull(data["nm_uf"])) _nm_uf = (string)data["nm_uf"];
                if (!Convert.IsDBNull(data["nm_arquivo"])) _nm_arquivo = (string)data["nm_arquivo"];
				if (!Convert.IsDBNull(data["dt_cadastro"])) _dt_cadastro = (DateTime) data["dt_cadastro"];
				if (!Convert.IsDBNull(data["dt_alterado"])) _dt_alterado = (DateTime) data["dt_alterado"];
				if (!Convert.IsDBNull(data["fl_ativa"])) _fl_ativa = (bool) data["fl_ativa"];
                if (!Convert.IsDBNull(data["nm_cnpj"])) _nm_cnpj = (string)data["nm_cnpj"];
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

    public void RetrieveCod()
    {
        SqlConnection sqlConn = new SqlConnection(pb.strConn());
        SqlCommand cmd = new SqlCommand("select * from t01_entidade where nm_entidade=@nm_entidade and nm_uf=@nm_uf order by dt_cadastro desc", sqlConn);
        cmd.Parameters.Add("@nm_entidade", SqlDbType.VarChar, 500).Value = _nm_entidade;
        cmd.Parameters.Add("@nm_uf", SqlDbType.VarChar, 200).Value = _nm_uf;
        SqlDataReader data;

        try
        {
            sqlConn.Open();
            cmd.CommandType = CommandType.Text;
            data = cmd.ExecuteReader();

            if (data.Read())
            {
                _found = true;
                if (!Convert.IsDBNull(data["t01_cd_entidade"])) _t01_cd_entidade = (int)data["t01_cd_entidade"];
                if (!Convert.IsDBNull(data["nm_entidade"])) _nm_entidade = (string)data["nm_entidade"];
                if (!Convert.IsDBNull(data["nm_uf"])) _nm_uf = (string)data["nm_uf"];
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
        SqlCommand cmd = new SqlCommand("insert into t01_entidade values(@nm_entidade, @nm_uf, @nm_arquivo, @dt_cadastro, @dt_alterado, @fl_ativa, @nm_cnpj)", sqlConn);
		bool result;

		cmd.Parameters.Add("@nm_entidade", SqlDbType.VarChar, 500).Value = _nm_entidade;
        cmd.Parameters.Add("@nm_uf", SqlDbType.VarChar, 200).Value = _nm_uf;
        cmd.Parameters.Add("@nm_arquivo", SqlDbType.VarChar, 500).Value = _nm_arquivo;
		cmd.Parameters.Add("@dt_cadastro", SqlDbType.DateTime).Value = _dt_cadastro;
		cmd.Parameters.Add("@dt_alterado", SqlDbType.DateTime).Value = _dt_alterado;
		cmd.Parameters.Add("@fl_ativa", SqlDbType.Bit).Value = _fl_ativa;
        cmd.Parameters.Add("@nm_cnpj", SqlDbType.VarChar, 100).Value = _nm_cnpj;

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
        SqlCommand cmd = new SqlCommand("update t01_entidade set nm_entidade=@nm_entidade, nm_uf=@nm_uf, dt_alterado=@dt_alterado, nm_cnpj=@nm_cnpj where t01_cd_entidade=@t01_cd_entidade", sqlConn);
		bool result;

		cmd.Parameters.Add("@t01_cd_entidade", SqlDbType.Int).Value = _t01_cd_entidade;
		cmd.Parameters.Add("@nm_entidade", SqlDbType.VarChar, 500).Value = _nm_entidade;
        cmd.Parameters.Add("@nm_uf", SqlDbType.VarChar, 200).Value = _nm_uf;
        cmd.Parameters.Add("@nm_arquivo", SqlDbType.VarChar, 500).Value = _nm_arquivo;
		//cmd.Parameters.Add("@dt_cadastro", SqlDbType.DateTime).Value = _dt_cadastro;
		cmd.Parameters.Add("@dt_alterado", SqlDbType.DateTime).Value = _dt_alterado;
		//cmd.Parameters.Add("@fl_ativa", SqlDbType.Bit).Value = _fl_ativa;
        cmd.Parameters.Add("@nm_cnpj", SqlDbType.VarChar, 100).Value = _nm_cnpj;

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
        SqlCommand cmd = new SqlCommand("update t01_entidade set fl_ativa=0, dt_alterado=getdate()  where t01_cd_entidade=@t01_cd_entidade", sqlConn);
        cmd.Parameters.Add("@t01_cd_entidade", SqlDbType.Int).Value = _t01_cd_entidade;
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