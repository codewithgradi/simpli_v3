namespace simpli.Application.Interfaces;

public interface IDatabaseCleanUpService
{
  Task CleanOldDataAsync(CancellationToken cancellationToken);
}