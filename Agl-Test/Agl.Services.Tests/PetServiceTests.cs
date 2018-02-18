using Agl.Common;
using Agl.Connectors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Agl.Services.Tests
{
    [TestClass]
    public class PetServiceTests
    {
        [TestMethod]
        public void GivenPetService_WhenEmptyPersonListRecievedFromServer_ThenReturnEmptyPetList()
        {
            //ARRANGE
            var emptyPersonList = new List<Person>();

            var mockConnector = new Mock<IPeopleConnector>();
            mockConnector.Setup(x => x.GetPeople()).Returns(emptyPersonList);

            var service = new PetService(mockConnector.Object);

            //ACT
            var pets = service.GetPetsByOwnerGender(PetType.Cat, GenderType.Male);

            //ASSERT
            Assert.IsTrue(pets.Count == 0);
            mockConnector.Verify(x => x.GetPeople(),Times.Once);
        }

        [TestMethod]
        public void GivenPetService_WhenPersonListContainsUnknownPetTypeAndUnknownGenderType_ThenReturnEmptyPetList()
        {
            //ARRANGE
            var emptyPersonList = new List<Person>();

            var mockConnector = new Mock<IPeopleConnector>();
            mockConnector.Setup(x => x.GetPeople()).Returns(emptyPersonList);

            var service = new PetService(mockConnector.Object);

            //ACT
            var pets = service.GetPetsByOwnerGender(PetType.Unknown, GenderType.Unknown);

            //ASSERT
            Assert.IsTrue(pets.Count == 0);
            mockConnector.Verify(x => x.GetPeople(), Times.Once);
        }

        [TestMethod]
        public void GivenPetService_WhenPersonListContainsTwoMalesOneWithZeroCatAndAnotherWithTwoCats_ThenReturnPetListContainingTwoCats()
        {
            //ARRANGE
            var mockConnector = new Mock<IPeopleConnector>();
            mockConnector.Setup(x => x.GetPeople()).Returns(GetPersonListTestData(PetType.Cat, GenderType.Male));

            var service = new PetService(mockConnector.Object);

            //ACT
            var pets = service.GetPetsByOwnerGender(PetType.Cat, GenderType.Male);

            //ASSERT
            Assert.IsTrue(pets.Count == 2);
            Assert.IsTrue(pets.All(x => x.Type == PetType.Cat));
            mockConnector.Verify(x => x.GetPeople(), Times.Once);
        }

        private List<Person> GetPersonListTestData(PetType petType, GenderType genderType)
        {
            var persons = new List<Person>
            {
                new Person
                {
                    Name = "Alice",
                    Age = 20,
                    Gender = GenderType.Male,
                    Pets = null
                },
                new Person
                {
                    Name = "Bob",
                    Age = 25,
                    Gender = GenderType.Male,
                    Pets = new List<Pet>
                        {
                           new Pet{ Type = petType, Name = "Tabby" },
                           new Pet{ Type = petType, Name = "Simba" }
                        }
                },
                new Person
                {
                    Name = "Ema",
                    Age = 30,
                    Gender = GenderType.Female,
                    Pets = new List<Pet>
                        {
                           new Pet{ Type = petType, Name = "Nemo" },
                           new Pet{ Type = petType, Name = "Jim" }
                        }
                }
            };

            return persons;
        }
    }
}