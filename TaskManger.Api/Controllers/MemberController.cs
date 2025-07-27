using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManger.Api.Entities;
using TaskManger.Api.Interfaces;

namespace TaskManger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]   //GET /api/member: Retrieve a list of all members.
        public async Task<IActionResult> GetMembers()
        {
            var members = await _unitOfWork.Members.GetAllAsync();
            return Ok(members);
        }
        [HttpGet("{id}")]  //GET /api/member/{id}: Retrieve a specific member by ID.
        public async Task<IActionResult> GetMember(int id)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }
        [HttpPost("Add-member")] //POST /api/member: Create a new member.

        public async Task<IActionResult> AddMember([FromBody] Member  member)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Members.AddAsync(member);
                await _unitOfWork.CommitAsync();
                return Ok("Member added successfully.");
            }
            return BadRequest("Invalid member data.");
        }
        [HttpPut("Update-member/{id}")] //PUT /api/member/{id}: Update an existing member by ID.
        public async Task<IActionResult> UpdateMember(int id, [FromBody] Member member)
        {
            if (id != member.Id)
            {
                return BadRequest("Member ID mismatch.");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Members.Update(member);
                await _unitOfWork.CommitAsync();
                return Ok("Member updated successfully.");
            }
            return BadRequest("Invalid member data.");
        }
        [HttpDelete("Delete-member/{id}")] //DELETE /api/member/{id}: Delete a member by ID.
        public async Task<IActionResult> DeleteMember(int id)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            _unitOfWork.Members.Delete(id);
            await _unitOfWork.CommitAsync();
            return Ok("Member deleted successfully.");
        }

    }
}
