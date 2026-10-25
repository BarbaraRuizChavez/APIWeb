using System.ComponentModel.DataAnnotations;

namespace APIWeb.Dtos;

public class LoginDto
{
    [Required, EmailAddress] public string Email { get; set; } = "";
    [Required] public string Password { get; set; } = "";
}

public class GoogleLoginDto
{
    [Required] public string IdToken { get; set; } = "";
}

public class SupplierDto
{
    public int SupplierId { get; set; }
    [Required, StringLength(40)] public string CompanyName { get; set; } = "";
    [StringLength(30)] public string? ContactName { get; set; }
    [StringLength(30)] public string? ContactTitle { get; set; }
    [StringLength(60)] public string? Address { get; set; }
    [StringLength(15)] public string? City { get; set; }
    [StringLength(15)] public string? Region { get; set; }
    [StringLength(10)] public string? PostalCode { get; set; }
    [StringLength(15)] public string? Country { get; set; }
    [StringLength(24)] public string? Phone { get; set; }
    [StringLength(24)] public string? Fax { get; set; }
    public string? HomePage { get; set; }

    // Acceso del proveedor al portal (opcional). La contraseña solo se recibe, nunca se devuelve.
    [EmailAddress, StringLength(120)] public string? AccessEmail { get; set; }
    [MinLength(8), StringLength(100)] public string? AccessPassword { get; set; }
}

public class OrderItemDto
{
    public int ProductId { get; set; }
    [Range(1, 1000)] public int Quantity { get; set; }
}

public class OrderCreateDto
{
    [Required, MinLength(1)] public List<OrderItemDto> Items { get; set; } = new();
}

// Lo que ve un cliente: sin datos de contacto del proveedor.
public class CatalogProductDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public string? CategoryName { get; set; }
    public string? SupplierName { get; set; }
    public string? QuantityPerUnit { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
}

// Lo que ve un proveedor de SUS productos (+ funcionalidad extra: reabasto y ventas).
public class MyProductDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public string? CategoryName { get; set; }
    public string? QuantityPerUnit { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
    public int UnitsOnOrder { get; set; }
    public int ReorderLevel { get; set; }
    public bool Discontinued { get; set; }
    public int UnitsSold { get; set; }

    public bool NeedsRestock => !Discontinued && ReorderLevel > 0 && UnitsInStock + UnitsOnOrder <= ReorderLevel;
    public bool OutOfStock => !Discontinued && UnitsInStock == 0;
}

