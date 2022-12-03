# WebAPIParking

## Problem statement
The townhall is planning for the new multi level parking due growing need of parking spaces near the shopping area. The parking should support two types of vehicles: Cars and motorbikes. 
The parking can have n number of floors and each flor can have m number of parking slots. Each vehicle has unique license number, can be parked into the next freely available slot.
Once the vehicle checks out, the system should be able to calculate the price based on the tarifs.

First 15 mins in free, and then 3 Euros per hour for Cars, and 2 Euros/hr for motorbikes. 
Whole day pass for Cras is 50 Euros and 20 Euros for motorbikes respectively.

The vehicle cannot be parked twice, and system should reject next parking is parking space is full.

## project

The project is implemented using .Net Core 6 and Entity Framework. Please open the project solution file in visual studio.  

## Database

The project uses peristent database (LocalDB) located unter AppData folder. Make sure you have SQL express installed on your machine.
The web config of the project contains the path or the connection string to the databse.
```
<add key="databaseConnectionString" value="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\WebAPIParking\WebAPIParking\AppData\LocalDB.mdf;Integrated Security=True" />
```

It has four tables:
1. Parking: , containing data about current parked vehicles in the garage with License no., Floor no, slot no. etc.
2. Slot table which can be modified using Slot API, which contains functions to add slot and remove slot from the respective floors.
3. Floor 
4. Price: Contains the traffic info.

## Usage

When you run the project in debug mode, it open opens with the browser with swagger, looks down
below. One can also try calling the API using postman.

Calling /Parking/GetAll returns the JSON list of all the current parking in the garage:
Checking new vehicle in the garage call /parking/CheckIn with license no and vehicle type(car or
motorbike)
Checking Out the valid vehicle /parking/CheckOut with license no. will give the calculated price to pay.

## License
[MIT](https://choosealicense.com/licenses/mit/)

