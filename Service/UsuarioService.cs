using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using UsuarioApi.Data.Dtos;
using UsuarioApi.Models;

namespace UsuarioApi.Service
{
    public class UsuarioService
    {

        private IMapper _mapper;
        private UserManager<Usuario> _userManager;

        public UsuarioService(IMapper mapper, UserManager<Usuario> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task Cadastrar(CreateUsuarioDto createUsuarioDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(createUsuarioDto);

            IdentityResult resultado = await _userManager.CreateAsync(usuario, createUsuarioDto.Senha);

            if (!resultado.Succeeded)
            {

                foreach (var erro in resultado.Errors.ToList())
                {
                    if (erro.Code.Equals("InvalidUserName"))
                        throw new ApplicationException("Nome de usuário é inválido!");

                    if (erro.Code.Equals("DuplicateUserName"))
                        throw new ApplicationException("Nome de usuário já existe!");

                    if (erro.Code.Equals("PasswordRequiresNonAlphanumeric"))
                        throw new ApplicationException("A senha deve conter letra maiuscula, numéro e um caracterer especial! Ex. Teste9999?");
                }

            }
        }
    }
}
