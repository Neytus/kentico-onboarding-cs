using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.BL;

namespace TodoList.DAL
{
    public class NodeRepository : INodeRepository
    {
        private const string FirstGuid = "d237bdda-e6d4-4e46-92db-1a7a0aeb9a72";
        private const string SecondGuid = "b84bbcc7-d516-4805-b2e3-20a2df3758a2";
        private const string ThirdGuid = "6171ec89-e3b5-458e-ae43-bc0e8ec061e2";
        private const string FourthGuid = "b61670fd-33ce-400e-a351-f960230e3aae";

        public async Task<NodeDto[]> GetAllAsync()
        {
            return await Task.FromResult(new NodeDto[]
            {
                new NodeDto {Id = new Guid(FirstGuid), Text = "poopy"},
                new NodeDto {Id = new Guid(SecondGuid), Text = "GEARS"},
                new NodeDto {Id = new Guid(ThirdGuid), Text = "Planet Music"},
                new NodeDto {Id = new Guid(FourthGuid), Text = "Time to get shwifty"}
            });
        }

        public async Task<NodeDto> GetByIdAsync(string id)
        {
            return await Task.FromResult(new NodeDto {Id = new Guid(FirstGuid), Text = "poopy"});
        }

        public async Task<NodeDto> AddAsync(string text)
        {
            return await Task.FromResult(new NodeDto { Id = new Guid(SecondGuid), Text = "GEARS" });
        }

        public async Task<NodeDto> UpdateAsync(string id, string text)
        {
            return await Task.FromResult(new NodeDto {Id = new Guid(ThirdGuid), Text = "Planet Music"});
        }

        public async Task<NodeDto> DeleteAsync(string id)
        {
            return await Task.FromResult(new NodeDto {Id = new Guid(FourthGuid), Text = "Time to get shwifty"});
        }
    }
}