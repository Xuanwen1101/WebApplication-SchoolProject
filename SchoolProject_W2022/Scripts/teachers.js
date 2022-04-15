function addTeacher() {

	if (!validateTeacher()) return;

	//goal: send a request with POST data of teacherfname, teacherlname, employeenumber, hiredate, and salary.
	//POST : http://localhost:56815/api/TeacherData/addTeacher

	var URL = "http://localhost:56815/api/TeacherData/addTeacher/";

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var EmployeeNumber = document.getElementById('EmployeeNumber').value;
	var TeacherSalary = document.getElementById('TeacherSalary').value;


	/*var TeacherData = {
		"TeacherFname": TeacherFname,
		"TeacherLname": TeacherLname,
		"EmployeeNumber": EmployeeNumber,
		"TeacherSalary": TeacherSalary
	};*/

	var TeacherData = {
		"TeacherFName": TeacherFname,
		"TeacherLName": TeacherLname,
		"EmployeeNumber": EmployeeNumber,
		"SalaryString": TeacherSalary
	};

	console.log(JSON.stringify(TeacherData));

	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//console.log(JSON.stringify(TeacherData));
		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));

}

function updateTeacher(TeacherId) {

	if (!validateTeacher()) return;

	//goal: send a request with POST data of teacherfname, teacherlname, employeenumber, hiredate, and salary.
	//POST : http://localhost:56815/api/TeacherData/UpdateTeacher/{id}

	var URL = "http://localhost:56815/api/TeacherData/UpdateTeacher/" + TeacherId;

	var rq = new XMLHttpRequest();

	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var EmployeeNumber = document.getElementById('EmployeeNumber').value;
	var TeacherSalary = document.getElementById('TeacherSalary').value;


	var TeacherData = {
		"TeacherFName": TeacherFname,
		"TeacherLName": TeacherLname,
		"EmployeeNumber": EmployeeNumber,
		"SalaryString": TeacherSalary
	};

	console.log(JSON.stringify(TeacherData));

	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			console.log(JSON.stringify(TeacherData));


		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));

}

function validateTeacher() {

	var IsValid = true;
	var ErrorMsg = "";
	var ErrorBox = document.getElementById("ErrorBox");
	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var EmployeeNumber = document.getElementById('EmployeeNumber').value;
	var TeacherSalary = document.getElementById('TeacherSalary').value;

	//First Name is one or more characters
	if (TeacherFname.length < 1) {
		IsValid = false;
		ErrorMsg += "First Name must be one or more characters.<br>";
	}
	//Last Name is one or more characters
	if (TeacherLname.length < 1) {
		IsValid = false;
		ErrorMsg += "Last Name must be one or more characters.<br>";
	}
	//EmployeeNumber is valid pattern
	if (!validateEmployeeNumber(EmployeeNumber)) {
		IsValid = false;
		ErrorMsg += "Employee Number must start with T and follow with three numbers.<br>";
	}

	//TeacherSalary
	if (!validateSalary(TeacherSalary)) {
		IsValid = false;
		ErrorMsg += "Please enter a valid Salary.<br>";
	}

	if (!IsValid) {
		ErrorBox.style.display = "block";
		ErrorBox.style.color = "red";
		ErrorBox.innerHTML = ErrorMsg;
	} else {
		ErrorBox.style.display = "none";
		ErrorBox.innerHTML = "";
	}


	return IsValid;
}

function validateEmployeeNumber(idValue) {
	var idPattern = /^[tT][0-9]{3}$/;
	return idPattern.test(idValue);
}

function validateSalary(salary) {
	var salaryPattern = /^[0-9]*.[0-9]{0,2}$/;
	return salaryPattern.test(salary);
}