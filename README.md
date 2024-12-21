# uploadFileUsingWebAPI
Task Title: Implement a New Feature in Web API

Objective:
Enhance the Web API functionality by adding a feature that supports file upload, data processing, and response formatting, based on the requirements provided.

Task Description:
	1.	File Upload Endpoint:
	•	Create an API endpoint /uploadFile that accepts file uploads via a POST request.
	•	Validate the file type (e.g., Excel, PDF, or Word) and ensure it does not exceed a size limit (e.g., 10 MB).
	2.	Data Processing:
	•	If the file is an Excel document:
	•	Read its content using a library like EPPlus or ClosedXML.
	•	Log the first 5 rows of data in the API response (JSON format).

	•	If the file is a PDF or Word document:
	•	Generate a dummy summary of its metadata (e.g., file size in(kb/Mb), number of pages).
	•	Save a copy in the server’s designated folder with a unique name.
	3.	Response:
	•	Return a standardized JSON response containing:
	•	Upload status (Success/Failure).
	•	Details of the file processed.
	4.	Error Handling:
	•	Handle errors gracefully for:
	•	Invalid file type.
	•	Large files.
	•	Issues in reading or processing the file.
	5.	Documentation:
	•	Update the Swagger/OpenAPI documentation to include the new endpoint, request schema, and response formats.

Deliverables:
	•	Fully functional /uploadFile API endpoint.
	•	Standardized responses and proper error handling.
	•	Updated API documentation with usage details.

Estimated Time: 8 hours

Tools/Libraries:
	•	.NET Framework/Core (depending on the project version)
	•	EPPlus/ClosedXML for Excel processing
	•	Spire.PDF or iTextSharp for PDF handling (if applicable)
