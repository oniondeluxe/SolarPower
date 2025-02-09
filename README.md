# SolarPower

This is the results of the software development home task for Uprise.
&copy; 2025 Daniel Zagar

## Personal note

The compilation of material serves two purposes:
* To solve the tasks at hand, as given in the home task
* To illustrate my programing style, and how I usually prefer to arrange Visual Studio projects

## Getting started

After cloning the material to a local disk, the following steps should be carried out:
1 Use a client connection to an MS SQL database (I used MS SQL Server Management Studio), and run the scripts in the folder:
$/SQL/DDL

2 Open Visual Studio, and run the NuGet package manager console. Set the default project to XX.YY. 
Run Update-Database

3 Still in the NuGet package manager console, switch the default project to XX.YY. 
Run Update-Database

4 Now, the application can be fired up. 

## Component structure

Separation of concerns was a driving factor for the component architecture, with the following requirements:
1 The dependency injection and configuration of containers, is forwarded to extension methods in corresponding sub system. This will keep Program.cs clean and tidy as well.
2 The ORM data layer is in general separated from the Dto layer. 
3 The authentication and authorization part is separated from the rest of the application, and also with respect to backing storage. Two differect SQL schemes were used to keep track of that separation in the very database (spu and spa), instead of the commonly used dbo.
4 The part that concerns a dependency to a specific RDBMS is kept in the projects with suffixes Orm. In this case, there is a dependency to MsSql Server there, but not elsewhere.

The part which is handling the connectivity with a meterological service, is implemented using a plugin architecture. It means that the application can dynamically handle various meterolocial services, if so desired. The appsetting.json contains a section which will point out what provider should be used at runtime. An assembly scoped attribute is used to tag the DLL for plugin identification. Only one provider is implemented, though (www.open-meteo.com).

## Logging


## Flaws and sources of improvements

Some shortcuts have been taken to limit the scoped
1 The project SolPwr.DomainModel.Orm has two areas of responsibility, and thus has a dependency to both the Domain model and the Dto model. A better approach would have been to put a separate implementation in a different projetc and use a pattern to support Entity Framework. For instance repository and unit of work.

## Taxonomy

The way projects and namespaces are arranged in the solution is following a strict taxonomy

Namespaces are all originated from the templated file Directory.Build.props, residing in the solution directory. All new projects are set to have the default namsepace set to the configuration property value $(MasterNamespace). This means that all VS projects will have an identical root namespace. Sub namespaces will then be named according to the folder structure in each project.

Please note that namespaces are in general orthogonal to the names of the projects and their physical names in the file system.

An exampel: The namespace

OnionDlx.SolPwr.BusinessObjects

is used in both the projects SolPwr.DomainModel and SolPwr.DomainModel.Orm

The purpose of this, is to keep a separation between the logical object orientaed abstractions inside the software stack, apart from the dependency and component hierarchy incarnated in the project files. These _can_ correlate incidentally, but not in general.

