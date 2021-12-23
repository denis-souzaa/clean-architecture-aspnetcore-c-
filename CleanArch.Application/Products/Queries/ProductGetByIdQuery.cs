using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Products.Queries
{
    public class ProductGetByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }

        public ProductGetByIdQuery(int id)
        {
            Id = id;
        }
    }
}