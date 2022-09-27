# AgorusApi
Simple file storage API service

## Endpoints and payloads
* GET: api/File
* GET: api/File/{id}
* DELETE: api/File/{id}
* DELETE: api/File/{id}/History/{historyId}
* PUT: api/File/{id}/History/{historyId}
* POST: api/File/
* No payload

## Instructions
* To execute the API, you can
    * Run it using Visual Studio or 
    * Install the API in the ISS and run it directly in your browser as configured in the ISS (see [details](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/?view=aspnetcore-6.0)).
* To open the swagger use the url like this (change if necessary as configured in ISS, if it's the case): 
```
https://localhost:7175/swagger/ 
```
## Notes
1. The project is using the minimal API only for GET and DELETE methods
2. For the PUT and POST methods, the project uses the controller because these methods are not fully implemented and it's complex to handle the IFormFile as a parameter