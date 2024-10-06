# 🚗 Garage project

The **Garage project** is a console based application that allows you to create and manage multiple garages. **When a garage is created, its capacity is set upon creation and cannot be changed later.** You can add, remove, and display vehicles within each garage based on its initial capacity.

Each garage can store a variety of vehicles, such as cars, motorcycles, boats, buses, and airplanes, each with unique properties. The project also supports saving and loading garage data from a file, making it easy to manage your garage setup across different sessions.

# Features
* **Multiple garages** - Create and manage multiple garages, each with its own capacity
* **Fixed capacity** - Once a garage's capacity is set during creation, $${\color{red}it\space cannot\space be\space modified}$$
* **Vehicle management** - Add, remove, and display vehicles within each garage
* **Supports multiple vehicle types** - Store cars, motorcycles, boats, buses, and airplanes
* **File management** - Save garage and vehicle information to a file `appsettings.txt` for future use
* **Custom vehicle properties** - Each vehicle type has unique properties, such as car doors, airplane wingspan etc
* **Interactive console application** - The user interacts with the project via console input and output

*Please note: The file write operation overwrites any existing content. If you want to preserve old content, make sure to load it before saving.*

# File structure

The main data structure representing the garage is formatted in a text file. Each line represents the current state of a garage, including its capacity and the vehicles currently parked inside.

### Format

Each line in the file follows this format:
`Capacity: <number>, Vehicles: [<vehicle1>, <vehicle2>, ...]`

### Components

1. **Capacity**: 
   - Indicates the total number of vehicles that the garage can hold
   - This is represented as an integer value (e.g., `Capacity: 2`)

2. **Vehicles**: 
   - This is a list containing the details of each vehicle currently parked in the garage
   - The list can be empty (i.e., `Vehicles: []`) or contain multiple vehicle entries

## Vehicle Details

Each vehicle entry is represented in the following format:
{Type: <VehicleType>, LicensePlate: <LicensePlate>, Color: <Color>, ModelYear: <Year>, FuelType: <FuelType>, AdditionalProperty: <Value>}
- **Type**: The type of vehicle (e.g., `Car`, `Motorcycle`)
- **LicensePlate**: The vehicle's registration number (e.g., `RTX291`)
- **Color**: The color of the vehicle (e.g., `Black`)
- **ModelYear**: The year the vehicle was manufactured (e.g., `2008`)
- **FuelType**: The type of fuel used by the vehicle (e.g., `Electric`, `Gas`)
- **AdditionalProperty**: Depending on the vehicle type, additional properties may include:
  - **NumberOfDoors**: For cars, the number of doors (e.g., `4`)
  - **EngineVolume**: For motorcycles, the engine size in cubic centimeters (e.g., `800`)
 
### Here is an example of how the file content may look:

```
Capacity: 2, Vehicles: [{Type: Car, LicensePlate: RTX291, Color: Black, ModelYear: 2008, FuelType: Electric, NumberOfDoors: 4}, {Type: Motorcycle, LicensePlate: RPG209, Color: Yellow, ModelYear: 1999, FuelType: Gas, EngineVolume: 800}]
Capacity: 3, Vehicles: []
```

# Example screenshot showing each vehicle
![image](https://github.com/user-attachments/assets/96450cee-a521-450d-bf14-b737635b1d01)

