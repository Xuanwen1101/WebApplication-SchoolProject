function addTeacher() {

	//goal: send a request which looks like this:
	//POST : http://localhost:56815/Teacher/Create/
	//with POST data of teacherfname, teacherlname, employeenumber, hiredate, and salary.

	//var URL = "http://localhost:56815/api/TeacherData/addTeacher/";

	var URL = "http://localhost:56815/Teacher/Create/";

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var EmployeeNumber = document.getElementById('EmployeeNumber').value;
	var TeacherSalary = document.getElementById('TeacherSalary').value;

	//var TeacherData = {
	//	"TeacherFName": TeacherFname,
	//	"TeacherLName": TeacherLname,
	//	"EmployeeNumber": EmployeeNumber,
	//	"Salary": TeacherSalary
	//};


	var TeacherData = {
		"TeacherFname": TeacherFname,
		"TeacherLname": TeacherLname,
		"EmployeeNumber": EmployeeNumber,
		"TeacherSalary": TeacherSalary
	};

	console.log(JSON.stringify(TeacherData));

	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished
			//nothing to render, the method returns nothing.

			//console.log(JSON.stringify(TeacherData));
		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));

}