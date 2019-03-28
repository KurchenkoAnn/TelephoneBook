using System;
using System.Data.Entity;

namespace TelephoneBook
{
    internal class CustomInit<T> : DropCreateDatabaseIfModelChanges<PhoneContext>
    {
        protected override void Seed(PhoneContext context)
        {
            base.Seed(context);
            context.People.Add(new Person
            {
                FirstName = "Petro",
                LastName = "Petrenko",
                BirthDay = new DateTime(2005,12,12),
                NickName="PetyPetechka123",
                Telephone="0981233451"
              
            });
            context.People.Add(new Person

            {
                FirstName = "Ivan",
                LastName = "Ivanenko",
                BirthDay = new DateTime(1995,10,12),
                NickName = "IvanIvan667",
                Telephone = "0998763451"
            });
       
            context.SaveChanges();
        }
    }
}