# Domain Driven Desing in Banking Server
This Web API project uses ddd and separation of concerns to implement a banking server. The project is divided into 3 layers:
1. Domain Layer
2. Application Layer
3. Infrastructure Layer

## Domain Layer
The domain layer contains the business logic of the application. It is divided into 3 sub-layers:
1. Entities (Models)
2. Value Objects
3. Services

### Entities
The Entities or the models 

### Value Objects
The Value Objects are the properties of the entities. They are immutable and are used to validate the entities.

### Services
The Services are the business logic of the application. They are used to validate the entities and perform the business logic.

## Application Layer
The application layer contains the application logic of the application. It is divided into 3 sub-layers:
1. Controllers
2. Services
3. Repositories

### Controllers
The Controllers are the entry point of the application. They are used to validate the input and call the application services.

### Services
The Services are the application logic of the application. They are used to validate the input and call the domain services.

### Repositories
The Repositories are the data access layer of the application. They are used to access the data from the database.

## Infrastructure Layer
The infrastructure layer contains the infrastructure logic of the application. It is composed of :
1. Database Context
   2. Database Migrations
   3. Database Seeders

### Controllers