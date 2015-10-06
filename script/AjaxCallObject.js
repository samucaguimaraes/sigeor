__AJAXCboList = new Array();
__PageForm = null;
__bPageIsStored = false;
__bUnloadStoredPage = false;
__bTracing = false;
__doingSubmit = false;
__PreviousPostBack = null;
__TraceWindows = new Array();
__ClockID = 0;
__IsOpera = false;
__IsIE = false;
__ClkEvent = null;

// Excluding from post flags
// To be used with the "ExcludeFlags" attribute
excfViewState = 1;
excfFingerprints = 2;
excfUserHidden = 4;
excfAllHidden = excfViewState | excfFingerprints | excfUserHidden;
excfFormElements = 8;
excfAllElements = excfAllHidden | excfFormElements;

function AjaxCallObject()
{
  this.Init();
}

AjaxCallObject.prototype.Init = function()
{
  this.XmlHttp = this.GetHttpObject();
}
 
AjaxCallObject.prototype.GetHttpObject = function()
{ 
  var xmlhttp;
  /*@cc_on
  @if (@_jscript_version >= 5)
    try {
      xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
    } catch (e) {
      try {
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
      } catch (E) {
        xmlhttp = false;
      }
    }
  @else
  xmlhttp = false;
  @end @*/
  if (!xmlhttp && typeof XMLHttpRequest != 'undefined') {
    try {
      xmlhttp = new XMLHttpRequest();
    } catch (e) {
      xmlhttp = false;
    }
  }
  return xmlhttp;
}

AjaxCallObject.prototype.GetAjaxCallType = function(element)
{
	if (element == null)
		return "none";
		
	var attrib = element.getAttribute("AjaxCall");
	if (attrib != null && attrib != '')
		return attrib.toLowerCase();

	if (element.parentNode == null || element.parentNode == document.body)
		return "none";
	else
		return this.GetAjaxCallType(element.parentNode);
}

AjaxCallObject.prototype.GetAjaxScopeID = function(element)
{
	if (element == null)
		return null;
		
	var attrib = element.getAttribute("AjaxLocalScope");
	if ( attrib != null && attrib.toLowerCase() == "true" )
		return element.getAttribute("id")

	if (element.parentNode == null || element.parentNode == document.body)
		return null;
	else
		return this.GetAjaxScopeID(element.parentNode);
}

AjaxCallObject.prototype.ExcludeFromPost = function(element, scopeID, flags)
{
	var excludeAttr = element.getAttribute("ExcludeFromPost");
	if (excludeAttr != null && excludeAttr.toLowerCase() == "true")
		return true;

	var name = element.name;

	if (element.type == "hidden")
	{
		if ( name == "__MAGICAJAX_CONFIG" )
			return false;

		if ( excfAllHidden == (flags & excfAllHidden) )
			return true;

		if (name == "__VIEWSTATE")
		{
			return ( excfViewState == (flags & excfViewState) );
		}
		
		var fprintConst = "__CONTROL_FINGERPRINTS_";
		if (name.indexOf(fprintConst) == 0)
		{
			if ( excfFingerprints == (flags & excfFingerprints) )
				return true;
			
			if (scopeID != null)
			{
				fprintElem = document.getElementById(name.substr(fprintConst.length));
				if ( ! this.IsInAjaxScope(fprintElem, scopeID) )
					return true;
			}
		}
		else
		{
			return ( excfUserHidden == (flags & excfUserHidden) );
		}
	}
	else
	{
		if ( excfFormElements == (flags & excfFormElements) )
			return true;

		return ( scopeID != null && !this.IsInAjaxScope(element, scopeID) )
	}	

	return false;
}

AjaxCallObject.prototype.GetExcludeFlags = function(element)
{
	if (element == null)
		return 0;

	var flags = 0;
	var flagsAttr = element.getAttribute("ExcludeFlags");
	if (flagsAttr != null)
		flags = eval(flagsAttr);
	
	if (element.parentNode == null || element.parentNode == document.body)
		return flags;
	else
		return ( flags | this.GetExcludeFlags(element.parentNode) );
}

AjaxCallObject.prototype.IsInAjaxScope = function(element, scopeID)
{
	var attrib = element.getAttribute("AjaxLocalScope");
	if ( attrib != null && attrib.toLowerCase() == "true" && element.getAttribute("id") == scopeID )
		return true;

	if (element.parentNode == null || element.parentNode == document.body)
		return false;
	else
		return this.IsInAjaxScope(element.parentNode, scopeID);
}

AjaxCallObject.prototype.GetTargetElement = function(eventTarget)
{
	var target = document.getElementById(eventTarget);
	if (target != null)
		return target;
		
	var elemUniqueID = eventTarget.split("$").join(":");
	var ids = elemUniqueID.split(":");

	// Checks the unique id and its parents until it finds a target element
	// i.e. for ajaxPanel_grid:row:field it checks
	//   ajaxPanel_grid_row_field
	//   ajaxPanel_grid_row
	//   ajaxPanel_grid
	for (var num=ids.length; num > 0; num--)
	{
		var elemID = "";
		for (var i=0; i < num; i++)
			elemID += (i==0 ? "" : "_") + ids[i];
		
		target = document.getElementById(elemID);
		if (target != null)
			break;
	}
	// If no element found, try last id (fixes .NET 1.1 calendar control bug)
	if (target == null && ids.length > 1)
		target = document.getElementById(ids[ids.length-1]);
	
	return target;
}

AjaxCallObject.prototype.AddEventListener = function(obj, eventName, fn, capture)
{
    if (typeof(capture)=="undefined") capture=false;
    if (obj.addEventListener)
	    obj.addEventListener(eventName, fn, capture);
    else
        obj.attachEvent("on"+eventName, fn);
}

AjaxCallObject.prototype.DispatchEvent = function(obj, eventName)
{
	if (obj.fireEvent)
		obj.fireEvent("on"+eventName);
	else
	{
		var evt = document.createEvent("Events")
		evt.initEvent(eventName, true, true);
		obj.dispatchEvent(evt);
	}
}

AjaxCallObject.prototype.HookAjaxCall = function(bPageIsStored, bUnloadStoredPage, bTracing, pageFormID)
{
	__IsIE = navigator.appName.indexOf("Internet Explorer") != -1;
    __IsOpera = window.opera ? true : false;
    __PageForm = document.getElementById(pageFormID);
    
    if (__PageForm == null) return;

	this.AddEventListener(__PageForm, "submit", this.OnFormSubmit);
	this.AddEventListener(__PageForm, "click", this.OnFormClick, true);

	if (typeof __doPostBack != 'undefined')
	{
		__PreviousPostBack = __doPostBack;
		__doPostBack = this.DoPostBack;
	}
	
	__bPageIsStored = bPageIsStored;
	__bUnloadStoredPage = bUnloadStoredPage;
	__bTracing = bTracing;

	if (typeof(RBS_Controls) != "undefined")
	{
		for (var i=0; i < RBS_Controls.length; i++)
			RBS_Controls_Store[i].setAttribute("ExcludeFromPost", "true");
	}

	if ( !bPageIsStored || !bUnloadStoredPage )
	{
		this.AddEventListener(window, "load", this.OnPageLoad);
		this.AddEventListener(window, "beforeunload", this.OnPageBeforeUnload);
	}

	this.AddEventListener(window, "unload", this.OnPageUnload);
}

AjaxCallObject.prototype.OnFormClick = function(e)
{
	__ClkEvent = e;
}

AjaxCallObject.prototype.OnFormSubmit = function(e)
{
	if (__doingSubmit)
	{
		__doingSubmit = false;
	    if (e.preventDefault)
	        e.preventDefault();
	    e.returnValue=false;
		return false;
	}

    if (typeof(Page_ClientValidate)=="function")
    {
        if (window.event && !__IsOpera)
        {
            if (window.event.returnValue == false)
                return;
        }
        else if (e.getPreventDefault && e.getPreventDefault() == true)
            return;
    }
    
	// Empty the cached html of RenderedByScript controls
	if (typeof(RBS_Controls) != "undefined")
	{
		for (var i=0; i < RBS_Controls.length; i++)
			RBS_Controls_Store[i].value = "";
	}

	var target;
	if ("activeElement"	in document)
	{
		// Internet Explorer and Opera
		target = document.activeElement;
	}
	else
	{
		// Firefox
		target = e ? e.explicitOriginalTarget : null;
	}

    if (target == null || target.name == null || target.name == "")
        return true;

	var cbType = AJAXCbo.GetAjaxCallType(target);
	if (cbType != "none")
	{
		__doingSubmit = true;
		AJAXCbo.DispatchEvent(__PageForm, "submit");
		__doingSubmit = false;

		var theData = "";
		//check if target is an input element of type 'image'
		if (target != null && target.type == "image")
		{
			if (e.offsetX)
			{
				// IE
				theData = target.name + ".x=" + (e.offsetX - target.offsetLeft) + "&" + target.name + ".y=" + (e.offsetY - target.offsetTop);
			}
			else
			{
				// Firefox
				theData = target.name + ".x=" + (__ClkEvent.pageX - target.offsetLeft) + "&" + target.name + ".y=" + (__ClkEvent.pageY - target.offsetTop);
			}
		}

	    if (AJAXCbo.DoAjaxCall(target.name, "", cbType, AJAXCbo.GetAjaxScopeID(target), theData))
	    {
	        if (e.preventDefault)
	            e.preventDefault();
	        e.returnValue = false;
			return false;
	    }
	    else
			return true;
	}
	else
	{
		AJAXCbo.ClearTracingWindows();
		return true;
	}
}

// Replaces normal __doPostBack
AjaxCallObject.prototype.DoPostBack = function(eventTarget, eventArgument)
{
    if (typeof(WebForm_OnSubmit)=="function")
    {
        if (__PageForm.onsubmit && (__PageForm.onsubmit() == false))
            return;
    }

	// Empty the cached html of RenderedByScript controls
	if (typeof(RBS_Controls) != "undefined")
	{
		for (var i=0; i < RBS_Controls.length; i++)
			RBS_Controls_Store[i].value = "";
	}
	
	var target = AJAXCbo.GetTargetElement(eventTarget);
	var cbType = AJAXCbo.GetAjaxCallType(target);
	if (cbType != "none")
	{
		AJAXCbo.DoAjaxCall(eventTarget, eventArgument, cbType, AJAXCbo.GetAjaxScopeID(target));
		
		if (target.tagName == "INPUT" && (target.type == "submit" || target.type == "image"))
		{
			if (window.event)
			{
				window.event.returnValue = false;
			}
			else if (__ClkEvent)
			{
				if (__ClkEvent.preventDefault)
					__ClkEvent.preventDefault();
			}
		}
	}
	else
	{
		if (__PreviousPostBack != null)
		{
			__PreviousPostBack(eventTarget, eventArgument);
		}
	}
}

AjaxCallObject.prototype.OnPageLoad = function()
{
	// Restore the html of RenderedByScript controls
	if (typeof(RBS_Controls) != "undefined")
	{
		for (var i=0; i < RBS_Controls.length; i++)
		{
			var html = RBS_Controls_Store[i].value;
			if (html != "")
			{
				RBS_Controls[i].innerHTML = html.substring(5, html.length);
				RBS_Controls_Store[i].value = "";
			}
		}
	}
}

AjaxCallObject.prototype.OnPageBeforeUnload = function()
{
	// Save the html of RenderedByScript controls, so that it can be restored for the
	// browser's "Back Button"
	if (typeof(RBS_Controls) != "undefined")
	{
		for (var i=0; i < RBS_Controls.length; i++)
			RBS_Controls_Store[i].value = "HTML:" + RBS_Controls[i].innerHTML;
	}
}

AjaxCallObject.prototype.OnPageUnload = function()
{
	AJAXCbo.ClearTracingWindows();

	if ( !__bPageIsStored || !__bUnloadStoredPage )
		return;
	
	if (__PageForm["__AJAX_PAGEKEY"] == null)
		return;

	var thePage = __PageForm.action;
	var index = thePage.indexOf("?");
	if (index != -1)
		thePage = thePage.substring(0, index);

	thePage = thePage + "?__AJAX_PAGEUNLOAD=" + encodeURIComponent(__PageForm["__AJAX_PAGEKEY"].value);

	var oThis = AJAXCbo;
	__AJAXCboList.push(oThis);
	AJAXCbo = new AjaxCallObject();

	if( oThis.XmlHttp )
	{
		oThis.XmlHttp.open('GET', thePage, true);
		oThis.XmlHttp.onreadystatechange = function(){ };
		oThis.XmlHttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
		oThis.XmlHttp.send(null);
	}
}
 
AjaxCallObject.prototype.DoAjaxCall = function(eventTarget, eventArgument, ajaxCallType, ajaxScopeID, additionalData)
{
	var target = this.GetTargetElement(eventTarget);

	//defaults
	if (!ajaxCallType) ajaxCallType = "async";
	if (typeof(ajaxScopeID) == "undefined")
		ajaxScopeID = this.GetAjaxScopeID( target );

	var theData = '';
	var theform = __PageForm;
	var thePage = theform.action;
	var eName = '';

	theData  = '__EVENTTARGET='  + this.EncodePostData(eventTarget.split("$").join(":")) + '&';
	theData += '__EVENTARGUMENT=' + this.EncodePostData(eventArgument) + '&';
	theData += '__AJAXCALL=true&';
  
	if (ajaxScopeID != null)
		theData += '__AJAXSCOPE=' + ajaxScopeID + '&';

	if (typeof(additionalData) != "undefined" && additionalData != "")
		theData += additionalData + "&";	

	var excludeFlags = this.GetExcludeFlags( target );

	var elemCount = theform.elements.length;
	for( var i=0; i<elemCount; i++ )
	{
		curElem = theform.elements[i];
		eName = curElem.name;
		if( eName && eName != '' && curElem.tagName != "EMBED")
		{
			if( eName == '__EVENTTARGET' || eName == '__EVENTARGUMENT' )
			{
				// Do Nothing
			}
			else if ( ! this.ExcludeFromPost(curElem, ajaxScopeID, excludeFlags) )
			{
				if ( __bPageIsStored && eName == '__VIEWSTATE' )
					continue;

				var type = curElem.type;
				var val = curElem.value;

				if ( type == "submit" || type == "button" )
					continue;

				val = this.EncodePostData(val);

				if ( type == "select-multiple" || type == "select-one" )
				{
					var selectLength = curElem.options.length;
					var optNameStr = this.EncodePostData(eName);
					for (var j=0; j < selectLength; j++)
						if (curElem.options[j].selected)
							theData = theData + optNameStr + '=' + this.EncodePostData(curElem.options[j].value) + '&';
				}
				else if ( (type != "checkbox" && type != "radio") || curElem.checked )
				{
					theData = theData + this.EncodePostData(eName) + '=' + val + '&';
				}
			}
		}
	}
  
	if (theData.substr(theData.length-1) == "&")
		theData = theData.substr(0, theData.length-1);
		
	if( this.XmlHttp )
	{		
		if (waitElement)
		{
			waitElement.style.visibility = 'visible';
			MoveWaitElement();
		}

		var oThis = this;
		__AJAXCboList.push(oThis);
		AJAXCbo = new AjaxCallObject();
		
		if (__bTracing)
		{
			this.CreateTracingWindow();
			this.TraceSentData(theData);		
		}

		if( this.XmlHttp.readyState == 4 || this.XmlHttp.readyState == 0 )
		{
			if ( ! ajaxCallType || ajaxCallType.toLowerCase() != "sync")
			{
				// Asynchronous
				this.XmlHttp.open("POST", thePage, true);
				this.XmlHttp.onreadystatechange = function(){ oThis.ReadyStateChange(); };
				this.XmlHttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
				this.XmlHttp.send(theData);
			}
			else
			{
				// Synchronous
				// Use a timeout so that the screen refreshes before getting stack waiting the AjaxCall.
				window.setTimeout(
					function()
					{
						oThis.XmlHttp.open("POST", thePage, false);
						oThis.XmlHttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
						oThis.XmlHttp.send(theData);

						if( oThis.XmlHttp.status == 200 && oThis.XmlHttp.statusText == "OK" )
							oThis.OnComplete(oThis.XmlHttp.responseText, oThis.XmlHttp.responseXML);
						else
							oThis.OnError(oThis.XmlHttp.status, oThis.XmlHttp.statusText, oThis.XmlHttp.responseText);
					}, 1);
			}			
		}
	}
	return true;
}

AjaxCallObject.prototype.OnLoading = function()
{
  // Loading
}

AjaxCallObject.prototype.OnLoaded = function()
{
  // Loaded
}

AjaxCallObject.prototype.OnInteractive = function()
{
  // Interactive
}

AjaxCallObject.prototype.OnComplete = function(responseText, responseXml)
{
	if (__bTracing)
	{
		this.TraceReceivedData(responseText);		
	}

	if (waitElement)		
		waitElement.style.visibility = 'hidden'; 

	// Checking if the data were fully loaded, without being aborted
	var flag = "'AJAX_LOADING_OK';";
	if (responseText.substr(responseText.length - flag.length) != flag)
		return false;

	eval(responseText);
	
	// Remove validators whose controltovalidate have been removed
	if (typeof(Page_Validators)!="undefined")
	{
	    for (i=0; i<Page_Validators.length; i++)
	    {
	        if (Page_Validators[i] && Page_Validators[i].controltovalidate
	            && document.getElementById(Page_Validators[i].controltovalidate)==null)
	        {
	            Page_Validators.splice(i,1);
	            i--;
	        }
	    }
	}

	return true;
}

AjaxCallObject.prototype.OnAbort = function()
{
	if (__bTracing)
	{
		this.WriteTrace("<b>!!! AjaxCall was aborted !!!</b>");
		this.CloseTrace();
	}

	if (waitElement)		
		waitElement.style.visibility = 'hidden'; 	
}

AjaxCallObject.prototype.OnError = function(status, statusText, responseText)
{
	if (status==200)
	{
		// a weird bug of Opera sometimes invokes OnError when there's no error
		this.OnComplete(responseText);
		return;
	}

	if (__bTracing)
	{
		this.WriteTrace("<b>!!! Server reported an Error !!!</b>");
		this.CloseTrace();
	}

	if (waitElement)
		waitElement.style.visibility = 'hidden';

	document.close();	// for IE
	document.write(responseText);
	document.close();	// for Firefox
}

AjaxCallObject.prototype.ReadyStateChange = function()
{
  if( this.XmlHttp.readyState == 1 )
  {
    this.OnLoading();
  }
  else if( this.XmlHttp.readyState == 2 )
  {
    this.OnLoaded();
  }
  else if( this.XmlHttp.readyState == 3 )
  {
    this.OnInteractive();
  }
  else if( this.XmlHttp.readyState == 4 )
  {
    if( this.XmlHttp.status == 0 )
      this.OnAbort();
    else if( this.XmlHttp.status == 200 )
      this.OnComplete(this.XmlHttp.responseText, this.XmlHttp.responseXML);
    else
      this.OnError(this.XmlHttp.status, this.XmlHttp.statusText, this.XmlHttp.responseText);   

	// Remove this AJAXCbo from the list
	for (var i=0; i < __AJAXCboList.length; i++)
		if (__AJAXCboList[i] == this)
		{
			__AJAXCboList[i].XmlHttp = null;
			__AJAXCboList.splice(i, 1);
			break;
		}
  }
}

AjaxCallObject.prototype.TraceWindow = null;

AjaxCallObject.prototype.SetIntervalForAjaxCall = function(milliSec)
{
	if (__ClockID != 0)
		this.ClearIntervalForAjaxCall();
	__ClockID = window.setInterval("AJAXCbo.DoAjaxCall('__AJAX_AJAXCALLTIMER','','async')", milliSec);
}

AjaxCallObject.prototype.ClearIntervalForAjaxCall = function()
{
	window.clearInterval(__ClockID);
	__ClockID = 0;
}

AjaxCallObject.prototype.CreateTracingWindow = function()
{
	for (var i=0; i < __TraceWindows.length; i++)
	{
		if (__TraceWindows[i].closed)
		{
			__TraceWindows.splice(i, 1);
			i--;
		}		
	}

	this.TraceWindow = null;
	for (var i=0; i < __TraceWindows.length; i++)
	{
		if (__TraceWindows[i].TraceFinished)
		{
			this.TraceWindow = __TraceWindows[i];
		}		
	}
	
	if (this.TraceWindow == null)
	{
		this.TraceWindow = window.open("","_blank","location=no,resizable=yes,scrollbars=yes");
		__TraceWindows.push(this.TraceWindow);
	}
	this.TraceWindow.TraceFinished = false;
}

AjaxCallObject.prototype.ClearTracingWindows = function()
{
	for (var i=0; i < __TraceWindows.length; i++)
	{
		__TraceWindows[i].close();
		__TraceWindows.splice(i, 1);
		i--;
	}
}

AjaxCallObject.prototype.TraceSentData = function(data)
{
	this.WriteTrace("<b>Ajax Call invoked at " + new Date().toLocaleTimeString() + "<br>");
	if (__bPageIsStored)
		this.WriteTrace("Page Store Mode: Stored (Session or Cache)<br>");
	else
		this.WriteTrace("Page Store Mode: NoStore<br>");
	this.WriteTrace("Form Data sent to server (" + data.length + " characters):<br>");
	this.WriteTrace("------------------------------</b><br>");

	var fields = data.split("&");
	for (var i=0; i < fields.length; i++)
	{
		var parts = fields[i].split("=");
		this.WriteTrace("<b>" + parts[0] + "=</b>");
		this.WriteTrace(this.EncodeTraceData(parts[1]) + "<br>");
	}
		
	this.WriteTrace("<b>------------------------------</b><br>");
	this.WriteTrace("Waiting response from server.....<br>");
}

AjaxCallObject.prototype.TraceReceivedData = function(data)
{
	this.WriteTrace("<b>Server responsed at " + new Date().toLocaleTimeString() + "<br>");
	this.WriteTrace("Javascript code received from server (" + data.length + " characters):<br>");
	this.WriteTrace("------------------------------</b><br>");
	this.WriteTrace(this.EncodeTraceData(data) + "<br>");
	this.WriteTrace("<b>------------------------------</b><br>");
	this.CloseTrace();
}

AjaxCallObject.prototype.WriteTrace = function(text)
{
	this.TraceWindow.document.write(text);
}

AjaxCallObject.prototype.CloseTrace = function()
{
	this.TraceWindow.document.close();
	this.TraceWindow.TraceFinished = true;
}

AjaxCallObject.prototype.EncodeTraceData = function(data)
{
	return data.split("<").join("&lt;").split(" ").join("&nbsp;").split("\n").join("<br>");
}

AjaxCallObject.prototype.EncodePostData = function(data)
{
	return data.split("%").join("%25").split("=").join("%3d").split("&").join("%26").split("+").join("%2b");
}

AjaxCallObject.prototype.SetAttributesOfControl = function(clientID, attributes)
{
	var place = document.getElementById(clientID);
	if (place != null && attributes != "")
	{
		var attribs = attributes.split("|");
		for (var i=0; i < attribs.length; i++)
		{
			var parts = attribs[i].split("=");
			place.setAttribute(parts[0], parts[1])
		}
	}
}

AjaxCallObject.prototype.AddElement = function(parentID, tagName, elementID, html, beforeElemID)
{
	var place = document.getElementById(parentID);
	var child = (elementID != "") ? document.getElementById(elementID) : null;
	if (place != null && child == null)
	{
		child = document.createElement(tagName);
		child.id = elementID;
		var before = (beforeElemID != null) ? document.getElementById(beforeElemID) : null;
		place.insertBefore(child, before);
		child.innerHTML = html;
	}
	else
		this.SetHtmlOfElement(html, elementID);
}

AjaxCallObject.prototype.AddScript = function(scriptText, scriptAttributes)
{
	var scriptHolder = document.createElement('script');
	scriptHolder.text = scriptText;

	if (scriptAttributes != null)
		for (i=0; i < scriptAttributes.length; i+=2)
			scriptHolder.setAttribute(scriptAttributes[i], scriptAttributes[i+1]);

	__PageForm.appendChild(scriptHolder);
}

AjaxCallObject.prototype.AddHiddenField = function(elementName, elementValue)
{
	var hiddenField = document.createElement('input');
	hiddenField.type = "hidden";
	hiddenField.name = elementName;
	hiddenField.id = elementName;
	hiddenField.value = elementValue;
	
	__PageForm.appendChild(hiddenField);
}

AjaxCallObject.prototype.RemoveElement = function(parentID, elementID)
{
	var place = document.getElementById(parentID);
	var child = document.getElementById(elementID);
	if (place != null && child != null)
		place.removeChild(child);
}

AjaxCallObject.prototype.SetField = function(fieldName, fieldValue)
{
	var field;
	if (__PageForm)
		field = __PageForm[fieldName];
	else
		field = document.all[fieldName];

	if (field != null)
		field.value = fieldValue;
}

AjaxCallObject.prototype.AddHeaderElement = function(tagName, innerText, attributes)
{	
	switch (tagName)
	{
		case "link": 
			var link = document.getElementsByTagName("head")[0].appendChild(document.createElement("link"));
			for (i=0; i < attributes.length; i+=2)
				link.setAttribute(attributes[i], attributes[i+1]);
			break;
		case "title":
			document.title = innerText;
			break;
		case "style":
			if (document.styleSheets && innerText != null)
			{
				// based on http://www.bobbyvandersluis.com/articles/dynamicCSS.php
				if (document.styleSheets.length == 0)
				{
					//no stylesheets yet, so create empty one
					var head = document.getElementsByTagName("head")[0];
					var style = document.createElement("style");
					style.type = "text/css";
					head.appendChild(style);
				}

				//add style rule to last stylesheet (forces proper cascading)
				var lastStyle = document.styleSheets[document.styleSheets.length - 1];
				var ieNewRule = typeof lastStyle.addRule == "object";
				var ffNewRule = typeof lastStyle.insertRule == "function";
				if (ieNewRule || ffNewRule)
				{
					var splitRules = innerText.split('}');
					for (i=0; i<splitRules.length-1; i++)
					{
						if (ffNewRule)
						{
							//Mozilla
							lastStyle.insertRule(splitRules[i] + "}", lastStyle.cssRules.length);
						}
						else
						{
							//IE
							var splitNameValue = splitRules[i].split('{');
							lastStyle.addRule(splitNameValue[0], splitNameValue[1]);
						}
					}
				}
			}
			break;
	}
}

AjaxCallObject.prototype.SetFieldIfEmpty = function(fieldName, fieldValue)
{
	var field;
	if (__PageForm)
		field = __PageForm[fieldName];
	else
		field = document.all[fieldName];
	
	if (field != null && field.value == '')
		field.value = fieldValue;
}

AjaxCallObject.prototype.SetHtmlOfElement = function(html, elementID)
{
	var place = document.getElementById(elementID);
	if (place != null)
		place.innerHTML=html;
}

AjaxCallObject.prototype.SetHtmlOfPage = function(html)
{
	document.close();	// for IE
	document.write(html);
	document.close();	// for Firefox
}

AjaxCallObject.prototype.SetVisibilityOfElement = function(elementID, visible)
{
	var place = document.getElementById(elementID);
	if (place != null)
		place.style.display = (visible) ? "" : "none";
}

AjaxCallObject.prototype.Alert = function(message)
{
	window.alert(message);
}

// It's used by AjaxPanel
AjaxCallObject.prototype.ExtendedSetHtmlOfElement = function(html, elementID)
{
	var place=document.getElementById(elementID);
	if (place != null)
	{
		var store = document.createElement(place.tagName);
		store.innerHTML = html;
		var markers = store.getElementsByTagName("span");
		for (var i=markers.length-1; i >= 0; i--)
		{
			var elem = markers[i];
			if (elem.getAttribute("name") == "__ajaxmark")
			{
				var elemOnPage = document.getElementById(elem.id);
				if (elemOnPage != null)
					elem.parentNode.replaceChild(elemOnPage, elem);
			}
		}
		
		if ("mergeAttributes" in store)
		{
			store.mergeAttributes(place, false);
		}
		else
		{
			for (var i=place.attributes.length-1; i >= 0; i--)
			{
				var attr = place.attributes[i];
				store.setAttribute(attr.name, attr.value);
			}
		}
		
		if (__IsIE)
			place.parentNode.replaceChild(store, place);
	    else
		{
			//Netscape/Firefox has a problem using replaceChild when replaced child contains a table
			place.parentNode.insertBefore(store, place);
			place.parentNode.removeChild(place);
		}
		
		place = null; //cleanup
	}
}

var AJAXCbo = new AjaxCallObject();

// wait element 
CreateWaitElement();
if (window.addEventListener) {
	window.addEventListener('scroll', MoveWaitElement, false);
	window.addEventListener('resize', MoveWaitElement, false);
}
else if (window.attachEvent) {
	window.attachEvent('onscroll', MoveWaitElement);
	window.attachEvent('onresize', MoveWaitElement);
}
var waitElement;
var scrollX, scrollY = -1;
function MoveWaitElement() {
	var scrollYT, scrollXT;
	if (!waitElement)
		CreateWaitElement();
	if (typeof(window.pageYOffset) == "number") { 
		scrollYT = window.pageYOffset; 
		scrollXT = window.pageXOffset; 
	} 
	else if (document.body && document.documentElement && document.documentElement.scrollTop) { 
		scrollYT = document.documentElement.scrollTop; 
		scrollXT = document.body.scrollLeft;
	}
	else if (document.body && typeof(document.body.scrollTop) == "number") { 
		scrollYT = document.body.scrollTop; 
		scrollXT = document.body.scrollLeft; 
	} 
	if (scrollX != scrollXT || scrollY != scrollYT) {
		scrollX = scrollXT;
		scrollY = scrollYT;
		var width = document.body.clientWidth;
		waitElement.style.top = scrollYT + "px";
		waitElement.style.right = -scrollXT +  "px";
	}
}
function CreateWaitElement() {
    var elem = document.getElementById('__AjaxCall_Wait');
    if (!elem) {
        elem = document.createElement("div");
        elem.id = '__AjaxCall_Wait';
        elem.style.position = 'absolute';
        elem.style.height = 17;
        elem.style.paddingLeft = "3px";
        elem.style.paddingRight = "3px";
        elem.style.fontSize = "11px";
        elem.style.fontFamily = 'Arial, Verdana, Tahoma';
        elem.style.border = "#000000 1px solid";
        elem.style.backgroundColor = "DimGray";
        elem.style.color = "#ffffff";
        elem.innerHTML = 'Loading ...';
        elem.style.visibility = 'hidden';
        document.body.insertBefore(elem, document.body.firstChild);
    }
    waitElement = elem;
}
// end wait element