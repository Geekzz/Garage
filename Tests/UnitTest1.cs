using Garage.Garage;
using Garage.Models;

namespace Tests
{
    public class GarageTests
    {
        [Fact]
        public void AddVehicle_WhenVehicleIsAdded_Success()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(2);
            Car car1 = new Car("RPG211", "Yellow", 1999, FuelType.Diesel, 2);
            Car car2 = new Car("RPG212", "Black", 1999, FuelType.Diesel, 2);

            // Act
            bool result1 = garage.AddVehicle(car1); // Adding first car
            bool result2 = garage.AddVehicle(car2); // Adding second car

            // Assert
            Assert.True(result1, "The first vehicle should be added successfully");
            Assert.True(result2, "The second vehicle should be added successfully");
        }

        [Fact]
        public void AddVehicle_WhenCapacityIsExceeded_Failure()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(2);
            Car car1 = new Car("RPG211", "Yellow", 1999, FuelType.Diesel, 2);
            Car car2 = new Car("RPG212", "Black", 1999, FuelType.Diesel, 2);
            Car car3 = new Car("RPG213", "Red", 1999, FuelType.Diesel, 2);

            // Act
            garage.AddVehicle(car1);
            garage.AddVehicle(car2);
            bool result3 = garage.AddVehicle(car3); // Attempt to add third car (exceeds capacity)

            // Assert
            Assert.False(result3, "The third vehicle should not be added due to capacity limits");
        }

        [Fact]
        public void RemoveVehicle_WhenVehicleExists_Success()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(2);
            Car car1 = new Car("RPG211", "Yellow", 1999, FuelType.Diesel, 2);
            Car car2 = new Car("RPG212", "Black", 1999, FuelType.Diesel, 2);
            garage.AddVehicle(car1);
            garage.AddVehicle(car2);

            // Act
            bool result1 = garage.RemoveVehicle("RPG211");
            bool result2 = garage.RemoveVehicle("RPG212");

            // Assert
            Assert.True(result1, "The vehicle RPG211 should be removed successfully");
            Assert.True(result2, "The vehicle RPG212 should be removed successfully");
        }

        [Fact]
        public void RemoveVehicle_WhenVehicleDoesNotExist_Failure()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(2);
            Car car1 = new Car("RPG211", "Yellow", 1999, FuelType.Diesel, 2);
            garage.AddVehicle(car1);

            // Act
            bool result = garage.RemoveVehicle("RPG555"); // Nonexistent plate number

            // Assert
            Assert.False(result, "Removing a non-existent vehicle should fail");
        }

        [Fact]
        public void GetCapacity_WhenCapacityIsChecked_ReturnsExpectedValue()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(2);
            uint expectedCapacity = 2;

            // Act
            uint actualCapacity = garage.GetCapacity();

            // Assert
            Assert.Equal(expectedCapacity, actualCapacity);
        }

        [Fact]
        public void GarageIsFull_WhenGarageIsFull_ReturnsTrue()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(2);
            garage.AddVehicle(new Car("RPG211", "Yellow", 1999, FuelType.Diesel, 2));
            garage.AddVehicle(new Car("RPG212", "Black", 1999, FuelType.Diesel, 2));

            // Act
            bool isFull = garage.IsGarageFull();

            // Assert
            Assert.True(isFull, "The garage should be full after adding two vehicles");
        }

        [Fact]
        public void EnumerateEmptyGarage_WhenEnumerated_ReturnsEmptySlots()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(2); // Empty garage with 2 parking spots

            // Act
            var vehicles = garage.ToList(); // Convert the enumerator to a list for easier assertion

            // Assert
            Assert.Equal(2, vehicles.Count); // The garage should have exactly 2 slots
            Assert.All(vehicles, vehicle => Assert.Null(vehicle)); // All slots should be null since no vehicles were added
        }

        [Fact]
        public void EnumerateFilledGarage_WhenEnumerated_ReturnsAllVehicles()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(2);
            Car car1 = new Car("RPG211", "Yellow", 1999, FuelType.Diesel, 2);
            Car car2 = new Car("RPG212", "Black", 1999, FuelType.Diesel, 2);
            garage.AddVehicle(car1);
            garage.AddVehicle(car2);

            // Act
            List<Vehicle> vehicles = garage.ToList(); // This uses the GetEnumerator method to create a list of vehicles

            // Assert
            Assert.Equal(2, vehicles.Count); // Assert that we have exactly 2 vehicles
            Assert.Contains(car1, vehicles); // Assert that the first car is in the list
            Assert.Contains(car2, vehicles); // Assert that the second car is in the list
            Assert.Equal(car1, vehicles[0]); // Assert that car1 is the first vehicle in order
            Assert.Equal(car2, vehicles[1]); // Assert that car2 is the second vehicle in order
        }

        [Fact]
        public void EnumeratePartialFilledGarage_WhenEnumerated_ReturnsFilledAndEmptySlots()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(3);
            Car car1 = new Car("RPG211", "Yellow", 1999, FuelType.Diesel, 2);
            Car car2 = new Car("RPG212", "Black", 1999, FuelType.Diesel, 2);
            garage.AddVehicle(car1);
            garage.AddVehicle(car2);

            // Act
            List<Vehicle> vehicles = garage.ToList(); // This uses GetEnumerator to get the list of vehicles

            // Assert
            Assert.Equal(3, vehicles.Count);  // Assert that it returns 3 elements (2 cars + 1 empty slot)
            Assert.Equal(car1, vehicles[0]);  // The first car is in the first position
            Assert.Equal(car2, vehicles[1]);  // The second car is in the second position
            Assert.Null(vehicles[2]);         // The third position should be null because the slot is empty
        }
    }
}