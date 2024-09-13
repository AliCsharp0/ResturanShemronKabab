//Validation Name if (/[^\u0600-\u06FF]/g.)
function ValidationName(elem) {
	var reg = /^[A-Za-zآابپتثجچحخدذرزژسشصضطظعغفقکگلمنوهی\s]+$/;//.test(x)
	var name = document.getElementById("Name").value;
	var isMatch = reg.test(name);
	var spnErrorName = elem.getAttribute("data-Err-Name");
	var msg = elem.getAttribute("data-msg");
	if (!isMatch) {
		document.getElementById(spnErrorName).innerHTML = msg;
		document.getElementById(spnErrorName).style.display = "block";
	}
	else {
		document.getElementById(spnErrorName).style.display = "none";
	}
}

function ValidationLast(elem) {
	var reg = /^[A-Za-zآابپتثجچحخدذرزژسشصضطظعغفقکگلمنوهی\s]+$/;//.test(x)
	var name = document.getElementById("Last").value;
	var isMatch = reg.test(name);
	var spnErrLast = elem.getAttribute("data-Err-Last");
	var msg = elem.getAttribute("data-msg");
	if (!isMatch) {
		document.getElementById(spnErrLast).innerHTML = msg;
		document.getElementById(spnErrLast).style.display = "block";
		elem.setCustomValidity(msg);
	}
	else {
		document.getElementById(spnErrLast).style.display = "none";
		elem.setCustomValidity('');
	}
}

//Validation Age
function ValidationAge(elem) {
	var reg = /^[1-9]?[0-9]{1}$|^100$/;
	var age = elem.value;
	var isMatch = reg.test(age);
	var spnErrorAge = elem.getAttribute("data-Err-Age");
	var msg = "Enter your Age correctly";

	if (!isMatch) {
		document.getElementById(spnErrorAge).innerHTML = msg;
		document.getElementById(spnErrorAge).style.display = "block";
		elem.setCustomValidity(msg);
	}
	else {
		document.getElementById(spnErrorAge).style.display = "none";
		elem.setCustomValidity('');
	}
}

function ValidationEmail(elem) {
	var reg = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
	var email = elem.value;
	var isMatch = reg.test(email);
	var spnErrorEmail = elem.getAttribute("data-Err-Email");
	var msg = "Enter your Email correctly Example(Ali@gmail.com)";
	if (!isMatch) {
		document.getElementById(spnErrorEmail).innerHTML = msg;
		document.getElementById(spnErrorEmail).style.display = "block";
		elem.setCustomValidity(msg);
	} else {
		document.getElementById(spnErrorEmail).style.display = "none";
		elem.setCustomValidity('');
	}
}


//Validation Mobile
function ValidationMobile(elem) {
	var reg = /^\(?(\d{3})\)?[- ]?(\d{3})[- ]?(\d{4})$/;
	var Mobile = elem.value;
	var isMatch = reg.test(Mobile);
	var spnErrorMobile = elem.getAttribute("data-Err-Mobile");
	var msg = "Enter your Mobile correctly For Example(9124586214)";
	if (!isMatch) {
		document.getElementById(spnErrorMobile).innerHTML = msg;
		document.getElementById(spnErrorMobile).style.display = "block";
		elem.setCustomValidity(msg);
	}
	else {
		document.getElementById(spnErrorMobile).style.display = "none";
		elem.setCustomValidity('');
	}
}

//Validation Password
function ValidationPassword(elem) {
	var reg = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*(\W|_)).{5,}$/;
	var password = document.getElementById("Password").value;
	var isMatch = reg.test(password);
	var spnErrorPassword = elem.getAttribute("data-Err-Password");
	var msg = "Must contain at least one number and one uppercase and lowercase letter Has one of the signs (#,$,*,/,\,|,.), and at least 8 or more characters";
	if (!isMatch) {
		document.getElementById(spnErrorPassword).innerHTML = msg;
		document.getElementById(spnErrorPassword).style.display = "block";
	}
	else {
		document.getElementById(spnErrorPassword).style.display = "none";
	}
}

//Validation UserName
function ValidationUserName(elem) {
	var reg = /^[a-zA-Z0-9]+([._]?[a-zA-Z0-9]+)*$/;
	var userName = elem.value;
	var isMatch = reg.test(userName);
	var spnErrUserName = elem.getAttribute("data-Err-userName");
	var msg = "Enter your User Name correctly";
	if (!isMatch) {
		document.getElementById(spnErrUserName).innerHTML = msg;
		document.getElementById(spnErrUserName).style.display = "block";
		elem.setCustomValidity(msg);
	}
	else {
		document.getElementById(spnErrUserName).style.display = "none";
		elem.setCustomValidity('');
	}
}


//Validation Address
function ValidationAddress(elem) {
	var reg = /^[A-Za-zآابپتثجچحخدذرزژسشصضطظعغفقکگلمنوهی0-9\s]+$/;
	var address = document.getElementById("Address").value;
	var isMatch = reg.test(address);
	var spnErrAddress = elem.getAttribute("data-Err-Address");
	var msg = "Enter your Address correctly";
	if (!isMatch) {
		document.getElementById(spnErrAddress).innerHTML = msg;
		document.getElementById(spnErrAddress).style.display = "block";
	}
	else {
		document.getElementById(spnErrAddress).style.display = "none";
	}
}


//Validation TelHome
function ValidationTelHome(elem) {
	var reg = /^(\(?(?:0(?:0|11)\)?[\s-]?\(?|\+)(44)\)?[\s-]?)?\(?0?(?:\)[\s-]?)?([1-9]\d{1,4}\)?[\d\s-]+)((?:x|ext\.?|\#)\d{3,4})?$/;
	var telHome = document.getElementById("TelHome").value;
	var isMatch = reg.test(telHome);
	var spnErrTelHome = elem.getAttribute("data-Err-TelHome");
	var msg = "Enter your Tel Home correctly + Code City For Example(02133782569)";
	if (!isMatch) {
		document.getElementById(spnErrTelHome).innerHTML = msg;
		document.getElementById(spnErrTelHome).style.display = "block";
		elem.setCustomValidity(msg);
	}
	else {
		document.getElementById(spnErrTelHome).style.display = "none";
		elem.setCustomValidity('');
	}
}

//Validation Rights
function ValidationRights(elem) {
	var reg = /^[0-9]+$/;
	var rights = document.getElementById("Rights").value;
	var isMatch = reg.test(rights);
	var spnErrRights = elem.getAttribute("data-Err-Rights");
	var msg = "Enter your Rights correctly(just Number)";
	if (!isMatch) {
		document.getElementById(spnErrRights).innerHTML = msg;
		document.getElementById(spnErrRights).style.display = "block";
		elem.setCustomValidity(msg);
	}
	else {
		document.getElementById(spnErrRights).style.display = "none";
		elem.setCustomValidity('');
	}
}

//Validation SmallDescription
function ValidationSmallDescription(elem) {
	var reg = /^[A-Za-zآابپتثجچحخدذرزژسشصضطظعغفقکگلمنوهی0-9\s]+$/;
	var smallDescription = document.getElementById("SmallDescription").value;
	var isMatch = reg.test(smallDescription);
	var spnErrSmallDescription = elem.getAttribute("data-Err-SmallDescription");
	var msg = "Enter your SmallDescription correctly";
	if (!isMatch) {
		document.getElementById(spnErrSmallDescription).innerHTML = msg;
		document.getElementById(spnErrSmallDescription).style.display = "block";
	}
	else {
		document.getElementById(spnErrSmallDescription).style.display = "none";
	}
}


//Validation UnitPrice
function ValidationUnitPrice(elem) {
	var reg = /^[0-9]+$/;
	var unitPrice = document.getElementById("UnitPrice").value;
	var isMatch = reg.test(unitPrice);
	var spnErrUnitPrice = elem.getAttribute("data-Err-UnitPrice");
	var msg = "Enter your UnitPrice correctly(just Number)";
	if (!isMatch) {
		document.getElementById(spnErrUnitPrice).innerHTML = msg;
		document.getElementById(spnErrUnitPrice).style.display = "block";
		elem.setCustomValidity(msg);
	}
	else {
		document.getElementById(spnErrUnitPrice).style.display = "none";
		elem.setCustomValidity('');
	}
}