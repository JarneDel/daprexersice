using LegoService.Models;
using LegoService.Repositories;

namespace LegoService.Services;

public class LegoService : ILegoService
{
    private readonly ILegoRepository _legoRepository;

    public LegoService(ILegoRepository legoRepository)
    {
        _legoRepository = legoRepository;
    }

    public async Task<List<LegoTheme>> GetThemes()
    {
        return await _legoRepository.GetThemes(); 
    }

    public async Task<List<LegoSet>> GetSets()
    {
        return await _legoRepository.GetSets();
    }

    public async Task<LegoSet> GetSet(string id)
    {
        return await _legoRepository.GetSet(id);
    }

    public async Task<List<LegoSet>> GetSetsForTheme(string themeId)
    {
        return await _legoRepository.GetSetsForTheme(themeId);
    }

    public async Task InsertTestData()
    {
        var legoThemes = new List<LegoTheme>
        {
            new()
            {
                Name = "City",
                Description = "Lego City is a range of Lego building sets based on city life."
            },
            new()
            {
                Name = "Star Wars",
                Description = "Lego Star Wars is a Lego theme that incorporates the Star Wars saga."
            }
        };

        var legoSets = new List<LegoSet>
        {
            new()
            {
                Name = "Police Station",
                ModelNumber = "60141",
                YearReleased = 2017,
                PieceCount = 894,
                Theme = "641823123d2acbc37e3fb0b5",
                Price = 99.99m
            },
            new()
            {
                Name = "Millennium Falcon",
                ModelNumber = "75192",
                YearReleased = 2017,
                PieceCount = 7541,
                Theme = "641823123d2acbc37e3fb0b6",
                Price = 799.99m
            }
        };
        await _legoRepository.AddThemes(legoThemes);
        await _legoRepository.AddSets(legoSets);
    }
}

public interface ILegoService
{
    Task<List<LegoTheme>> GetThemes();
    Task<List<LegoSet>> GetSets();
    Task<LegoSet> GetSet(string id);
    Task<List<LegoSet>> GetSetsForTheme(string themeId);

    Task InsertTestData();
}