using System.ComponentModel.DataAnnotations;

namespace api.DTO;

public class Foo
{
    [Required]
    public int? Amount { get; set; }
    [Required]
    public List<Item> Items { get; set; }
    [Required]
    public string Name { get; set; }
}