using Microsoft.AspNetCore.Mvc;
using Test1.Data;
using Test1.Models;

namespace Test1.Controllers
{
    [ApiController] //to declare that this is an API's controller
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        // public IActionResult Index()
        // {
        //    return View();
        // }

        private readonly ContactsAPI_DbContext dbContext;
        public ContactsController(ContactsAPI_DbContext dbContext)
        {
            this.dbContext = dbContext;

        }


        [HttpGet]
        public IActionResult GetContacts()
        {
            return Ok(dbContext.Contacts.ToList());
            // <not using View because we are using SWAGER> return View();
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = dbContext.Contacts.Find(id);
            if (contact == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(contact);
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContact addContactReq)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContactReq.Address,
                Email = addContactReq.Email,
                Fullname = addContactReq.Fullname,
                Phone = addContactReq.Phone
            };

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContact updateContactReq)
        {
            var contact = dbContext.Contacts.Find(id);

            if (contact == null)
            {
                return BadRequest();
            }
            else
            {
                contact.Fullname = updateContactReq.Fullname;
                contact.Address = updateContactReq.Address;
                contact.Email = updateContactReq.Email;
                contact.Phone = updateContactReq.Phone;

                await dbContext.SaveChangesAsync();

                return Ok(contact);

            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return BadRequest();
            }
            else
            {
                dbContext.Remove(id);
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }

        }
    }
}
