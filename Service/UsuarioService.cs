﻿using AutoMapper;
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
        private SignInManager<Usuario> _signInManager;
        private TokenService _tokenService;

        public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, TokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            this._tokenService = tokenService;
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

        public async Task<string> Login(LoginUsuarioDto loginUsuarioDto)
        {
            var resultado = await _signInManager.PasswordSignInAsync(loginUsuarioDto.UserName, loginUsuarioDto.Password, false, false);

            if (!resultado.Succeeded)
                throw new ApplicationException("Usuário não autenticado!");

            var usuario = _signInManager.UserManager.Users.FirstOrDefault(usuario => usuario.NormalizedUserName == loginUsuarioDto.UserName.ToUpper());

            var token = _tokenService.GenerateToken(usuario);

            return token;
        }
    }
}
