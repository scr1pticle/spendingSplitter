using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace api.Services
{
    public interface IShareService
    {
        Task<ShareReadDTO?> PostShareAsync(int id, ShareCreateDTO createDTO);
        Task<bool> RemoveShareAsync(int id);
        Task<ShareReadDTO?> GetShareAsync(int id);
        Task<IEnumerable<ShareReadDTO>?> GetSharesAsync(int id);
    }
}