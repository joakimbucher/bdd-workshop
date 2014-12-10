using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateCheck
{
    public class MySystem
    {
        private List<Person> persons = new List<Person>();

        public void AddPerson(Person person)
        {
            var duplicate = FindPerson(person.FirstName, person.LastName, person.DateOfBirth);
            if (duplicate != null)
            {
                throw new DuplicateCheckException(string.Format("Duplicate with name '{0} {1}' and date of birth : {2} found.", duplicate.FirstName, duplicate.LastName, duplicate.DateOfBirth));
            }

            persons.Add(person);
        }

        public Person FindPerson(string firstName, string lastName, DateTime dateOfBirth)
        {
            return persons.FirstOrDefault(p => p.FirstName == firstName && p.LastName == lastName && p.DateOfBirth == dateOfBirth);
        }
    }
}
