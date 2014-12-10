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

        public void AddPerson(string firstName, string lastName)
        {
            var duplicate = FindPerson(firstName, lastName);
            if (duplicate != null)
            {
                throw new DuplicateCheckException(string.Format("Duplicate with name '{0} {1}' found.", duplicate.FirstName, duplicate.LastName));
            }

            persons.Add(new Person(firstName, lastName));
        }

        public Person FindPerson(string firstName, string lastName)
        {
            return persons.FirstOrDefault(p => p.FirstName == firstName && p.LastName == lastName);
        }
    }
}
