using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


public class t32_usuariolinha
{

    #region Declarations
    pageBase pb = new pageBase();
	private string _t02_cd_usuario;
	private int _t01_cd_entidade;
	private bool _found;
    private string _order;
	#endregion

    #region Properties

    public string t02_cd_usuario
    {
        get { return _t02_cd_usuario; }
        set { _t02_cd_usuario = value; }
    }

    public int t01_cd_entidade
    {
        get { return _t01_cd_entidade; }
        set { _t01_cd_entidade = value; }
    }
    public bool Found
    {
        get { return _found; }
    }
    public string order
    {
        get { return _order; }
        set { _order = value; }
    }
    #endregion

    #region Functions/Routines

    #region List

    public DataSet List()
    {
        SqlConnection sqlConn = new SqlConnection(pb.strConn());
        SqlDataAdapter adp = new SqlDataAdapter("select * from t02_usuario where t02_cd_usuario in " +
            "(select t02_cd_usaurio from t32_usuariolinha where t01_cd_entidade = @t01_cd_entidade)", sqlConn);

        adp.SelectCommand.Parameters.Add("@t01_cd_entidade", SqlDbType.Int).Value = _t01_cd_entidade;
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

    #region Save

    public bool Save()
    {
        SqlConnection sqlConn = new SqlConnection(pb.strConn());
        SqlCommand cmd = new SqlCommand("insert into t32_usuariolinha values(@t01_cd_entidade, @t02_cd_usuario)", sqlConn);
        bool result;

        cmd.Parameters.Add("@t02_cd_usuario", SqlDbType.VarChar, 30).Value = _t02_cd_usuario;
        cmd.Parameters.Add("@t01_cd_entidade", SqlDbType.Int).Value = _t01_cd_entidade;

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
        SqlCommand cmd = new SqlCommand("delete from t32_usuariolinha where t01_cd_entidade=@t01_cd_entidade and t02_cd_usuario=@t02_cd_usuario", sqlConn);
        cmd.Parameters.Add("@t01_cd_entidade", SqlDbType.Int).Value = _t01_cd_entidade;
        cmd.Parameters.Add("@t02_cd_usuario", SqlDbType.VarChar,30).Value = _t02_cd_usuario;
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
