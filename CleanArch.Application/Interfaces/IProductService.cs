using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArch.Application.DTOs;

namespace CleanArch.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProductById(int? id);
        Task<ProductDTO> GetProductCategory(int? id);

        Task Add(ProductDTO product);
        Task Update(ProductDTO product);
        Task Remove(int? id);
    }
}