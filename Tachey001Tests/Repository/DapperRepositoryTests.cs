using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tachey001.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Tachey001.Repository.Tests
{
    [TestClass()]
    public class DapperRepositoryTests
    {
        [TestMethod()]
        public void GetCourseTest()
        {
            // Arrange
            DapperRepository dapperRepository = new DapperRepository();

            // Act
            Object result = dapperRepository.GetCourse();

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod()]
        public void GetOwners()
        {
            //// Arrange
            //DapperRepository dapperRepository = new DapperRepository();

            //// Act
            //Object result = dapperRepository.GetOwners();

            //// Assert
            //Assert.IsNotNull(result);
        }
        [TestMethod()]
        public void GetCartPartialViewModel()
        {
            // Arrange
            DapperRepository dapperRepository = new DapperRepository();

            // Act
            Object result = dapperRepository.GetCourse();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}