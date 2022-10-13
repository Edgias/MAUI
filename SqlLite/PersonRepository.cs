using SqlLite.Models;
using SQLite;
namespace SqlLite
{
    public class PersonRepository
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        private SQLiteAsyncConnection _connection;
        private async Task Init()
        {
            // TODO: Add code to initialize the repository
            if (_connection != null)
            {
                return;
            }
            _connection = new SQLiteAsyncConnection(_dbPath);
            await _connection.CreateTableAsync<Person>();
        }

        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task AddNewPerson(string name)
        {
            try
            {
                await Init();

                // basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                int result = await _connection.InsertAsync(new Person { Name = name });

                StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }

        }

        public async Task<List<Person>> GetAllPeople()
        {
            // TODO: Init then retrieve a list of Person objects from the database into a list
            try
            {
                await Init();
                return await _connection.Table<Person>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Person>();
        }
    }
}
