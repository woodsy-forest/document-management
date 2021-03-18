# document-management
This solution has been created in Visual Studio 2019. 
It is written in c#, .net core 5.0, EF 5.0, SQL Server 2019. 

Create an empty database and set the connection string in appsettings.Development.json
The files are store in the folder set up in the appsettings.Development.json
The app runs on: http://localhost:5000 this will create the document table.

The API can be tested using Postman, for example:

POST, http://localhost:5000/api/document , in body, tick form-data, key: file, type File, select the file in value and send

DELETE, http://localhost:5000/api/document/{id}

GET, http://localhost:5000/api/document