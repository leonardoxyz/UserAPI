using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class UsuarioService
    {
        private IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;
        private TokenService _tokenService;

        public UsuarioService(UserManager<Usuario> userManager, IMapper mapper, SignInManager<Usuario> signInManager, TokenService tokenService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task Cadastra(CreateUsuarioDto dto)
        {
            Usuario usuario = _mapper.Map<Usuario>(dto);
            IdentityResult result = await _userManager.CreateAsync(usuario, dto.Password);

            if (!result.Succeeded)
            {
                throw new ApplicationException("Erro ao Cadastrar o usuário!");
            }
        }

        internal async Task<string> Login(LoginUsuarioDto dto)
        {
           var result = await _signInManager
                .PasswordSignInAsync(dto.Username, dto.Password, false, false);
        if (!result.Succeeded)
            {
                throw new ApplicationException("Erro ao autenticar o usuário!");
            }
            
        var usuario = _signInManager
                .UserManager
                .Users
                .FirstOrDefault(user => user.NormalizedUserName == dto.Username.ToUpper());

            var token = _tokenService.GenerateToken(usuario);

            return token;
        }
    }
}
