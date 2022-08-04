using Dapper;
using Npgsql;

public interface IUserRepository
{
    Task<IEnumerable<UserDto>> GetUsers();
    Task<User> Create(UserDto userDto);
    Task Update(UpdateUserDto userDto, string email);
    Task<bool> Authenticate(LoginDto loginDto);
    Task<User> GetUserByEmail(string email);
}

public class UserRepository : IUserRepository
{
    private IConfiguration Configuration;

    public UserRepository(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public async Task<User> Create(UserDto userDto)
    {
        await using (var conn = new NpgsqlConnection(Configuration["ConnectionStrings:Postgres"]))
        {
            var command = "INSERT INTO users (\"FirstName\", \"LastName\", \"Email\", \"Password\") VALUES (@firstName, @lastName, @email, @password);";

            var count = await conn.ExecuteAsync(command, userDto);

            if (count > 0)
            {
                return new User
                {
                    Email = userDto.Email,
                    FirstName = userDto.FirstName,
                };
            }

            return null;
        }
    }

    public async Task<bool> Authenticate(LoginDto loginDto)
    {
        var existingUser = await GetUserByEmail(loginDto.Email);

        return IsValidPassword(existingUser, loginDto);
    }

    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        await using (var conn = new NpgsqlConnection(Configuration["ConnectionStrings:Postgres"]))
        {
            return await conn.QueryAsync<UserDto>("SELECT \"FirstName\", \"LastName\", \"Email\" FROM users");
        }
    }

    public async Task Update(UpdateUserDto userDto, string email)
    {
        await using (var conn = new NpgsqlConnection(Configuration["ConnectionStrings:Postgres"]))
        {
            var existingUser = await GetUserByEmail(email);

            if (existingUser == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(userDto.FirstName))
            {
                existingUser.FirstName = userDto.FirstName;
            }

            if (!string.IsNullOrEmpty(userDto.LastName))
            {
                existingUser.LastName = userDto.LastName;
            }

            var command = "UPDATE users SET \"FirstName\" = @FirstName, \"LastName\" = @LastName "
            + "WHERE \"Id\" = @Id";

            var count = await conn.ExecuteAsync(command, existingUser);
        }
    }

    public async Task<User> GetUserByEmail(string email)
    {
        await using var conn = new NpgsqlConnection(Configuration["ConnectionStrings:Postgres"]);
        return await conn.QueryFirstOrDefaultAsync<User>
        (
            "SELECT * from users WHERE \"Email\" = @Email",
            new { Email = email }
        );
    }

    private bool IsValidPassword(User existingUser, LoginDto loginDto) =>
        existingUser != null && (existingUser.Email == loginDto.Email && existingUser.Password == loginDto.Password);

}
