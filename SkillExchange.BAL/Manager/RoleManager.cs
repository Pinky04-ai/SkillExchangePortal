using SkillExchange.API.DTO.Roles;
using SkillExchange.BAL.Interfaces;
using SkillExchange.DAL.Entities;
using SkillExchange.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.BAL.Manager
{
    public class RoleManager : IRoleManager
    {
        private readonly IRole _roleRepo;
        private readonly IAppUser _userRepo;

        public RoleManager(IRole roleRepo , IAppUser userRepo)
        {
            _roleRepo = roleRepo;
            _userRepo = userRepo;
        }
        public async Task AssignRoleToUserAsync(int userId, int roleId)
        {
           var user = await _userRepo.GetByIdAsync(userId);
           var role = await _roleRepo.GetByIdAsync(roleId);
            if (user == null || role == null)
                throw new Exception("Invalid User or Role");
            user.UserRoles.Add(new UserRole {  UserId = userId ,RoleId = roleId });
            await _userRepo.UpdateAsync(user);
          
        }

        public async Task<RoleDTO> CreateRoleAsync(CreateRoleDTO dto)
        {
            var role = new Role { RoleName = dto.RoleName };
            await _roleRepo.AddAsync(role);
            return new RoleDTO
            {
                Id = role.Id,
                RoleName = role.RoleName,
                UserCount = 0
            };
        }

        public Task<bool> DeleteRoleAsync(int id)
        {
            throw new NotImplementedException();
        }

        //public async Task<bool> DeleteRoleAsync(int id)
        //{
        //    var role = await _roleRepo.GetByIdAsync(id);
        //    if (role == null) return false;
        //    await _roleRepo.DeleteAsync(id);
        //    return true;

        //}

        public async Task<IEnumerable<RoleDTO>> GetAllRolesAsync()
        {
            var roles = await _roleRepo.GetAllAsync();
            return roles.Select(r => new RoleDTO
            {
                Id = r.Id,
                RoleName = r.RoleName,
                UserCount = r.UserRoles?.Count ?? 0
            });
        }

        public async Task<RoleDTO?> GetRoleByIdAsync(int id)
        {
            var role = await _roleRepo.GetByIdAsync(id);
            if (role == null) return null;

            return new RoleDTO
            {
                Id = role.Id,
                RoleName = role.RoleName,
                UserCount = role.UserRoles?.Count ?? 0
            };
        }

        public async Task RemoveRoleFromUserAsync(int userId, int roleId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) throw new Exception("User not found");

            var userRole = user.UserRoles.FirstOrDefault(ur => ur.RoleId == roleId);
            if (userRole != null)
            {
                user.UserRoles.Remove(userRole);
                await _userRepo.UpdateAsync(user);
            }
        }

        public async Task<RoleDTO?> UpdateRoleAsync(UpdateRoleDTO dto)
        {
            var role = await _roleRepo.GetByIdAsync(dto.Id);
            if (role == null) return null;

            role.RoleName = dto.RoleName;
            await _roleRepo.UpdateAsync(role);

            return new RoleDTO
            {
                Id = role.Id,
                RoleName = role.RoleName,
                UserCount = role.UserRoles?.Count ?? 0
            };
        }
    }
}
