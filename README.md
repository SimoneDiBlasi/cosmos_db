# Cosmos DB CRUD Application with .NET 8

This repository demonstrates how to build a .NET 8 application that interacts with **Azure Cosmos DB** using basic CRUD operations. The project leverages the Cosmos DB SQL API (NoSQL) and demonstrates partitioning, LINQ queries, and asynchronous data retrieval.

## Features

- **Cosmos DB Integration**: Connect to Azure Cosmos DB using the `CosmosClient` class.
- **Partition Key Support**: Queries and operations are executed based on the partition key (`TenantId`).
- **LINQ Queries**: Perform Cosmos DB queries using LINQ with partition key and filter support.
- **CRUD Operations**: Basic Create, Read, Update, and Delete operations on `Post` entities.

## Getting Started

### Prerequisites

To run this project, you'll need:

- **.NET 8 SDK**: Ensure .NET 8 is installed on your machine.
- **Azure Cosmos DB**: Set up an Azure Cosmos DB instance with the SQL API (NoSQL).
- **Azure Cosmos DB Emulator** (optional): For local development, you can use the Azure Cosmos DB Emulator.

### Project Structure

- **Controllers**: Contains a controller to handle API requests for performing CRUD operations.
- **Models**: Defines the `Post` class, which represents the data structure used in Cosmos DB.
- **Services**: The interface `IPostService` defines the CRUD operations.
- **Program.cs**: Sets up the CosmosClient connection and dependency injection.

### Setting up Cosmos DB

1. **Configure Azure Cosmos DB**: 

   Update the `appsettings.json` with your Cosmos DB details:

   ```json
   {
     "CosmosDB": {
       "ConnectionString": "<your-connection-string>"
     }
   }
