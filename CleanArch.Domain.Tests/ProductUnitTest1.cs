using System;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Validation;
using FluentAssertions;
using Xunit;

namespace CleanArch.Domain.Tests
{
    public class ProductUnitTest1
    {
        [Fact(DisplayName = "Create product with valid state")]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Name product", "description product", 10m, 10, "product image");
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }
        
        [Fact(DisplayName = "Create product negative value Id")]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Name product", "description product", 10m, 10, "product image");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid Id value");
        }
        
        [Fact(DisplayName = "Create product short name value")]
        public void CreateProduct_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Product(1, "Pr", "description product", 10m, 10, "product image");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name, too short, minimum 3 characteres");
        }
        
        [Fact(DisplayName = "Create product long image name")]
        public void CreateProduct_LongImageName_DomainExceptionShortName()
        {
            Action action = () => new Product(1, "Product name", "description product", 10m, 10,
                "product image image image image image image image image image image image image image image image image image image image image image image image image image image image image image image image image image image image image image image image image image image");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid image name, too long, maximum 250 characters");
        }
        
        [Fact(DisplayName = "Create product with null image name")]
        public void CreateProduct_WithNullImageName_NoDomainException()
        {
            Action action = () => new Product(1, "Product name", "description product", 10m, 10, null);
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }
        
        [Fact(DisplayName = "Create product with null image name")]
        public void CreateProduct_WithNullImageName_NoNullReferenceException()
        {
            Action action = () => new Product(1, "Product name", "description product", 10m, 10, null);
            action.Should()
                .NotThrow<NullReferenceException>();
        }
        
        [Fact(DisplayName = "Create product with empty image name")]
        public void CreateProduct_EmptyImageName_NoDomainException()
        {
            Action action = () => new Product(1, "Product name", "description product", 10m, 10, "");
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }
        
        [Theory(DisplayName = "Create product with negative value stock")]
        [InlineData(-5)]
        public void CreateProduct_InvalidPriceValue_DomainExceptionNegativeValue(int value)
        {
            Action action = () => new Product(1, "Product name", "description product", value, 10, "");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid price value");
        }
        
        [Theory(DisplayName = "Create product with negative value stock")]
        [InlineData(-5)]
        public void CreateProduct_InvalidStockValue_DomainExceptionNegativeValue(int value)
        {
            Action action = () => new Product(1, "Product name", "description product", 10m, value, "");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid stock value");
        }
    }
}