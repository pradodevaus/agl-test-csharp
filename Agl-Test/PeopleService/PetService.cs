using Agl.Common;
using Agl.Connectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleService
{
    public class PetService
    {
        private readonly IPeopleConnector _peopleConnector;

        public PetService()
        {
            this._peopleConnector = new PeopleConnector();
        }

        public PetService(IPeopleConnector peopleConnector)
        {
            this._peopleConnector = peopleConnector;
        }

        public List<Pet> GetPetsByOwnerGender(GenderType gender)
        {
            List<Pet> pets = new List<Pet>();

            var people = this._peopleConnector.GetPeople();

            pets = people.Where(x => x.Gender == gender && x.Pets != null).SelectMany(x => x.Pets).OrderBy(x => x.Name).ToList();

            return pets;
        }
    }
}
