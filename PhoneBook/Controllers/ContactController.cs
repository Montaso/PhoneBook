using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.src.Services;
using PhoneBook.src.Dtos;
using PhoneBook.src.Functions;
using PhoneBook.src.Models;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;
    private readonly ICategoryService _categoryService;


    public ContactController(IContactService contactService, ICategoryService categoryService)
    {
        _contactService = contactService;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllContacts()
    {
        var contacts = await _contactService.GetAllContactsAsync();
        return Ok(SimpleContactsToResponseFunction.Apply(contacts));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContactById(string id)
    {
        var result = await _contactService.GetContactByIdAsync(id);

        if (result.Result is Ok<Contact> okContact)
        {
            return Ok(ContactToResponseFunction.Apply(okContact.Value));
        }

        return NotFound();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddContact([FromBody] PutContactRequest request)
    {
        var result = await _contactService.AddContactAsync(RequestToContactFunction.Apply(Guid.NewGuid(), request));
        return CreatedAtAction(nameof(GetContactById), new { id = result.Value.Id }, result.Value);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateContact([FromBody] PutContactRequest request)
    {
        var result = await _contactService.AddContactAsync(RequestToContactFunction.Apply(Guid.NewGuid(), request));
        return NoContent();
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContact([FromRoute] string id, [FromBody] PutContactRequest request)
    {
        var result = await _contactService.UpdateContactAsync(RequestToContactFunction.Apply(Guid.Parse(id), request));
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact([FromRoute] string id)
    {
        var result = await _contactService.DeleteContactAsync(id);
        if (result.Result is NoContent) return NoContent();
        else return NotFound();
    }
}
