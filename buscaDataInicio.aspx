
<%@ Register Src="~/ucNavegacaoMonitoramento.ascx" TagName="NavMon" TagPrefix="uc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<META HTTP-EQUIV="CONTENT-TYPE" CONTENT="text/html; charset=iso-8859-1">
<title>Agenda Bahia</title>
</head>

<body>
<p><img src="images/interna_04_06_2008.jpg" width="2000" height="100" /></p>

<form id="form" name="form" method="post" action="MonMarcosDataInicio.aspx?fl_status=T">
   
  <label for="pesquisar"></label>
    <br>
    <strong><font color="#000080">Informe Ano de Início:  </font></strong>
				    <select id="Select1" name="pesquisar">
					<option value=" ">  </option>
					<option value="2008"> 2008 </option>						
					<option value="2009"> 2009 </option>	   
					<option value="2010"> 2010 </option>
					<option value="2011"> 2011 </option>						
					<option value="2012"> 2012 </option>	   
					<option value="2013"> 2013 </option>
					<option value="2014"> 2014 </option>
					<option value="2015"> 2015 </option>						
					<option value="2016"> 2016 </option>	   
					<option value="2017"> 2017 </option>                                              					
		  
       </select>
       
  <input type="submit" name="button" id="button" value="Pesquisar" />

</form>

<p>&nbsp;</p>
</body>
</html>