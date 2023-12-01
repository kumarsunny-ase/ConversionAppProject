using System;
using ConvertionToWordsApp.Data;
using ConvertionToWordsApp.Models;
using ConvertionToWordsApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ConvertionToWordsApp.Repository.Implentation
{
	public class InputRepository : IinputRepository
	{
        private readonly AppDbContext _dbContext;

        public InputRepository(AppDbContext dbContext)
		{
            _dbContext = dbContext;
        }

        public async Task<Input> CreateAsync(Input input)
        {
            await _dbContext.inputs.AddAsync(input);
            await _dbContext.SaveChangesAsync();

            return input;
        }

        public async Task<IEnumerable<Input>> GetAllAsync()
        {
            return await _dbContext.inputs.ToListAsync();
        }
    }
}

