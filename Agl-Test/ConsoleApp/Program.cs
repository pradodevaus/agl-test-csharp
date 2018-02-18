using Agl.Common;
using Agl.Services;
using Serilog;
using System;
using System.IO;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Setting Logger
            SetupLogger();

            try
            {
                Log.Debug("Application started");

                var service = new PetService();

                var maleOwnerPets = service.GetPetsByOwnerGender(PetType.Cat, GenderType.Male);

                Console.WriteLine("Fetching data from server...");

                Console.WriteLine("Male");
                foreach (var pet in maleOwnerPets)
                {
                    Console.WriteLine("\t - " + pet.Name);
                }

                var femaleOwnerPets = service.GetPetsByOwnerGender(PetType.Cat, GenderType.Female);

                Console.WriteLine("Female");
                foreach (var pet in femaleOwnerPets)
                {
                    Console.WriteLine("\t - " + pet.Name);
                }

                Log.Debug("Application finished");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static void SetupLogger()
        {
            var logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs\\{Date}.txt");
            Console.WriteLine($"Log file path: {logFilePath}");

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.RollingFile(logFilePath,
                fileSizeLimitBytes: 52428800, //50MB
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] [{SourceContext}] {Message}{NewLine}{Exception}")
            .CreateLogger();
        }
    }
}