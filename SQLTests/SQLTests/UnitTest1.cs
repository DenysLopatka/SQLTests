using System.Collections.Generic;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;


namespace SQLTests
{
    public class Tests
    {
        private SqlHelper _sqlHelper;

        [SetUp]
        public void Setup()
        {
            _sqlHelper = new SqlHelper("Database");
            _sqlHelper.OpenConnection();
        }

        [TearDown]
        public void TearDown()
        {
            _sqlHelper.ExecuteNonQuery("delete from [Database].[dbo].[Products] where id = 23");
            _sqlHelper.CloseConnection();
        }

        [Test]
        public void InsertTest()
        {
            _sqlHelper.Insert("Products",
                new Dictionary<string, string> { { "Id", "23" }, { "Name", "'Test23'" }, { "Count", "234" } });

            var res = _sqlHelper.IsRowExistedInTable("Products",
                new Dictionary<string, string> { { "Id", "23" }, { "Name", "'Test23'" }, { "Count", "234" } });

            Assert.True(res);
        }

        [Test]
        public void UpdateTest()
        {
            _sqlHelper.Update("Products",
                new Dictionary<string, string> {{"Count", "15"}}, new Dictionary<string, string> { {"Id", "1" } });

            var res = _sqlHelper.IsRowExistedInTable("Products",
                new Dictionary<string, string> { { "Count", "15" } });

            Assert.True(res);
        }

        [Test]
        public void DeleteTest()
        {
            _sqlHelper.Insert("Products",
               new Dictionary<string, string> { { "Id", "3" }, { "Name", "'Test23'" }, { "Count", "3" } });

            _sqlHelper.Delete("Products",
                new Dictionary<string, string> { { "Count", "3" }, { "Name", "'Test23'" } });

            var res = _sqlHelper.IsRowExistedInTable("Products",
                new Dictionary<string, string> { { "Count", "3" },{ "Name", "'Test23'"} });

            Assert.False(res);
        }
    }
}