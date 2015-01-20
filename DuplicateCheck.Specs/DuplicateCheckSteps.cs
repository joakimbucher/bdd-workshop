using System.Linq;

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
                mySystem.AddPersonWithoutDuplicateCheck(person);
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
            Assert.IsTrue(ScenarioContext.Current.ContainsKey(ErrorKey));

            var error = ScenarioContext.Current.Get<DuplicateCheckException>(ErrorKey);
            var person = ScenarioContext.Current.Get<Person>(PersonKey);
            
            Assert.NotNull(error);
            //Assert.IsTrue(error.Message.Contains(person.FirstName));
            //Assert.IsTrue(error.Message.Contains(person.LastName));
            //Assert.IsTrue(error.Message.Contains(person.DateOfBirth.ToString()));
        }

        [Then(@"the system accepts my entry without dublicate message")]
        public void ThenTheSystemAcceptsMyEntryWithoutDublicateMessage()
        {
            Assert.IsFalse(ScenarioContext.Current.ContainsKey(ErrorKey));

            var person = ScenarioContext.Current.Get<Person>(PersonKey);
           
            Assert.IsTrue(mySystem.FindPerson(person.FirstName, person.LastName, person.DateOfBirth).Any());
        }

        [Then(@"the duplicate check result is (.*)")]
        public void ThenTheDuplicateCheckResultIs(string duplicateCheckResult)
        {
            var isDuplicate = duplicateCheckResult == "Duplicate";

            if (isDuplicate)
            {
                ThenTheSystemTellsMeThatITryToAddADuplicate();
            }
            else
            {
                ThenTheSystemAcceptsMyEntryWithoutDublicateMessage();
            }
        }

        [Then(@"the duplicate check details are:")]
        public void ThenTheDuplicateCheckDetailsAre(Table duplicateCheckDetails)
        {
            if (!ScenarioContext.Current.ContainsKey(ErrorKey))
            {
                return;
            }

            var error = ScenarioContext.Current.Get<DuplicateCheckException>(ErrorKey);
           
            var duplicateDetailsSet = duplicateCheckDetails.CreateSet<DuplicateDetails>().Where(dd => !string.IsNullOrWhiteSpace(dd.Name)).ToList();
            Assert.AreEqual(duplicateDetailsSet.Count(), error.DuplicateDetails.Count());

            for (int i = 0; i < duplicateDetailsSet.Count(); i++)
            {
                var expectedDuplicateDetails = duplicateDetailsSet[i];
                var duplicateDetails = error.DuplicateDetails.ToList()[i];

                Assert.AreEqual(expectedDuplicateDetails.Name, duplicateDetails.Name);
                Assert.AreEqual(expectedDuplicateDetails.Probability, duplicateDetails.Probability);
            }
        }

        //[Then(@"the system tells me that there are (.*) persons that are likely to be duplicates with a probability of (.*) % and higher")]
        //public void ThenTheSystemTellsMeThatThereArePersonsThatAreLikelyToBeDuplicatesWithAProbabilityOfAndHigher(int numberOfPersons, int minProbability)
        //{
        //    ScenarioContext.Current.Get<LastResult>()
        //}
    }
}
