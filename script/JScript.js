    // JScript File

    function MascaraMoeda(objTextBox, SeparadorMilesimo, SeparadorDecimal, e){
        var sep = 0;
        var key = '';
        var i = j = 0;
        var len = len2 = 0;
        var strCheck = '0123456789';
        var aux = aux2 = '';
        var whichCode = (window.Event) ? e.which : e.keyCode;
        if (whichCode == 13) return true;
        key = String.fromCharCode(whichCode); // Valor para o código da Chave
        if (strCheck.indexOf(key) == -1) return false; // Chave inválida
        len = objTextBox.value.length;
        for(i = 0; i < len; i++)
            if ((objTextBox.value.charAt(i) != '0') && (objTextBox.value.charAt(i) != SeparadorDecimal)) break;
        aux = '';
        for(; i < len; i++)
            if (strCheck.indexOf(objTextBox.value.charAt(i))!=-1) aux += objTextBox.value.charAt(i);
        aux += key;
        len = aux.length;
        if (len == 0) objTextBox.value = '';
        if (len == 1) objTextBox.value = '0'+ SeparadorDecimal + '0' + aux;
        if (len == 2) objTextBox.value = '0'+ SeparadorDecimal + aux;
        if (len > 2) {
            aux2 = '';
            for (j = 0, i = len - 3; i >= 0; i--) {
                if (j == 3) {
                    aux2 += SeparadorMilesimo;
                    j = 0;
                }
                aux2 += aux.charAt(i);
                j++;
            }
            objTextBox.value = '';
            len2 = aux2.length;
            for (i = len2 - 1; i >= 0; i--)
            objTextBox.value += aux2.charAt(i);
            objTextBox.value += SeparadorDecimal + aux.substr(len - 2, len);
        }
        return false;
    }

    function textCounter(field,MaxLength) { 
        obj = document.all(field); 
        if (MaxLength !=0) { 
        if (obj.value.length > MaxLength) 
        obj.value = obj.value.substring(0, MaxLength); 
    } 
    } 

    var _oldColor;
   function SetNewColor(source)
    {
       _oldColor = source.style.backgroundColor;
        source.style.backgroundColor = '#FCFCCE';
    }
    function SetOldColor(source)
    {
       source.style.backgroundColor = _oldColor;
   }
   
   function checkIt(evt) {
    evt = (evt) ? evt : window.event
    var charCode = (evt.which) ? evt.which : evt.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false
    }
    return true
}



//########## FORMATAÇÃO DE NÚMEROS #############
//INICIO
 function GetDecimalDelimiter(countryCode)
{
 
  switch (countryCode)
  {
    case 3:   
           return '#';
    case 2:   
           return ',';
    case 1:   
           return ',';
    default:
           return '.';
  }
}

function GetCommaDelimiter(countryCode)
{
 
  switch (countryCode)
  { 
    case 3:          
           return '*';
    case 2:   
           return ',';
    case 1:   
           return '.';
    default:
           return ',';
  }
 
}

function FormatClean(num)
{
     var sVal='';
     var nVal = num.length;
     var sChar='';
     
   try
   {
      for(c=0;c<nVal;c++)
      {
         sChar = num.charAt(c);
         nChar = sChar.charCodeAt(0);
         if ((nChar >=48) && (nChar <=57))  { sVal += num.charAt(c);   }
      }
   }
    catch (exception) { AlertError("Format Clean",exception); }
    return sVal;
}
  

function FormatNumber(num,countryCode,decimalPlaces)
{       

  var minus='';
  var comma='';
  var dec='';
  var preDecimal='';
  var postDecimal='';
  
  try 
  {
   
    decimalPlaces = parseInt(decimalPlaces);
    comma = GetCommaDelimiter(countryCode);
    dec = GetDecimalDelimiter(countryCode);
    
    if (decimalPlaces < 1) { dec = ''; }
    if (num.lastIndexOf("-") == 0) { minus='-'; }
   
    preDecimal = FormatClean(num);
    
    // preDecimal doesn't contain a number at all.
    // Return formatted zero representation.
    
    if (preDecimal.length < 1)
    {
       return minus + FormatEmptyNumber(dec,decimalPlaces);
    }
    
    // preDecimal is 0 or a series of 0's.
    // Return formatted zero representation.
    
    if (parseInt(preDecimal) < 1)
    {
       return minus + FormatEmptyNumber(dec,decimalPlaces);
    }
    
    // predecimal has no numbers to the left.
    // Return formatted zero representation.
    
    if (preDecimal.length == decimalPlaces)
    {
      return minus + '0' + dec + preDecimal;
    }
    
    // predecimal has fewer characters than the
    // specified number of decimal places.
    // Return formatted leading zero representation.
    
    if (preDecimal.length < decimalPlaces)
    {
       if (decimalPlaces == 2)
       {
        return minus + FormatEmptyNumber(dec,decimalPlaces - 1) + preDecimal;
       }
       return minus + FormatEmptyNumber(dec,decimalPlaces - 2) + preDecimal;
    }
    
    // predecimal contains enough characters to
    // qualify to need decimal points rendered.
    // Parse out the pre and post decimal values
    // for future formatting.
    
    if (preDecimal.length > decimalPlaces)
    {
      postDecimal = dec + preDecimal.substring(preDecimal.length - decimalPlaces,
                                               preDecimal.length);
      preDecimal = preDecimal.substring(0,preDecimal.length - decimalPlaces);
    }

    // Place comma oriented delimiter every 3 characters
    // against the numeric represenation of the "left" side
    // of the decimal representation.  When finished, return
    // both the left side comma formatted value together with
    // the right side decimal formatted value.
    
    var regex  = new RegExp('(-?[0-9]+)([0-9]{3})');
 
    while(regex.test(preDecimal))
    {
       preDecimal = preDecimal.replace(regex, '$1' + comma + '$2');
    }
       
  }
  catch (exception) { AlertError("Format Number",exception); }
  return minus + preDecimal + postDecimal;
}

function FormatEmptyNumber(decimalDelimiter,decimalPlaces)
{
    var preDecimal = '0';
    var postDecimal = '';
 
    for(i=0;i<decimalPlaces;i++)
    {
      if (i==0) { postDecimal += decimalDelimiter; }
      postDecimal += '0';
    }
   return preDecimal + postDecimal;
}
  

 function AlertError(methodName,e)
 {
            if (e.description == null) { alert(methodName + " Exception: " + e.message); }
            else {  alert(methodName + " Exception: " + e.description); }
 }
 
 

//######### FORMATA SOMENTE NUMEROS ############

     /*function numeros() {
          tecla = event.keyCode;

          // Se for número (48 a 57) ou "/" (47)
          if ( (tecla >= 48 && tecla <= 57) || (tecla == 47)) {
               return true;
          }
          else {
               return false;
          }
     }*/
     function numeros(evt)
    {
        var key_code = evt.keyCode  ? evt.keyCode  :
                       evt.charCode ? evt.charCode :
                       evt.which    ? evt.which    : void 0;


        // Habilita teclas <DEL>, <TAB>, <ENTER>, <ESC> e <BACKSPACE>
        if (key_code == 8  ||  key_code == 9  ||  key_code == 13  ||  key_code == 27  ||  key_code == 46)
        {
            return true;
        }

        // Habilita teclas <HOME>, <END>, mais as quatros setas de navegação (cima, baixo, direta, esquerda)
        else if ((key_code >= 35)  &&  (key_code <= 40))
        {
            return true
        }

        // Habilita números de 0 a 9
        else if ((key_code >= 48)  &&  (key_code <= 57))
        {
            return true
        }

        return false;
    }


//FIM

//######### EXIBIR / OCULTAR ############
function switchMenu(obj) {
	var el = document.getElementById(obj);
	if ( el.style.display != "none" ) {
		el.style.display = 'none';
	}
	else {
		el.style.display = '';
	}
}
//######### EXIBIR / OCULTAR ############




 //Validator for a TEXTBOX 
// Keep user from entering more than maxLength characters
function doKeypress(control){
    maxLength = control.attributes["maxLength"].value;
    value = control.value;
     if(maxLength && value.length > maxLength-1){
          event.returnValue = false;
          maxLength = parseInt(maxLength);
     }
}
// Cancel default behavior
function doBeforePaste(control){
    maxLength = control.attributes["maxLength"].value;
     if(maxLength)
     {
          event.returnValue = false;
     }
}
// Cancel default behavior and create a new paste routine
function doPaste(control){
    maxLength = control.attributes["maxLength"].value;
    value = control.value;
     if(maxLength){
          event.returnValue = false;
          maxLength = parseInt(maxLength);
          var oTR = control.document.selection.createRange();
          var iInsertLength = maxLength - value.length + oTR.text.length;
          var sData = window.clipboardData.getData("Text").substr(0,iInsertLength);
          oTR.text = sData;
     }
}



	
