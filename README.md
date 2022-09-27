# AgorusApi
Simple file storage API service

## Endpoints and payloads
* GET: ```api/File```
  * Read all files with all histories
* GET: ```api/File/{id}```
  * Read file by id
* DELETE: ```api/File/{id}```
  * Delete file by id and all its histories
* DELETE: ```api/File/{id}/History/{historyId}```
  * Delete the history by its id
* PUT: ```api/File/{id}/History/{historyId}```
  * Update the history by its id and the File sent
* POST: ```api/File/```
  * Create row in FileModel table and put its content in the history table (FileHistoryModel)
* No payload

## Instructions
* To execute the API, you can
  * Run it using Visual Studio or 
  * Install the API in the ISS and run it directly in your browser as configured in the ISS (see [details](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/?view=aspnetcore-6.0)).
* To open the swagger use the url like this (change if necessary as configured in ISS, if it's the case): ```https://localhost:7175/swagger```
* The solution is divided into 3 parts:
  * **AgorusApi**: this is the Web API. It uses the service to store the files in the database
  * **AgorusService**: Service that calls the repository and has all business rules
  * **AgorusCrossSharing**: contains DTOs and Models to share between projects
  * **AgorusRepository**: access the database
* The project uses SQLite and the database file it'll be installed in the LocalApplicationData. If in Windows OS for example: ```C:\Users\UserName\AppData\Local\Agorus\Agorus.db```
* The database filename can be changed in appsettings.json:
```json
  "DbConfig": {
    "FileName": "Agorus.db"
  }
```

## Notes
1. The project is using the minimal API only for GET and DELETE methods
2. For the PUT and POST methods, the project uses the controller because these methods are not fully implemented and it's complex to handle the IFormFile as a parameter
3. The main table (FileModel) contains: ```FileModelId, Name, ContentType```
4. The detail table (FileHistoryModel) contains: ```FileHistoryModelId, FileModelId, Date, Content```
  