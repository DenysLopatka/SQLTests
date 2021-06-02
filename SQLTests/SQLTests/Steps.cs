using SQLTests.FF;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using System.Linq;
using TechTalk.SpecFlow.Assist;
using NUnit.Framework;
using System;

namespace SQLTests
{
    [Binding]
    public class Steps
    {        
        private readonly SqlHelper _sqlHelper = new SqlHelper("Database");

        [Given (@"Connection with database is opened")]
        public void ConnectionWithDatabaseIsopened()
        {
            _sqlHelper.OpenConnection();
        }

        [When (@"I insert this row with data to table")]
        public void IInsertRowWithDataToTable(Table table)
        {
            var dbModel = table.CreateSet<DBModel>().ToList();

            _sqlHelper.Insert("Products",
                new Dictionary<string, string> { { "Id", $"{dbModel[0].Id}" }, { "Name", $"{dbModel[0].Name}" }, { "Count", $"{dbModel[0].Count}" }});
        }

        [Then (@"New row was added with data")]
        public void NewRowWasAddedWithData(Table table)
        {
            var dbModel = table.CreateSet<DBModel>().ToList();

            var result = _sqlHelper.IsRowExistedInTable("Products",
                new Dictionary<string, string> { { "Id", $"{dbModel[0].Id}" }, { "Name", $"{dbModel[0].Name}" }, { "Count", $"{dbModel[0].Count}" } });

            Assert.IsTrue(result);
        }

        [When (@"i delete row with data")]
        public void IDeleteRowWithData(Table table)
        {
            var dbModel = table.CreateSet<DBModel>().ToList();

            _sqlHelper.Delete("Products",
                new Dictionary<string, string> { { "Id", $"{dbModel[0].Id}" } });
        }

        [Then (@"the row with this data doest't exist")]
        public void TheRowWithThisDataNotExist(Table table)
        {
            var dbModel = table.CreateSet<DBModel>().ToList();

            var result = _sqlHelper.IsRowExistedInTable("Products",
                new Dictionary<string, string> { { "Id", $"{dbModel[0].Id}" }, { "Name", $"{dbModel[0].Name}" }, { "Count", $"{dbModel[0].Count}" } });

            Assert.IsFalse(result);
        }

        [When (@"i update row with data where id")]
        public void IUpdateRowWithData (Table table)
        {
            var dbModel = table.CreateSet<DBModel>().ToList();

            _sqlHelper.Update("Products",
                new Dictionary<string, string> { { "Name", $"{dbModel[0].UpdatedName}" } }, new Dictionary<string, string> { { "Id", $"{dbModel[0].Id}" } });            
        }

        [Then (@"the row updated and have this data")]
        public void TheRowUpdatedAndHaveThisData(Table table)
        {
            var dbModel = table.CreateSet<DBModel>().ToList();

            var result = _sqlHelper.IsRowExistedInTable("Products",
                new Dictionary<string, string> { { "Id", $"{dbModel[0].Id}" }, { "Name", $"{dbModel[0].UpdatedName}" }, { "Count", $"{dbModel[0].Count}" } });

            Assert.IsTrue(result);
        }        
    }
}
