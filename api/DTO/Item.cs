using System.ComponentModel.DataAnnotations;

namespace api.DTO;

public class Item
{
    [Required] 
    public string Name { get; set; }
    
    [Required]
    [Range(1,10)]
    public int? Value { get; set; }
}