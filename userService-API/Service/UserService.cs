using userService_API.DTO;
using userService_API.Interfaces;
using userService_API.models;

namespace userService_API.Service
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDTO>> GetAlluser()
        {
            var userList =  await _userRepository.GetAllAsync();
            var userDTOList = userList.Select(user => new UserDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                ProfilePicture  = user.ProfilePicture
            }).ToList();

            return userDTOList;
        }
        public async Task<UserDTO> GetUserByIdAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return new UserDTO { Id = user.Id, FullName = user.FullName, Email = user.Email,Address = user.Address,PhoneNumber = user.PhoneNumber,ProfilePicture = user.ProfilePicture };
        }
        public async Task CreateUserAsync(UserDTO dto)
        {
            var user = new UserProfile
            {
                Id = dto.Id,  // comes from IdentityService
                Email = dto.Email,
                FullName = dto.FullName
            };

            await _userRepository.AddUserAsync(user);
        }

        public async Task UpdateUserAsync(UserProfile user)
        {
            await _userRepository.UpdateUserdetailsAsync(user);
        }

       
    }
}
