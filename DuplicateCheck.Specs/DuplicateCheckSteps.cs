using NUnit.Framework;

using TechTalk.SpecFlow;

namespace DuplicateCheck.Specs
{
    [Binding]
    public class DuplicateCheckSteps
    {
        private const string FirstnameKey = "FirstName";
        private const string LastnameKey = "LastName";

        private const string ErrorKey = "Error";

        private readonly MySystem mySystem;

        public DuplicateCheckSteps(MySystem mySystem)
        {
            this.mySystem = mySystem;
        }

        [Given(@"I have a person with firstname ""(.*)"" and lastname ""(.*)"" in the system")]
        public void GivenIHaveAPersonWithFirstnameAndLastnameInTheSystem(string firstName, string lastName)
        {
            mySystem.AddPerson(firstName, lastName);
        }

        [When(@"I add a person with fistname ""(.*)"" and lastname ""(.*)"" to the system")]
        public void WhenIAddAPersonWithFistnameAndLastnameToTheSystem(string firstName, string lastName)
        {
            
            ScenarioContext.Current.Add(FirstnameKey, firstName);
            ScenarioContext.Current.Add(LastnameKey, lastName);

            try
            {
                mySystem.AddPerson(firstName, lastName);
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

            Assert.NotNull(error);
            Assert.IsTrue(error.Message.Contains(firstName));
            Assert.IsTrue(error.Message.Contains(lastName));
        }

        [Then(@"the system accepts my entry without dublicate message")]
        public void ThenTheSystemAcceptsMyEntryWithoutDublicateMessage()
        {
            Assert.IsFalse(ScenarioContext.Current.ContainsKey(ErrorKey));

            var firstName = ScenarioContext.Current.Get<string>(FirstnameKey);
            var lastName = ScenarioContext.Current.Get<string>(LastnameKey);

            Assert.IsNotNull(mySystem.FindPerson(firstName, lastName));
        }
    }
}
