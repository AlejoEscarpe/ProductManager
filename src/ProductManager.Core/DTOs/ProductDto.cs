using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManager.Core.DTOs
{
    public record ProductDto(int Id, string Name, decimal Price, int Stock);
    public record CreateProductDto(string Name, decimal Price, int Stock);
    public record UpdateProductDto(string Name, decimal Price, int Stock);
}
