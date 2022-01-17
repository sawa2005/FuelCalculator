// Code by Samuel Ward

using System.Text.Json;

namespace FuelCalculator
{
    public class SavedRace {
        private string filename = @"savedrace.json";
        private List<Race> races = new List<Race>();

        // Read all saved races
        public SavedRace() {
            if (File.Exists(@"savedrace.json")==true) {
                string jsonString = File.ReadAllText(filename);
                races = JsonSerializer.Deserialize<List<Race>>(jsonString);
            }
        }

        // Save new race
        public Race addRace(Race race) {
            races.Add(race);
            marshal();
            return race;
        }

        // Delete saved race
        public int delRace(int index) {
            races.RemoveAt(index);
            marshal();
            return index;
        }

        // Returns saved races as part of a List<T> Class
        public List<Race> getRaces() {
            return races;
        }
        
        // Serializes and saves guests to a .json file
        private void marshal() {
            var jsonString = JsonSerializer.Serialize(races);
            File.WriteAllText(filename, jsonString);
        }
    }

    public class Race {
        // Car of saved race
        private string car;
        public string Car {
            set { this.car = value; }
            get { return this.car; }
        }

        // Track of saved race
        private string track;
        public string Track {
            set { this.track = value; }
            get { return this.track; }
        }

        // Consumption of saved race
        private string consumption;
        public string Consumption {
            set { this.consumption = value; }
            get { return this.consumption; }
        }

        // Length of saved race
        private string length;
        public string Length {
            set { this.length = value; }
            get { return this.length; }
        }

        // Fuel requirement of saved race
        private string fuel;
        public string Fuel {
            set { this.fuel = value; }
            get { return this.fuel; }
        }
    }

    // Class to run the console application
    class Program
    {
        static void Main(string[] args)
        {
            SavedRace savedrace = new SavedRace();
            int i = 0;
            
            while (true) {
                Console.Clear();
                Console.CursorVisible = false;

                Console.WriteLine("RACE FUEL CALCULATOR\n");
                
                Console.WriteLine("Calculate Fuel Consumption");
                Console.WriteLine("1. Lap Race");
                Console.WriteLine("2. Timed Race\n");
                Console.WriteLine("3. Delete Saved Race\n");
                Console.WriteLine("X. Exit\n");

                Console.WriteLine("Saved Races");
                i = 0;
                foreach (Race race in savedrace.getRaces()) {
                    Console.WriteLine("[" + i++ + "] " + race.Car + " | " + race.Track + " | " + race.Consumption + " liters per lap | " + race.Length + " | " + race.Fuel + " liters required");
                }

                // Menu system
                int inp = (int) Console.ReadKey(true).Key;
                switch (inp) {
                    case '1':
                        Console.CursorVisible = true;

                        Console.Write("\nFuel consumption per lap (liters): ");
                        string fuelPerLap = Console.ReadLine();
                        Console.Write("Number of laps: ");
                        string lapAmount = Console.ReadLine();
                        Console.Write("Any extra laps? (0 for no extra laps): ");
                        string lapAdd = Console.ReadLine();


                        if (!String.IsNullOrEmpty(fuelPerLap) && !String.IsNullOrEmpty(lapAmount) && !String.IsNullOrEmpty(lapAdd)) {
                            if (lapAdd == "0") {
                                double result = Convert.ToDouble(fuelPerLap) * Convert.ToDouble(lapAmount);
                                Console.Write("\nThe amount of fuel needed for your race is: " + result + " liters\n");
                                
                                Console.WriteLine("\nWould you like to save this race?");
                                Console.WriteLine("Y. Yes");
                                Console.WriteLine("N. No");

                                inp = (int) Console.ReadKey(true).Key;
                                switch (inp) {
                                    case 89:
                                        Console.CursorVisible = true;
                                        
                                        Console.Write("Enter car name: ");
                                        string carName = Console.ReadLine();
                                        Console.Write("Enter track name: ");
                                        string trackName = Console.ReadLine();

                                        Race obj = new Race();
                                        obj.Car = carName;
                                        obj.Track = trackName;
                                        obj.Consumption = fuelPerLap;
                                        obj.Length = lapAmount + " laps";
                                        obj.Fuel = result.ToString("0.00");

                                        if (!String.IsNullOrEmpty(carName) && !String.IsNullOrEmpty(trackName)) {
                                            savedrace.addRace(obj);
                                        }
                                        break;
                                    
                                    case 78: break;
                                }
                            } else {
                                double result = Convert.ToDouble(fuelPerLap) * Convert.ToDouble(lapAmount);
                                double resultExtra = Convert.ToDouble(fuelPerLap) * (Convert.ToDouble(lapAmount) + Convert.ToDouble(lapAdd));
                                Console.Write("\nThe minimum amount of fuel needed for your race is: " + result + " liters\n");
                                Console.Write("The amount of fuel needed for your race with " + lapAdd + " extra laps is: " + resultExtra + " liters\n");

                                Console.WriteLine("\nWould you like to save this race?");
                                Console.WriteLine("Y. Yes");
                                Console.WriteLine("N. No");

                                inp = (int) Console.ReadKey(true).Key;
                                switch (inp) {
                                    case 89:
                                        Console.CursorVisible = true;
                                        
                                        Console.Write("Enter car name: ");
                                        string carName = Console.ReadLine();
                                        Console.Write("Enter track name: ");
                                        string trackName = Console.ReadLine();

                                        Race obj = new Race();
                                        obj.Car = carName;
                                        obj.Track = trackName;
                                        obj.Consumption = fuelPerLap;
                                        obj.Length = lapAmount + " laps with " + lapAdd + " extra lap/laps";
                                        obj.Fuel = resultExtra.ToString("0.00");

                                        if (!String.IsNullOrEmpty(carName) && !String.IsNullOrEmpty(trackName)) {
                                            savedrace.addRace(obj);
                                        }
                                        break;
                                    
                                    case 78: break;
                                }
                            }
                        }
                        break;
                        
                    case '2':
                        Console.CursorVisible = true;

                        Console.Write("Fuel consumption per lap (liters): ");
                        fuelPerLap = Console.ReadLine();
                        Console.Write("Lap time (1/3 - enter minutes first): ");
                        string lapMinutes = Console.ReadLine();
                        Console.Write("Lap time (2/3 - now enter seconds): ");
                        string lapSeconds = Console.ReadLine();
                        Console.Write("Lap time (3/3 - lastly enter milliseconds): ");
                        string lapMilli = Console.ReadLine();
                        Console.Write("Race time (minutes): ");
                        string raceTime = Console.ReadLine();
                        Console.Write("Any extra laps? (0 for no extra laps): ");
                        lapAdd = Console.ReadLine();


                        if (!String.IsNullOrEmpty(fuelPerLap) && !String.IsNullOrEmpty(lapMinutes) && !String.IsNullOrEmpty(lapSeconds) && !String.IsNullOrEmpty(lapMilli) && !String.IsNullOrEmpty(raceTime) && !String.IsNullOrEmpty(lapAdd)) {
                            if (lapAdd == "0") {
                                double lapTime = Convert.ToDouble(lapMinutes) + (Convert.ToDouble(lapSeconds) / 60) + (Convert.ToDouble(lapMilli) / 60000);
                                double lapCount = (Convert.ToDouble(raceTime) + Convert.ToDouble(lapTime) - 1) / Convert.ToDouble(lapTime);
                                double result = Convert.ToDouble(fuelPerLap) * Convert.ToDouble(lapCount);
                                Console.Write("\nThe amount of fuel needed for your race is: " + result + " liters\n");

                                Console.WriteLine("\nWould you like to save this race?");
                                Console.WriteLine("Y. Yes");
                                Console.WriteLine("N. No");

                                inp = (int) Console.ReadKey(true).Key;
                                switch (inp) {
                                    case 89:
                                        Console.CursorVisible = true;
                                        
                                        Console.Write("Enter car name: ");
                                        string carName = Console.ReadLine();
                                        Console.Write("Enter track name: ");
                                        string trackName = Console.ReadLine();

                                        Race obj = new Race();
                                        obj.Car = carName;
                                        obj.Track = trackName;
                                        obj.Consumption = fuelPerLap;
                                        obj.Length = raceTime + " minutes";
                                        obj.Fuel = result.ToString("0.00");

                                        if (!String.IsNullOrEmpty(carName) && !String.IsNullOrEmpty(trackName)) {
                                            savedrace.addRace(obj);
                                        }
                                        break;
                                    
                                    case 78: break;
                                }
                            } else {
                                double lapTime = Convert.ToDouble(lapMinutes) + (Convert.ToDouble(lapSeconds) / 60) + (Convert.ToDouble(lapMilli) / 60000);
                                double lapCount = (Convert.ToDouble(raceTime) + Convert.ToDouble(lapTime) - 1) / Convert.ToDouble(lapTime);
                                double lapExtra = lapCount + Convert.ToDouble(lapAdd);
                                double result = Convert.ToDouble(fuelPerLap) * Convert.ToDouble(lapCount);
                                double resultExtra = Convert.ToDouble(fuelPerLap) * Convert.ToDouble(lapExtra);
                                Console.Write("\nThe minimum amount of fuel needed for your race is: " + result + " liters\n");
                                Console.Write("The amount of fuel needed for your race with " + lapAdd + " extra laps is: " + resultExtra + " liters\n");

                                Console.WriteLine("Would you like to save this race?");
                                Console.WriteLine("Y. Yes");
                                Console.WriteLine("N. No");

                                inp = (int) Console.ReadKey(true).Key;
                                switch (inp) {
                                    case 89:
                                        Console.CursorVisible = true;

                                        Console.Write("Enter car name: ");
                                        string carName = Console.ReadLine();
                                        Console.Write("Enter track name: ");
                                        string trackName = Console.ReadLine();

                                        Race obj = new Race();
                                        obj.Car = carName;
                                        obj.Track = trackName;
                                        obj.Consumption = fuelPerLap;
                                        obj.Length = raceTime + " minutes with " + lapAdd + " extra lap/laps";
                                        obj.Fuel = resultExtra.ToString("0.00");

                                        if (!String.IsNullOrEmpty(carName) && !String.IsNullOrEmpty(trackName)) {
                                            savedrace.addRace(obj);
                                        }
                                        break;
                                    
                                    case 78: break;
                                }
                            }
                        }
                        break;

                    case '3':
                        Console.CursorVisible = true;

                        Console.Write("Enter index to delete: ");
                        string index = Console.ReadLine();
                        savedrace.delRace(Convert.ToInt32(index));
                        break;

                    case 88:
                        Console.CursorVisible = true;

                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}