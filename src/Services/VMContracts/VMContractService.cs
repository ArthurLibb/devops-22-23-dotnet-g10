using Shared.VMContracts;
using System.Linq;
using Persistence;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.VirtualMachines.Contract;
using System;

/*
namespace Services.VMContracts
{
    public class VMContractService : IVMContractService
    {
        public VMContractService(DotNetDbContext dbContext)
        {
            _dbContext = dbContext;
            _VMContracts = dbContext.VMContracts;
        }

        private readonly DotNetDbContext _dbContext;
        private readonly DbSet<VMContract> _VMContracts;


        private IQueryable<VMContract> GetVMContractById(int id) => _VMContracts
                .AsNoTracking()
                .Where(p => p.Id == id);

        public async Task<VMContractResponse.Create> CreateAsync(VMContractRequest.Create request)
        {
            VMContractResponse.Create response = new();
            var VMContract = _VMContracts.Add(new VMContract(
                request.VMContract.CustomerId,
                request.VMContract.VMId,
                request.VMContract.StartDate,
                request.VMContract.EndDate
            ));
            await _dbContext.SaveChangesAsync();
            response.VMContractId = VMContract.Entity.Id;
            return response;
        }

        public async Task DeleteAsync(VMContractRequest.Delete request)
        {
            _VMContracts.RemoveIf(p => p.Id == request.VMContractId);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<VMContractResponse.Edit> EditAsync(VMContractRequest.Edit request)
        {
            VMContractResponse.Edit response = new();
            var VMContract = await GetVMContractById(request.VMContractId).SingleOrDefaultAsync();

            if (VMContract is not null)
            {
                var model = request.VMContract;

                // You could use a VMContract.Edit method here.
                VMContract.CustomerId = model.CustomerId;
                VMContract.VMId = model.VMId;
                VMContract.StartDate = model.StartDate;
                VMContract.EndDate = model.EndDate;

                _dbContext.Entry(VMContract).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                response.VMContractId = VMContract.Id;
            }

            return response;
        }


        public async Task<VMContractResponse.GetDetail> GetDetailAsync(VMContractRequest.GetDetail request)
        {
            VMContractResponse.GetDetail response = new();
            response.VMContract = await GetVMContractById(request.VMContractId)
                .Select(x => new VMContractDto.Detail
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    VMId = x.VMId,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate

                })
                .SingleOrDefaultAsync();
            return response;
        }

        public async Task<VMContractResponse.GetIndex> GetIndexAsync(VMContractRequest.GetIndex request)
        {
            VMContractResponse.GetIndex response = new();
            var query = _VMContracts.AsQueryable().AsNoTracking();
            response.VMContracts = await query.Select(x => new VMContractDto.Index
            {
                Id = x.Id,
                CustomerId = x.CustomerId,
                VMId = x.VMId,
                StartDate = x.StartDate,
                EndDate = x.EndDate
            }).ToListAsync();
            return response;
        }

        public Task<VMContractResponse.Index> GetFromDate(VMContractRequest.GetByDate request)
        {
            throw new NotImplementedException();
        }

        Task<VMContractResponse.Delete> IVMContractService.DeleteAsync(VMContractRequest.Delete request)
        {
            throw new NotImplementedException();
        }
    }
}
*/