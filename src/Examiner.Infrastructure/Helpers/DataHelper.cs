using Examiner.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Examiner.Infrastructure.Helpers;

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