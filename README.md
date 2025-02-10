# SolarPower

This is the results of the software development home task for Uprise.\
&copy; 2025 Daniel Zagar

## Personal note

The compilation of material serves two purposes:
* To solve the tasks at hand, as given in the home task
* To illustrate my programming style, and how I usually prefer to arrange Visual Studio projects

## Getting started

After cloning the material to a local disk, the following steps should be carried out:  
1 Use a client connection to an MS SQL database (I used MS SQL Server Management Studio), and run the scripts in the folder: `$/SQL/DDL`  

2 Open Visual Studio, and run the NuGet package manager console. Set the default project to `SolPwr.DomainModel.Orm`. Run `Update-Database -context UtilitiesContext`  

3 Still in the NuGet package manager console, switch the default project to `SolPwr.AuthModel`. Run `Update-Database -context AuthIdentityContext`  

4 Now, the application can be fired up. 

## Using the API

### Authorize
Create at least one user account with the **POST** call:

`http://localhost:5132/auth/register`  

Fill in the body according to this example:  
```
{
    "Email":"a.b@com",
    "Password":"X.x1",
    "ConfirmPassword":"X.x1"
}
```

To retrieve a JWT token, user can now authenticate with **POST**:

`http://localhost:5132/auth/signon`  

Fill in the body:  
```
{
    "Email":"a.b@com",
    "Password":"X.x1",
}
```

### CRUD Operations
> [!NOTE]
> All CRUD operations must be authorized with a JWT bearer token in the header.

To select all power plants, call this **GET**  
`http://localhost:5132/api/GetAllPlants`  

To create a new plant, use this **POST**  

`http://localhost:5132/api/CreatePlant`  

Fill in the body according to this example:  
```
{
    "id": "F530A983-3DAA-44B7-8AFC-74A5994967F6",
    "plantName": "Plant API",
    "utcInstallDate": "2025-02-09T09:40:00",
    "location": {
        "latitude": 10,
        "longitude": 20
    },
    "powerCapacity": 400
}
```

To delete an existing plant, and all its related power history. Use the following **PUT**:  

`http://localhost:5132/api/DeletePlant?id={Plant ID}`  

where `Plant ID` is the GUID value of the plant in question. Example: 

`http://localhost:5132/api/DeletePlant?id=9C8D7E47-5F83-4BE8-B323-D9D2F1439FD0`  

To update the properties of a plant (albeit altering the location dosen't make sense in reality), use the **PUT**:  

`http://localhost:5132/api/UpdatePlant?id={Plant ID}`  

Fill in the body according to this example:  
```
{
    "plantName": "Plant API",
    "utcInstallDate": "2025-02-09T09:40:00",
    "location": {
        "latitude": 10,
        "longitude": 20
    },
    "powerCapacity": 400
}
```


### Power queries
> [!NOTE]
> All power queries must be authorized with a JWT bearer token in the header.

To retrieve power data, use the follwing **GET**:  

`http://localhost:5132/api/GetPowerData?id={Plant ID}&type={Type Code}&resolution={Resolution Key}&timespan={Time Span Value}&timespancode={Time Span Type}`  

The fields in the query string can have the follwoing values:  

| Field | Values |
| ------- | ------- |
| `Plant ID` | The GUID id for the plant |
| `Type Code` | 1 = History<br/> 2 = Prognosis |
| `Resolution Key` | 15 = Every fifteen minutes\ 60 = Every hour |
| `Time Span Value` | The quantity of the desired time, according to its units |
| `Time Span Type` | ['d' \| 'h' \| 'm'] |

## Component structure

Separation of concerns was a driving factor for the component architecture, with the following characteristics:  
1 The dependency injection and configuration of containers, is forwarded to extension methods in corresponding sub system. This will hide details from `Program.cs` and keep it clean and tidy as well.  
2 The ORM data layer is in general separated from the Dto data layer.  
3 The authentication and authorization part is separated from the rest of the application, and also with respect to backing storage. Two different SQL schemes were used to keep track of that separation in the very database (`spu` and `spa`), instead of the commonly used `dbo`.  
4 The part that concerns a dependency to a specific RDBMS is kept in the projects with suffixes Orm. In this case, there is a dependency to MsSql Server there, but not elsewhere.  

The part which is handling the connectivity to a meteorological service, is implemented using a plugin architecture. It means that the application can dynamically handle various meteorological services (one at a time), if so desired. The `appsettings.json` contains a section which will point out what provider should be used at runtime. An assembly scoped attribute is used inside the plugin project to tag the DLL for plugin identification. Only one provider is implemented, though (www.open-meteo.com).  

Please note that the `MeteoService` configuration section has the value `open-meteo.com`. That is the name of the plugin identifier, and has nothing to do with the URL to that very meteo service. In fact, any name could have been used there.

## VS Projects

| Project | Purpose |
| ------- | ------- |
| `SolPwr.Application.Api` | The main REST executable |
| `SolPwr.Core` | Contains base classes and various primitives |
| `SolPwr.DomainModel` | Domain classes for code first |
| `SolPwr.DomainModel.Orm` | The Entity Framework implementation |
| `SolPwr.AuthModel` | The identity implementation using JWT |
| `SolPwr.Integrations.Core` | Generic loading and management of any meteorological extension |
| `SolPwr.Integrations.Meteo` | Specific provider implementation for the selected service |
| `SolPwr.Protocols` | Service contracts and DTOs |


## Logging

Logging is not covered everywhere or on every level, but more like to illustrate some useful patterns.

## Flaws, sources of improvements and other remarks

Some shortcuts have been taken to limit the scope, and to stay on focus  
1 The project `SolPwr.DomainModel.Orm` has two areas of responsibility, and thus has a dependency to both the Domain model and the Dto model. A better approach would have been to put a separate implementation in a different projects and use a pattern to support Entity Framework. For instance repository and/or unit of work.  
2 The Tools controller and seeding API is not authorized, for testing convenience, but could be very easily.  
3 The instantatation of the `IntegrationEndpoint` and the plugin loader with the `IBackgroundWorker`pattern, might be a bit inside-out. Could be refactored to become more straightforward. But, as this is beoynd the programming task anyway, I'll leave it like that.  
4 And, of course the obvious - no test projects are included.

## Taxonomy

The way projects and namespaces are arranged in the solution is following a strict taxonomy

Namespaces are all originated from the templated file `Directory.Build.props`, residing in the solution directory. All new projects are set to have the default namespace set to the configuration property value `$(MasterNamespace)`. This means that all VS projects will have an identical root namespace. Sub namespaces will then be named according to the folder structure in each project.

Please note that namespaces are in general orthogonal to the names of the projects and their physical names in the file system.

An example: The namespace

`OnionDlx.SolPwr.BusinessObjects`

is used in both the projects `SolPwr.DomainModel` and `SolPwr.DomainModel.Orm` 

The purpose of this, is to keep a separation between the logical object oriented abstractions inside the software stack, apart from the dependency and component hierarchy incarnated in the project files. These _can_ correlate incidentally, but not in general.

