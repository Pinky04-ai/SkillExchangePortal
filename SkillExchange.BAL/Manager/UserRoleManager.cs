using SkillExchange.API.DTO.UserRole;
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
    public class UserRoleManager : IUserRoleManager
    {
        private readonly IUserRole _userRoleRepo;

        public UserRoleManager(IUserRole userRoleRepo)
        {
            _userRoleRepo = userRoleRepo;
        }
        public async Task AssignRolesAsyc(AssignRoleDTO dto)
        {
            var existing = await _userRoleRepo.GetUserRoleAsync(dto.UserId, dto.RoleId);
            if (existing != null)
                throw new Exception("Role already assigned to user");

            await _userRoleRepo.AssignRoleAsync(new UserRole
            {
                UserId = dto.UserId,
                RoleId = dto.RoleId
            });
        }

        public async Task<IEnumerable<UserRoleDTO>> GetRolesByUserAsync(int userId)
        {
            var roles = await _userRoleRepo.GetRolesByUserAsync(userId);
            return roles.Select(ur => new UserRoleDTO
            {
                UserId = ur.UserId,
                UserEmail = ur.User.Email,
                RoleId = ur.RoleId,
                RoleName = ur.Role.RoleName
            }).ToList();
        }

        public async Task<UserRoleDTO?> GetUserRoleAsync(int userId, int roleId)
        {
            var ur = await _userRoleRepo.GetUserRoleAsync(userId, roleId);
            if (ur == null) return null;

            return new UserRoleDTO
            {
                UserId = ur.UserId,
                UserEmail = ur.User.Email,
                RoleId = ur.RoleId,
                RoleName = ur.Role.RoleName
            };
        }

        public async Task<IEnumerable<UserRoleDTO>> GetUsersByRoleAsync(int roleId)
        {
            var users = await _userRoleRepo.GetUsersByRoleAsync(roleId);
            return users.Select(ur => new UserRoleDTO
            {
                UserId = ur.UserId,
                UserEmail = ur.User.Email,
                RoleId = ur.RoleId,
                RoleName = ur.Role.RoleName
            }).ToList();
        }

        public async Task RemoveRoleAsync(int userId, int roleId)
        {
            var existing = await _userRoleRepo.GetUserRoleAsync(userId, roleId);
            if (existing == null)
                throw new Exception("Role not found for user");

            await _userRoleRepo.RemoveRoleAsync(userId, roleId);
        }
    }
}
