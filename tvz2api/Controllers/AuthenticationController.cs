using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using tvz2api.Data;
using tvz2api.Models.DTO;
using tvz2api.Models;

namespace tvz2api.Controllers {
    [Route("api/auth")]
    [ApiController]
    public class AuthController: ControllerBase {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly tvz2Context _context;

        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper, tvz2Context context) 
        {
            _repo = repo;
            _config = config;
            _mapper = mapper;
            _context = context;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto) 
        {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
            if(await _repo.userExists(userForRegisterDto.Username)) {
                return BadRequest("Username already exists!");
            }
            var userBeingPrepared = new Korisnik() {
                Username = userForRegisterDto.Username,
            };
            var createdUser = await _repo.Register(userBeingPrepared, userForRegisterDto.Password);
            return(StatusCode(201));
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto) 
        {
            var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);
            
            if(userFromRepo == null) {
                return(Unauthorized());
            }
            
            // Token contains two claims, one is the ID and the other is the username
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };
            
            // In order to make sure the claims are valid, created a key and hash it
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            
            // Create the token
            var tokenDescriptor = new SecurityTokenDescriptor() {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var user = _mapper.Map<Korisnik, UserForListDto>(userFromRepo);
            
            return(Ok(new {
                token = tokenHandler.WriteToken(token),
                user
            }));
        }
    }
}