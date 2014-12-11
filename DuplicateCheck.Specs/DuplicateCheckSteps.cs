using System;
using System.Globalization;

using NUnit.Framework;

using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DuplicateCheck.Specs
{
    [Binding]
    public class DuplicateCheckSteps
    {
        private const string PersonKey = "PersonName";
        private const string ErrorKey = "Error";

        private readonly MySystem mySystem;

        public DuplicateCheckSteps(MySystem mySystem)
        {
            this.mySystem = mySystem;
        }

        [Given(@"I have the following persons in the system:")]
        public void GivenIHaveTheFollowingPersonsInTheSystem(Table table)
        {
            foreach (var person in table.CreateSet<Person>())
            {
                mySystem.AddPerson(person);
            }
        }
        
        [When(@"I add the following person to the system:")]
        public void WhenIAddTheFollowingPersonToTheSystem(Table table)
        {
            var person = table.CreateInstance<Person>();
            ScenarioContext.Current.Add(PersonKey, person);
           
            try
            {
                mySystem.AddPerson(person);
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
            var person = ScenarioContext.Current.Get<Person>(PersonKey);
            
            Assert.NotNull(error);
            Assert.IsTrue(error.Message.Contains(person.FirstName));
            Assert.IsTrue(error.Message.Contains(person.LastName));
            Assert.IsTrue(error.Message.Contains(person.DateOfBirth.ToString()));
        }

        [Then(@"the system accepts my entry without dublicate message")]
        public void ThenTheSystemAcceptsMyEntryWithoutDublicateMessage()
        {
            Assert.IsFalse(ScenarioContext.Current.ContainsKey(ErrorKey));

            var person = ScenarioContext.Current.Get<Person>(PersonKey);
           
            Assert.IsNotNull(mySystem.FindPerson(person.FirstName, person.LastName, person.DateOfBirth));
        }
    }
}
