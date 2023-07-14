using Examiner.Domain.Entities.Authentication;
using Examiner.Domain.Entities.Content;
using Examiner.Domain.Entities.Notifications.Emails;
using Examiner.Domain.Entities.Users;
using Examiner.Infrastructure.Contexts;
using Examiner.Infrastructure.Repositories;
using Examiner.Infrastructure.Repositories.Interfaces;
using Examiner.Infrastructure.UnitOfWork.Interfaces;

namespace Examiner.Infrastructure.UnitOfWork;

/// <summary>
/// Implements contract for unit of work
/// </summary>

public class UnitOfWork : IUnitOfWork
{
    private readonly ExaminerContext _dbContext;
    private UserRepository<UserIdentity>? _userRepository;
    private UserProfileRepository<UserProfile>? _userProfileyRepository;
    private CodeVerificationRepository<CodeVerification>? _codeVerificationRepository;
    private KickboxVerificationRepository<KickboxVerification>? _kickboxVerificationRepository;
    private SubjectRepository<Subject>? _subjectRepository;
    private SubjectCategoryRepository<SubjectCategory>? _subjectCategoryRepository;
    private CountryRepository<Country>? _countryRepository;
    private StateRepository<State>? _stateRepository;
    private ExperienceLevelRepository<ExperienceLevel>? _experienceLevelRepository;
    private EducationDegreeRepository<EducationDegree>? _educationDegreeRepository;

    public UnitOfWork(ExaminerContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository<UserIdentity>(_dbContext));

    public ICodeVerificationRepository CodeVerificationRepository =>
    _codeVerificationRepository ??
    (_codeVerificationRepository = new CodeVerificationRepository<CodeVerification>(_dbContext));

    public IUserProfileRepository UserProfileRepository =>
    _userProfileyRepository ??
    (_userProfileyRepository = new UserProfileRepository<UserProfile>(_dbContext));

    public IKickboxVerificationRepository KickboxVerificationRepository =>
    _kickboxVerificationRepository ??
    (_kickboxVerificationRepository = new KickboxVerificationRepository<KickboxVerification>(_dbContext));

    public ISubjectRepository SubjectRepository =>
    _subjectRepository ?? (_subjectRepository = new SubjectRepository<Subject>(_dbContext));

    public ISubjectCategoryRepository SubjectCategoryRepository =>
    _subjectCategoryRepository ?? (_subjectCategoryRepository = new SubjectCategoryRepository<SubjectCategory>(_dbContext));
    public ICountryRepository CountryRepository =>
    _countryRepository ?? (_countryRepository = new CountryRepository<Country>(_dbContext));
    public IStateRepository StateRepository =>
    _stateRepository ?? (_stateRepository = new StateRepository<State>(_dbContext));
    public IExperienceLevelRepository ExperienceLevelRepository =>
    _experienceLevelRepository ?? (_experienceLevelRepository = new ExperienceLevelRepository<ExperienceLevel>(_dbContext));
    public IEducationDegreeRepository EducationDegreeRepository =>
    _educationDegreeRepository ?? (_educationDegreeRepository = new EducationDegreeRepository<EducationDegree>(_dbContext));

    public async Task CompleteAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    private bool _disposed;

    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);

        // Take this object off the finalization queue to prevent
        // finalization code for this object from executing a second time
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Cleans up any resources being used
    /// </summary>
    /// <param name="disposing"> Whether or not we are disposing </param>
    /// <returns><see cref="ValueTask"/></returns>

    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // dispose managed resources
                await _dbContext.DisposeAsync();
            }

            // dispose any unmanaged resources here

            _disposed = true;
        }
    }
}