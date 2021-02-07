using AutoMapper;
using BookStore_API.Contracts;
using BookStore_API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_API.Controllers
{
    /// <summary>
    /// Endpoint used to interact with the Authors in the bokk store's database
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        public AuthorsController(IAuthorRepository authorRepository, ILoggerService loggerService, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _logger = loggerService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets All Authors.
        /// </summary>
        /// <returns>List of Authors</returns>
        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            try
            {
                _logger.LogInfo("Attemped GetAll Authors");
                var authors = await _authorRepository.FindAll();
                var response = _mapper.Map<IList<AuthorDTO>>(authors);
                _logger.LogInfo("Successfully got all Authors");
                return Ok(response);

            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} - {ex.InnerException}");
                return StatusCode(500, "Something went wrong. Please contact the Administrator");
            }
        }
    }
}
