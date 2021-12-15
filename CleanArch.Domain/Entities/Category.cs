using System.Collections.Generic;
using CleanArch.Domain.Validation;

namespace CleanArch.Domain.Entities
{
    public sealed class Category : Entity
    {
        public string Name { get; private set; }
        public ICollection<Product> Products { get; private set; }
        
        public Category(int id, string name)
        {
            DomainExceptionValidation.When(id < 0, "Invalid value Id");
            Id = id;
            ValidateDomain(name);
        }
        
        public Category(string name)
        {
            ValidateDomain(name);
        }

        public void Update(string name)
        {
            ValidateDomain(name);
        }

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name),
                "Invalid name.Name is required");
            
            DomainExceptionValidation.When(name.Length < 3,
                "Invalid name, too short, minimum 3 characteres");

            Name = name;
        }
    }
}