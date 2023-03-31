using Examiner.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Examiner.Infrastructure.Helpers;

/// <summary>
/// Performs operations that sync local database with remote storage
/// </summary>

public static class DataHelper
{

    public static async Task ManageDataAsync(IServiceProvider serviceProvider)
    {
        //Service: An instance of db context
        var dbContextService = serviceProvider.GetRequiredService<ExaminerContext>();

        //Migration: This is the programmatic equivalent to Update-Database
        await dbContextService.Database.MigrateAsync();
    }
}