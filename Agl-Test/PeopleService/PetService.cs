using Agl.Common;
using Agl.Connectors;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace Agl.Services
{
    public class PetService
    {
        private readonly IPeopleConnector _peopleConnector;
        private readonly ILogger _log = Log.ForContext<PetService>();

        public PetService()
        {
            this._peopleConnector = new PeopleConnector();
        }

        public PetService(IPeopleConnector peopleConnector)
        {
            this._peopleConnector = peopleConnector;
        }

        public List<Pet> GetPetsByOwnerGender(PetType petType, GenderType gender)
        {
            List<Pet> pets = new List<Pet>();

            var people = this._peopleConnector.GetPeople();

            _log.Debug($"{people.Count} people returned from server");

            pets = people.Where(x => x.Gender == gender && x.Pets != null)
                .SelectMany(x => x.Pets)
                .Where(x => x.Type == petType)
                .OrderBy(x => x.Name)
                .ToList();

            _log.Debug($"Total pets as {petType.ToString()} for {gender.ToString()} owners are {pets.Count}");
            return pets;
        }
    }
}