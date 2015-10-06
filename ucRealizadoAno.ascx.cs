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

public partial class ucRealizadoAno : System.Web.UI.UserControl
{
    private bool _editar;
    public bool editar
    {
        get { return _editar; }
        set { _editar = value; }
    }
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, System.EventArgs e)
    {
        int numcells = 6;
        int j;

        TableRow HeaderRow = new TableRow();
        HeaderRow.Style["color"] = "white";
        HeaderRow.Style["font-weight"] = "bold";
        HeaderRow.Style["text-align"] = "center";
        HeaderRow.Style["background-color"] = "#5D7B9D";
        tbAnos.Rows.Add(HeaderRow);

        //-- Add header cells to the row 
        TableCell HeaderCell_1 = new TableCell();
        HeaderCell_1.Text = "Ano";
        HeaderRow.Cells.Add(HeaderCell_1);

        TableCell HeaderCellP_1 = new TableCell();
        HeaderCellP_1.Text = "1º Trim";
        HeaderRow.Cells.Add(HeaderCellP_1);

        TableCell HeaderCellP_2 = new TableCell();
        HeaderCellP_2.Text = "2º Trim";
        HeaderRow.Cells.Add(HeaderCellP_2);

        TableCell HeaderCellP_3 = new TableCell();
        HeaderCellP_3.Text = "3º Trim";
        HeaderRow.Cells.Add(HeaderCellP_3);

        TableCell HeaderCellP_4 = new TableCell();
        HeaderCellP_4.Text = "4º Trim";
        HeaderRow.Cells.Add(HeaderCellP_4);

        TableCell HeaderCell_5 = new TableCell();
        HeaderCell_5.Text = "Total";
        HeaderRow.Cells.Add(HeaderCell_5);
        if (_editar)
            HeaderCell_5.Visible = false;

        t08_acao t08 = new t08_acao();
        t08.t08_cd_acao = pb.cd_acao();
        t08.Retrieve();
        if (t08.Found)
        {
            for (j = t08.dt_inicio.Year; j <= t08.dt_fim.Year; j++)
            {
                TableRow r = new TableRow();
                r.Style["background-color"] = "#F1F5F5";
                int i;
                for (i = 0; i <= numcells - 1; i++)
                {
                    TableCell c = new TableCell();
                    TextBox UserTextBox = new TextBox();
                    if (!_editar)
                    {
                        UserTextBox.Attributes.Add("style", "background:#F1F5F5;border:none;text-align:right;");
                        UserTextBox.ReadOnly = true;
                    }
                    CompareValidator val = new CompareValidator();
                    switch (i)
                    {
                        case 0:
                            //ANO 
                            c.Controls.Add(new LiteralControl(j.ToString()));
                            r.Cells.Add(c);
                            break;
                        case 1:
                            //REALIZADO

                            UserTextBox.ID = "txtvl_r" + i.ToString() + j.ToString();
                            UserTextBox.Columns = 18;
                            UserTextBox.MaxLength = 18;
                            UserTextBox.EnableViewState = true;
                            UserTextBox.Text = "0";

                            val.ID = "valr" + i.ToString() + j.ToString();
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

                            UserTextBox.ID = "txtvl_r" + i.ToString() + j.ToString();
                            UserTextBox.Columns = 18;
                            UserTextBox.MaxLength = 18;
                            UserTextBox.EnableViewState = true;
                            UserTextBox.Text = "0";

                            val.ID = "valr" + i.ToString() + j.ToString();
                            val.ControlToValidate = UserTextBox.ID;
                            val.ErrorMessage = "<br>*formato inválido";
                            val.Display = ValidatorDisplay.Dynamic;
                            val.Operator = ValidationCompareOperator.DataTypeCheck;
                            val.Type = ValidationDataType.Currency;
                            c.Controls.Add(UserTextBox);
                            c.Controls.Add(val);
                            r.Cells.Add(c);

                            break;
                        case 3:
                            //REALIZADO

                            UserTextBox.ID = "txtvl_r" + i.ToString() + j.ToString();
                            UserTextBox.Columns = 18;
                            UserTextBox.MaxLength = 18;
                            UserTextBox.EnableViewState = true;
                            UserTextBox.Text = "0";

                            val.ID = "valr" + i.ToString() + j.ToString();
                            val.ControlToValidate = UserTextBox.ID;
                            val.ErrorMessage = "<br>*formato inválido";
                            val.Display = ValidatorDisplay.Dynamic;
                            val.Operator = ValidationCompareOperator.DataTypeCheck;
                            val.Type = ValidationDataType.Currency;
                            c.Controls.Add(UserTextBox);
                            c.Controls.Add(val);
                            r.Cells.Add(c);

                            break;
                        case 4:
                            //REALIZADO

                            UserTextBox.ID = "txtvl_r" + i.ToString() + j.ToString();
                            UserTextBox.Columns = 18;
                            UserTextBox.MaxLength = 18;
                            UserTextBox.EnableViewState = true;
                            UserTextBox.Text = "0";

                            val.ID = "valr" + i.ToString() + j.ToString();
                            val.ControlToValidate = UserTextBox.ID;
                            val.ErrorMessage = "<br>*formato inválido";
                            val.Display = ValidatorDisplay.Dynamic;
                            val.Operator = ValidationCompareOperator.DataTypeCheck;
                            val.Type = ValidationDataType.Currency;
                            c.Controls.Add(UserTextBox);
                            c.Controls.Add(val);
                            r.Cells.Add(c);

                            break;
                        case 5:
                            //REALIZADO TOTAL

                            UserTextBox.ID = "txtvl_rtotal" + i.ToString() + j.ToString();
                            UserTextBox.Columns = 18;
                            UserTextBox.MaxLength = 18;
                            UserTextBox.EnableViewState = true;
                            UserTextBox.Text = "0,00";
                            UserTextBox.ReadOnly = true;
                            if (!_editar)
                            {
                                c.Controls.Add(UserTextBox);
                                r.Cells.Add(c);
                            }
                            break;

                    }


                }
                tbAnos.Rows.Add(r);
            }
        }
    } 
}
