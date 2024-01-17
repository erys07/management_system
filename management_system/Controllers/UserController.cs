using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToDoList.Models;

namespace Management_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly List<UserModel> _userList = new List<UserModel>();

        // Endpoint para obter todos os usuários
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_userList);
        }

        // Endpoint para obter um usuário por ID
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                UserModel user = GetUserByIdInternal(id);
                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Endpoint para criar um novo usuário
        [HttpPost]
        public IActionResult CreateUser(UserModel newUser)
        {
            try
            {
                newUser.Validate();
                newUser.Id = GenerateUniqueId();
                _userList.Add(newUser);
                return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint para atualizar um usuário existente
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserModel updatedUser)
        {
            try
            {
                UserModel existingUser = GetUserByIdInternal(id);
                existingUser.Email = updatedUser.Email;
                existingUser.Name = updatedUser.Name;
                existingUser.Password = updatedUser.Password;
                existingUser.Validate();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint para excluir um usuário
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                UserModel userToDelete = GetUserByIdInternal(id);
                _userList.Remove(userToDelete);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        private UserModel GetUserByIdInternal(int id)
        {
            UserModel user = _userList.Find(u => u.Id == id);
            if (user == null)
            {
                throw new InvalidOperationException("Usuário não encontrado.");
            }

            return user;
        }

        private int GenerateUniqueId()
        {
            return _userList.Count + 1;
        }
    }
}
