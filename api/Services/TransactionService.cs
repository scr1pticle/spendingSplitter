using Microsoft.EntityFrameworkCore;
using api.DTOs;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly SplitterContext _context;
        private readonly IMapper _mapper;

        public TransactionService(SplitterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TransactionReadDTO?> AddTransactionAsync(int groupId, TransactionCreateDTO createDTO){
            var group = await _context.Groups.FindAsync(groupId);
            var payerMember = await _context.Members.FindAsync(createDTO.PayerMemberId);
            if(group == null || payerMember == null) return null;
            Transaction transaction = new()
            {
                Title = createDTO.Title,
                Group = group,
                PayerMember = payerMember,
                FullAmount = createDTO.FullAmount,
                Shares = new List<Share>()
            };
            if(createDTO.SplitType == "equal"){
                var groupMembers = await _context.Members.
                    Where(m => m.GroupId == groupId)
                    .ToListAsync();
                decimal eachAmount = createDTO.FullAmount/groupMembers.Count;
                
                foreach(var member in groupMembers){
                    var share = new Share(){
                        Transaction = transaction,
                        Member = member,
                        Amount = eachAmount
                    };
                    _context.Shares.Add(share);
                }
            }
            else if(createDTO.SplitType == "exact"){
                foreach(var createDTOShare in createDTO.Shares){
                    var member = await _context.Members.FindAsync(createDTOShare.MemberId);
                    if(member == null || createDTOShare.Amount == 0) continue;
                    var share = new Share(){
                        Transaction = transaction,
                        Member = member,
                        Amount = createDTOShare.Amount
                    };
                    _context.Shares.Add(share);
                }
            }
            else{
                foreach(var createDTOShare in createDTO.Shares){
                    var member = await _context.Members.FindAsync(createDTOShare.MemberId);
                    if(member == null || createDTOShare.Amount == 0) continue;
                    var share = new Share(){
                        Transaction = transaction,
                        Member = member,
                        Amount = createDTO.FullAmount*(createDTOShare.Amount/100)
                    };
                    _context.Shares.Add(share);
                }
            }
            
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return _mapper.Map<TransactionReadDTO>(transaction);
        }

        public async Task<IEnumerable<TransactionReadDTO>?> GetTransactionsAsync(int id){
            var transactions = await _context.Transactions
                .Where(t => t.GroupId == id)
                .Include(t => t.PayerMember)
                .Include(t => t.Shares)
                    .ThenInclude(s => s.Member)
                .ToListAsync();
            if (transactions.Count == 0) return [];

            return _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionReadDTO>>(transactions);
        }

        public async Task<bool> DeleteTransactionAsync(int id){
            var transaction = await _context.Transactions.FindAsync(id);
            if(transaction == null) return false;
            
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TransactionReadDTO?> GetTransactionAsync(int id){
            var transaction = await _context.Transactions.FindAsync(id);
            if(transaction == null) return null;
            return _mapper.Map<TransactionReadDTO>(transaction);
        }
    }
}