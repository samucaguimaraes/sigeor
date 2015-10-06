<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmLogin.aspx.cs" Inherits="frmLogin" %>
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Agenda Bahia</title>
<style type="text/css">
body {
	background-color: #FBFBFB;
}
.style2 {	color: #000000;
	font-family: Helvetica, Verdana, Arial, sans-serif;
	font-size: 10px;
}
.style5 {color: #000000; font-family: Helvetica, Verdana, Arial, sans-serif; font-size: 24px; }
.btnLogin{ border: solid 1px #3F3F3F; font-family: Helvetica, Verdana, Arial, sans-serif;
           font-size: 10px; padding:0px; margin-right:5px;}
</style></head>
<body>
<form id="form1" runat="server">
<table width="800" border="0" align="center">
  <tr>
    <td align="right">
        
        <asp:Login ID="Login1" runat="server"  
        OnAuthenticate="Login1_Authenticate"
        OnLoggingIn="Login1_LoggingIn"
        OnLoginError="Login1_LoginError">
            <LayoutTemplate>
            <div class="style2">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">
                                       Usuário:</asp:Label>

                                        <asp:TextBox ID="UserName" Width="100px" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                            ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                            ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                 &nbsp;
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Senha:</asp:Label>
                                    
                                        <asp:TextBox ID="Password" Width="100px"  runat="server" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                            ControlToValidate="Password" ErrorMessage="Password is required." 
                                            ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                   <asp:Button ID="LoginButton" runat="server" ForeColor="White" 
                                        BackColor="Brown" CssClass="btnLogin"
                                         CommandName="Login" Text="OK" 
                                            ValidationGroup="Login1" />
                                            
                                   <asp:Button ID="btnVisitante" ForeColor="White" 
                                        BackColor="Brown" CssClass="btnLogin"
                                   runat="server" Text="Visitante" onclick="btnVisitante_Click" />        
                                        <asp:CheckBox ID="RememberMe" Visible="false" runat="server" Text="Remember me next time." />
                                  <span style="color: MidnightBlue; font-size:10px">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        </span>
                                   
                                        
            </div>
            </LayoutTemplate>
        </asp:Login>
        &nbsp;
    
      </td>
  </tr>
  <tr>
    <td height="-1" align="right"><hr color="#8B4726"></td>
  </tr>
  <tr>
    <td width="800" background="IMAGES/BG_04_06_2008.jpg" style="height: 533px"><div align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div></td>
  </tr>
  <tr>
    <td align="center"><hr color="#8B4726">
      <p><span class="style2">Sistema de informação desenvolvido por Macroplan Prospectiva, Estratégia & Gestão para a disseminação da GEOR junto a parceiros do SEBRAE<br>
	  <p><span class="style2">Versão customizada pela SEPROMI<br>
   </span></p></td>
  </tr>
</table>
<p>&nbsp;</p>


</form>
</body>
</html>
