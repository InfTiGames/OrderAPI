# OrderAPI

OrderAPI API

Overview
OrderAPI API is a RESTful web service built using .NET 8 that provides functionalities for managing orders.
This API allows you to create orders, view all orders, and process payments for orders. 
It utilizes Entity Framework Core with an in-memory database for persistence and integrates asynchronous payment processing using a background service.

Features
Create an Order: Allows users to create a new order with a specified customer name, order date, and order items.
Get All Orders: Retrieve a list of all orders in the system.
Process Payment: Update the status of an order based on payment information.
Asynchronous Payment Processing: Payments are processed asynchronously using a background service and a channel.

Architecture
The application follows a clean architecture with the following components:

Controllers: Handles HTTP requests and responses.
Data: Contains the Entity Framework Core database context.
Models: Defines the data structures used in the application.
Repositories: Implements data access logic.
Services: Contains business logic and handles payment processing.
Utilities: Provides additional functionality such as exception handling middleware.
Getting Started
Prerequisites
.NET 8 SDK
Visual Studio or any other .NET-compatible IDE

Installation
Clone the repository:
git clone https://github.com/InfTiGames/OrderAPI.git

Navigate to the project directory:

cd OrderAPI

Restore the dependencies:

dotnet restore

Run the application:

dotnet run

API Endpoints

Create an Order

URL: /api/orders
Method: POST
Request Body: OrderDTO object
Success Response: 201 Created with the created OrderDTO

Get All Orders

URL: /api/orders
Method: GET
Success Response: 200 OK with a list of OrderDTO objects

Get Order By ID

URL: /api/orders/{id}
Method: GET
URL Params: id (integer)
Success Response: 200 OK with OrderDTO object
Error Response: 404 Not Found if the order does not exist

Process Payment

URL: /api/orders/payment
Method: POST
Request Body: PaymentInfo object
Success Response: 204 No Content

Testing with Swagger
Swagger is integrated for interactive API documentation and testing. To access Swagger UI:

Run the application.
Open a web browser and navigate to http://localhost:5000/swagger (or the port your application is running on).

Configuration
The application is configured to use an in-memory database for development purposes. It includes:

In-Memory Database: For persistence using Entity Framework Core.
Background Payment Processing: Uses a channel and background service for asynchronous payment handling.
Exception Middleware: Handles exceptions globally and returns appropriate error responses.

Contribution
Contributions to this project are welcome. Please fork the repository and submit a pull request with your changes.

License
This project is licensed under the MIT License - see the LICENSE file for details.

Contact
For any questions or feedback, please open an issue in the GitHub repository or contact your-email@example.com.

