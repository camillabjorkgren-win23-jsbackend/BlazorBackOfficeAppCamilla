using BlazorBackOfficeCamilla.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorBackOfficeCamilla.Data.Repository;

public class ApplicationUserRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

}
