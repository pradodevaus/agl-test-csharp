using Agl.Common;
using Agl.Connectors;
using PeopleService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new PetService();

            var maleOwnerPets = service.GetPetsByOwnerGender(GenderType.Male);

            Console.WriteLine("Male");
            foreach (var pet in maleOwnerPets)
            {
                Console.WriteLine("\t - " + pet.Name);
            }

            var femaleOwnerPets = service.GetPetsByOwnerGender(GenderType.Female);

            Console.WriteLine("Female");
            foreach (var pet in femaleOwnerPets)
            {
                Console.WriteLine("\t - " + pet.Name);
            }
        }
    }
}
