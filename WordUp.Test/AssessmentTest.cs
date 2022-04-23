using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordUp.Model;

namespace WordUp.Test
{
    [TestClass]
    public class AssessmentTest
    {
        private readonly IAssessment _assessment;
        public AssessmentTest()
        {
            var services = new ServiceCollection();

            services.AddScoped<IAssessment, Assessment>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            _assessment = serviceProvider.GetService<IAssessment>();

        }

        [TestMethod]
        public void Test_WithMax_HasMaxValue41()
        {

            //arrange
            var scores = new List<Score>
            {
                new Score {Value = 10},
                new Score {Value = 30},
                new Score {Value = 20},
                new Score {Value = 40},
                new Score {Value = 2},
                new Score {Value = 3},
                new Score {Value = 41},
                new Score {Value = 32},
            };




            //act
            var max = _assessment.WithMax(scores);

            //assert


            Assert.AreEqual(41, max.Value);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_WithMax_NullScores_HasNullException()
        {

            //act
            var max = _assessment.WithMax(null);

            //assert
        }

        [TestMethod]
        public void Test_WithMax_EmptyInput_IsNull()
        {


            //act
            var max = _assessment.WithMax(new List<Score>());


            //assert
            Assert.IsNull(max);
        }




        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_GetAverageOrDefault_NullInput_HasNullException()
        {

            //act
            _assessment.GetAverageOrDefault(null);

        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_GetAverageOrDefault_EmptyInput_IsNull()
        {

            //act
            var result = _assessment.GetAverageOrDefault(null);

            //assert
            Assert.IsNull(result);
        }



        [TestMethod]
        public void Test_GetAverageOrDefault_AllDataSame()
        {
            //arrange
            var items = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            //act
            var result = _assessment.GetAverageOrDefault(items);

            //assert
            Assert.AreEqual(1, result);
        }


        [TestMethod]
        public void Test_GetAverageOrDefault_AverageIs_5()
        {
            //arrange
            var items = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //act
            var result = _assessment.GetAverageOrDefault(items);

            //assert
            Assert.AreEqual(5, result);
        }


        [TestMethod]
        public void Test_GetFullName()
        {
            //arrange
            var data = new Group
            {
                Name = "APPLE",
                Parent = new Group
                {
                    Name = "Mobile",
                    Parent = new Group
                    {
                        Name = "Electronic",
                        Parent = null
                    }
                }
            };

            //act
            var result = _assessment.GetFullName(data);



            //assert
            Assert.AreEqual("Electronic/Mobile/APPLE", result);

        }



        [TestMethod]
        public void Test_ClosestToAverageOrDefault_AllDataSame()
        {
            var list=new List<int>(){1,1,1,1,1,1,1,1};

            var result = _assessment.ClosestToAverageOrDefault(list);

            Assert.AreEqual(1,result);
        }


        [TestMethod]
        public void Test_ClosestToAverageOrDefault_ResultIs4()
        {
            var items = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var result = _assessment.ClosestToAverageOrDefault(items);

            Assert.AreEqual(4, result);
        }


        [TestMethod]
        public void Test_Group()
        {
            //arrange
            var data = new List<Booking>
            {
                new Booking
                {
                    Date = new DateTime(2022,04,04),
                    Allocation = 10,
                    Project = "CRM",
                },
                new Booking
                {
                    Date = new DateTime(2022,04,05),
                    Allocation = 15,
                    Project = "CRM",
                },
                new Booking
                {
                    Date = new DateTime(2022,04,05),
                    Allocation = 10,
                    Project = "HR",
                },
                new Booking
                {
                    Date = new DateTime(2022,04,07),
                    Allocation = 15,
                    Project = "HR",
                },
                new Booking
                {
                    Date = new DateTime(2022,04,08),
                    Allocation = 5,
                    Project = "CRM",
                }
            };


            //act
            var result=_assessment.Group(data);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2,result.Count());
            Assert.AreEqual(10,result.ToList()[0].Items[1].Allocation);
            Assert.AreEqual(25,result.ToList()[0].Items[0].Allocation);
            Assert.AreEqual(15,result.ToList()[1].Items[0].Allocation);
            Assert.AreEqual(5, result.ToList()[1].Items[1].Allocation);


        }



        [TestMethod]
        public void Test_Merge_Count()
        {
            //arrange
            var first = new List<int> { 1, 2, 3 };
            var second = new List<int> { 4, 5, 6 };

            //act
            var result = _assessment.Merge(first,second);

            //assert
            Assert.AreEqual(6,result.Count());
        }


        [TestMethod]
        public void Test_Merge_Count_SecondIsEmpty()
        {
            //arrange
            var first = new List<int> { 1, 2, 3 };
            var second = new List<int> {  };

            //act
            var result = _assessment.Merge(first, second);

            //assert
            Assert.AreEqual(3, result.Count());
        }


        [TestMethod]
        public void Test_Merge_Count_FirstIsEmpty()
        {
            //arrange
            var first = new List<int> {  };
            var second = new List<int> { 1, 2, 3 };

            //act
            var result = _assessment.Merge(first, second);

            //assert
            Assert.AreEqual(3, result.Count());
        }


        [TestMethod]
        public void Test_Merge_Count_FirstIsNULL()
        {
            //arrange
            
            var second = new List<int> { 1, 2, 3 };

            //act
            var result = _assessment.Merge(null, second);

            //assert
            Assert.AreEqual(3, result.Count());
        }


        [TestMethod]
        public void Test_Merge_Count_SecondIsNULL()
        {
            //arrange

            var first = new List<int> { 1, 2, 3 };

            //act
            var result = _assessment.Merge(first, null);

            //assert
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void Test_Merge_Count_BothIsNULL()
        {
            //arrange

            

            //act
            var result = _assessment.Merge(null, null);

            //assert
            Assert.AreEqual(0, result.Count());
        }

    }


}
