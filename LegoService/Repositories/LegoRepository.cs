using LegoService.DataContext;
using LegoService.Models;
using MongoDB.Driver;

namespace LegoService.Repositories;

public class LegoRepository: ILegoRepository
{
    private readonly IMongoContext _context;

    public LegoRepository(IMongoContext context)
    {
        _context = context;
    }
    public async Task<List<LegoTheme>> GetThemes()
    {
        var query = Builders<LegoTheme>.Filter.Empty;
        return await _context.ThemeCollection.Find(query).ToListAsync();
    }

    public async Task<List<LegoSet>> GetSets()
    {
        var query = Builders<LegoSet>.Filter.Empty;
        return await _context.SetCollection.Find(query).ToListAsync();
    }

    public async Task<LegoSet> GetSet(string id)
    {
        var query = Builders<LegoSet>.Filter.Eq("Id", id);
        return await _context.SetCollection.Find(query).FirstOrDefaultAsync();
    }

    public async Task<List<LegoSet>> GetSetsForTheme(string themeId)
    {
        var query = Builders<LegoSet>.Filter.Eq("theme", themeId);
        return await _context.SetCollection.Find(query).ToListAsync();
    }

    public async Task AddSets(IEnumerable<LegoSet> sets)
    {
        await _context.SetCollection.InsertManyAsync(sets);
    }

    public async Task AddThemes(IEnumerable<LegoTheme> themes)
    {
        await _context.ThemeCollection.InsertManyAsync(themes);
    }
}

public interface ILegoRepository
{

    public Task<List<LegoTheme>> GetThemes();
    public Task<List<LegoSet>> GetSets();
    public Task<LegoSet> GetSet(string id);
    public Task<List<LegoSet>> GetSetsForTheme(string themeId);
    public Task AddSets(IEnumerable<LegoSet> sets);
    public Task AddThemes(IEnumerable<LegoTheme> themes);
}
