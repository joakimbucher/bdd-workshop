using System;
using System.Collections.Generic;
using System.Linq;
using DuoVia.FuzzyStrings;

namespace DuplicateCheck
{
    public class MySystem
    {
        private const double RequieredPropabilityScore = 0.4;

        private List<Person> persons = new List<Person>();

        public void AddPerson(Person person)
        {
            var duplicate = FindPerson(person.FirstName, person.LastName, person.DateOfBirth).ToList();
            if (duplicate.Any())
            {
                throw new DuplicateCheckException("Duplicates found.") { DuplicateDetails = duplicate };
            }

            AddPersonWithoutDuplicateCheck(person);
        }

        public void AddPersonWithoutDuplicateCheck(Person person)
        {
            persons.Add(person);
        }

        public IEnumerable<DuplicateDetails> FindPerson(string firstName, string lastName, DateTime dateOfBirth)
        {
            return persons.Where(p => p.FirstName.FuzzyEquals(firstName, RequieredPropabilityScore) && p.LastName.FuzzyEquals(lastName, RequieredPropabilityScore) && p.DateOfBirth == dateOfBirth)
                .Select(p => new DuplicateDetails
                                 {
                                     Name = p.Fullname, 
                                     Probability = Math.Round(Math.Min(p.LastName.FuzzyMatch(lastName), p.FirstName.FuzzyMatch(firstName)), 2)
                                 });
        }
    }
}
