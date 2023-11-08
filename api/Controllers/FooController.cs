using api.DTO;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FooController : ControllerBase
{
    [HttpPost]
    public ActionResult<Bar> Post(Foo model)
    {

        return Ok(new Bar
        {
            Name = "Bar",
            Items = model.Items
        });
    }
}