using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.src.Services;
using PhoneBook.src.Dtos;
using PhoneBook.src.Functions;
using PhoneBook.src.Models;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;


    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllContacts()
    {
        var contacts = await _contactService.GetAllContactsAsync();
        return Ok(ContactsToResponseFunction.Apply(contacts));
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

    [HttpPost]
    public async Task<IActionResult> AddContact([FromBody] PutContactRequest request)
    {
        var result = await _contactService.AddContactAsync(RequestToContactFunction.Apply(Guid.NewGuid(), request));
        return CreatedAtAction(nameof(GetContactById), new { id = result.Value.Id }, result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateContact([FromBody] PutContactRequest request)
    {
        var result = await _contactService.AddContactAsync(RequestToContactFunction.Apply(Guid.NewGuid(), request));
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContact([FromRoute] string id, [FromBody] PutContactRequest request)
    {
        var result = await _contactService.UpdateContactAsync(RequestToContactFunction.Apply(Guid.Parse(id), request));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact([FromRoute] string id)
    {
        var result = await _contactService.DeleteContactAsync(id);
        if (result.Result is NoContent) return NoContent();
        else return NotFound();
    }
}
