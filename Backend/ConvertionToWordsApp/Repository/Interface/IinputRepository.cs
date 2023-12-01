using System;
using ConvertionToWordsApp.Models;

namespace ConvertionToWordsApp.Repository.Interface
{
	public interface IinputRepository
	{
		Task<Input> CreateAsync(Input input);

		Task<IEnumerable<Input>> GetAllAsync();
	}
}

