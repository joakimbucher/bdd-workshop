using System;
using TechTalk.SpecFlow;

namespace DuplicateCheck.Specs
{
    [Binding]
    public class DuplicateCheckSteps
    {
        [Given(@"I have a person with firstname ""(.*)"" and lastname ""(.*)"" in the system")]
        public void GivenIHaveAPersonWithFirstnameAndLastnameInTheSystem(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I add a person with fistname ""(.*)"" and lastname ""(.*)"" to the system")]
        public void WhenIAddAPersonWithFistnameAndLastnameToTheSystem(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the system tells me that I try to add a duplicate")]
        public void ThenTheSystemTellsMeThatITryToAddADuplicate()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the system accepts my entry without dublicate message")]
        public void ThenTheSystemAcceptsMyEntryWithoutDublicateMessage()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
