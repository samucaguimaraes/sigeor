using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for Arquivo
/// </summary>
public class uploadArquivo
{
    // private members
    private FileUpload _fu;
    private string _msg;
    private string _pasta;
    private string _nomeinicial;
    private string _nomearquivo;


    // public accessors
    public FileUpload fu
    {
        get { return _fu; }
        set { _fu = value; }
    }
    public string msg
    {
        get { return _msg; }
        set { _msg = value; }
    }
    public string nomeinicial
    {
        get { return _nomeinicial; }
        set { _nomeinicial = value; }
    }
    public string nomearquivo
    {
        get { return _nomearquivo; }
        set { _nomearquivo = value; }
    }
    public string pasta
    {
        get { return _pasta; }
        set { _pasta = value; }
    }

    public bool Save()
    {
        int maxSize = 10485760; //1 megabyte = 1 048 576 bytes
        string extension = "";
        bool result = false;
        try
        {
            if (!fu.HasFile){
                
                msg = "Nenhum arquivo foi selecionado!";
            }
            else if (fu.PostedFile.ContentLength > maxSize)
            {
                msg = "Arquivo utrapassou o tamanho máximo de 10 MB!";
            }
            else
            {
                extension = System.IO.Path.GetExtension(fu.FileName);
                nomearquivo = _nomeinicial + DateTime.Now.Year + DateTime.Now.Month + 
                    DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + 
                    DateTime.Now.Second + extension;
                fu.PostedFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(".") + @"\" + _pasta + "\\" + _nomearquivo);
                //Server.MapPath(".") + @"\Documentos\" +
                msg = "Arquivo enviado com sucesso!";
                result = true;
            }
        }
        catch (Exception ex) { System.Web.HttpContext.Current.Response.Write(ex.Message); result = false; }
        finally
        {
           
        }
        return result;
    }
}

