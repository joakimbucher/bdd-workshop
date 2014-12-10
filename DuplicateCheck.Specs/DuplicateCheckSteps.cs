using System;
using System.Globalization;

using NUnit.Framework;

using TechTalk.SpecFlow;

namespace DuplicateCheck.Specs
{
    [Binding]
    public class DuplicateCheckSteps
    {
        private const string FirstnameKey = "FirstName";
        private const string LastnameKey = "LastName";
        private const string DateOfBirthKey = "DateOfBirth";

        private const string ErrorKey = "Error";

        private readonly MySystem mySystem;

        public DuplicateCheckSteps(MySystem mySystem)
        {
            this.mySystem = mySystem;
        }

        [Given(@"I have the following persons in the system:")]
        public void GivenIHaveTheFollowingPersonsInTheSystem(Table table)
        {
            foreach (var row in table.Rows)
            {
                var firstName = row[FirstnameKey];
                var lastName = row[LastnameKey];
                var dateOfBirth = DateTime.Parse(row[DateOfBirthKey], CultureInfo.CreateSpecificCulture("de-CH"));

                mySystem.AddPerson(new Person(firstName, lastName, dateOfBirth));
            }
        }
        
        [When(@"I add the following person to the system:")]
        public void WhenIAddTheFollowingPersonToTheSystem(Table table)
        {
            var firstName = table.Rows[0]["Value"];
            var lastName = table.Rows[1]["Value"];
            var dateOfBirth = DateTime.Parse(table.Rows[2]["Value"], new CultureInfo("de-CH"));

            ScenarioContext.Current.Add(FirstnameKey, firstName);
            ScenarioContext.Current.Add(LastnameKey, lastName);
            ScenarioContext.Current.Add(DateOfBirthKey, dateOfBirth);

            try
            {
                mySystem.AddPerson(new Person(firstName, lastName, dateOfBirth));
            }
            catch (DuplicateCheckException dce)
            {
                ScenarioContext.Current.Add(ErrorKey, dce);
            }
        }

        [Then(@"the system tells me that I try to add a duplicate")]
        public void ThenTheSystemTellsMeThatITryToAddADuplicate()
        {
            var error = ScenarioContext.Current.Get<DuplicateCheckException>(ErrorKey);
            var firstName = ScenarioContext.Current.Get<string>(FirstnameKey);
            var lastName = ScenarioContext.Current.Get<string>(LastnameKey);
            var dateOfBirth = ScenarioContext.Current.Get<DateTime>(DateOfBirthKey);

            Assert.NotNull(error);
            Assert.IsTrue(error.Message.Contains(firstName));
            Assert.IsTrue(error.Message.Contains(lastName));
            Assert.IsTrue(error.Message.Contains(dateOfBirth.ToString()));
        }

        [Then(@"the system accepts my entry without dublicate message")]
        public void ThenTheSystemAcceptsMyEntryWithoutDublicateMessage()
        {
            Assert.IsFalse(ScenarioContext.Current.ContainsKey(ErrorKey));

            var firstName = ScenarioContext.Current.Get<string>(FirstnameKey);
            var lastName = ScenarioContext.Current.Get<string>(LastnameKey);
            var dateOfBirth = ScenarioContext.Current.Get<DateTime>(DateOfBirthKey);

            Assert.IsNotNull(mySystem.FindPerson(firstName, lastName, dateOfBirth));
        }
    }
}
