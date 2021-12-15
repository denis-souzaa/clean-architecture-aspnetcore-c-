using System;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Validation;
using FluentAssertions;
using Xunit;

namespace CleanArch.Domain.Tests
{
    public class CategoryUnitTest1
    {
        [Fact(DisplayName = "Create category with valid state")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Category(1, "Name category");
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }
        
        [Fact(DisplayName = "Create category negative value Id")]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Category(-1, "Name category");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid value Id");
        }
        
        [Fact(DisplayName = "Create category short name value")]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Category(1, "Ca");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name, too short, minimum 3 characteres");
        }
        
        [Fact(DisplayName = "Create category missing name value")]
        public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Category(1, " ");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name.Name is required");
        }
        
        [Fact(DisplayName = "Create category with null name value")]
        public void CreateCategory_WithNullNameValue_DomainExceptionInvalidName()
        {
            Action action = () => new Category(1, null);
            action.Should()
                .Throw<DomainExceptionValidation>();
        }
    }
}