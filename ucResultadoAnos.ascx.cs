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

public partial class ucResultadoAnos : System.Web.UI.UserControl
{
    int cd_projeto;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (Session["cd_projeto"] != null)
        {
            cd_projeto = Int32.Parse(Session["cd_projeto"].ToString());
        }

        int numcells = 3;
        int j;

        TableRow HeaderRow = new TableRow();
        HeaderRow.Style["color"] = "white";
        HeaderRow.Style["font-weight"]= "bold";
        HeaderRow.Style["text-align"] = "center";
        HeaderRow.Style["background-color"] = "#5D7B9D";
        tbAnos.Rows.Add(HeaderRow);

        //-- Add header cells to the row 
        TableCell HeaderCell_1 = new TableCell();
        HeaderCell_1.Text = "Ano";
        HeaderRow.Cells.Add(HeaderCell_1);

        TableCell HeaderCell_2 = new TableCell();
        HeaderCell_2.Text = "Previsto";
        HeaderRow.Cells.Add(HeaderCell_2);

        TableCell HeaderCell_3 = new TableCell();
        HeaderCell_3.Text = "Realizado";
        HeaderRow.Cells.Add(HeaderCell_3);

        t03_projeto t03 = new t03_projeto();
        t03.t03_cd_projeto = cd_projeto;
        t03.Retrieve();
        if (t03.Found)
        {
            for (j = t03.dt_inicio.Year; j <= t03.dt_fim.Year; j++)
            {
                TableRow r = new TableRow();
                r.Style["background-color"] = "#F1F5F5";
                int i;
                for (i = 0; i <= numcells - 1; i++)
                {
                    TableCell c = new TableCell();
                    TextBox UserTextBox = new TextBox();
                    CompareValidator val = new CompareValidator();
                    switch (i)
                    {
                        case 0:
                            //ANO 
                            c.Controls.Add(new LiteralControl(j.ToString()));
                            r.Cells.Add(c);
                            break;
                        case 1:
                            //PREVISTO 
                            UserTextBox.ID = "txtPrev" + j.ToString();
                            UserTextBox.Columns = 18;
                            UserTextBox.MaxLength = 18;
                            UserTextBox.EnableViewState = true;
                            UserTextBox.Text = "0";

                            val.ID = "val" + j.ToString();
                            val.ControlToValidate = UserTextBox.ID;
                            val.ErrorMessage = "<br>*formato inválido";
                            val.Display = ValidatorDisplay.Dynamic;
                            val.Operator = ValidationCompareOperator.DataTypeCheck;
                            val.Type = ValidationDataType.Currency;
                            c.Controls.Add(UserTextBox);
                            c.Controls.Add(val);
                            r.Cells.Add(c);
                            break;
                        case 2:
                            //REALIZADO 

                            UserTextBox.ID = "txtReal" + j.ToString();
                            UserTextBox.Columns = 18;
                            UserTextBox.MaxLength = 18;
                            UserTextBox.EnableViewState = true;
                            UserTextBox.Text = "0";
                            
                            val.ID = "valr" + j.ToString();
                            val.ControlToValidate = UserTextBox.ID;
                            val.ErrorMessage = "<br>*formato inválido";
                            val.Display = ValidatorDisplay.Dynamic;
                            val.Operator = ValidationCompareOperator.DataTypeCheck;
                            val.Type = ValidationDataType.Currency;

                            c.Controls.Add(UserTextBox);
                            c.Controls.Add(val);
                            r.Cells.Add(c);
                            break;
                    }


                }
                tbAnos.Rows.Add(r);
            }
        }
    } 
}
