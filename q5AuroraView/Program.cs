using System;
using System.Collections.Generic;

namespace q5AuroraView
{
    class Program
    {

        static void Main(string[] args)
        {

            List<Person> people = new List<Person>();
            List<Person> fivePeople = new List<Person>();
            List<FirstNamePerson> firstNamePeople = new List<FirstNamePerson>();
            RandomPeople(people);
            fivePeople = FindFive(people);
            //or we can use list of firstName 
            firstNamePeople = FirstNamesOfPeople(people);


        }
        static void RandomPeople(List<Person> people)
        {
            Random rand = new Random();
            for (int i = 0; i < 8000000; i++)
            {
                people.Add(new Person
                {
                    Id = i,
                    FirstName = "FirstName" + rand.Next(0, 8000000),
                    LastName = "LastName" + i
                });
            }
        }
        static List<FirstNamePerson> FirstNamesOfPeople(List<Person> people)
        {
            List<FirstNamePerson> FirstNamePersonList = new List<FirstNamePerson>();
            List<Person> first = new List<Person>();
            first.Add(people[0]);
            FirstNamePerson newPerson = new FirstNamePerson(people[0].FirstName, first);
            FirstNamePersonList.Add(newPerson);
            bool foundPerson = false;
            foreach (var findPersonName in people)
            {
                foreach (var PersonNameInList in FirstNamePersonList)
                    if (PersonNameInList.FirstName == findPersonName.FirstName)
                    {
                        foundPerson = true;
                        PersonNameInList.allThePeopleWithTheSameFirstName.Add(findPersonName);
                    }
                if (!foundPerson)
                {
                    newPerson.FirstName = findPersonName.FirstName;
                    newPerson.allThePeopleWithTheSameFirstName.Add(findPersonName);
                    FirstNamePersonList.Add(newPerson);
                }
                foundPerson = false;
            }
            return FirstNamePersonList;

        }
        static List<Person> FindFive(List<Person> people)
        {
            Program prog = new Program();
            int count = 0;
            int Size = 5;
            List<Person> find = new List<Person>();
            foreach (var personInPeople in people)
            {
                if (count == Size)
                    return find;
                foreach (var verifyPerson in find)
                {
                    if (verifyPerson.FirstName == personInPeople.FirstName)
                        break;
                }
                if (prog.verify(personInPeople.FirstName))
                {
                    count++;
                    find.Add(personInPeople);
                    foreach (var findPersonName in people)
                    {
                        if (findPersonName.FirstName == personInPeople.FirstName)
                        {
                            count++;
                            find.Add(personInPeople);
                        }
                    }

                }
            }
            return find;
        }
        static List<Person> FindFiveWithList(List<FirstNamePerson> firstNamePeople)
        {
            Program prog = new Program();
            int count = 0;
            int Size = 5;
            List<Person> find = new List<Person>();
            foreach (var personInPeople in firstNamePeople)
            {
                if (count == Size)
                    return find;
                if (prog.verify(personInPeople.FirstName))
                {
                    count += personInPeople.allThePeopleWithTheSameFirstName.Count;
                    find.AddRange(personInPeople.allThePeopleWithTheSameFirstName);
                }
            }
            return find;
        }
        public  bool verify(string name)
        {
            return true;
        }
    }
    class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    class FirstNamePerson
    {
        public string FirstName { get; set; }
        public List<Person> allThePeopleWithTheSameFirstName { get; set; }
        public FirstNamePerson(string firstN, List<Person> ListPerson)
        {
            FirstName = firstN;
            allThePeopleWithTheSameFirstName = ListPerson;
        }
    }

}

