using Microsoft.EntityFrameworkCore;
using api.DTOs;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace api.Services
{
    public class ShareService : IShareService
    {
        private readonly SplitterContext _context;
        private readonly IMapper _mapper;

        public ShareService(SplitterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ShareReadDTO?> PostShareAsync(int id, ShareCreateDTO createDTO){
            var transaction = await _context.Transactions.FindAsync(id);
            if(transaction == null) return null;
            var share = _mapper.Map<Share>(createDTO);
            _context.Shares.Add(share);
            await _context.SaveChangesAsync();
            return _mapper.Map<ShareReadDTO>(share);

        }

        public async Task<IEnumerable<ShareReadDTO>?> GetSharesAsync(int id){
            var transaction = await _context.Transactions.Include(t => t.Shares).FirstOrDefaultAsync(t => t.Id == id);
            if(transaction == null) return null;
            return _mapper.Map<IEnumerable<Share>, IEnumerable<ShareReadDTO>>(transaction.Shares);
        }

        public async Task<bool> RemoveShareAsync(int id){
            var share = await _context.Shares.FindAsync(id);
            if(share == null) return false;
            _context.Shares.Remove(share);
            return true;
        }

        public async Task<ShareReadDTO?> GetShareAsync(int id){
            var share = await _context.Shares.FindAsync(id);
            if(share == null) return null;
            return _mapper.Map<ShareReadDTO>(share);
        }
    }
}