﻿- This application requires a local SQL Server instance (2014 or greater)
- In order to run the application and run the tests, change the connection string for your local sql database in classes TruckContext.cs and Unittest1, and also in the json configuration file.
- The constructor of the unit test method will clear and seed the database for testing purposes.