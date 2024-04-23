using Microsoft.AspNetCore.Mvc;
using UsuarioApi.Data.Dtos;
using UsuarioApi.Service;

namespace UsuarioApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class UsuarioController : ControllerBase
{

    private UsuarioService _usuarioService;

    public UsuarioController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost("cadastro")]
    public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto createUsuarioDto)
    {
        await _usuarioService.Cadastrar(createUsuarioDto);
        return Ok("Usuário cadastrado com sucesso!");
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogarUsuarioAsync(LoginUsuarioDto loginUsuarioDto)
    {
        var token = await _usuarioService.Login(loginUsuarioDto);
        return Ok(token);
    }
}
