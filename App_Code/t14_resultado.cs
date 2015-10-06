/* Generated by VB & C#.NET Class Generator */

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t14_resultado
{
	#region Declarations
    pageBase pb = new pageBase();
	private int _t14_cd_resultado;
	private int _t03_cd_projeto;
	private string _nm_resultado;
    private string _ds_resultado;
	private string _nm_medida;
    private int _nu_ano;
	private decimal _vl_t0;
    private bool _fl_acumulado;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	private bool _fl_ativa;
	private bool _found;
    private string _nm_unid;

	#endregion

	#region Properties

	public int t14_cd_resultado
	{
		get { return _t14_cd_resultado; }
		set { _t14_cd_resultado = value; }
	}

	public int t03_cd_projeto
	{
		get { return _t03_cd_projeto; }
		set { _t03_cd_projeto = value; }
	}

	public string nm_resultado
	{
		get { return _nm_resultado; }
		set { _nm_resultado = value; }
	}
    
    public string ds_resultado
    {
        get { return _ds_resultado; }
        set { _ds_resultado = value; }
    }

	public string nm_medida
	{
		get { return _nm_medida; }
		set { _nm_medida = value; }
	}

    public int nu_ano
    {
        get { return _nu_ano; }
        set { _nu_ano = value; }
    }

	public decimal vl_t0
	{
		get { return _vl_t0; }
		set { _vl_t0 = value; }
	}

    public bool fl_acumulado
    {
        get { return _fl_acumulado; }
        set { _fl_acumulado = value; }
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

	public bool Found
	{
		get { return _found; }
	}

    public string nm_unid
    {
        get { return _nm_unid; }
        set { _nm_unid = value; }
    }

	#endregion

	#region Functions/Routines

	#region List

	public DataSet List()
	{
		SqlConnection sqlConn = new SqlConnection(pb.strConn());
        SqlDataAdapter adp = new SqlDataAdapter("select * from t14_resultado where fl_ativa=1 and t03_cd_projeto=@t03_cd_projeto order by nm_resultado", sqlConn);
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
        SqlCommand cmd = new SqlCommand("select * from t14_resultado where t14_cd_resultado=@t14_cd_resultado", sqlConn);
        cmd.Parameters.Add("@t14_cd_resultado", SqlDbType.Int).Value = _t14_cd_resultado;
		SqlDataReader data;

		try
		{
			sqlConn.Open();
			cmd.CommandType = CommandType.Text;
			data = cmd.ExecuteReader();

			if (data.Read())
			{
				_found = true;
				if (!Convert.IsDBNull(data["t14_cd_resultado"])) _t14_cd_resultado = (int) data["t14_cd_resultado"];
				if (!Convert.IsDBNull(data["t03_cd_projeto"])) _t03_cd_projeto = (int) data["t03_cd_projeto"];
				if (!Convert.IsDBNull(data["nm_resultado"])) _nm_resultado = (string) data["nm_resultado"];
                if (!Convert.IsDBNull(data["ds_resultado"])) _ds_resultado = (string)data["ds_resultado"];
				if (!Convert.IsDBNull(data["nm_medida"])) _nm_medida = (string) data["nm_medida"];
                if (!Convert.IsDBNull(data["nu_ano"])) _nu_ano = (int)data["nu_ano"];
				if (!Convert.IsDBNull(data["vl_t0"])) _vl_t0 = (decimal) data["vl_t0"];
                if (!Convert.IsDBNull(data["fl_acumulado"])) _fl_acumulado = (bool)data["fl_acumulado"];
				if (!Convert.IsDBNull(data["dt_cadastro"])) _dt_cadastro = (DateTime) data["dt_cadastro"];
				if (!Convert.IsDBNull(data["dt_alterado"])) _dt_alterado = (DateTime) data["dt_alterado"];
				if (!Convert.IsDBNull(data["fl_ativa"])) _fl_ativa = (bool) data["fl_ativa"];
                if (!Convert.IsDBNull(data["nm_unid"])) _nm_unid = (string)data["nm_unid"];
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
        SqlCommand cmd = new SqlCommand("select * from t14_resultado where t03_cd_projeto=@t03_cd_projeto and nm_resultado=@nm_resultado order by dt_cadastro desc", sqlConn);
        cmd.Parameters.Add("@t03_cd_projeto", SqlDbType.Int).Value = _t03_cd_projeto;
        cmd.Parameters.Add("@nm_resultado", SqlDbType.VarChar, 500).Value = _nm_resultado;
        SqlDataReader data;

        try
        {
            sqlConn.Open();
            cmd.CommandType = CommandType.Text;
            data = cmd.ExecuteReader();

            if (data.Read())
            {
                _found = true;
                if (!Convert.IsDBNull(data["t14_cd_resultado"])) _t14_cd_resultado = (int)data["t14_cd_resultado"];
                if (!Convert.IsDBNull(data["t03_cd_projeto"])) _t03_cd_projeto = (int)data["t03_cd_projeto"];
                if (!Convert.IsDBNull(data["nm_resultado"])) _nm_resultado = (string)data["nm_resultado"];
                if (!Convert.IsDBNull(data["ds_resultado"])) _ds_resultado = (string)data["ds_resultado"];
                if (!Convert.IsDBNull(data["nm_medida"])) _nm_medida = (string)data["nm_medida"];
                if (!Convert.IsDBNull(data["nu_ano"])) _nu_ano = (int)data["nu_ano"];
                if (!Convert.IsDBNull(data["vl_t0"])) _vl_t0 = (decimal)data["vl_t0"];
                if (!Convert.IsDBNull(data["fl_acumulado"])) _fl_acumulado = (bool)data["fl_acumulado"];
                if (!Convert.IsDBNull(data["dt_cadastro"])) _dt_cadastro = (DateTime)data["dt_cadastro"];
                if (!Convert.IsDBNull(data["dt_alterado"])) _dt_alterado = (DateTime)data["dt_alterado"];
                if (!Convert.IsDBNull(data["fl_ativa"])) _fl_ativa = (bool)data["fl_ativa"];
                if (!Convert.IsDBNull(data["nm_unid"])) _nm_medida = (string)data["nm_unid"];
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
        SqlCommand cmd = new SqlCommand("insert into t14_resultado values(@t03_cd_projeto, @nm_resultado,@ds_resultado, @nm_medida, @nu_ano, @vl_t0, @fl_acumulado, @dt_cadastro, @dt_alterado, 1, @nm_unid)", sqlConn);
		bool result;

		cmd.Parameters.Add("@t03_cd_projeto", SqlDbType.Int).Value = _t03_cd_projeto;
		cmd.Parameters.Add("@nm_resultado", SqlDbType.VarChar, 500).Value = _nm_resultado;
        cmd.Parameters.Add("@ds_resultado", SqlDbType.Text).Value = _ds_resultado;
		cmd.Parameters.Add("@nm_medida", SqlDbType.VarChar, 200).Value = _nm_medida;
        
        if (_nu_ano > 0) { cmd.Parameters.Add("@nu_ano", SqlDbType.Int).Value = _nu_ano; }
        else { cmd.Parameters.Add("@nu_ano", SqlDbType.Int).Value = DBNull.Value; }

        if (_vl_t0 > 0) { cmd.Parameters.Add("@vl_t0", SqlDbType.Decimal).Value = _vl_t0; }
        else { cmd.Parameters.Add("@vl_t0", SqlDbType.Decimal).Value = DBNull.Value; }

        cmd.Parameters.Add("@fl_acumulado", SqlDbType.Bit).Value = _fl_acumulado;
		cmd.Parameters.Add("@dt_cadastro", SqlDbType.DateTime).Value = _dt_cadastro;
		cmd.Parameters.Add("@dt_alterado", SqlDbType.DateTime).Value = _dt_alterado;
		//cmd.Parameters.Add("@fl_ativa", SqlDbType.Bit).Value = _fl_ativa;
        cmd.Parameters.Add("@nm_unid", SqlDbType.VarChar, 200).Value = _nm_unid;

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
        SqlCommand cmd = new SqlCommand("update t14_resultado set nm_resultado=@nm_resultado,ds_resultado=@ds_resultado, nm_medida=@nm_medida, fl_acumulado=@fl_acumulado, nu_ano=@nu_ano, vl_t0=@vl_t0, dt_alterado=@dt_alterado, nm_unid=@nm_unid where t14_cd_resultado=@t14_cd_resultado", sqlConn);
		bool result;

		cmd.Parameters.Add("@t14_cd_resultado", SqlDbType.Int).Value = _t14_cd_resultado;
		cmd.Parameters.Add("@t03_cd_projeto", SqlDbType.Int).Value = _t03_cd_projeto;
		cmd.Parameters.Add("@nm_resultado", SqlDbType.VarChar, 500).Value = _nm_resultado;
        cmd.Parameters.Add("@ds_resultado", SqlDbType.Text).Value = _ds_resultado;
		cmd.Parameters.Add("@nm_medida", SqlDbType.VarChar, 200).Value = _nm_medida;
        
        if (_nu_ano > 0){cmd.Parameters.Add("@nu_ano", SqlDbType.Int).Value = _nu_ano;}
        else{cmd.Parameters.Add("@nu_ano", SqlDbType.Int).Value = DBNull.Value;}

        if (_vl_t0 > 0) {cmd.Parameters.Add("@vl_t0", SqlDbType.Decimal).Value = _vl_t0;}
        else {cmd.Parameters.Add("@vl_t0", SqlDbType.Decimal).Value = DBNull.Value;}
		
        cmd.Parameters.Add("@fl_acumulado", SqlDbType.Bit).Value = _fl_acumulado;
		cmd.Parameters.Add("@dt_cadastro", SqlDbType.DateTime).Value = _dt_cadastro;
		cmd.Parameters.Add("@dt_alterado", SqlDbType.DateTime).Value = _dt_alterado;
		cmd.Parameters.Add("@fl_ativa", SqlDbType.Bit).Value = _fl_ativa;
        cmd.Parameters.Add("@nm_unid", SqlDbType.VarChar, 200).Value = _nm_unid;

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
        SqlCommand cmd = new SqlCommand("update t14_resultado set fl_ativa=0, dt_alterado=getdate()  where t14_cd_resultado=@t14_cd_resultado", sqlConn);
        cmd.Parameters.Add("@t14_cd_resultado", SqlDbType.Int).Value = _t14_cd_resultado;
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
