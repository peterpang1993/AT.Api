using AT.Application.DTOs;

namespace AT.Application.Interfaces
{
    public interface IApplicationService
    {
        public Task<GetApplicationDTO> GetApplicationByIdAsync(int id);
        public Task<IEnumerable<GetApplicationDTO>> GetAllApplicationAsync();
        public Task<GetApplicationDTO> CreateApplicationAsync(CreateApplicationDTO dto);
        public Task<GetApplicationDTO> UpdateApplicationStatusAsync(int id, UpdateApplicationStatusDTO dto);
    }
}
