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

## Taxonomy

The way projects and namespaces are arranged in the solution is following a strict taxonomy

Namespaces are all originated from the templated file Directory.Build.props, residing in the solution directory. All new projects are set to have the default namsepace set to the configuration property value $(MasterNamespace). This means that all VS projects will have an identical root namespace. Sub namespaces will then be named according to the folder structure in each project.

Please note that namespaces are in general orthogonal to the names of the projects and their physical names in the file system.

An exampel: The namespace

OnionDlx.SolPwr.BusinessObjects

is used in both the projects SolPwr.DomainModel and SolPwr.DomainModel.Orm

The purpose of this, is to keep a separation between the logical object orientaed abstractions inside the software stack, apart from the dependency and component hierarchy incarnated in the project files. These _can_ correlate incidentally, but not in general.

