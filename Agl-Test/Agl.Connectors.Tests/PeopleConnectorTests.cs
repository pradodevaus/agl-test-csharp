using Agl.Common;
using Agl.Connectors.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Agl.Connectors.Tests
{
    [TestClass]
    public class PeopleConnectorTests
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GivenPeopleConnector_WhenServerReturnsEmptyResponse_ThenThrowException()
        {
            //ARRANGE
            var mockDataHelper = new Mock<IDataHelper>();
            mockDataHelper.Setup(x => x.GetJsonData(It.IsAny<string>())).Returns(string.Empty);

            var peopleConnnector = new PeopleConnector(mockDataHelper.Object);

            var errorMsg = "Empty response returned from the server";

            var serviceUrl = ConfigurationManager.AppSettings["PeopleServiceUrl"];

            try
            {
                //ACT
                var personList = peopleConnnector.GetPeople();
            }
            catch (Exception ex)
            {
                //ASSERT
                Assert.IsTrue(ex.Message.Contains(errorMsg));
                mockDataHelper.Verify(x => x.GetJsonData(serviceUrl), Times.Once);
                throw;
            }
        }

        [TestMethod]
        public void GivenPeopleConnector_WhenServerReturnsValidResponse_ThenReturnPersonList()
        {
            //ARRANGE
            var mockJsonData = @"[{'name':'Pradeep','age':30,'gender':'Male','pets':[{'name':'Tom','type':'Cat'}]}]";

            var mockDataHelper = new Mock<IDataHelper>();
            mockDataHelper.Setup(x => x.GetJsonData(It.IsAny<string>())).Returns(mockJsonData);

            var peopleConnnector = new PeopleConnector(mockDataHelper.Object);

            var serviceUrl = ConfigurationManager.AppSettings["PeopleServiceUrl"];

            //ACT
            var personList = peopleConnnector.GetPeople();

            //ASSERT
            mockDataHelper.Verify(x => x.GetJsonData(serviceUrl), Times.Once);
            mockDataHelper.Verify(x => x.DeserializeData<List<Person>>(It.Is<string>(y => y == mockJsonData)), Times.Once);
        }
    }
}